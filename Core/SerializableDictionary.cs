using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MiskCore
{
    // TODO: 之後可以補上更多方法
    [Serializable]
    public class SerializableDictionary<TKey, TValue> : ISerializationCallbackReceiver
    {
        #region Property Name
        public const string PropertyName_keys = "keys";
        public const string PropertyName_values = "values";
        #endregion

        [SerializeField] private List<TKey> keys = new List<TKey>();
        [SerializeField] private List<TValue> values = new List<TValue>();

        private Dictionary<TKey, TValue> _Map;

        public Dictionary<TKey, TValue> _
        {
            get
            {
                if (_Map == null)
                    RebuildMap();
                return _Map;
            }
        }

        public TValue this[TKey index] => _[index];
        public bool ContainsKey(TKey key) => _.ContainsKey(key);
        public bool ContainsValue(TValue value) => _.ContainsValue(value);
        public int Count => _.Count;

        public void OnBeforeSerialize()
        {
            // 如果需要將 _Map 內容回灌到 lists，可在此同步
        }

        public void OnAfterDeserialize()
        {
            RebuildMap();
        }

        private void RebuildMap()
        {
            _Map = new Dictionary<TKey, TValue>();
            int count = Mathf.Min(keys.Count, values.Count);
            for (int i = 0; i < count; i++)
            {
                // 需要避免重複 key 的話可加判斷
                if (!_Map.ContainsKey(keys[i]))
                    _Map.Add(keys[i], values[i]);
            }
        }


#if UNITY_EDITOR
        /// <summary>
        /// 以傳入的 keys/values 直接覆寫這個 SerializableDictionary 的序列化資料。
        /// </summary>
        /// <param name="property">指向字典欄位（例如 A.dir）的 SerializedProperty</param>
        /// <param name="newKeys">要寫入的 Key 清單</param>
        /// <param name="newValues">要寫入的 Value 清單</param>
        /// <param name="clearBefore">寫入前是否先清空（預設 true）</param>
        public static void ModifySerialized(
            UnityEditor.SerializedProperty property,
            IList<TKey> newKeys,
            IList<TValue> newValues,
            bool clearBefore = true)
        {
            if (property == null) throw new ArgumentNullException(nameof(property));

            var keysProp = property.FindPropertyRelative(PropertyName_keys);
            var valuesProp = property.FindPropertyRelative(PropertyName_values);

            if (keysProp == null || valuesProp == null)
                throw new InvalidOperationException(
                    $"找不到 '{PropertyName_keys}' / '{PropertyName_values}'，請確認欄位名稱。");

            if (clearBefore)
            {
                keysProp.ClearArray();
                valuesProp.ClearArray();
            }

            // 只寫入能一一配對的最小長度
            int count = Mathf.Min(newKeys?.Count ?? 0, newValues?.Count ?? 0);
            for (int i = 0; i < count; i++)
            {
                int idx = keysProp.arraySize;
                keysProp.InsertArrayElementAtIndex(idx);
                valuesProp.InsertArrayElementAtIndex(idx);

                var keyElem = keysProp.GetArrayElementAtIndex(idx);
                var valueElem = valuesProp.GetArrayElementAtIndex(idx);

                // 寫入 Key
                WriteValueToProperty(keyElem, newKeys[i], isKey: true);

                // 寫入 Value
                WriteValueToProperty(valueElem, newValues[i], isKey: false);
            }
        }

        /// <summary>
        /// 針對常見型別（enum / Object / int / float / bool / string）做安全賦值。
        /// 你的案例（enum + Transform）會走 enumValueIndex / objectReferenceValue。
        /// </summary>
        private static void WriteValueToProperty<T>(UnityEditor.SerializedProperty prop, T value, bool isKey)
        {
            var t = typeof(T);

            // enum（最常見的 key）
            if (t.IsEnum)
            {
                prop.enumValueIndex = Convert.ToInt32(value);
                return;
            }

            // UnityEngine.Object（最常見的 value：Transform/Collider/ScriptableObject...）
            if (typeof(UnityEngine.Object).IsAssignableFrom(t))
            {
                prop.objectReferenceValue = value as UnityEngine.Object;
                return;
            }

            // 一些基本值型態（視需要擴充）
            if (t == typeof(int))
            {
                prop.intValue = Convert.ToInt32(value);
                return;
            }
            if (t == typeof(float))
            {
                prop.floatValue = Convert.ToSingle(value);
                return;
            }
            if (t == typeof(bool))
            {
                prop.boolValue = Convert.ToBoolean(value);
                return;
            }
            if (t == typeof(string))
            {
                prop.stringValue = value as string;
                return;
            }

            // 其他進階型別（Vector、Color、Quaternion、AnimationCurve、ManagedReference 等）
            // 需要對應 SerializedPropertyType 個別賦值；這裡先丟出例外避免靜默錯誤。
            throw new NotSupportedException(
                $"{(isKey ? "Key" : "Value")} 類型 {t.Name} 尚未實作寫入對應，請擴充 WriteValueToProperty。");
        }
#endif
    }
}
