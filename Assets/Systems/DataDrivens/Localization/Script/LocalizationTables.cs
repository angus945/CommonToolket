using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

namespace DataDriven.Localization
{
    public class LocalizationTable
    {
        //Header, Key, Value
        Dictionary<string, Dictionary<string, LocalizationItem>> table;

        public LocalizationTable(StreamingFile[] files)
        {
            table = new Dictionary<string, Dictionary<string, LocalizationItem>>();

            for (int i = 0; i < files.Length; i++)
            {
                string header = files[i].name;
                string content = files[i].ReadString();
                LocalizationItem[] items = TextAnalize.FromJsonArray<LocalizationItem>(content);

                if (!table.ContainsKey(header))
                {
                    table.Add(header, new Dictionary<string, LocalizationItem>());
                }

                AddItems(header, items);
            }
        }
        void AddItems(string header, LocalizationItem[] items)
        {
            for (int i = 0; i < items.Length; i++)
            {
                LocalizationItem item = items[i];

                table[header].Add(item.key, item);
            }
        }

        public Dictionary<string, LocalizationItem> GetTable(string header)
        {
            return table[header];
        }
    }
    public class LocalizationTables : MonoBehaviour
    {
        static string _activeLanguage;
        public static string activeLanguage
        {
            get => _activeLanguage;
            set
            {
                _activeLanguage = value;

                onActiveLanguageChanged?.Invoke();
            }
        }
        public static event Action onActiveLanguageChanged;

        static Dictionary<string, LocalizationTable> localizationTable;
        public static string[] languages { get; private set; }

        public static void LoadText()
        {
            localizationTable = new Dictionary<string, LocalizationTable>();

            StreamingItem[] contents = StreamingLoader.GetItemsWithType(KeywordDefine.LOCALIZATION);

            for (int i = 0; i < contents.Length; i++)
            {
                StreamingFolder folder = contents[i].rootFolder;

                string language = folder.folderName;
                LocalizationTable table = new LocalizationTable(folder.childFiles);

                localizationTable.Add(language, table);
            }

            languages = localizationTable.Keys.ToArray();
        }

        static Dictionary<string, LocalizationItem> GetTable(string language, string header)
        {
            if (localizationTable == null)
            {
                LoadText();
            }

            return localizationTable[language].GetTable(header);
        }
        public static string[] GetTexts(string language, string header)
        {
            List<LocalizationItem> values = new List<LocalizationItem>(GetTable(language, header).Values);

            return values.ConvertAll<string>(n => n.GetResult()).ToArray();
        }
        public static string GetText(string language, string header, string key)
        {
            if (GetTable(language, header).TryGetValue(key, out LocalizationItem item))
            {
                return item.GetResult();
            }
            else return $"<misseing content: {key}>";
        }

        public static string ReplaceTag(string language, string input, string header, out string[] tags)
        {
            Dictionary<string, LocalizationItem> table = GetTable(language, header);
            List<string> matchTags = new List<string>();

            input = Regex.Replace(input, "<(.+?)>", (Match m) =>
            {
                string key = m.Result("$1");
                if (table.TryGetValue(key, out LocalizationItem item))
                {
                    matchTags.Add(key);

                    return item.GetResult();
                }
                else return $"<{key}>";
            });

            tags = matchTags.ToArray();

            return input;
        }
    }

}