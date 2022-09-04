using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameObjectExtension
{
    public static void SetObjectLayer(this GameObject gameObject, int layer)
    {
        gameObject.layer = layer;

        foreach (Transform child in gameObject.transform)
        {
            SetObjectLayer(child, layer);
        }
    }
    public static void SetObjectLayer(this MonoBehaviour mono, int layer)
    {
        SetObjectLayer(mono.gameObject, layer);
    }
    public static void SetObjectLayer(this Component comp, int layer)
    {
        SetObjectLayer(comp.gameObject, layer);
    }


}
