using System.Collections.Generic;
using UnityEngine;
using ModdingLab.Instance.Visual;

namespace ModdingLab.Instance.Componentized
{
    public class SpriteSheetAnimator : MonoBehaviour
    {
        Dictionary<string, SpriteSheetAnimation> animationTable = new Dictionary<string, SpriteSheetAnimation>();

        SpriteSheetRenderer renderer;

        //
        float time;
        SpriteSheetAnimation activeAnimation;

        public void Initial(SpriteSheetAnimation[] animations, string defaultAnimation)
        {
            animationTable.Clear();

            for (int i = 0; i < animations.Length; i++)
            {
                SpriteSheetAnimation animation = animations[i];
                animationTable.Add(animation.name, animation);
            }

            SetAnimation(defaultAnimation);
        }

        void Start()
        {
            renderer = GetComponent<SpriteSheetRenderer>();
        }
        void Update()
        {
            if (renderer == null) return;
            if (activeAnimation == null) return;

            PlayingAnimation();
        }

        void PlayingAnimation()
        {
            int index = activeAnimation.index;
            int frame = Mathf.FloorToInt(time / activeAnimation.duration) % activeAnimation.length;

            renderer.SetSprite(index, frame);

            time += Time.deltaTime;
        }

        public void SetAnimation(string name)
        {
            if (animationTable.TryGetValue(name, out SpriteSheetAnimation animation))
            {
                time = 0;
                activeAnimation = animation;
            }
        }
    }
}
