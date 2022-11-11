using System.ComponentModel;
using System.Xml.Serialization;

namespace ModdingLab.Definition
{
    public abstract class EntityModule : IDefinition
    {
        string IDefinition.id { get => id; }

        [XmlAttribute]
        public string id;

        [XmlAttribute]
        public string include;

        public string[] includes
        {
            get
            {
                return (include != null) ? include.Split(' ') : new string[0];
            }
        }
    }
}
