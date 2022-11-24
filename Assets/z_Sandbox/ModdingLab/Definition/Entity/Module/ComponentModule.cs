using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using ModdingLaboratory.Definition.Componentized;

namespace ModdingLaboratory.Definition
{
    [XmlType("Components")]
    [System.Serializable]
    public class ComponentModule : EntityModule
    {
        //protected override IList moduleContents { get => components; }

        [UnityEngine.SerializeReference]
        [XmlElement("Collision", typeof(Collision))]
        [XmlElement("Rigidbody", typeof(Rigidbody))]
        [XmlElement("Rendering", typeof(SpriteSheetRendering))]
        [XmlElement("Animation", typeof(SpriteSheetAnimaton))]
        [XmlElement("DebugFlag", typeof(DebugFlag))]
        public List<ComponentDefine> components;

        public int length { get => components.Count; }
        public ComponentDefine this[int index]
        {
            get => components[index];
        }

        internal void Include(IEnumerable<ComponentModule> includeComponents)
        {
            foreach (var includeComponent in includeComponents)
            {
                for (int i = 0; i < includeComponent.length; i++)
                {
                    //if (components.Find(n => n.id == includeComponent.id)).Contains(includeComponent[i])) continue;

                    components.Add(includeComponent[i]);
                }
            }
        }
    }

}
