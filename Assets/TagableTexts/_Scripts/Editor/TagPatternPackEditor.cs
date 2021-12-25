using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

namespace TaggableTexts
{
    [CustomEditor(typeof(TagPatternPack))]
    public class TagPatternPackEditor : Editor
    {

        ReorderableList list;
        TagPatternPack patternToken;

        void OnEnable()
        {
            patternToken = (TagPatternPack)target;

            list = new ReorderableList(serializedObject, serializedObject.FindProperty("patternTokens"), true, true, true, true);
        }
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            list.DoLayoutList();
        }

    }
}
