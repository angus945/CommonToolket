using System;
using System.Xml.Serialization;
using UnityEngine;
using ModdingLab.Instance;
using ModdingLab.Instance.Visual;
using ModdingLab.Instance.Componentized;

namespace ModdingLab.Definition.Componentized
{
    [XmlType]
    [System.Serializable]
    public class SpriteSheetAnimaton : ComponentDefine
    {
        protected override string defaultID => "Animation";
       
        public override Type RequireComponentType { get => typeof(SpriteSheetAnimator); }

        [XmlAttribute("spriteSheet")]
        public string spriteSheetID;

        public override void InitialComponent(GameEntity entity, Component component)
        {
            SpriteSheetAnimator animator = component as SpriteSheetAnimator;

            SpriteSheetAnimation[] animations = VisualDatabase.TryGetAnimations(spriteSheetID, out string defaultAnim);

            animator.Initial(animations, defaultAnim);
        }
    }

}
