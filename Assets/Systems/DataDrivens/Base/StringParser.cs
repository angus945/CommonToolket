using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataDriven
{
    public static class StringParser 
    {
        public static float ParseToFloat(this string input)
        {
            try
            {
                return float.Parse(input);
            }
            catch (System.Exception)
            {
                LogPrinter.Print($"parsing error: {input}");

                return 0;
            }
        }

        public static Vector2 ParseToVector2(this string input)
        {
            string[] values = input.Split(' ');

            try
            {
                float x = float.Parse(values[0]);
                float y = float.Parse(values[1]);
                return new Vector2(x, y);
            }
            catch (System.Exception)
            {
                LogPrinter.Print($"parsing error: {input}");

                return Vector2.zero;
            }
        }
        public static Vector3 ParseToVector3(this string input)
        {
            string[] values = input.Split(' ');

            try
            {
                float x = float.Parse(values[0]);
                float y = float.Parse(values[1]);
                float z = float.Parse(values[2]);
                return new Vector3(x, y, z);
            }
            catch (System.Exception)
            {
                LogPrinter.Print($"parsing error: {input}");

                return Vector3.zero;
            }
        }
    }
}
