using System.Xml;
using System.Xml.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModdingLab
{
    [XmlType("Animation")]
    [System.Serializable]
    public class AnimationData
    {
        [XmlAttribute] public string name;
        [XmlAttribute] public int row;
        [XmlAttribute] public int length;
        [XmlAttribute] public float duration;
    }

    [XmlType("SpriteSheet")]
    [System.Serializable]
    public class SpriteSheet
    {
        [XmlAttribute] public string id;
        [XmlAttribute] public string source;

        [XmlAttribute] public int width;
        [XmlAttribute] public int height;

        [XmlElement("DefaultAnim")] public string defaultAnimation;

        [XmlArray("Animations")]
        [XmlArrayItem("Animation", typeof(AnimationData))]
        public AnimationData[] animations;

        [XmlIgnore] public Texture2D texture;
        public Vector2Int textureSize { get => new Vector2Int(texture.width, texture.height); }


    }
}
