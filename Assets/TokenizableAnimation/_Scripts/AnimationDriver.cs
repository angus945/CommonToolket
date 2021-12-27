using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TokenizableAnimation
{
    /*
        behaviour tree?

    */

    [CreateAssetMenu(fileName = "new AnimationDriver (Token)", menuName = "TokenizableAnimation/AnimationDriver")]
    public class AnimationDriver : ScriptableObject
    {

        [SerializeField] float frameRate = 20;

        public void Update(AnimationState state, AnimationListener listener)
        {
            state.time += state.deltaTime;

            int current = Mathf.FloorToInt(state.time * frameRate) % state.animation.frames.Length;
            if(state.frameIndex != current)
            {
                state.frameIndex = current;
                //listener.TriggerEvents();
                //TODO Trigger event
            }

            AnimationToken.AnimationFrame frame = state.animation.frames[state.frameIndex];
            state.sprite = frame.sprite;
        }
    }

}
