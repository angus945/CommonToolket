using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TokenizableAnimation
{

    /*
        圖片打包成動畫令牌 AnimationToken
        
        動畫事件令牌
        同步切換

        https://www.youtube.com/watch?v=5aHhmRiVpZI
    */


    [CreateAssetMenu(fileName = "new Animation (Token)", menuName = "TokenizableAnimation/Animation")]
    public class AnimationToken : ScriptableObject
    {
        [System.Serializable]
        public struct AnimationFrame
        {
            public Sprite sprite;

        }


        [SerializeField] AnimationFrame[] animationFrames = null;
        public AnimationFrame[] frames { get => animationFrames; }
    }
}


