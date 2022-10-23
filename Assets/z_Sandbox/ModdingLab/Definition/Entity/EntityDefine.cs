using System.Xml;
using System.Xml.Serialization;
using DataDriven;
using ModdingLab.Definition.Componentized;

namespace ModdingLab.Definition
{
    [XmlType("Entity")]
    [System.Serializable]
    public class EntityDefine : IDefinition
    {
        string IDefinition.id { get => id; }

        [XmlAttribute] public string id;
        [XmlElement("Name")] public string name;
        [XmlElement("Describe")] public string describe;

        [XmlArray("Tags")]
        [XmlArrayItem("Tag")]
        public string[] tags;

        [XmlArray("Visual")]
        [XmlArrayItem("SpriteSheet", IsNullable = false)]
        public string[] spriteSheets;

        [XmlArray("Components", IsNullable = false)]
        [XmlArrayItem("Collision", typeof(Collision))]
        [XmlArrayItem("Rigidbody", typeof(Rigidbody))]
        [XmlArrayItem("Rendering", typeof(SpriteSheetRendering))]
        [XmlArrayItem("Animation", typeof(SpriteSheetAnimaton))]
        public ComponentDefine[] components;

        void IDefinition.Initial(StreamingDirectory directory)
        {

        }
    }

}
