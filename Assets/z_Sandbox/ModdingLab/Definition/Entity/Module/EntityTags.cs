using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace ModdingLab.Definition
{
    [XmlType("Tags")]
    [System.Serializable]
    public class EntityTags : EntityModule<string>
    {
        protected override List<string> moduleContents { get => tags; }

        [XmlElement("Tag", IsNullable = false)]
        public List<string> tags;
    }

}
