using System;
using System.Collections;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

namespace DataDriven
{
    [CreateAssetMenu]
    public class FileWrideNode : DataParsingNode
    {
        [SerializeField] string writeLocation = "";
        [SerializeField] string fileFormat = "json";

        public override IEnumerator ParsingRoutine(ParsingDatas[] input, Action<ParsingDatas[]> onFinishedCallback)
        {
            yield return null;

            for (int i = 0; i < input.Length; i++)
            {
                ParsingDatas datas = input[i];

                //Debug.Log(datas.contentFlags.Length);

                string path = $"{Application.dataPath}/{writeLocation}/{datas.dataName}.{fileFormat}";
                File.WriteAllText(path, datas.contents.PrintOut());

                Debug.Log(path);
            }

            AssetDatabase.Refresh();
            onFinishedCallback?.Invoke(input);
        }
    }
}
