using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CommonToolket.TokenizationObjectPool_;

namespace Exampleuse
{
    public class SoundEffectContainer : TokenizableObjectContainer<AudioClip, SoundEffectToken, SoundEffectContainer>
    {
        AudioSource audioSource = null;

        public override void Initialize()
        {
            audioSource = GetComponent<AudioSource>();

            base.Initialize();
        }
        public override SoundEffectContainer EnableObject(SoundEffectToken token)
        {
            audioSource.clip = token.objectReference;

            return base.EnableObject(token);
        }
    }

}
