using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CommonToolket.TokenizationObjectPool;

namespace Exampleuse
{
    public class SoundEffectContainer : TokenizableObjectContainer<AudioClip, SoundEffectToken, SoundEffectContainer>
    {
        AudioSource audioSource = null;

        protected override void OnContainerInit()
        {
            audioSource = GetComponent<AudioSource>();
        }
        protected override void OnContainerEnable(SoundEffectToken token)
        {
            audioSource.clip = token.objectReference;
        }
    }

}
