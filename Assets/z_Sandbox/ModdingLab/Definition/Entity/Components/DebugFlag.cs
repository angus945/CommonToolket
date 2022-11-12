using ModdingLab.Instance;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

namespace ModdingLab.Definition.Componentized
{
    [XmlType]
    [System.Serializable]
    public class DebugFlag : ComponentDefine
    {
        public override Type RequireComponentType { get => typeof(Instance.Componentized.DebugFlag); }

        protected override string defaultID => "DebugFlag";

        [XmlAttribute]
        public string flag = "";

        public override void InitialComponent(Component component)
        {
            Instance.Componentized.DebugFlag debugFlag = component as Instance.Componentized.DebugFlag;

            debugFlag.Initial(flag);
        }
    }
}
