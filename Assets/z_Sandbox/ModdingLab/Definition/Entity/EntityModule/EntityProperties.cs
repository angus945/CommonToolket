using System.Collections.Generic;
using System.Xml.Serialization;

namespace ModdingLab.Definition.Componentized
{

    [XmlType]
    [System.Serializable]
    public class EntityProperties : EntityModule<ProperityField>
    {
        protected override List<ProperityField> moduleContents { get => properityFields; }

        [XmlElement("Prop", IsNullable = false)] 
        public List<ProperityField> properityFields;
    }
}
