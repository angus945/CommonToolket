using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;

namespace Toolket.Interface
{
    [CustomPropertyDrawer(typeof(InterfaceField<>))]
    public class InterfaceFieldEditor : PropertyDrawer
    {
        
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var valueProperty = property.FindPropertyRelative("reference");
            return EditorGUI.GetPropertyHeight(valueProperty);
        }
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {

            SerializedProperty referenceProperty = property.FindPropertyRelative("reference");

            object target = fieldInfo.GetValue(property.serializedObject.targetObject);
            EditorGUI.ObjectField(position, referenceProperty);

            GameObject obj = referenceProperty.objectReferenceValue as GameObject;

            if(obj != null)
            {
                Type closedType = target.GetType();
                for (int i = 0; i < 10; i++)
                {
                    //泛型陣列無法直接 GetGenericArguments，要轉換成 Element
                    if (closedType.IsArray) closedType = closedType.GetElementType();
                    else break;
                }

                Type genericType = closedType.GetGenericArguments()[0];

                if (obj.TryGetComponent(genericType, out _))
                {
                    //Debug.Log("Have Interface");
                }
                else referenceProperty.objectReferenceValue = null;
            }

            //if(edgui)

        }
        

    }

}
