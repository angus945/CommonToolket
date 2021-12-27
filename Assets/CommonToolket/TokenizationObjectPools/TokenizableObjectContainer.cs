using UnityEngine;

namespace CommonToolket.TokenizationObjectPool
{
    public abstract class TokenizableObjectContainer<Type, Token, Container> : MonoBehaviour
        where Type : Object
        where Token : TokenizableObjectToken<Type, Token, Container>
        where Container : TokenizableObjectContainer<Type, Token, Container>
    {

        protected Token token;
        protected float lifeTime;

        protected Transform containerTrans;

        public void Initialize()
        {
            containerTrans = GetComponent<Transform>();

            OnContainerInitialed();
        }
        public void ResetContainer(Transform parent)
        {
            containerTrans.parent = parent;

            OnContainerReseted();
        }
        public void EnableObject(Token enableToken, ReferenceData<Type> reference)
        {
            if (enableToken == null) return;

            token = enableToken;

            lifeTime = reference.lifeTime;

            OnContainerEnabled(reference);
        }

        protected virtual void OnContainerInitialed() { }
        protected virtual void OnContainerReseted() { }
        protected virtual void OnContainerEnabled(ReferenceData<Type> reference) { }

        /// <summary>
        /// Return true as lifetime end
        /// </summary>
        public virtual bool FrameUpdate(float deltaTime)
        {
            lifeTime -= deltaTime;

            return lifeTime < 0;
        }
    }

}

