using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace ModdingLab.Definition
{
    [XmlType("Visual")]
    [System.Serializable]
    public class VisualModule : EntityModule
    {
        //protected override IList moduleContents { get => spriteSheets; }

        [XmlElement("SpriteSheet", IsNullable = false)]
        public List<string> spriteSheets;

        public int length { get => spriteSheets.Count; }
        public string this[int index]
        {
            get => spriteSheets[index];
        }
    }

}
