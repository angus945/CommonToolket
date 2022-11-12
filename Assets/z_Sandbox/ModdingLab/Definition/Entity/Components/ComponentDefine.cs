using System;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;
using ModdingLab.Instance;

namespace ModdingLab.Definition.Componentized
{
    [XmlType]
    [System.Serializable]
    public abstract class ComponentDefine
    {
        [XmlAttribute("id")] [SerializeField] string _id;

        public string id { get => string.IsNullOrEmpty(_id) ? defaultID : _id; }
        protected abstract string defaultID { get; }

        public abstract Type RequireComponentType { get; }
        public abstract void InitialComponent(Component component);
    }

}
