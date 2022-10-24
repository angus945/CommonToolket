using MoonSharp.Interpreter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModdingLab.Instance.Behavior
{
    enum UpdateType
    {
        Auto = 0,
        Time = 1,
        Frame = 2,
    }

    public class LuaFunction
    {
        string name;
        float call;

        UpdateType type;

        float timer;
        DynValue function;

        public LuaFunction(string name,  float call, int type)
        {
            this.name = name;
            this.call = call;
            this.type = (UpdateType)type;
        }

        public void Update(float delta)
        {

        }
    }

    public class LuaBehavior
    {
        bool isActive;

        Script script;

        LuaFunction[] functions;

        public LuaBehavior(bool isActive, Script script, LuaFunction[] functions)
        {
            this.isActive = isActive;
            this.script = script;
            this.functions = functions;

            Debug.Log(isActive);
        }
    }

}
