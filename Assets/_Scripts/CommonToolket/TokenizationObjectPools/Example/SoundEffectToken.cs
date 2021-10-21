using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CommonToolket.TokenizationObjectPool_;

namespace Exampleuse
{
    [CreateAssetMenu(fileName = "SoundEffectToken", menuName = "ObjectToken/SoundToken")]
    public class SoundEffectToken : TokenizableObjectToken<AudioClip, SoundEffectToken, SoundEffectContainer>
    {


    }

}
