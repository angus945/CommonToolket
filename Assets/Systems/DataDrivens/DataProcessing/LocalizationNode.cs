using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataDriven.Localization;

namespace DataDriven.TextProcess
{

    [CreateAssetMenu(fileName = "New LocalizationNode", menuName = TextProcessNode.MENU_BASE + "Localization")]
    public class LocalizationNode : TextProcessNode
    {
        const string keep_color = "color";
        const string keep_note = "note";

        public override IEnumerator ProcessingRoutine(List<ProcessingData> input, Action<List<ProcessingData>> onFinishedCallback)
        {
            List<ProcessingData> outputDatas = new List<ProcessingData>();

            for (int i = 0; i < input.Count; i++)
            {
                string[] contents = TextAnalize.ParseToArray(input[i].contents);

                Dictionary<string, List<string>> table = GetLanguageTable(contents);
                foreach (KeyValuePair<string, List<string>> item in table)
                {
                    ProcessingData data = new ProcessingData(input[i].dataName, item.Key, item.Value.ToArray(), null);
                    outputDatas.Add(data);
                }
            }

            onFinishedCallback.Invoke(outputDatas);

            yield return null;
        }

        static Dictionary<string, List<string>> GetLanguageTable(string[] contentRows)
        {
            Dictionary<string, List<string>> languageTable = new Dictionary<string, List<string>>();

            for (int i = 0; i < contentRows.Length; i++)
            {
                string[] rowItems = TextAnalize.ParseToItems(contentRows[i]);

                if (rowItems.Length == 0) continue;

                TextAnalize.AnalizeItem(rowItems[0], out _, out string key);
                string color = TextAnalize.TakeItem(keep_color, ref rowItems);
                string note = TextAnalize.TakeItem(keep_note, ref rowItems);

                for (int languageIndex = 1; languageIndex < rowItems.Length; languageIndex++)
                {
                    TextAnalize.AnalizeItem(rowItems[languageIndex], out string language, out string value);

                    if(!languageTable.ContainsKey(language))
                    {
                        languageTable.Add(language, new List<string>());
                    }

                    LocalizationItem localize = new LocalizationItem(key, value, color, note);
                    languageTable[language].Add(JsonUtility.ToJson(localize, true));
                }
            }

            return languageTable;

        }

    }
}
