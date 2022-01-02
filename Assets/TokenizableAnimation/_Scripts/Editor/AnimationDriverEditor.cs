using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace TokenizableAnimation
{
    //https://youtu.be/CZ39btQ0XlE
    //https://github.com/Lontoone/MyUnityToolLab/tree/master/Level%20Flow%20Manager/Editor/GraphView

    [CustomEditor(typeof(AnimationDriver))]
    public class AnimationDriverEditor : Editor
    {

        public void OnEnable()
        {

        }
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            //graphView.strethtoparentsize
        }
    }
}
