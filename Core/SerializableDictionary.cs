using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MiskCore
{
    // TODO: ����i�H�ɤW��h��k
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
            // �p�G�ݭn�N _Map ���e�^��� lists�A�i�b���P�B
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
                // �ݭn�קK���� key ���ܥi�[�P�_
                if (!_Map.ContainsKey(keys[i]))
                    _Map.Add(keys[i], values[i]);
            }
        }


#if UNITY_EDITOR
        /// <summary>
        /// �H�ǤJ�� keys/values �����мg�o�� SerializableDictionary ���ǦC�Ƹ�ơC
        /// </summary>
        /// <param name="property">���V�r�����]�Ҧp A.dir�^�� SerializedProperty</param>
        /// <param name="newKeys">�n�g�J�� Key �M��</param>
        /// <param name="newValues">�n�g�J�� Value �M��</param>
        /// <param name="clearBefore">�g�J�e�O�_���M�š]�w�] true�^</param>
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
                    $"�䤣�� '{PropertyName_keys}' / '{PropertyName_values}'�A�нT�{���W�١C");

            if (clearBefore)
            {
                keysProp.ClearArray();
                valuesProp.ClearArray();
            }

            // �u�g�J��@�@�t�諸�̤p����
            int count = Mathf.Min(newKeys?.Count ?? 0, newValues?.Count ?? 0);
            for (int i = 0; i < count; i++)
            {
                int idx = keysProp.arraySize;
                keysProp.InsertArrayElementAtIndex(idx);
                valuesProp.InsertArrayElementAtIndex(idx);

                var keyElem = keysProp.GetArrayElementAtIndex(idx);
                var valueElem = valuesProp.GetArrayElementAtIndex(idx);

                // �g�J Key
                WriteValueToProperty(keyElem, newKeys[i], isKey: true);

                // �g�J Value
                WriteValueToProperty(valueElem, newValues[i], isKey: false);
            }
        }

        /// <summary>
        /// �w��`�����O�]enum / Object / int / float / bool / string�^���w����ȡC
        /// �A���רҡ]enum + Transform�^�|�� enumValueIndex / objectReferenceValue�C
        /// </summary>
        private static void WriteValueToProperty<T>(UnityEditor.SerializedProperty prop, T value, bool isKey)
        {
            var t = typeof(T);

            // enum�]�̱`���� key�^
            if (t.IsEnum)
            {
                prop.enumValueIndex = Convert.ToInt32(value);
                return;
            }

            // UnityEngine.Object�]�̱`���� value�GTransform/Collider/ScriptableObject...�^
            if (typeof(UnityEngine.Object).IsAssignableFrom(t))
            {
                prop.objectReferenceValue = value as UnityEngine.Object;
                return;
            }

            // �@�ǰ򥻭ȫ��A�]���ݭn�X�R�^
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

            // ��L�i�����O�]Vector�BColor�BQuaternion�BAnimationCurve�BManagedReference ���^
            // �ݭn���� SerializedPropertyType �ӧO��ȡF�o�̥���X�ҥ~�קK�R�q���~�C
            throw new NotSupportedException(
                $"{(isKey ? "Key" : "Value")} ���� {t.Name} �|����@�g�J�����A���X�R WriteValueToProperty�C");
        }
#endif
    }
}
