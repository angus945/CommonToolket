using System.Xml.Serialization;

namespace ModdingLab.Definition.Componentized
{
    [XmlType]
    [System.Serializable]
    public class ProperityField
    {
        [XmlAttribute]
        public string name;

        [XmlText]
        public float value;
    }
}
