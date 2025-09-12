using System.Collections.Generic;
using UnityEngine;


namespace MiskCore
{
    [ExecuteAlways]
    public class PathSpline : MonoBehaviour
    {
        [Header("控制點（依序擺在此物件底下，直接在 Scene 拖動）")]
        public List<Transform> points = new List<Transform>();

        [Header("樣條設定")]
        public bool closed = false;                                 // 是否閉合
        [Range(2, 64)] public int samplesPerSeg = 16;               // Gizmo 每段取樣密度（越高畫越細）
        public Color gizmoColor = new Color(0.2f, 0.9f, 0.6f, 1f);

        // 緩存
        private int _count = 0;
        private Vector3[] _ws; // 世界座標快取

        // 弧長表（把 t 對應到長度），供「依距離取樣」
        private bool _lenDirty = true;
        private float _lenTotal = 0f;
        private readonly List<float> _tSamples = new List<float>(1024);
        private readonly List<float> _sCum = new List<float>(1024); // 累積弧長(0~lenTotal)


        /// <summary>
        /// 求曲線與圓的所有交點。
        /// 回傳每個交點對應在樣條上的弧長 s（單位：公尺）。若沒有交點，回傳空陣列。
        /// - 以弧長取樣的粗掃描找變號，並對每個候選用二分法收斂。
        /// - 封閉曲線會額外檢查尾端→起點的封口段。
        /// </summary>
        public float[] IntersectCircleAll(Vector3 center, float radius, int refineIters = 24, float tol = 1e-4f)
        {
            PrepareLengthTable();
            if (_lenTotal <= 1e-5f || _count < 2 || radius <= 0f)
                return System.Array.Empty<float>();

            float L = _lenTotal;
            float r2 = radius * radius;

            // 局部：f(s) = ||Pos(s) - center||^2 - r^2
            float F(float s)
            {
                Vector3 p = GetPositionByDistance(NormS(s));
                return (p - center).sqrMagnitude - r2;
            }

            // 使用已存在的弧長取樣點作為粗掃描節點
            // _sCum[0]=0, 最後一個約= L
            int N = _sCum.Count; // PrepareLengthTable 已保證 >=2
            var roots = new List<float>(8);

            // 小工具：把根加入結果，避免重複（數值誤差）
            void AddRoot(float s)
            {
                s = NormS(s);
                for (int i = 0; i < roots.Count; i++)
                {
                    if (Mathf.Abs(Mathf.DeltaAngle(roots[i] / L * 360f, s / L * 360f)) < 1e-3f) // 角度比較避免 wrap
                        return;
                }
                roots.Add(s);
            }

            // 掃描一般相鄰樣點
            for (int i = 1; i < N; i++)
            {
                float s0 = _sCum[i - 1];
                float s1 = _sCum[i];

                float f0 = F(s0);
                float f1 = F(s1);

                if (f0 == 0f) AddRoot(s0);
                if (f1 == 0f) AddRoot(s1);

                // 變號或跨零 → 二分
                if (f0 * f1 < 0f)
                {
                    float lo = s0, hi = s1;
                    float flo = f0, fhi = f1;

                    for (int it = 0; it < refineIters; it++)
                    {
                        float mid = 0.5f * (lo + hi);
                        float fmid = F(mid);

                        if (Mathf.Abs(hi - lo) <= tol || Mathf.Abs(fmid) <= tol)
                        {
                            AddRoot(mid);
                            break;
                        }

                        if ((flo <= 0f && fmid <= 0f) || (flo >= 0f && fmid >= 0f))
                        {
                            lo = mid; flo = fmid;
                        }
                        else
                        {
                            hi = mid; fhi = fmid;
                        }

                        if (it == refineIters - 1) AddRoot(0.5f * (lo + hi));
                    }
                }
            }

            // 封閉曲線：再掃描「最後樣點 → 第一樣點(+L)」這段
            if (closed && N >= 2)
            {
                float s0 = _sCum[N - 1];
                float s1 = _sCum[0] + L;

                float f0 = F(s0);
                float f1 = F(s1);

                if (f0 * f1 < 0f)
                {
                    float lo = s0, hi = s1;
                    float flo = f0, fhi = f1;

                    for (int it = 0; it < refineIters; it++)
                    {
                        float mid = 0.5f * (lo + hi);
                        float fmid = F(mid);

                        if (Mathf.Abs(hi - lo) <= tol || Mathf.Abs(fmid) <= tol)
                        {
                            AddRoot(mid); // AddRoot 內部會做 Repeat
                            break;
                        }

                        if ((flo <= 0f && fmid <= 0f) || (flo >= 0f && fmid >= 0f))
                        {
                            lo = mid; flo = fmid;
                        }
                        else
                        {
                            hi = mid; fhi = fmid;
                        }

                        if (it == refineIters - 1) AddRoot(0.5f * (lo + hi));
                    }
                }
            }

            return roots.ToArray();
        }

