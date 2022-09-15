using System;
using System.Collections;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

namespace DataDriven.TextProcess
{
    [CreateAssetMenu(fileName = "New FileWriteNode", menuName = "DataDriven/DataProcess/IO")]
    public class FileWriteNode : TextProcessNode
    {
        [SerializeField] string writeLocation = "";
        [SerializeField] string fileFormat = "json";

        [SerializeField] bool prittyPrint = false;
        [SerializeField] bool relativelyFlag = false;

        public override IEnumerator ProcessingRoutine(ProcessingData[] input, Action<ProcessingData[]> onFinishedCallback)
        {
            for (int i = 0; i < input.Length; i++)
            {
                ProcessingData datas = input[i];

                string folderPath = relativelyFlag ? $"{Application.dataPath}/{writeLocation}/{datas.dataFlag}" : $"{Application.dataPath}/{writeLocation}";
                string path = $"{folderPath}/{datas.dataName}.{fileFormat}";

                Directory.CreateDirectory(folderPath);

                yield return null;

                File.WriteAllText(path, TextAnalize.ToJson(datas.contents, prittyPrint));

                yield return null;
            }

            AssetDatabase.Refresh();
            onFinishedCallback?.Invoke(input);

            yield return null;
        }
    }
}
