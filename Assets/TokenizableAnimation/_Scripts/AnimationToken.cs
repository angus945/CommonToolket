using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TokenizableAnimation
{

    /*
        �Ϥ����]���ʵe�O�P AnimationToken
        
        �ʵe�ƥ�O�P
        �P�B����

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


