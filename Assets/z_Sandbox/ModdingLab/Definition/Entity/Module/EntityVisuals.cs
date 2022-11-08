using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace ModdingLab.Definition
{
    [XmlType("Visual")]
    [System.Serializable]
    public class EntityVisuals : EntityModule<string>
    {
        protected override List<string> moduleContents { get => spriteSheets; }

        [XmlElement("SpriteSheet", IsNullable = false)]
        public List<string> spriteSheets;
    }

}
