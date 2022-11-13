using ModdingLaboratory.Definition.Componentized;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace ModdingLaboratory.Definition
{

    [XmlType("Properties")]
    [System.Serializable]
    public class ProperityModule : EntityModule
    {
        //protected override IList moduleContents { get => properityFields; }

        [XmlElement("Prop", IsNullable = false)] 
        public List<ProperityField> properityFields;

        public int length { get => properityFields.Count; }
        public ProperityField this[int index]
        {
            get => properityFields[index];
        }
    }
}
