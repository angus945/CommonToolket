using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CommonToolket.TokenizationObjectPool;

public class VisualEffectToken : TokenizableObjectToken<ParticleSystem,VisualEffectToken,VisualEffectContainer>
{
    [SerializeField] ReferenceData<ParticleSystem> _objectReference;
    protected override ReferenceData<ParticleSystem> objectReference { get => _objectReference; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
