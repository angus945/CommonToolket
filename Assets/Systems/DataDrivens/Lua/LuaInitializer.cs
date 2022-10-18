using System;
using System.Collections;
using System.Collections.Generic;
using MoonSharp.Interpreter;
using DataDriven.Lua.Library;

namespace DataDriven.Lua
{
    // include = {"Math", "Vector"}
    public class LuaInitializer
    {
        static bool isInitialized;

        static Action<string> logHandler;

        public static void Initialize(Action<string> logHandler = null, params System.Type[] registerTypes)
        {
            if (isInitialized) return;

            SetLogger(logHandler);
            RegisterCommonType();
            RegisterLiberary();
            RegisterTypes(registerTypes);

            isInitialized = true;
        }
        static void SetLogger(Action<string> logHandler)
        {
            LuaInitializer.logHandler = logHandler;

            Script.DefaultOptions.DebugPrint = logHandler;
        }
        static void RegisterCommonType()
        {
            UserData.RegisterType<UnityEngine.Vector2>();
            UserData.RegisterType<UnityEngine.Vector3>();

            UserData.RegisterType<UnityEngine.Transform>();
        }
        static void RegisterLiberary()
        {
            UserData.RegisterType<DataDriven.Lua.Library.Math>();
            UserData.RegisterType<DataDriven.Lua.Library.Vector>();
            UserData.RegisterType<DataDriven.Lua.Library.Time>();
        }
        public static void RegisterTypes(params System.Type[] types)
        {
            for (int i = 0; i < types.Length; i++)
            {
                UserData.RegisterType(types[i]);
            }
        }

        public static void IncludeLiberary(Script script)
        {
            Initialize(UnityEngine.Debug.Log);

            DynValue includes = script.Globals.Get("include");
            foreach (DynValue libName in includes.Table.Values)
            {
                if(LuaLibrarys.LibraryTable.TryGetValue(libName.String, out System.Type type))
                {
                    script.Globals[libName] = type;
                }
                else
                {
                    logHandler.Invoke($"Undefined Include Type: {libName}");
                }
            }
        }

    }
}
