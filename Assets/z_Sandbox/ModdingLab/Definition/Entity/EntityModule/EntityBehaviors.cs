using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using ModdingLab.Definition.TypeScript;

namespace ModdingLab.Definition
{
    [System.Serializable]
    public class EntityBehaviors
    {
        [XmlElement("Script", IsNullable = false)]
        public List<BehaviorDefine> behaviors;
    }

}
