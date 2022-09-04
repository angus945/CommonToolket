using System.Collections;
using System.Collections.Generic;
using Unity.EditorCoroutines.Editor;
using UnityEditor;
using UnityEngine;

namespace DataDriven.GoogleAppScript
{
    [CustomEditor(typeof(GASAccessTokenCollection))]
    public class GASAccessTokenCollectionEditor : Editor
    {
        GASAccessTokenCollection collection;

        void OnEnable()
        {
            collection = (GASAccessTokenCollection)target;
        }
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            //SerializedProperty bindTokens = serializedObject.FindProperty("bindTokens");
            //for (int i = 0; i < bindTokens.arraySize; i++)
            //{
            //    SerializedObject token = new SerializedObject(bindTokens.GetArrayElementAtIndex(i).objectReferenceValue);
            //    token.Update();
            //    token.FindProperty("bindCollection").objectReferenceValue = collection;
            //    token.ApplyModifiedProperties();
            //}

            if (GUILayout.Button("Download"))
            {
                EditorCoroutineUtility.StartCoroutine(GASAccessHandle.DownLoadDatas(collection.excelID, collection.sheetNames, collection.pathes, null), this);
            }
        }

    }
}
