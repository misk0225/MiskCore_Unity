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
    public interface ICondition<A1>
    {
        public bool Do(A1 arg1);
    }
    public interface ICondition<A1, A2>
    {
        public bool Do(A1 arg1, A2 arg2);
    }


    public struct Condition : ICondition
    {
        public static readonly Condition True = new Condition(() => true);
        public static readonly Condition False = new Condition(() => false);

        public static Condition And(ICondition c1, ICondition c2)
        {
            return new Condition(() => c1.Do() && c2.Do());
        }
        public static Condition And(ICondition[] cs)
        {
            return new Condition(() =>
            {
                if (cs.Length == 0) return false;

                foreach (ICondition c in cs)
                {
                    if (!c.Do())
                        return false;
                }

                return true;
            });
        }
        public static Condition Or(ICondition c1, ICondition c2)
        {
            return new Condition(() => c1.Do() || c2.Do());
        }
        public static Condition Or(ICondition[] cs)
        {
            return new Condition(() =>
            {
                if (cs.Length == 0) return false;

                foreach (ICondition c in cs)
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
    public struct Condition<A1> : ICondition<A1>
    {
        public static readonly Condition<A1> True = new Condition<A1>((a1) => true);
        public static readonly Condition<A1> False = new Condition<A1>((a1) => false);

        public static Condition<A1> And(ICondition<A1> c1, ICondition<A1> c2)
        {
            return new Condition<A1>((a1) => c1.Do(a1) && c2.Do(a1));
        }
        public static Condition<A1> And(ICondition<A1>[] cs)
        {
            return new Condition<A1>((a1) =>
            {
                if (cs.Length == 0) return false;

                foreach (ICondition<A1> c in cs)
                {
                    if (!c.Do(a1))
                        return false;
                }

                return true;
            });
        }
        public static Condition<A1> Or(ICondition<A1> c1, ICondition<A1> c2)
        {
            return new Condition<A1>((a1) => c1.Do(a1) || c2.Do(a1));
        }
        public static Condition<A1> Or(ICondition<A1>[] cs)
        {
            return new Condition<A1>((a1) =>
            {
                if (cs.Length == 0) return false;

                foreach (ICondition<A1> c in cs)
                {
                    if (c.Do(a1))
                        return true;
                }

                return false;
            });
        }



        private Func<A1, bool> _Func;

        public Condition(Func<A1, bool> func)
        {
            _Func = func;
        }

        public bool Do(A1 arg1) => _Func(arg1);


    }
    public struct Condition<A1, A2> : ICondition<A1, A2>
    {
        public static readonly Condition<A1, A2> True = new Condition<A1, A2>((a1, a2) => true);
        public static readonly Condition<A1, A2> False = new Condition<A1, A2>((a1, a2) => false);

        public static Condition<A1, A2> And(ICondition<A1, A2> c1, ICondition<A1, A2> c2)
        {
            return new Condition<A1, A2>((a1, a2) => c1.Do(a1, a2) && c2.Do(a1, a2));
        }
        public static Condition<A1, A2> And(ICondition<A1, A2>[] cs)
        {
            return new Condition<A1, A2>((a1, a2) =>
            {
                if (cs.Length == 0) return false;

                foreach (ICondition<A1, A2> c in cs)
                {
                    if (!c.Do(a1, a2))
                        return false;
                }

                return true;
            });
        }
        public static Condition<A1, A2> Or(ICondition<A1, A2> c1, ICondition<A1, A2> c2)
        {
            return new Condition<A1, A2>((a1, a2) => c1.Do(a1, a2) || c2.Do(a1, a2));
        }
        public static Condition<A1, A2> Or(ICondition<A1, A2>[] cs)
        {
            return new Condition<A1, A2>((a1, a2) =>
            {
                if (cs.Length == 0) return false;

                foreach (ICondition<A1, A2> c in cs)
                {
                    if (c.Do(a1, a2))
                        return true;
                }

                return false;
            });
        }



        private Func<A1, A2, bool> _Func;

        public Condition(Func<A1, A2, bool> func)
        {
            _Func = func;
        }

        public bool Do(A1 arg1, A2 arg2) => _Func(arg1, arg2);


    }


    public class ConditionAdapter<A1> : ICondition<A1>
    {
        private ICondition _Condition;

        public ConditionAdapter(ICondition condition)
        {
            _Condition = condition;
        }

        public bool Do(A1 arg1)
        {
            return _Condition.Do();
        }
    }
    public class ConditionAdapter<A1, A2> : ICondition<A1, A2>
    {
        private ICondition<A1> _Condition;

        public ConditionAdapter(ICondition<A1> condition) { _Condition = condition; }
        public ConditionAdapter(ICondition condition) : this(new ConditionAdapter<A1>(condition)) { }

        public bool Do(A1 arg1, A2 arg2)
        {
            return _Condition.Do(arg1);
        }
    }
}

