﻿using System;
using System.Collections.Generic;


public static class ArrayExtensions
{ 
    public static void Shuffle<T>(this IList<T> collection)
    {
        for (int i = 0; i < collection.Count; i++)
        {
            int target = UnityEngine.Random.Range(0, collection.Count);

            T elementValue = collection[i];
            T targetValue = collection[target];

            collection[i] = targetValue;
            collection[target] = elementValue;
        }
    }
    public static T Random<T>(this IList<T> collection)
    {
        int index = UnityEngine.Random.Range(0, collection.Count);
        return collection[index];
    }
    public static string PrintOut<T>(this IList<T> collection, bool pritty = false)
    {
        if (collection == null) return "[]";

        string data = "";
        for (int i = 0; i < collection.Count; i++)
        {
            T item = collection[i];

            if (i == 0)
            {
                data += item.ToString();
            }
            else if(pritty)
            {
                data += ",\n" + item.ToString();
            }
            else data += "," + item.ToString();
        }

        //new string(data.arr, 4);
        //data.Replace("\n", "\n    ");

        return pritty ? $"[\n{data}\n]" :  $"[{data}]";
    }
}

