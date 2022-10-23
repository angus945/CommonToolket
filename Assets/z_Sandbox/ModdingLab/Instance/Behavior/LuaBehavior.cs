using MoonSharp.Interpreter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModdingLab.Instance.Behavior
{
    enum UpdateType
    {
        Auto,
        Time,
        Frame,
    }

    public class LuaFunction
    {
        DynValue function;

        float timer;
        float call;

        UpdateType type;

        public void Update(float delta)
        {

        }
    }

    public class LuaBehavior
    {
        bool isActive;

        Script script;

        LuaFunction[] functions;
    }

}
