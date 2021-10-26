using System;
using UnityEngine;

namespace CommonToolket.TokenizationObjectPool
{
    [System.Serializable]
    public struct ReferenceData<Type> where Type : UnityEngine.Object
    {
        public Type reference;
        public float lifeTime;

        public ReferenceData(Type objectReference, float lifeTime)
        {
            this.reference = objectReference;
            this.lifeTime = lifeTime;
        }
    }

    public delegate bool EnableTokenObjectHandler<Type, Token, Container>(Token token, ReferenceData<Type> reference, out Container container) where Type : UnityEngine.Object where Token : TokenizableObjectToken<Type, Token, Container> where Container : TokenizableObjectContainer<Type, Token, Container>;

    public abstract class TokenizableObjectToken<Type, Token, Container> : ScriptableObject
        where Type : UnityEngine.Object
        where Token : TokenizableObjectToken<Type, Token, Container>
        where Container : TokenizableObjectContainer<Type, Token, Container>
    {

        protected abstract ReferenceData<Type> objectReference { get; }
        protected virtual bool AllowTokenEnable { get => true; }

        EnableTokenObjectHandler<Type, Token, Container> EnableHandler;
        public void Initial(EnableTokenObjectHandler<Type, Token, Container> enableHandler)
        {
            hideFlags = HideFlags.DontUnloadUnusedAsset;

            EnableHandler = enableHandler;

            OnInitialed();
        }
        public bool TryEnableTokenObject(out Container container, Action<Container> enabledHandler = null)
        {
            container = null;

            if (!AllowTokenEnable) return false;

            if (EnableHandler.Invoke((Token)this, objectReference, out container))
            {
                enabledHandler?.Invoke(container);
                OnContainerEnable(container);
                return true;
            }
            else return false;
        }

        /// <summary>
        /// Call After Token Initinalized
        /// </summary>
        protected virtual void OnInitialed() { }

        /// <summary>
        /// Call After Token Container Enabled
        /// </summary>
        /// <param name="container"></param>
        protected virtual void OnContainerEnable(in Container container) { }

    }

}
