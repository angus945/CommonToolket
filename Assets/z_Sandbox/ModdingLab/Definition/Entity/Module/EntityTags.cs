using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace ModdingLaboratory.Definition
{
    [XmlType("Tags")]
    [System.Serializable]
    public class EntityTags : EntityModule
    {
        //protected override IList moduleContents { get => tags; }

        [XmlElement("Tag", IsNullable = false)]
        public List<string> tags;

        public void Include(IEnumerable<EntityTags> includeTags)
        {
            foreach (EntityTags tag in includeTags)
            {
                for (int i = 0; i < tag.tags.Count; i++)
                {
                    if (tags.Contains(tag.tags[i])) continue;

                    tags.Add(tag.tags[i]);
                }
            }
        }
    }

}
