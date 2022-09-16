using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

#if UNITY_EDITOR
namespace DataDriven.TextProcess
{

    [CreateAssetMenu(fileName = "New FileWriteNode", menuName = TextProcessNode.MENU_BASE + "IO")]
    public class FileWriteNode : TextProcessNode
    {
        [SerializeField] string writeLocation = "";
        [SerializeField] string fileFormat = "json";

        [SerializeField] bool prittyPrint = false;
        [SerializeField] bool relativelyFlag = false;

        public override IEnumerator ProcessingRoutine(List<ProcessingData> input, Action<List<ProcessingData>> onFinishedCallback)
        {
            for (int i = 0; i < input.Count; i++)
            {
                ProcessingData datas = input[i];

                string folderPath = relativelyFlag ? $"{Application.dataPath}/{writeLocation}/{datas.dataFlag}" : $"{Application.dataPath}/{writeLocation}";
                string path = $"{folderPath}/{datas.dataName}.{fileFormat}";

                Directory.CreateDirectory(folderPath);

                yield return null;

                switch (datas.type)
                {
                    case ProcessingType.Single:
                        File.WriteAllText(path, datas.contents[0]);
                        break;

                    case ProcessingType.Multiple:
                        File.WriteAllText(path, TextAnalize.ToJsonArray(datas.contents, prittyPrint));
                        break;
                }

                yield return null;
            }

            AssetDatabase.Refresh();
            onFinishedCallback?.Invoke(input);

            yield return null;
        }
    }
}
#endif