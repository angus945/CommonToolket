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

            OnContainerInit();
        }
        public void ResetContainer(Transform parent)
        {
            containerTrans.parent = parent;

            OnContainerReset();
        }
        public Container EnableObject(Token token)
        {
            if (token == null) return null;

            this.token = token;
            lifeTime = token.lifeTime;

            OnContainerEnable(token);

            return (Container)this;
        }

        protected virtual void OnContainerInit() { }
        protected virtual void OnContainerReset() { }
        protected virtual void OnContainerEnable(Token token) { }

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

