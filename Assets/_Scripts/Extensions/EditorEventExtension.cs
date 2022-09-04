#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ScriptExtensions
{
    public static class EditorEventExtension
    {
        public static Vector3 MouseInWorld(this Event editorEvent)
        {
            Vector3 mousePosition = editorEvent.mousePosition;
            mousePosition.y = SceneView.currentDrawingSceneView.camera.pixelHeight - mousePosition.y;
            mousePosition = SceneView.currentDrawingSceneView.camera.ScreenToWorldPoint(mousePosition);

            return mousePosition;
        }

    }

}

#endif