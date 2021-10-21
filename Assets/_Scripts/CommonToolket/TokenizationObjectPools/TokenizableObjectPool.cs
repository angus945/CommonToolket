using System.Collections.Generic;
using UnityEngine;

namespace CommonToolket.TokenizationObjectPool_
{
    public abstract class TokenizableObjectPool<Type, Token, Container> : MonoBehaviour
        where Type : Object
        where Token : TokenizableObjectToken<Type, Token, Container>
        where Container : TokenizableObjectContainer<Type, Token, Container>
    {

        [SerializeField] int defaultContainerAmount = 20;
        [SerializeField] Container containerPrefab = null;
        protected Queue<Container> idleContainers = new Queue<Container>();
        protected List<Container> activeContainer = new List<Container>();

        protected void InitialToken(Token[] tokens)
        {
            for (int i = 0; i < tokens.Length; i++)
            {
                tokens[i].Initial(EnableTokenObject);
            }
        }
        protected void InstantiateContainers()
        {
            for (int i = 0; i < defaultContainerAmount; i++)
            {
                Container newContainer = Instantiate(containerPrefab);

                newContainer.Initialize();
                newContainer.ResetContainer(transform);

                idleContainers.Enqueue(newContainer);
            }
        }

        public virtual Container EnableTokenObject(Token token)
        {
            Container container = idleContainers.Dequeue();
            activeContainer.Add(container);

            return container.EnableObject(token);
        }
        protected virtual void FrameUpdate(float deltaTime)
        {
            for (int i = 0; i < activeContainer.Count; i++)
            {
                Container container = activeContainer[i];
                if (container.FrameUpdate(deltaTime))
                {
                    container.ResetContainer(transform);

                    idleContainers.Enqueue(container);
                    activeContainer.RemoveAt(i--);
                }
            }
        }
    }

}
