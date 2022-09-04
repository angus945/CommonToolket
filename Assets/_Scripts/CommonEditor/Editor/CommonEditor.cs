using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

namespace EditorToolket
{
    public static class CommonEditor
    {
        //Extension
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

        //Draw GUI
        public static void DrawLayoutGroup(string header, string layout , Action drawElementHandler)
        {
            //HelpBox, GroupBox, window

            GUILayout.BeginVertical(header, layout);

            GUILayout.Space(20);
            drawElementHandler.Invoke();

            GUILayout.EndVertical();

            GUILayout.Space(15);
        }
        public static void EnumToolbar<T>(ref T type) where T : Enum
        {
            int index = (int)(object)type;
            type = (T)(object)GUILayout.Toolbar(index, System.Enum.GetNames(typeof(T)));
        }
        public static int GUIEnumPopup<T>(Rect rect, int index) where T : Enum
        {
            return (int)(object)EditorGUI.EnumPopup(rect, (T)(object)index);
        }

        //Draw Image
        public static void DrawTextureSprite(Rect position, Sprite sprite)
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
            ratio.x = position.width / size.x;
            ratio.y = position.height / size.y;
            float minRatio = Mathf.Min(ratio.x, ratio.y);

            Vector2 center = position.center;
            position.width = size.x * minRatio;
            position.height = size.y * minRatio;
            position.center = center;

            GUI.DrawTextureWithTexCoords(position, sprite.texture, coords);
        }

        //Time
        public static void EditorTime(ref float lastTime, out float delta)
        {
            delta = Time.realtimeSinceStartup - lastTime;
            lastTime = Time.realtimeSinceStartup;
        }
    }

}
