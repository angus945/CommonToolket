using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CommonToolket.TokenizationObjectPool_;

namespace Exampleuse
{
    public class SoundEffectPool : TokenizableObjectPool<AudioClip, SoundEffectToken, SoundEffectContainer>
    {


        [SerializeField] SoundEffectToken testToken;

        void Start()
        {
            base.InitialToken(new SoundEffectToken[] { testToken });
            base.InstantiateContainers();

            testToken.EnableObject().transform.parent = null;
        }
        void Update()
        {
            base.FrameUpdate(Time.deltaTime);
        }
    }

}
