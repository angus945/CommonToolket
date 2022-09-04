using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Unity.EditorCoroutines.Editor;

//Packge Manager > Editor Coroutines
namespace DataDriven.GoogleAppScript
{
    [CustomEditor(typeof(GASDataAccessToken))]
    public class GASDataAccessTokenEditor : Editor
    {
        GASDataAccessToken token;

        bool downloading;

        void OnEnable()
        {
            token = (GASDataAccessToken)target;
        }
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();



            DownloadButton();

            DisplayData();

            //if(GUILayout.Button("Test"))
            //{
            //    Debug.Log(Application.streamingAssetsPath);
            //}
        }

        void DownloadButton()
        {
            EditorGUI.BeginDisabledGroup(downloading);
            if (GUILayout.Button("Download"))
            {
                downloading = true;
                EditorCoroutineUtility.StartCoroutine(GASAccessHandle.DownLoadDatas(token.excelID, token.sheetName, token.path, DownloadComplete), this);
            }
            EditorGUI.EndDisabledGroup();
        }
        void DownloadComplete(bool success, string data)
        {
            downloading = false;

            if (success)
            {
                SerializedProperty datas = serializedObject.FindProperty("_sourceData");
                datas.stringValue = data;        
            }

            serializedObject.ApplyModifiedProperties();
        }

        void DisplayData()
        {
            string displayData = token.sourceData;
            displayData = displayData.Replace("},", "},\n");
            displayData = "SourceData: \n" + displayData;
            GUILayout.Label(displayData);
        }

    }
}
