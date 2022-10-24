using MoonSharp.Interpreter;
using System;

namespace ModdingLab.Instance.Behavior
{

    public class LuaFunction
    {
        public readonly string name;
        float call;

        float timer;
        DynValue function;

        Action<float> updateHandler;

        public LuaFunction(string name, float call, int type)
        {
            this.name = name;
            this.call = call;

            switch (type)
            {
                case 0:
                    updateHandler = Update_Auto;
                    break;

                case 1:
                    updateHandler = Update_Counter;
                    break;

                case 2:
                    updateHandler = Update_Timer;
                    break;
            }
        }
        public void Initial(Script script)
        {
            function = script.Globals.Get(name);
        }
        public void Update(float delta)
        {
            if (function == null) return;
            if (function.Function == null) return;

            updateHandler.Invoke(delta);
        }

        void Update_Auto(float delta)
        {
            function.Function.Call();
        }
        void Update_Timer(float delta)
        {
            timer += delta;

            if (timer >= call)
            {
                function.Function.Call();
                timer = 0;
            }
        }
        void Update_Counter(float delta)
        {
            timer += 1;

            if (timer >= call)
            {
                function.Function.Call();
                timer = 0;
            }
        }
    }

    public class LuaBehavior
    {
        bool isActive;

        DynValue reset;
        LuaFunction[] functions;

        public LuaBehavior(bool isActive, Script script, LuaFunction[] functions)
        {
            this.isActive = isActive;
            this.functions = functions;

            reset = script.Globals.Get("Reset");

            for (int i = 0; i < functions.Length; i++)
            {
                LuaFunction function = functions[i];

                function.Initial(script);
            }
        }

        public void Reset()
        {
            reset.Function.Call();
        }
        public void Update(float delta)
        {
            if (!isActive) return;

            for (int i = 0; i < functions.Length; i++)
            {
                functions[i].Update(delta);
            }
        }
    }

}
