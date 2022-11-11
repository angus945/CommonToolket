using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace ModdingLab.Definition
{
    [XmlType("Tags")]
    [System.Serializable]
    public class EntityTags : EntityModule
    {
        //protected override IList moduleContents { get => tags; }

        [XmlElement("Tag", IsNullable = false)]
        public List<string> tags;
    }

}
