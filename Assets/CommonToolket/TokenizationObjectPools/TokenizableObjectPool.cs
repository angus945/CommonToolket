using System.Collections.Generic;
using UnityEngine;

namespace CommonToolket.TokenizationObjectPool
{
    public abstract class TokenizableObjectPool<Type, Token, Container> : MonoBehaviour
        where Type : Object
        where Token : TokenizableObjectToken<Type, Token, Container>
        where Container : TokenizableObjectContainer<Type, Token, Container>
    {

        [SerializeField] int defaultContainerAmount = 20;
        [SerializeField] Container containerPrefab = null;
        protected Queue<Container> idleContainies = new Queue<Container>();
        protected List<Container> activeContainies = new List<Container>();

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

                idleContainies.Enqueue(newContainer);
            }
        }

        bool EnableTokenObject(Token token, ReferenceData<Type> reference, out Container container)
        {
            container = null;

            if (idleContainies.Count == 0) return false;

            container = idleContainies.Dequeue();
            container.EnableObject(token,  reference);

            activeContainies.Add(container);
            OnContainerEnabled(container);
            return container;
        }
        protected virtual void FrameUpdate(float deltaTime)
        {
            for (int i = 0; i < activeContainies.Count; i++)
            {
                Container container = activeContainies[i];
                if (container.FrameUpdate(deltaTime))
                {
                    container.ResetContainer(transform);

                    idleContainies.Enqueue(container);
                    activeContainies.RemoveAt(i--);
                }
            }
        }

        /// <summary>
        /// call after container as enabled
        /// </summary>
        /// <param name="container"></param>
        protected virtual void OnContainerEnabled(Container container) { }
    }

}
