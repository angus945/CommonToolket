using System;
using System.Xml.Serialization;
using UnityEngine;
using ModdingLaboratory.Instance;
using ModdingLaboratory.Instance.Visual;
using ModdingLaboratory.Instance.Componentized;

namespace ModdingLaboratory.Definition.Componentized
{
    [XmlType]
    [System.Serializable]
    public class SpriteSheetAnimaton : ComponentDefine
    {
        protected override string defaultID => "Animation";
       
        public override Type RequireComponentType { get => typeof(SpriteSheetAnimator); }

        [XmlAttribute("spriteSheet")]
        public string spriteSheetID;

        public override void InitialComponent(Component component)
        {
            SpriteSheetAnimator animator = component as SpriteSheetAnimator;

            //SpriteSheetAnimation[] animations = VisualDatabase.TryGetAnimations(spriteSheetID, out string defaultAnim);
            //Debug.LogError(spriteSheetID);
            animator.Initial(spriteSheetID);
        }
    }

}
