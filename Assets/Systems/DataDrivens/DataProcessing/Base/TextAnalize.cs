using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace DataDriven
{

    public static class TextAnalize
    {
        static string[] Foreach(string[] texts, Func<string, string[]> handler)
        {
            List<string> result = new List<string>();
            for (int i = 0; i < texts.Length; i++)
            {
                result.AddRange(handler.Invoke(texts[i]));
            }
            return result.ToArray();
        }

        //
        static readonly Regex arrayMatch = new Regex("{(.+?)}");
        public static string[] ParseToArray(params string[] texts)
        {
            return Foreach(texts, (text) =>
            {
                text = Regex.Replace(text, @"\t|\n|\r", "");

                MatchCollection elements = arrayMatch.Matches(text);
                string[] array = new string[elements.Count];

                for (int i = 0; i < elements.Count; i++)
                {
                    array[i] = elements[i].Result("{$1}");
                }
                return array;
            });
        }
        public static string FlatToString(string[] text)
        {
            string data = "";
            for (int i = 0; i < text.Length; i++)
            {
                string item = text[i];

                if (i == 0)
                {
                    data += item.ToString();
                }
                else data += ", " + item.ToString();
            }

            return $"[{data}]";
        }

        public static string ToJsonArray(string[] contents, bool prittyPrint)
        {
            if (contents == null) return "[]";

            string data = "";
            for (int i = 0; i < contents.Length; i++)
            {
                string item = contents[i];

                if (i == 0)
                {
                    data += item.ToString();
                }
                else if (prittyPrint)
                {
                    data += ",\n" + item.ToString();
                }
                else data += "," + item.ToString();
            }

            if(prittyPrint)
            {
                data = $"\n{data}";
                data = data.Replace("\n", "\n\t");
                data = $"{data}\n";
            }

            data = $"[{data}]";
            

            return data;
        }

        public static T[] FromJsonArray<T>(string json)
        {
            string[] jsonArray = ParseToArray(json);
            T[] items = new T[jsonArray.Length];

            for (int i = 0; i < jsonArray.Length; i++)
            {
                items[i] = JsonUtility.FromJson<T>(jsonArray[i]);
            }

            return items;
        }

        static readonly Regex itemMatch = new Regex("\"(.*?)\":\"(.*?)\"");
        public static string[] ParseToItems(string text)
        {
            MatchCollection matches = itemMatch.Matches(text);
            string[] items = new string[matches.Count];

            for (int i = 0; i < matches.Count; i++)
            {
                items[i] = matches[i].Result("$1:$2");
            }
            return items;
        }
        public static string TakeItem(string take, ref string[] items)
        {
            for (int i = 0; i < items.Length; i++)
            {
                string item = items[i];
                AnalizeItem(item, out string name, out string value);

                if(name == take)
                {
                    List<string> temp = new List<string>(items);
                    temp.RemoveAt(i);

                    items = temp.ToArray();

                    return value;
                }
            }

            return "";
        }
        public static void AnalizeItem(string item, out string itemName, out string itemValue)
        {
            string[] data = item.Split(':');
            itemName = data[0];
            itemValue = data[1];
        }

    }    

}
