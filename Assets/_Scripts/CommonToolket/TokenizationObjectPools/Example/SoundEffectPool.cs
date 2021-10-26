using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CommonToolket.TokenizationObjectPool;

namespace Example
{
    public class SoundEffectPool : TokenizableObjectPool<AudioClip, SoundEffectToken, SoundEffectContainer>
    {


        [SerializeField] SoundEffectToken testToken;

        void Start()
        {
            base.InitialToken(new SoundEffectToken[] { testToken });
            base.InstantiateContainers();

            testToken.TryEnableTokenObject(out _);
        }
        void Update()
        {
            base.FrameUpdate(UnityEngine.Time.deltaTime);
        }
    }

}
