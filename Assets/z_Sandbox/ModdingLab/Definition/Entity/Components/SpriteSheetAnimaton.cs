using System;
using System.Xml.Serialization;
using UnityEngine;
using ModdingLab.Instance;
using ModdingLab.Instance.Componentized;

namespace ModdingLab.Definition.Componentized
{
    [XmlType]
    [System.Serializable]
    public class SpriteSheetAnimaton : ComponentData
    {
        public override Type RequireComponentType { get => typeof(SpriteSheetAnimator); }

        [XmlAttribute("spriteSheet")]
        public string spriteSheetID;

        public override void InitialComponent(GameEntity entity, Component component)
        {
            SpriteSheetAnimator animator = component as SpriteSheetAnimator;
            SpriteSheetDefine animSpriteSheet = entity.GetSpriteSheetByID(spriteSheetID);

            animator.SetAnimationData(animSpriteSheet.animationDatas);
        }
    }

}
