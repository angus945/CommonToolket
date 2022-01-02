using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TokenizableAnimation.TokenizableAnimator;

namespace TokenizableAnimation
{
    /*
        behaviour tree?
        
        https://youtu.be/nKpM98I7PeM
        https://www.youtube.com/channel/UCAurXAUc29EBKsEQTjw7KqQ
        https://www.notion.so/Unity-Editor-GraphView-96a459b1cb4a42aeb05f2c53f9bbe6ab
        
        https://www.youtube.com/watch?v=BoGuwM8dP5M&list=PLOZ3CbLbuxPwQvkDMBiIe4v1pTwAE380z&index=18
    */
    [CreateAssetMenu(fileName = "new AnimationDriver (Token)", menuName = "TokenizableAnimation/AnimationDriver")]
    public class AnimationDriver : ScriptableObject
    {

        [SerializeField] float speed = 1;



        public void Update(AnimationState state, AnimationListeners listener)
        {
            state.time += state.deltaTime * speed;

            AnimationFrame frame = state.animation.GetAnimationFrame(state.time, out int index);
            if(state.frameIndex != index)
            {
                state.frameIndex = index;
                listener.TriggerEvents(frame.listenerToken);
            }

            state.sprite = frame.frameSprite;
        }
    }

}
