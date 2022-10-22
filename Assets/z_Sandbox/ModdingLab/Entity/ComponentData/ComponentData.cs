using System;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;

namespace ModdingLab
{
    [XmlType]
    [System.Serializable]
    public abstract class ComponentData
    {
        public abstract Type RequireComponentType { get; }
        public abstract void InitialComponent(GameEntity entity, Component component);

        [XmlAttribute] public string id;
    }

}
