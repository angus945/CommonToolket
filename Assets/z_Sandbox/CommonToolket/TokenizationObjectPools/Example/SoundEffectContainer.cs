using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CommonToolket.TokenizationObjectPool;

namespace Example
{
    public class SoundEffectContainer : TokenizableObjectContainer<AudioClip, SoundEffectToken, SoundEffectContainer>
    {
        AudioSource audioSource = null;

        protected override void OnContainerInitialed()
        {
            audioSource = GetComponent<AudioSource>();
        }
        protected override void OnContainerEnabled(ReferenceData<AudioClip> reference)
        {
            audioSource.clip = reference.reference;
        }

    }

}
