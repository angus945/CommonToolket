using System.Xml.Serialization;

namespace ModdingLaboratory.Definition
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
