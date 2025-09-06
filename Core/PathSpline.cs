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
        public bool closed = false;                 // 是否閉合
        [Range(2, 64)] public int samplesPerSeg = 16; // Gizmo 每段取樣密度（越高畫越細）
        public Color gizmoColor = new Color(0.2f, 0.9f, 0.6f, 1f);

        // ---- 公開 API ----
        /// t ∈ [0,1] 回傳位置（Catmull-Rom）
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

        /// 切線（方向）；用小量差分估
        public Vector3 Tangent01(float t)
        {
            float dt = 1e-3f;
            Vector3 a = Position01(Mathf.Clamp01(t - dt));
            Vector3 b = Position01(Mathf.Clamp01(t + dt));
            Vector3 v = (b - a);
            v.y = 0f; // 若要只在平面就把 v.y=0
            return v.sqrMagnitude > 1e-8f ? v.normalized : Vector3.forward;
        }

        /// 以「距離（公尺）」取位置/切線（會自動計算總長與查表）
        public Vector3 GetPositionByDistance(float dist)
        {
            PrepareLengthTable();
            if (_lenTotal <= 1e-5f) return transform.position;
            float t = dist / _lenTotal;
            if (closed) t = Mathf.Repeat(t, 1f);
            else t = Mathf.Clamp01(t);
            return Position01(LookupTByNormalizedArc(t));
        }

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

        public float Length()
        {
            PrepareLengthTable();
            return _lenTotal;
        }

        // ---- 內部：Catmull–Rom ----
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

        private int Wrap(int i, int n) => (i % n + n) % n;

        // ---- 內部：緩存 ----
        private int _count = 0;
        private Vector3[] _ws; // 世界座標快取

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

        // ---- 弧長表（把 t 對應到長度），供「依距離取樣」 ----
        private bool _lenDirty = true;
        private float _lenTotal = 0f;
        private readonly List<float> _tSamples = new List<float>(1024);
        private readonly List<float> _sCum = new List<float>(1024); // 累積弧長(0~lenTotal)

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

#if UNITY_EDITOR
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

        private void OnTransformChildrenChanged()
        {
            _lenDirty = true;
        }
        private void OnValidate()
        {
            _lenDirty = true;
        }
#endif
    }
}
