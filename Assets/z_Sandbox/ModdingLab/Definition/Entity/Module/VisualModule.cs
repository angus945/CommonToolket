using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace ModdingLaboratory.Definition
{
    [XmlType("Visual")]
    [System.Serializable]
    public class VisualModule : EntityModule
    {
        [System.Serializable]
        public struct SpriteSheet
        {
            [XmlAttribute]
            public string id;

            [XmlText]
            public string sheetName;
        }

        public override void SetGroup(string group)
        {
            base.SetGroup(group);

            for (int i = 0; i < length; i++)
            {
                SpriteSheet sheet = spriteSheets[i];
                sheet.sheetName = base.ApplyGroupID(spriteSheets[i].sheetName);
                spriteSheets[i] = sheet;
            }
        }

        [XmlElement("SpriteSheet", IsNullable = false)]
        public List<SpriteSheet> spriteSheets;

        public int length { get => spriteSheets.Count; }
        public SpriteSheet this[int index]
        {
            get => spriteSheets[index];
        }

        public void Include(IEnumerable<VisualModule> includeVisuals)
        {
            foreach (VisualModule visual in includeVisuals)
            {
                for (int i = 0; i < visual.spriteSheets.Count; i++)
                {
                    //if (spriteSheets.Contains(visual.spriteSheets[i])) continue;

                    spriteSheets.Add(visual.spriteSheets[i]);
                }
            }
        }
    }

}
