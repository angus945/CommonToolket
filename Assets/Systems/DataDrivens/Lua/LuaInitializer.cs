using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoonSharp.Interpreter;
using DataDriven.Lua.Library;

namespace DataDriven.Lua
{
    // include = {"Math", "Vector"}

    public class LuaInitializer
    {
        public static void SetLuaLogger()
        {
            Script.DefaultOptions.DebugPrint = (s) => Debug.Log(s);
        }
        public static void RegisterCommonType()
        {
            UserData.RegisterType<Vector2>();
            UserData.RegisterType<Vector3>();

            UserData.RegisterType<Transform>();
        }
        public static Script CreateScript(string code)
        {
            Script script = new Script();

            script.DoString(code);

            DynValue includes = script.Globals.Get("include");
            foreach (DynValue libName in includes.Table.Values)
            {
                if(LuaLibrarys.LibraryTable.TryGetValue(libName.String, out System.Type type))
                {
                    UserData.RegisterType(type);
                    script.Globals[libName] = type;
                }
                else
                {
                    Debug.LogWarning($"Undefined Include Type: {libName}");
                }
            }

            return script;
        }

    }
}
namespace DataDriven.Lua.Library
{
    public static class LuaLibrarys
    {
        public static readonly Dictionary<string, System.Type> LibraryTable = new Dictionary<string, System.Type>()
        {
            ["Math"] = typeof(Math),
            ["Vector"] = typeof(Vector),
        };
    }

    [MoonSharpUserData]
    public class Math
    {
        public static float Pow(float f, float p)
        {
            return Mathf.Pow(f, p);
        }
    }

    [MoonSharpUserData]
    public class Vector
    {
        public static Vector3 up => Vector3.up;

        public static float Distance(Vector3 a, Vector3 b)
        {
            return Vector3.Distance(a, b);
        }
    }
}