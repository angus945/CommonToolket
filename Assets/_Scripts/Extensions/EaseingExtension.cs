using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EaseingExtension
{
    public static float EaseOut(this float t)
    {
        return 1 - Mathf.Pow(1 - t, 2);
    }

}
