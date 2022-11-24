using System.Xml;
using System.Xml.Serialization;
using DataDriven;
using ModdingLaboratory.Definition.Componentized;

namespace ModdingLaboratory.Definition
{

    [XmlType("Entity")]
    [System.Serializable]
    public class EntityDefine : DefinitionBase
    {
        protected override string localID { get => id; }
        public override void SetGroup(string group)
        {
            base.SetGroup(group);

            tags.SetGroup(group);
            visuals.SetGroup(group);
            properties.SetGroup(group);
            components.SetGroup(group);
            behaviors.SetGroup(group);
        }

        [XmlAttribute("id")] public string id;

        [XmlElement("Name")] public string name;
        [XmlElement("Describe")] public string describe;

        [XmlElement("Tags", IsNullable = false)]
        public EntityTags tags;

        [XmlElement("Visual", IsNullable = false)]
        public VisualModule visuals;

        [XmlElement("Properties", IsNullable = false)]
        public ProperityModule properties;

        [XmlElement("Components", IsNullable = false)]
        public ComponentModule components;

        [XmlElement("Behavior", IsNullable = false)]
        public BehaviorModule behaviors;
    }

}
