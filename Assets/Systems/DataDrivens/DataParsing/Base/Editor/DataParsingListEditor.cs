using System.Collections;
using System.Collections.Generic;
using Unity.EditorCoroutines.Editor;
using UnityEditor;
using UnityEngine;

namespace DataDriven
{
    [CustomEditor(typeof(DataParsingList))]
    public class DataParsingListEditor : Editor
    {
        DataParsingList parsingList;

        private void OnEnable()
        {
            parsingList = (DataParsingList)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            DownloadButton();

            //if(GUILayout.Button("Test"))
            //{
            //    Debug.Log(Application.streamingAssetsPath);
            //}
        }

        void DownloadButton()
        {
            //EditorGUI.BeginDisabledGroup(downloading);
            if (GUILayout.Button("ParstDatas"))
            {
                //downloading = true;
                //EditorCoroutineUtility.StopCoroutine
                EditorCoroutineUtility.StartCoroutine(parsingList.ParsingRoutine(PrintOutDatas), this);
            }
            //EditorGUI.EndDisabledGroup();
        }
        void PrintOutDatas(ParsingDatas[] datas)
        {
            foreach (ParsingDatas item in datas)
            {
                Debug.Log(item);
            }
        }
        //void DownloadComplete(bool success, string data)
        //{
        //    downloading = false;

        //    if (success)
        //    {
        //        //SerializedProperty datas = serializedObject.FindProperty("_sourceData");
        //        //datas.stringValue = data;        
        //    }

        //    Repaint();
        //}
    }
}
