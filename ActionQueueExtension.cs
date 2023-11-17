using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MiskCore
{
    public static class ActionQueueExtension
    {
        public static void PushCallback(this ActionQueue action, Action callback)
        {
            action.Push(() =>
            {
                callback();
                action.Next();
            });
        }
        public static void PushCallback(this ActionQueue action, Action<ActionQueue> callback)
        {
            action.Push((instance) =>
            {
                callback(instance);
                instance.Next();
            });
        }
        public static void InsertCallback(this ActionQueue action, Action callback)
        {
            action.InsertFirst(() =>
            {
                callback();
                action.Next();
            });
        }
        public static void InsertCallback(this ActionQueue action, Action<ActionQueue> callback)
        {
            action.InsertFirst((instance) =>
            {
                callback(instance);
                instance.Next();
            });
        }
    }
}

