using MoonSharp.Interpreter;
using System.Collections.Generic;
using UnityEngine;

namespace DataDriven.Lua.Library
{
    public static class LuaLibrarys
    {
        public static readonly Dictionary<string, System.Type> LibraryTable = new Dictionary<string, System.Type>()
        {
            ["Math"] = typeof(Math),
            ["Vector"] = typeof(Vector),
            ["Time"] = typeof(Time),
        };
    }

    [MoonSharpUserData]
    public class Math
    {
        public static float lerp(float a, float b, float t)
        {
            return Mathf.Lerp(a, b, t);
        }
        public static float Pow(float f, float p)
        {
            return Mathf.Pow(f, p);
        }
    }

    [MoonSharpUserData]
    public class Vector
    {
        public static Vector2 up => Vector2.up;
        public static Vector2 down => Vector2.down;
        public static Vector2 right => Vector2.right;
        public static Vector2 left => Vector2.left;

        public static Vector2 Direction(Vector2 from, Vector2 to)
        {
            return (to - from).normalized;
        }
        public static float Distance(Vector2 a, Vector2 b)
        {
            return Vector2.Distance(a, b);
        }
    }

    [MoonSharpUserData]
    public class Time
    {
        public static float time => UnityEngine.Time.time;
        public static float delta => UnityEngine.Time.deltaTime;

        public static float fixedTime => UnityEngine.Time.fixedTime;
        public static float fixedDelta => UnityEngine.Time.fixedDeltaTime;
    }
}