using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CommonToolket.TokenizationObjectPool;

namespace Exampleuse
{
    [CreateAssetMenu(fileName = "SoundEffectToken", menuName = "ObjectToken/SoundToken")]
    public class SoundEffectToken : TokenizableObjectToken<AudioClip, SoundEffectToken, SoundEffectContainer>
    {

        [SerializeField] AudioClip _objectReference = null;
        [SerializeField] float _lifeTime = 1;

        public override AudioClip objectReference { get => _objectReference; }
        public override float lifeTime { get => _lifeTime; }
    }

}
