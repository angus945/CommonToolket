using UnityEngine;

namespace TokenizableAnimation
{
    [System.Serializable]
    public struct AnimationFrame
    {
        public Sprite frameSprite;
        public AnimationEventListener listenerToken;
    }
}


