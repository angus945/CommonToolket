using System;
using UnityEngine;

namespace CommonToolket.TokenizationObjectPool
{
    public abstract class TokenizableObjectToken<Type, Token, Container> : ScriptableObject 
        where Type : UnityEngine.Object
        where Token : TokenizableObjectToken<Type, Token, Container>
        where Container : TokenizableObjectContainer<Type, Token, Container>
    {

        public abstract Type objectReference { get; }
        public abstract float lifeTime { get; }

        Func<Token, Container> EnableHandler;
        public void Initial(Func<Token, Container> enableHandler)
        {
            EnableHandler = enableHandler;

            OnInitial();
        }
        public Container EnableObject()
        {
            return EnableHandler.Invoke((Token)this);
        }

        protected virtual void OnInitial() { }
    }

}
