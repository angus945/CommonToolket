using System;
using UnityEngine;

namespace DataDriven.Localization
{
    [System.Serializable]
    public class LocalizationItem
    {
        public string key;
        public string value;
        //public string origin_value;

        public string color;
        public string note;

        public LocalizationItem(string key, string value, string color, string note)
        {
            this.key = key;
            this.value = value;
            this.color = color;
            this.note = note;
        }
        public override string ToString()
        {
            return $"key: {key}, value: {value}";
        }

        public string GetResult()
        {
            if (string.IsNullOrEmpty(color))
            {
                return value;
            }
            else return $"<color={color}>{value}</color>";
        }
    }

    [System.Serializable]
    class LocalizationFont
    {
        const string resourcesFontFolder = "Fonts";

        public string fontName;
        public bool fromOS;
        public int size;

        public Font GetFont()
        {
            if(fromOS)
            {
                return Font.CreateDynamicFontFromOSFont(fontName, size);
            }
            else
            {
                return Resources.Load<Font>($"{resourcesFontFolder}/{fontName}");
            }
        }
    }

}
