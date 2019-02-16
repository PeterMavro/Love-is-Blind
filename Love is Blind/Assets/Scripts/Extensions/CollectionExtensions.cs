using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public static class CollectionExtensions
{
    public static bool ValidIndex<T>(this IList<T> collec, int index)
    {
        return index >= 0 && index < collec.Count;
    }
}