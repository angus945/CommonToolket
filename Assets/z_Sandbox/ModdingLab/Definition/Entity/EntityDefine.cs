using System.Xml;
using System.Xml.Serialization;
using DataDriven;
using ModdingLaboratory.Definition.Componentized;

namespace ModdingLaboratory.Definition
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
        public VisualModule spriteSheets;

        [XmlElement("Properties", IsNullable = false)]
        public ProperityModule properties;

        [XmlElement("Components", IsNullable = false)]
        public ComponentModule components;

        [XmlElement("Behavior", IsNullable = false)]
        public BehaviorModule behaviors;
    }

}
