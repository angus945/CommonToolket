using System;
using System.Collections;
using System.IO;
using Unity.EditorCoroutines.Editor;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

namespace DataDriven.GoogleAppScript
{
    public class GASAccessHandle
    {
        // https://script.google.com/home/projects/1Ut46bal5TXN1WpkOTJHSVdv1beqQgrKPoNUB_6wB5YaHvAsNfHsMx40q/edit
        // https://docs.google.com/spreadsheets/d/1JYSKXemhUYgxK7O_SeJeppz25g4bFpqpP_Ld12xpLFQ/edit?usp=sharing

        public const string postURL = "https://script.google.com/macros/s/AKfycbygvwXyQTff_GmkDAURYHHwfJnks7Nlt717-9GPBIIEq8AxWywHbVYv0ZhaaE9Yt0ZG/exec";

        //public static void DownloadDatas(string excelID, string[] sheetNames, string[] pathes, Action finishedCallback)
        //{
        //    EditorCoroutineUtility.StartCoroutine(DownLoadDatas());
        //}
        public static IEnumerator DownLoadDatas(string excelID, string[] sheetNames, string[] pathes, Action finishedCallback)
        {
            for (int i = 0; i < sheetNames.Length; i++)
            {
                yield return DownLoadDatas(excelID, sheetNames[i], pathes[i], null);
            }

            finishedCallback?.Invoke();
        }
        public static IEnumerator DownLoadDatas(string excelID, string sheetName, string path, Action<bool, string> finishedCallback)
        {
            WWWForm form = new WWWForm();
            form.AddField("id", excelID);
            form.AddField("name", sheetName);

            using (UnityWebRequest www = UnityWebRequest.Post(postURL, form))
            {
                yield return www.SendWebRequest();

                if (www.result != UnityWebRequest.Result.Success)
                {
                    Debug.Log(www.error);
                    finishedCallback?.Invoke(false, "");
                }
                else
                {
                    finishedCallback?.Invoke(true, www.downloadHandler.text);

                    File.WriteAllText(path, www.downloadHandler.text);
                    AssetDatabase.Refresh();
                    
                    Debug.Log("download complete!");
                }
            }
        }

        //public static void WriteFile(string location, string name, string content)
        //{
        //    string path = $"{Application.dataPath}/{location}/{name}";

        //    File.WriteAllText(path, content);            
        //}
    }
}
