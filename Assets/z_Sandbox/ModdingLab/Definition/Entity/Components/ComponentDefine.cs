using System;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;
using ModdingLaboratory.Instance;

namespace ModdingLaboratory.Definition.Componentized
{
    [XmlType]
    [System.Serializable]
    public abstract class ComponentDefine
    {
        [XmlAttribute("id")] public string id;

        public string localID { get => string.IsNullOrEmpty(id) ? defaultID : id; }
        protected abstract string defaultID { get; }

        public abstract Type RequireComponentType { get; }
        public abstract void InitialComponent(Component component);
    }

}
