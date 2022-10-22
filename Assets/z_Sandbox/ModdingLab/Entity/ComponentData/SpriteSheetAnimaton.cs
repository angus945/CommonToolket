using System;
using System.Xml.Serialization;
using UnityEngine;

namespace ModdingLab
{
    [XmlType]
    [System.Serializable]
    public class SpriteSheetAnimaton : ComponentData
    {
        public override Type RequireComponentType { get => typeof(SpriteSheetAnimator); }

        [XmlAttribute("spriteSheet")]
        public string spriteSheet;

        public override void InitialComponent(GameEntity entity, Component component)
        {
            SpriteSheetAnimator animator = component as SpriteSheetAnimator;
            animator.spriteSheet = entity.GetSpriteSheetByID(spriteSheet);
        }
    }

}
