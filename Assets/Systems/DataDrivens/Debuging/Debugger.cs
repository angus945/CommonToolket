using System;
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

    public class Debugger
    {
        static float tick;
        static string recordMessage;

        public static event Action<string> OnLogPrinting;

        public static void Tick(string message = null)
        {
            float last = tick;
            tick = Time.realtimeSinceStartup;

            if (message == null) return;

            float duration = tick - last;
            string text = $"Time: {duration.ToString("f4")}, {message}";

            Debugger.RecordLog(text);
        }
        public static void RecordLog(object message)
        {
            recordMessage += message + "\n";

            Debug.Log($"message recorded: {message}");
        }
        public static void PrintLog()
        {
            if (string.IsNullOrEmpty(recordMessage)) return;

            string message = $"Log: \n{recordMessage}\n";

            recordMessage = "";

            OnLogPrinting?.Invoke(message);

            //Debug.Log(message);
        }
    }
}
