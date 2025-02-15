using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ListExtension
{
    public static T Random<T> (this IList<T> collection)
    {
        if (collection.Count > 0)
            return collection[UnityEngine.Random.Range(0, collection.Count)];
        else
            return default(T);
    }

    public static T Pop<T> (this IList<T> collection, int index)
    {
        T e = collection[index];
        collection.RemoveAt(index);
        return e;
    }
}
