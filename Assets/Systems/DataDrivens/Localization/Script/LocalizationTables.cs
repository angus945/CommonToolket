using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace DataDriven.Localization
{
    public class LocalizationTable
    {
        //Header, Key, Value
        Dictionary<string, Dictionary<string, string>> table;

        public LocalizationTable(StreamingFile[] files)
        {
            table = new Dictionary<string, Dictionary<string, string>>();

            for (int i = 0; i < files.Length; i++)
            {
                string header = files[i].name;
                string content = files[i].ReadString();
                LocalizationItem[] items = TextAnalize.FromJsonArray<LocalizationItem>(content);

                if(!table.ContainsKey(header))
                {
                    table.Add(header, new Dictionary<string, string>());
                }

                AddItems(header, items);
            }
        }
        void AddItems(string header, LocalizationItem[] items)
        {
            for (int i = 0; i < items.Length; i++)
            {
                LocalizationItem item = items[i];

                table[header].Add(item.key, item.value);
            }
        }

        public Dictionary<string, string> GetTable(string header)
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

        static Dictionary<string, string> GetTable(string language, string header)
        {
            if (localizationTable == null)
            {
                LoadText();
            }

            return localizationTable[language].GetTable(header);
        }
        public static string[] GetTexts(string language, string header)
        {
            return new List<string>(GetTable(language, header).Values).ToArray();
        }
        public static string GetText(string language, string header, string key)
        {
            if (GetTable(language, header).TryGetValue(key, out string text))
            {
                return text;
            }
            else return $"<misseing content: {key}>";
        }

    }

}