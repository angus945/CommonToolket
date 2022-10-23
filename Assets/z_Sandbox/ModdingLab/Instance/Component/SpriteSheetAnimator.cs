using System.Collections.Generic;
using UnityEngine;
using ModdingLab.Definition;

namespace ModdingLab.Instance.Componentized
{
    public class SpriteSheetAnimator : MonoBehaviour
    {
        Dictionary<string, AnimationData> animationTable;

        SpriteSheetRenderer renderer;

        //
        float time;
        AnimationData activeAnimation = null;

        public void SetAnimationData(AnimationDatas animationDatas)
        {
            animationTable = new Dictionary<string, AnimationData>();

            for (int i = 0; i < animationDatas.Length; i++)
            {
                AnimationData animation = animationDatas[i];
                animationTable.Add(animation.name, animation);
            }

            SetAnimation(animationDatas.defaultAnimation);
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
            if (animationTable.TryGetValue(name, out AnimationData animation))
            {
                time = 0;
                activeAnimation = animation;
            }
        }
    }
}
