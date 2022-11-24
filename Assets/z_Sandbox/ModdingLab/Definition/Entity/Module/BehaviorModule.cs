using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using ModdingLaboratory.Definition.TypeScript;

namespace ModdingLaboratory.Definition
{
    [XmlType("Behavior")]
    [System.Serializable]
    public class BehaviorModule : EntityModule
    {
        public override void SetGroup(string group)
        {
            base.SetGroup(group);

            for (int i = 0; i < length; i++)
            {
                behaviors[i].scriptName = base.ApplyGroupID(behaviors[i].scriptName);
            }
        }

        [XmlElement("Script", IsNullable = false)]
        public List<BehaviorDefine> behaviors;

        public int length { get => behaviors.Count; }
        public BehaviorDefine this[int index]
        {
            get => behaviors[index];
        }

        internal void Include(IEnumerable<BehaviorModule> includeBehaviors)
        {
            foreach (BehaviorModule includeBehavior in includeBehaviors)
            {
                for (int i = 0; i < includeBehavior.length; i++)
                {
                    //if (behaviors.Contains(includeBehavior[i])) continue;

                    behaviors.Add(includeBehavior[i]);
                }
            }
        }
    }

}
