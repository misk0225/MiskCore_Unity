using MiskCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MiskCore
{
    public class DecorateAmplifierValue<T> : AmplifierValue<T>
    {
        private Dictionary<string, Context> _ContextsMap = new Dictionary<string, Context>();

        public DT GetContext <DT> () where DT : Context, new()
        {
            string key = typeof(DT).FullName;
            if (_ContextsMap.ContainsKey(key))
                return (DT)_ContextsMap[key];
            else
            {
                DT context = new DT();
                context.Amplifier = this;
                _ContextsMap.Add(key, context);
                return context;
            }
        }

        public class Context
        {
            public AmplifierValue<T> Amplifier;
        }
    }


    public class SFAmplifierValue<T, A1> : AmplifierValue<T, A1>
    {
        private Dictionary<string, object> _ContextsMap = new Dictionary<string, object>();

        public DT GetContext<DT>() where DT : new()
        {
            string key = typeof(DT).FullName;
            if (_ContextsMap.ContainsKey(key))
                return (DT)_ContextsMap[key];
            else
            {
                DT context = new DT();
                _ContextsMap.Add(key, context);
                return context;
            }
        }
    }
}

