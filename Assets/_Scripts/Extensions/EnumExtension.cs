using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnumExtension
{
    public static T Next<T>(this T type) where T : Enum
    {
        int count = Enum.GetNames(typeof(T)).Length;
        int index = (int)(object)type;

        int next = (index + 1) % count;

        return (T)(object)next;
    }
}