        /// <summary>
        /// 以 Catmull–Rom 樣條，在參數域 t ∈ [0,1] 上取曲線上的世界座標。
        /// </summary>
        public Vector3 Position01(float t)
        {
            Prepare();
            if (_count < 2) return transform.position;

            // 映射 t 到段落
            float totalSegs = closed ? _count : _count - 1;
            float segT = Mathf.Clamp01(t) * totalSegs;
            int iSeg = Mathf.FloorToInt(segT);
            float u = segT - iSeg;

            // 取四個點（Catmull-Rom 需要 p0,p1,p2,p3）
            GetCRPoints(iSeg, out Vector3 p0, out Vector3 p1, out Vector3 p2, out Vector3 p3);

            return CatmullRom(p0, p1, p2, p3, u);
        }

        /// <summary>
        /// 計算參數域 t ∈ [0,1] 上的切線方向。
        /// 目前用小量差分近似（可改解析導數）。
        /// </summary>
        public Vector3 Tangent01(float t)
        {
            float dt = 1e-3f;
            Vector3 a = Position01(Mathf.Clamp01(t - dt));
            Vector3 b = Position01(Mathf.Clamp01(t + dt));
            Vector3 v = (b - a);
            v.y = 0f; // 若要只在平面就把 v.y=0
            return v.sqrMagnitude > 1e-8f ? v.normalized : Vector3.forward;
        }

        /// <summary>
        /// 以「距離（公尺）」取樣條上的世界座標。
        /// （自動計算總長與查表）
        /// </summary>
        public Vector3 GetPositionByDistance(float dist)
        {
            PrepareLengthTable();
            if (_lenTotal <= 1e-5f) return transform.position;
            float t = dist / _lenTotal;
            if (closed) t = Mathf.Repeat(t, 1f);
            else t = Mathf.Clamp01(t);
            return Position01(LookupTByNormalizedArc(t));
        }

        /// <summary>
        /// 以「距離（公尺）」取樣條上的切線方向。
        /// （自動計算總長與查表）
        /// </summary>
        public Vector3 GetTangentByDistance(float dist)
        {
            PrepareLengthTable();
            if (_lenTotal <= 1e-5f) return transform.forward;
            float t = dist / _lenTotal;
            if (closed) t = Mathf.Repeat(t, 1f);
            else t = Mathf.Clamp01(t);
            float t01 = LookupTByNormalizedArc(t);
            return Tangent01(t01);
        }

        /// <summary>
        /// 取得曲線的總長度。
        /// </summary>
        public float Length()
        {
            PrepareLengthTable();
            return _lenTotal;
        }

