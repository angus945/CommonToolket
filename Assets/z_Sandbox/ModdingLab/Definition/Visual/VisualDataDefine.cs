using System.Xml;
using System.Xml.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataDriven;

namespace ModdingLaboratory.Definition
{
    [XmlType("Animation")]
    [System.Serializable]
    public class AnimationData
    {
        [XmlAttribute] public string name;
        [XmlAttribute] public int index;
        [XmlAttribute] public int length;
        [XmlAttribute] public bool loop;

        [XmlAttribute] public float duration;
    }

    [XmlType("Animatios")]
    [System.Serializable]
    public class AnimationDatas
    {
        [XmlAttribute("default")]
        public string defaultAnimation;

        [XmlElement("Animation")]
        public AnimationData[] animations;
    }

    [XmlType("VisualData")]
    [System.Serializable]
    public class VisualDataDefine : DefinitionBase
    {
        protected override string localID { get => id; }
        public override void SetGroup(string group)
        {
            base.SetGroup(group);

            source = base.ApplyGroupID(source);
        }

        [XmlAttribute("id")] public string id;
        [XmlAttribute] public string source;

        [XmlAttribute] public FilterMode filter;

        [XmlAttribute] public int width;
        [XmlAttribute] public int height;

        [XmlElement("Animations")] public AnimationDatas animationDatas;
    }
}
