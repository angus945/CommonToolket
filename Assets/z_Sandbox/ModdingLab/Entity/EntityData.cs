using System.Xml;
using System.Xml.Serialization;

namespace ModdingLab
{

    //[System]

    [XmlType("Entity")]
    [System.Serializable]
    public class EntityData
    {
        [XmlAttribute] public string id;
        [XmlElement("Name")] public string name;
        [XmlElement("Describe")] public string describe;

        [XmlArray("Tags")]
        [XmlArrayItem("Tag")]
        public string[] tags;

        [XmlArray("Visual")]
        [XmlArrayItem("SpriteSheet")]
        public string[] spriteSheets;

        [XmlArray("Components")]
        [XmlArrayItem("Collision", typeof(Collision))]
        [XmlArrayItem("Rigidbody", typeof(Rigidbody))]
        [XmlArrayItem("Animation", typeof(SpriteSheetAnimaton))]
        public ComponentData[] components;

        public override string ToString()
        {
            string comp = "";
            for (int i = 0; i < components.Length; i++)
            {
                comp += components[i].GetType();
                comp += "\n";
            }
            return comp;
        }
    }

}
