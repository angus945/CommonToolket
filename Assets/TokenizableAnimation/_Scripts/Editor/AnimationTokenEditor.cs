using EditorToolket;
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

            InitialList();
        }
        public override void OnInspectorGUI()
        {
            this.DrawScriptLine<AnimationToken>();

            serializedObject.Update();

            SerializedProperty frameRate = serializedObject.FindProperty("frameRate");
            EditorGUILayout.PropertyField(frameRate);

            frameList.DoLayoutList();

            serializedObject.ApplyModifiedProperties();
        }

        void InitialList()
        {
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
                SerializedProperty sprite = element.FindPropertyRelative("frameSprite");
                SerializedProperty listeners = element.FindPropertyRelative("listenerToken");

                int previewSize = 60;
                Rect fieldRect = new Rect(rect.x, rect.y, rect.width - previewSize - 5, 20);
                EditorGUI.PropertyField(fieldRect, sprite);

                Rect previewRect = new Rect(rect.width - previewSize * 0.3f, rect.y, previewSize, previewSize);
                CommonEditor.DrawTextureSprite(previewRect, (Sprite)sprite.objectReferenceValue);

                Rect listenerRect = new Rect(fieldRect.x, fieldRect.y + fieldRect.height, fieldRect.width, 20);
                EditorGUI.PropertyField(listenerRect, listeners);
            };
            frameList.elementHeight = 80;
        }

        float lastTime;

        //animation Preview
        //https://forum.unity.com/threads/drawing-a-sprite-in-editor-window.419199/
        float previewTime = 0;
        bool isPause;
        int frameIndex;
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

            if(GUILayout.Button(isPause ? "Play" : "Pause"))
            {
                isPause = !isPause;
            }
            if(GUILayout.Button("Next Frame"))
            {
                frameIndex++;
            }
        }
        public override void OnInteractivePreviewGUI(Rect rect, GUIStyle background)
        {
            AnimationFrame[] frames = token.frames;
            if (frames == null || frames.Length == 0) return;

            CommonEditor.EditorTime(ref lastTime, out float delta);

            if(!isPause)
            {
                previewTime += delta * token.fps;
                frameIndex = Mathf.FloorToInt(previewTime);

                if (previewTime >= int.MaxValue) previewTime = 0;
            }

            frameIndex = frameIndex % frames.Length;
            Sprite sprite = frames[frameIndex].frameSprite;
            //TODO listener preview

            CommonEditor.DrawTextureSprite(rect, sprite);
        }

        //Draw Preview
    }

}
