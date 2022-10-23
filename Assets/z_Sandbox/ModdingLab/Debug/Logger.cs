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
