using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModdingLab
{
    public enum LogType
    {
        Normal,
        Warning,
        Error,
    }

    public class Logger
    {
        static float tick;

        public static void Tick(string message = "")
        {
            float last = tick;
            tick = Time.realtimeSinceStartup;

            if (message == null) return;

            float duration = tick - last;
            string text = $"Time: {duration.ToString("f4")}, {message}";

            Logger.Log(text);
        }
        public static void Log(object message, LogType type = LogType.Normal)
        {
            switch (type)
            {
                case LogType.Normal:
                    Debug.Log(message);
                    break;

                case LogType.Warning:
                    Debug.LogWarning(message);
                    break;

                case LogType.Error:
                    Debug.LogError(message);
                    break;
            }
        }
    }
}
