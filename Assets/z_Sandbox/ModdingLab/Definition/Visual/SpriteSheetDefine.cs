using System.Xml;
using System.Xml.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataDriven;

namespace ModdingLab.Definition
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

    [XmlType("SpriteSheet")]
    [System.Serializable]
    public class SpriteSheetDefine : IDefinition
    {
        string IDefinition.id { get => id; }

        [XmlAttribute] public string id;
        [XmlAttribute] public string source;

        [XmlAttribute] public FilterMode filter;

        [XmlAttribute] public int width;
        [XmlAttribute] public int height;

        [XmlElement("Animations")] public AnimationDatas animationDatas;

        //[XmlIgnore] public Texture2D texture { get; private set; }
        //public Vector2Int textureSize { get => new Vector2Int(texture.width, texture.height); }
    }
}
