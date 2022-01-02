using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Experimental.GraphView;

namespace TokenizableAnimation.Graph
{
    public class RootNode : BaseNode
    {
        public RootNode(Vector2 pos, EditorWindow window, AnimationGraphView graphView)
        {
            title = "Root";
            SetPosition(new Rect(pos, nodeSize));
            NodeGuid = Guid.NewGuid().ToString();

            AddOutputPort("Output", Port.Capacity.Single);

            RefreshExpandedState();
            RefreshPorts();
        }

    }

}
