using UnityEngine;

namespace CommonToolket.TokenizationObjectPool_
{
    public abstract class TokenizableObjectContainer<Type, Token, Container> : MonoBehaviour 
        where Type : Object 
        where Token : TokenizableObjectToken<Type, Token, Container>
        where Container : TokenizableObjectContainer<Type, Token, Container>
    {

        protected Token token;
        protected float lifeTime;

        protected Transform containerTrans;

        public virtual void Initialize()
        {
            containerTrans = GetComponent<Transform>();
        }
        public virtual void ResetContainer(Transform parent)
        {
            containerTrans.parent = parent;
        }
        public virtual Container EnableObject(Token token)
        {
            if (token == null) return null;

            this.token = token;
            lifeTime = token.lifeTime;

            return (Container)this;
        }

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

