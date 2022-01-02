//using EditorToolket;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEditor;
//using UnityEditorInternal;
//using UnityEngine;

//namespace TokenizableAnimation
//{
//    [CustomPropertyDrawer(typeof(AnimationFrame))]
//    public class AnimationFramePropertyDrawer : PropertyDrawer
//    {

//        //ReorderableList listenerList;

//        public override void OnGUI(Rect rect, SerializedProperty element, GUIContent label)
//        {

//            SerializedProperty sprite = element.FindPropertyRelative("sprite");
//            SerializedProperty listeners = element.FindPropertyRelative("frameListeners");

//            int previewSize = 60;
//            Rect fieldRect = new Rect(rect.x, rect.y, rect.width - previewSize - 3, 20);
//            EditorGUI.PropertyField(fieldRect, sprite);

//            Rect previewRect = new Rect(rect.width - previewSize * 0.3f, rect.y, previewSize, previewSize);
//            CommonEditor.DrawTextureSprite(previewRect, (Sprite)sprite.objectReferenceValue);

//            Rect listenerRect = new Rect(fieldRect.x, fieldRect.y + fieldRect.height, fieldRect.width, rect.height);
//            EditorGUI.PropertyField(listenerRect, listeners);

//        }
//        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
//        {
//            return 100;
//        }


//    }
//}
