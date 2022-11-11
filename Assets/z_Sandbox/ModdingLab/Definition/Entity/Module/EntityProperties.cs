using ModdingLab.Definition.Componentized;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace ModdingLab.Definition
{

    [XmlType("Properties")]
    [System.Serializable]
    public class EntityProperties : EntityModule
    {
        //protected override IList moduleContents { get => properityFields; }

        [XmlElement("Prop", IsNullable = false)] 
        public List<ProperityField> properityFields;
    }
}
