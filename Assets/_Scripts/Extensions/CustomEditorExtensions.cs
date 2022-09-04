#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

namespace EditorExtensions
{
    public static class GUIFieldExtensions
    {
        public static bool CheckingObjectField(Rect rect, SerializedProperty property)
        {
            UnityEngine.Object recordObj = property.objectReferenceValue;
            EditorGUI.PropertyField(rect, property);
            return recordObj != property.objectReferenceValue;
        }
        public static bool CheckingIntField(Rect rect, SerializedProperty property)
        {
            int recordInt = property.intValue;
            EditorGUI.PropertyField(rect, property);
            return recordInt != property.intValue;
        }
        public static bool CheckingClampIntField(Rect rect, SerializedProperty property, int min, int max)
        {
            int recordInt = property.intValue;
            EditorGUI.PropertyField(rect, property);
            property.intValue = Mathf.Clamp(property.intValue, min, max);
            return recordInt != property.intValue;
        }
    }
    public static class GUITextureExtensions
    {
        //Draw Image
        public static void DrawTexture(Rect rect, SerializedProperty property, bool alphaBlend = true)
        {
            if (property == null) return;
            if (property.objectReferenceValue == null) return;

            Texture2D texture = (Texture2D)property.objectReferenceValue;
            DrawTexture(rect, texture, alphaBlend);
        }
        public static void DrawTexture(Rect rect, Texture texture, bool alphaBlend = true)
        {
            if (texture == null) return;

            GUI.DrawTextureWithTexCoords(rect, texture, new Rect(0, 0, 1, 1), alphaBlend);
        }
        public static void DrawSprite(Rect rect, Sprite sprite)
        {
            if (sprite == null) return;

            Vector2 fullSize = new Vector2(sprite.texture.width, sprite.texture.height);
            Vector2 size = new Vector2(sprite.textureRect.width, sprite.textureRect.height);

            Rect coords = sprite.textureRect;
            coords.x /= fullSize.x;
            coords.width /= fullSize.x;
            coords.y /= fullSize.y;
            coords.height /= fullSize.y;

            Vector2 ratio;
            ratio.x = rect.width / size.x;
            ratio.y = rect.height / size.y;
            float minRatio = Mathf.Min(ratio.x, ratio.y);

            Vector2 center = rect.center;
            rect.width = size.x * minRatio;
            rect.height = size.y * minRatio;
            rect.center = center;

            GUI.DrawTextureWithTexCoords(rect, sprite.texture, coords);
        }
    }
    public static class CustomEditorExtensions
    {
        //Basis
        public static void DrawScriptLine<T>(this Editor editor) where T : UnityEngine.Object
        {
            EditorGUI.BeginDisabledGroup(true);
            switch (editor.target)
            {
                case MonoBehaviour monoBehaviour:
                    EditorGUILayout.ObjectField("Script", MonoScript.FromMonoBehaviour(monoBehaviour), typeof(T), true);
                    break;

                case ScriptableObject scriptableObject:
                    EditorGUILayout.ObjectField("Script", MonoScript.FromScriptableObject(scriptableObject), typeof(T), true);
                    break;
            }
            EditorGUI.EndDisabledGroup();
        }
        public static void PropertyField(this Editor editor, string name)
        {
            SerializedProperty property = editor.serializedObject.FindProperty(name);
            EditorGUILayout.PropertyField(property);
        }

     

        public static bool ChangeEnumToolbar<T>(this Editor _, ref T type) where T : Enum
        {
            int oritinIndex = (int)(object)type;
            int toolbarIndex = GUILayout.Toolbar(oritinIndex, System.Enum.GetNames(typeof(T)));
            type = (T)(object)toolbarIndex;

            return oritinIndex != toolbarIndex;
        }
        public static bool DrawLayoutGroup(this Editor _, string header, string layout , Action drawElementHandler)
        {
            //HelpBox, GroupBox, window
            EditorGUI.BeginChangeCheck();

            GUILayout.BeginVertical(header, layout);

            GUILayout.Space(20);
            drawElementHandler.Invoke();

            GUILayout.EndVertical();

            GUILayout.Space(15);

            return EditorGUI.EndChangeCheck();
        }
        public static int GUIEnumPopup<T>(Rect rect, int index) where T : Enum
        {
            return (int)(object)EditorGUI.EnumPopup(rect, (T)(object)index);
        }

        public static void FlexButton(this Editor _, string header, Action onClick)
        {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button(header))
            {
                onClick?.Invoke();
            }
            GUILayout.EndHorizontal();
        }

        //Time
        public static void EditorTime(ref float lastTime, out float delta)
        {
            delta = Time.realtimeSinceStartup - lastTime;
            lastTime = Time.realtimeSinceStartup;
        }
    }



}

#endif