        /// <summary>
        /// Catmull–Rom 核心公式：四點 + 參數 t → 插值點。
        /// </summary>
        private static Vector3 CatmullRom(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
        {
            float t2 = t * t;
            float t3 = t2 * t;
            return 0.5f * (
                (2f * p1) +
                (-p0 + p2) * t +
                (2f * p0 - 5f * p1 + 4f * p2 - p3) * t2 +
                (-p0 + 3f * p1 - 3f * p2 + p3) * t3
            );
        }

        /// <summary>
        /// 依據段落索引，取出 Catmull–Rom 需要的四個控制點。
        /// </summary>
        private void GetCRPoints(int segIndex, out Vector3 p0, out Vector3 p1, out Vector3 p2, out Vector3 p3)
        {
            int n = _count;
            int i1 = Wrap(segIndex, n);
            int i2 = Wrap(segIndex + 1, n);
            int i0 = Wrap(segIndex - 1, n);
            int i3 = Wrap(segIndex + 2, n);

            if (!closed)
            {
                i0 = Mathf.Clamp(segIndex - 1, 0, n - 1);
                i1 = Mathf.Clamp(segIndex, 0, n - 1);
                i2 = Mathf.Clamp(segIndex + 1, 0, n - 1);
                i3 = Mathf.Clamp(segIndex + 2, 0, n - 1);
            }

            p0 = _ws[i0]; p1 = _ws[i1]; p2 = _ws[i2]; p3 = _ws[i3];
        }

        /// <summary>
        /// 安全取模，確保索引落在 [0, n)。
        /// </summary>
        private int Wrap(int i, int n) => (i % n + n) % n;

        /// <summary>
        /// 準備控制點的世界座標快取 _ws。
        /// （自動同步子物件）
        /// </summary>
        private void Prepare()
        {
            // 同步 points 清單（允許你直接拖拉子物件）
            if (points == null || points.Count == 0)
            {
                // 嘗試自動抓子物件
                points = new List<Transform>();
                foreach (Transform c in transform) points.Add(c);
            }

            _count = points.Count;
            if (_count == 0)
            {
                _ws = System.Array.Empty<Vector3>();
                return;
            }

            if (_ws == null || _ws.Length != _count) _ws = new Vector3[_count];
            for (int i = 0; i < _count; i++)
                _ws[i] = points[i] ? points[i].position : transform.position;
        }

        /// <summary>
        /// 建立弧長表，用於「依距離取樣」。
        /// 會計算總長 _lenTotal，並填入 _sCum / _tSamples。
        /// </summary>
        private void PrepareLengthTable()
        {
            Prepare();
            if (_count < 2) { _lenTotal = 0f; return; }
            if (!_lenDirty && _sCum.Count > 1) return;

            _tSamples.Clear(); _sCum.Clear();
            _tSamples.Add(0f); _sCum.Add(0f);
            Vector3 prev = Position01(0f);
            float s = 0f;

            int segs = Mathf.Max(1, (_count - (closed ? 0 : 1)) * samplesPerSeg);
            for (int i = 1; i <= segs; i++)
            {
                float t = (float)i / segs;
                Vector3 p = Position01(t);
                s += Vector3.Distance(prev, p);
                _tSamples.Add(t);
                _sCum.Add(s);
                prev = p;
            }

            _lenTotal = s;
            _lenDirty = false;
        }

        /// <summary>
        /// 給定標準化的弧長比例 t01 (0~1)，查表換算回參數域 t。
        /// （用二分搜尋找區間）
        /// </summary>
        private float LookupTByNormalizedArc(float t01)
        {
            if (_sCum.Count < 2) return t01;
            float targetS = t01 * _lenTotal;

            int hi = _sCum.Count - 1;
            int lo = 0;
            while (lo < hi)
            {
                int mid = (lo + hi) >> 1;
                if (_sCum[mid] < targetS) lo = mid + 1; else hi = mid;
            }

            int i = Mathf.Clamp(lo, 1, _sCum.Count - 1);
            float s0 = _sCum[i - 1];
            float s1 = _sCum[i];
            float u = (targetS - s0) / Mathf.Max(1e-5f, s1 - s0);
            return Mathf.Lerp(_tSamples[i - 1], _tSamples[i], u);
        }

        /// <summary>
        /// 規範弧長 s：若 closed 則 Repeat，否則 Clamp。
        /// </summary>
        private float NormS(float s)
        {
            if (closed) return Mathf.Repeat(s, _lenTotal);
            return Mathf.Clamp(s, 0f, _lenTotal);
        }

#if UNITY_EDITOR

        /// <summary>
        /// 在 SceneView 繪製 Gizmos（曲線 + 控制點）。
        /// </summary>
        private void OnDrawGizmos()
        {
            Prepare();
            if (_count < 2) return;

            Gizmos.color = gizmoColor;
            Vector3 prev = Position01(0f);
            int segs = Mathf.Max(1, (_count - (closed ? 0 : 1)) * samplesPerSeg);
            for (int i = 1; i <= segs; i++)
            {
                float t = (float)i / segs;
                Vector3 p = Position01(t);
                Gizmos.DrawLine(prev, p);
                prev = p;
            }

            // 畫控制點
            Gizmos.color = Color.white;
            for (int i = 0; i < _count; i++)
            {
                Vector3 p = _ws[i];
                Gizmos.DrawSphere(p, 0.1f);
            }
        }

        /// <summary>
        /// 當子物件結構改變時，標記長度表為 dirty。
        /// </summary>
        private void OnTransformChildrenChanged()
        {
            _lenDirty = true;
        }

        /// <summary>
        /// 當 Inspector 參數改變時，標記長度表為 dirty。
        /// </summary>
        private void OnValidate()
        {
            _lenDirty = true;
        }
#endif
    }
}