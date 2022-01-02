using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;

namespace TokenizableAnimation.Graph
{
    
    public class LeafNode : BaseNode
    {
        public LeafNode(Vector2 pos, EditorWindow window, AnimationGraphView graphView)
        {
            title = "Leaf";
            SetPosition(new Rect(pos, nodeSize));
            NodeGuid = Guid.NewGuid().ToString();

            //AddOutputPort("Output", Port.Capacity.Single);
            AddInputPort("Input", Port.Capacity.Single);

            RefreshExpandedState();
            RefreshPorts();

            //animation token field
            ObjectField objectField = new ObjectField("AnimationToken");
            objectField.objectType = typeof(AnimationToken);
            mainContainer.Add(objectField);
        }
    }
}