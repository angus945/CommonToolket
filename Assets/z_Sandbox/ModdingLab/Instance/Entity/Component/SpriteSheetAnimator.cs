using System.Collections.Generic;
using UnityEngine;
using ModdingLaboratory.Instance.Visual;

namespace ModdingLaboratory.Instance.Componentized
{
    public class SpriteSheetAnimator : MonoBehaviour
    {
        SpriteSheet spriteSheet;
        SpriteSheetRenderer renderer;

        //
        string sheet;
        float time;
        SpriteSheetAnimation activeAnimation;

        public void Initial(string defaultSheet)
        {
            sheet = defaultSheet;
        }

        void Start()
        {
            renderer = GetComponent<SpriteSheetRenderer>();

            spriteSheet = GetComponent<GameEntity>().GetSpriteSheetByID(sheet);

            SetAnimation(spriteSheet.defaultAnimation);
        }
        void Update()
        {
            if (renderer == null) return;
            if (activeAnimation == null) return;

            PlayingAnimation();
        }

        void PlayingAnimation()
        {
            int x = Mathf.FloorToInt(time / activeAnimation.duration) % activeAnimation.length;
            int y = activeAnimation.index;

            renderer.SetSprite(x, y);

            time += Time.deltaTime;
        }

        public void SetAnimation(string name)
        {
            if (spriteSheet.animations.TryGetValue(name, out SpriteSheetAnimation animation))
            {
                time = 0;
                activeAnimation = animation;
            }
        }
    }
}
