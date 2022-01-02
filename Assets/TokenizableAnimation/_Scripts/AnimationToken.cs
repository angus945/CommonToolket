using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TokenizableAnimation
{

    /*
        圖片打包成動畫令牌 AnimationToken
        
        動畫事件令牌
        同步切換

        https://youtu.be/nA2IChvy_QU
        https://www.youtube.com/watch?v=5aHhmRiVpZI
    */


    [CreateAssetMenu(fileName = "new Animation (Token)", menuName = "TokenizableAnimation/Animation")]
    public partial class AnimationToken : ScriptableObject
    {

        [SerializeField] float frameRate = 20;
        public float fps { get => frameRate; }

        [SerializeField] AnimationFrame[] animationFrames = null;
        public AnimationFrame[] frames { get => animationFrames; }
        public int length { get => animationFrames.Length; }

        public AnimationFrame GetAnimationFrame(float time, out int index)
        {
            index = Mathf.FloorToInt(time * frameRate) % length;

            return animationFrames[index];
        }
    }
}


