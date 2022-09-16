using DataDriven.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DataDriven.TextProcess
{
    [CreateAssetMenu(fileName = "New LocalizationNode", menuName = TextProcessNode.MENU_BASE + "Localization")]
    public class LocalizationNode : TextProcessNode
    {

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

        static Dictionary<string, List<string>> GetLanguageTable(string[] contents)
        {
            Dictionary<string, List<string>> languageTable = new Dictionary<string, List<string>>();

            for (int i = 0; i < contents.Length; i++)
            {
                string[] item = TextAnalize.ParseToItems(contents[i]);

                if (item.Length == 0) continue;

                TextAnalize.AnalizeItem(item[0], out _, out string key);

                for (int languageIndex = 1; languageIndex < item.Length; languageIndex++)
                {
                    TextAnalize.AnalizeItem(item[languageIndex], out string language, out string value);

                    if(!languageTable.ContainsKey(language))
                    {
                        languageTable.Add(language, new List<string>());
                    }

                    LocalizationItem localize = new LocalizationItem(key, value);
                    languageTable[language].Add(JsonUtility.ToJson(localize, true));
                }
            }

            return languageTable;

        }

    }
}
