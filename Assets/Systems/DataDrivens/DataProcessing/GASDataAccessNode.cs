using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace DataDriven.TextProcess
{
    [CreateAssetMenu(fileName = "New GASAccessNode", menuName = TextProcessNode.MENU_BASE + "GASAccess")]
    public class GASDataAccessNode : TextProcessNode
    {
        public const string postURL = "https://script.google.com/macros/s/AKfycbygvwXyQTff_GmkDAURYHHwfJnks7Nlt717-9GPBIIEq8AxWywHbVYv0ZhaaE9Yt0ZG/exec";

        [SerializeField] string excelID = "";
        [SerializeField] string[] sheetNames = null;

        public override IEnumerator ProcessingRoutine(List<ProcessingData> input, Action<List<ProcessingData>> onFinishedCallback)
        {
            List<ProcessingData> datas = new List<ProcessingData>(input);

            for (int i = 0; i < sheetNames.Length; i++)
            {
                WWWForm form = new WWWForm();
                form.AddField("id", excelID);
                form.AddField("name", sheetNames[i]);

                using (UnityWebRequest www = UnityWebRequest.Post(postURL, form))
                {
                    yield return www.SendWebRequest();

                    if (www.result != UnityWebRequest.Result.Success)
                    {
                        Debug.Log(www.error);
                        //onFinishedCallback?.Invoke(false, "");
                    }
                    else
                    {
                        datas.Add(new ProcessingData(sheetNames[i], "", www.downloadHandler.text, ""));

                        Debug.Log("download complete!");
                    }
                }
            }

            onFinishedCallback?.Invoke(datas);
        }
    }
}
