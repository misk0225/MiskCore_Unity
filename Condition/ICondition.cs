using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MiskCore.Condition
{
    public interface ICondition
    {
        public bool Do();
    }

    public struct Condition : ICondition
    {
        public static readonly Condition True = new Condition(() => true);
        public static readonly Condition False = new Condition(() => false);

        public static Condition And(Condition c1, Condition c2)
        {
            return new Condition(() => c1.Do() && c2.Do());
        }
        public static Condition And(Condition[] cs)
        {
            return new Condition(() =>
            {
                if (cs.Length == 0) return false;

                foreach (Condition c in cs)
                {
                    if (!c.Do())
                        return false;
                }

                return true;
            });
        }
        public static Condition Or(Condition c1, Condition c2)
        {
            return new Condition(() => c1.Do() || c2.Do());
        }
        public static Condition Or(Condition[] cs)
        {
            return new Condition(() =>
            {
                if (cs.Length == 0) return false;

                foreach (Condition c in cs)
                {
                    if (c.Do())
                        return true;
                }

                return false;
            });
        }



        private Func<bool> _Func;

        public Condition(Func<bool> func)
        {
            _Func = func;
        }

        public bool Do() => _Func();


    }
}

