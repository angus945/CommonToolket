using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace TokenizableAnimation
{
    [CustomEditor(typeof(AnimationToken))]
    public class AnimationTokenEditor : Editor
    {

        AnimationToken token;
        ReorderableList frameList;

        public void OnEnable()
        {
            token = target as AnimationToken;

            SerializedProperty array = serializedObject.FindProperty("animationFrames");
            frameList = new ReorderableList(serializedObject, array, true, true, true, true);
            frameList.drawHeaderCallback += (Rect rect) =>
            {
                EditorGUI.LabelField(rect, "Frames");
            };
            frameList.drawElementCallback += (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                rect.y += 1;

                SerializedProperty element = array.GetArrayElementAtIndex(index);
                SerializedProperty sprite = element.FindPropertyRelative("sprite");

                int previewSize = 60;
                Rect fieldRect = new Rect(rect.x, rect.y, rect.width - previewSize - 3, 20);
                EditorGUI.PropertyField(fieldRect, sprite);

                Rect previewRect = new Rect(rect.width - previewSize * 0.3f, rect.y , previewSize, previewSize);
                //EditorGUI.DrawRect(previewRect, Color.blue);
                DrawTexturePreview(previewRect, (Sprite)sprite.objectReferenceValue);
            };
            frameList.elementHeight = 80;
        }
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            frameList.DoLayoutList();
        }

        //animation Preview
        //https://forum.unity.com/threads/drawing-a-sprite-in-editor-window.419199/
        static float previewFPS = 20;
        public override GUIContent GetPreviewTitle()
        {
            return new GUIContent("Animation Preview");
        }
        public override bool RequiresConstantRepaint()
        {
            return true;
        }
        public override bool HasPreviewGUI()
        {
            return true;
        }
        public override void OnPreviewSettings()
        {
            base.OnPreviewSettings();

            previewFPS = EditorGUILayout.FloatField("previewFPS", previewFPS);
        }
        public override void OnInteractivePreviewGUI(Rect rect, GUIStyle background)
        {
            AnimationToken.AnimationFrame[] frames = token.frames;
            if (frames == null || frames.Length == 0) return;

            //TODO serialize proterty
            int spriteIndex = Mathf.FloorToInt(Time.realtimeSinceStartup * previewFPS) % frames.Length;
            Sprite sprite = frames[spriteIndex].sprite;
            DrawTexturePreview(rect, sprite);
        }

        //Draw Preview
        private void DrawTexturePreview(Rect position, Sprite sprite)
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
    }

}
