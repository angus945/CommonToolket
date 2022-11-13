using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataDriven
{
    public enum LogType
    {
        Normal,
        Warning,
        Error,
    }

    public class LogPrinter
    {
        static float tick;

        public static void Tick(string message = null)
        {
            float last = tick;
            tick = Time.realtimeSinceStartup;

            if (message == null) return;

            float duration = tick - last;
            string text = $"Time: {duration.ToString("f4")}, {message}";

            LogPrinter.Print(text);
        }
        public static void Print(object message, LogType type = LogType.Normal)
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
