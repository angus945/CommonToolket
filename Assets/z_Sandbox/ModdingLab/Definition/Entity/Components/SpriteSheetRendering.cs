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
        public override Type RequireComponentType { get => typeof(SpriteSheetRenderer); }

        [XmlAttribute("spriteSheet")]
        public string spriteSheetID;

        public override void InitialComponent(GameEntity entity, Component component)
        {
            SpriteSheetRenderer renderer = component as SpriteSheetRenderer;
            SpriteSheet spriteSheet = entity.GetSpriteSheetByID(spriteSheetID);

            renderer.Initial();
            renderer.SetSpriteSheet(spriteSheet);
        }
    }

}
