using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace TokenizableAnimation.Graph
{
    public class BaseNode : Node
    {
        string nodeGuid;

        public List<Port> inPorts = new List<Port>();
        public List<Port> outPorts = new List<Port>();

        protected AnimationGraphView graphView;
        protected AnimationDriveEditorWindow window;
        protected Vector2 nodeSize = new Vector2(200, 250);

        protected string NodeGuid { get => nodeGuid; set => nodeGuid = value; }

        public BaseNode()
        {

        }
        public Port AddOutputPort(string name, Port.Capacity capacity = Port.Capacity.Single)
        {
            Port outputPort = GetPortInstance(Direction.Output, capacity);
            outputPort.portName = name;
            outputPort.portColor = Color.green;
            outputContainer.Add(outputPort);

            outPorts.Add(outputPort);

            return outputPort;
        }
        public Port AddInputPort(string name, Port.Capacity capacity = Port.Capacity.Multi)
        {
            Port inputPort = GetPortInstance(Direction.Input, capacity);
            inputPort.portName = name;
            inputContainer.Add(inputPort);

            inPorts.Add(inputPort);

            return inputPort;
        }

        public Port GetPortInstance(Direction nodeDirection, Port.Capacity capacity = Port.Capacity.Single)
        {
            return InstantiatePort(Orientation.Horizontal, nodeDirection, capacity, typeof(float));
        }

    }

}
