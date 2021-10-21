using System;
using UnityEngine;

namespace CommonToolket.TokenizationObjectPool_
{
    public abstract class TokenizableObjectToken<Type, Token, Container> : ScriptableObject 
        where Type : UnityEngine.Object
        where Token : TokenizableObjectToken<Type, Token, Container>
        where Container : TokenizableObjectContainer<Type, Token, Container>
    {
        [SerializeField] Type _objectReference = null;
        [SerializeField] float _lifeTime = 1;
        public Type objectReference { get => _objectReference; }
        public float lifeTime { get => _lifeTime; }

        Func<Token, Container> EnableHandler;
        public virtual void Initial(Func<Token, Container> enableHandler)
        {
            EnableHandler = enableHandler;
        }

        public Container EnableObject()
        {
            return EnableHandler.Invoke((Token)this);
        }
    }

}
