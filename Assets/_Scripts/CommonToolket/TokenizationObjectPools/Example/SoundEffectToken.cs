using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CommonToolket.TokenizationObjectPool;

namespace Example
{
    [CreateAssetMenu(fileName = "SoundEffectToken", menuName = "ObjectToken/SoundToken")]
    public class SoundEffectToken : TokenizableObjectToken<AudioClip, SoundEffectToken, SoundEffectContainer>
    {

        [SerializeField] AudioClip _objectReference = null;

        protected override ReferenceData<AudioClip> objectReference 
        {
            get => new ReferenceData<AudioClip>(_objectReference, _objectReference.length); 
        }
    }

}
