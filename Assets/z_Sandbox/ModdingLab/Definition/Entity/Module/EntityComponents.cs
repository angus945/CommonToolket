using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using ModdingLab.Definition.Componentized;

namespace ModdingLab.Definition
{
    [XmlType("Componens")]
    [System.Serializable]
    public class EntityComponents : EntityModule<ComponentDefine>
    {
        protected override List<ComponentDefine> moduleContents { get => components; }

        [UnityEngine.SerializeReference]
        [XmlElement("Collision", typeof(Collision))]
        [XmlElement("Rigidbody", typeof(Rigidbody))]
        [XmlElement("Rendering", typeof(SpriteSheetRendering))]
        [XmlElement("Animation", typeof(SpriteSheetAnimaton))]
        public List<ComponentDefine> components;
    }

}
