using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using ModdingLab.Instance;
using ModdingLab.Instance.Componentized;
using ModdingLab.Instance.Visual;

namespace ModdingLab.Definition.Componentized
{
    public class SpriteSheetRendering : ComponentDefine
    {
        protected override string defaultID => "Rendering";

        public override Type RequireComponentType { get => typeof(SpriteSheetRenderer); }

        [XmlAttribute("spriteSheet")]
        public string spriteSheetID;

        public override void InitialComponent(Component component)
        {
            SpriteSheetRenderer renderer = component as SpriteSheetRenderer;

            renderer.Initial(spriteSheetID);
        }
    }

}
