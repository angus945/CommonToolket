using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using ModdingLab.Definition.TypeScript;

namespace ModdingLab.Definition
{
    [XmlType("Behavior")]
    [System.Serializable]
    public class EntityBehaviors : EntityModule
    {
        //protected override IList moduleContents { get => behaviors; }

        [XmlElement("Script", IsNullable = false)]
        public List<BehaviorDefine> behaviors;
    }

}
