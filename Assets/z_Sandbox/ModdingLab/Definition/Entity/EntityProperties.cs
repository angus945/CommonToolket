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

    [XmlType]
    [System.Serializable]
    public class EntityProperties
    {
        [XmlElement("Prop")] public ProperityField[] properityFields;

        public ProperityField this[int index]
        {
            get => properityFields[index];
        }
        public int Length { get => properityFields.Length; }
    }
}
