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
        public static Vector3 up => Vector3.up;
        public static Vector3 down => Vector3.down;
        public static Vector3 right => Vector3.right;
        public static Vector3 left => Vector3.left;

        public static Vector3 Direction(Vector3 from, Vector3 to)
        {
            return Vector3.Normalize(to - from);
        }
        public static float Distance(Vector3 a, Vector3 b)
        {
            return Vector3.Distance(a, b);
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