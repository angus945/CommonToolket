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

        [XmlElement("Tags", IsNullable = false)]
        public EntityTags tags;

        [XmlElement("Visual", IsNullable = false)]
        public EntityVisuals spriteSheets;

        [XmlElement("Properties", IsNullable = false)]
        public EntityProperties properties;

        [XmlElement("Components", IsNullable = false)]
        public EntityComponents components;

        [XmlElement("Behavior", IsNullable = false)]
        public EntityBehaviors behaviors;

    }

}
