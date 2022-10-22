using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

namespace ModdingLab.Define.Componentized
{
    public class SpriteSheetRendering : ComponentData
    {
        public override Type RequireComponentType { get => typeof(SpriteSheetRenderer); }

        [XmlAttribute("spriteSheet")]
        public string spriteSheetID;

        public override void InitialComponent(GameEntity entity, Component component)
        {
            SpriteSheetRenderer renderer = component as SpriteSheetRenderer;
            SpriteSheet sheet = entity.GetSpriteSheetByID(spriteSheetID);

            renderer.Initial();
            renderer.SetSpriteSheet(sheet);
        }
    }

}