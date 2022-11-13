using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace ModdingLaboratory.Definition
{
    [XmlType("Visual")]
    [System.Serializable]
    public class VisualModule : EntityModule
    {
        [System.Serializable]
        public struct SpriteSheet
        {
            [XmlAttribute]
            public string id;

            [XmlText]
            public string sheet;
        }
        //protected override IList moduleContents { get => spriteSheets; }

        [XmlElement("SpriteSheet", IsNullable = false)]
        public List<SpriteSheet> spriteSheets;

        public int length { get => spriteSheets.Count; }
        public SpriteSheet this[int index]
        {
            get => spriteSheets[index];
        }
    }

}
