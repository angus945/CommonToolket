using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using DataDriven;

namespace ModdingLab.Definition.TypeScript
{
    public enum UpdateType
    {
        Auto,
        Frame,
        Time,
    }

    [XmlType]
    [System.Serializable]
    public class Function
    {
        [XmlAttribute("function")] public string functionName;

        [XmlAttribute("type")] public UpdateType type;

        [XmlAttribute("call")] public float call;
    }

    [XmlType]
    [System.Serializable]
    public class BehaviorDefine
    {
        [XmlAttribute] public string id;
        [XmlAttribute("script")] public string scriptName;
        [XmlAttribute] public bool active;

        [XmlElement("Function")] public Function[] functions;

    }
}
