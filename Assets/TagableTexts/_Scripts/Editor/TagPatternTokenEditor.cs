using EditorToolket;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace TaggableTexts
{
    [CustomEditor(typeof(TagPatternToken))]
    public class TagPatternTokenEditor : Editor
    {

        TagPatternToken patternToken;

        void OnEnable()
        {
            patternToken = (TagPatternToken)target;
        }
        public override void OnInspectorGUI()
        {
            this.DrawScriptLine<TagPatternToken>();

            serializedObject.Update();

            DrawInputSetting();
            DrawOutputSetting();

            PatternPreviewEditor.DrawPatternPreivew(patternToken);

            serializedObject.ApplyModifiedProperties();
        }

        void DrawInputSetting()
        {
            CommonEditor.DrawLayoutGroup("Pattern Input", "Helpbox", () =>
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("tagKeyword"));

                SerializedProperty inputRule = serializedObject.FindProperty("inputRule");
                SerializedProperty ruleType = inputRule.FindPropertyRelative("ruleType");
                SerializedProperty customPattern = inputRule.FindPropertyRelative("customPattern");

                EditorGUILayout.PropertyField(ruleType);
                if(ruleType.intValue == 0)
                {
                    GUI.color = patternToken.isValidRule ? Color.white : Color.red;
                    EditorGUILayout.PropertyField(customPattern);
                    GUI.color = Color.white;
                }

                EditorGUILayout.LabelField("Regex Pattern", patternToken.patternRegex);
            });
        }
        void DrawOutputSetting()
        {
            CommonEditor.DrawLayoutGroup("Pattern Output", "Helpbox", () =>
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("replaceWord"));
            });
        }



    }
}
