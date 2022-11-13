using MoonSharp.Interpreter;
using System;

namespace ModdingLab.Instance.Behavior
{

    public class LuaFunction
    {
        public readonly string name;

        float timer;
        float callTime;
        Func<float, bool> timerHandler;

        DynValue function;

        GameEntity entity;

        public LuaFunction(float call, int type, GameEntity entity, DynValue function)
        {
            this.callTime = call;
            this.entity = entity;
            this.function = function;

            switch (type)
            {
                case 0:
                    timerHandler = Timer_None;
                    break;

                case 1:
                    timerHandler = Timer_Frame;
                    break;

                case 2:
                    timerHandler = Timer_Time;
                    break;
            }
        }
        public void Update(float delta)
        {
            if (function == null) return;
            if (function.Function == null) return;

            if(timerHandler.Invoke(delta))
            {
                function.Function.Call(entity);
            }
        }

        bool Timer_None(float delta)
        {
            return true;
        }
        bool Timer_Time(float delta)
        {
            timer += delta;

            if (timer >= callTime)
            {
                timer = 0;
                return true;
            }

            return false;
        }
        bool Timer_Frame(float delta)
        {
            timer += 1;

            if (timer >= callTime)
            {
                timer = 0;
                return true;
            }

            return false;
        }
    }

    public class LuaBehavior
    {
        bool isActive;

        LuaFunction[] functions;

        public LuaBehavior(bool isActive, LuaFunction[] functions)
        {
            this.isActive = isActive;
            this.functions = functions;
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
