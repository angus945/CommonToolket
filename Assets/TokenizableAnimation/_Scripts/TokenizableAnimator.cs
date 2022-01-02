using System;
using System.Collections.Generic;
using UnityEngine;

namespace TokenizableAnimation
{

    [System.Serializable]
    public class AnimationState
    {
        public float time;
        public AnimationToken animation;
        public int frameIndex;

        Action<Sprite> setSpriteHandler;
        Func<float> deltaTimeHandler;

        public AnimationState(Action<Sprite> setSpriteHandler, Func<float> deltaTimeHandler)
        {
            this.setSpriteHandler = setSpriteHandler;
            this.deltaTimeHandler = deltaTimeHandler;
        }

        public float deltaTime { get => deltaTimeHandler.Invoke(); }
        public Sprite sprite { set => setSpriteHandler.Invoke(value); }
    }

    [System.Serializable]
    public class AnimationListeners
    {
        Dictionary<AnimationEventListener, Action> listeners;

        public AnimationListeners()
        {
            listeners = new Dictionary<AnimationEventListener, Action>();
        }

        public void AddListener(AnimationEventListener token, Action eventHandler)
        {
            if (listeners.ContainsKey(token))
            {
                listeners[token] += eventHandler;
            }
            else listeners.Add(token, eventHandler);
        }
        public void RemoveListener(AnimationEventListener token)
        {
            listeners.Remove(token);
        }
        public void ClearListener()
        {
            listeners.Clear();
        }

        public void TriggerEvents(AnimationEventListener token)
        {
            if (token == null) return;

            if (listeners.ContainsKey(token))
            {
                listeners[token]?.Invoke();
            }
        }
    }

    [RequireComponent(typeof(SpriteRenderer))]
    public class TokenizableAnimator : MonoBehaviour
    {
        //Components
        SpriteRenderer spriteRenderer;

        //Animation
        [SerializeField] AnimationDriver driver = null;

        [Header("Debug")]
        [SerializeField] AnimationToken activeAnimation;
        [SerializeField] AnimationEventListener eventToken;

        AnimationState state;
        AnimationListeners listeners;

        void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();

            state = new AnimationState(SetSprite, () => Time.deltaTime);
            listeners = new AnimationListeners();
        }
        void Start()
        {
            //Debug
            AddListener(eventToken, () => Debug.Log("Trigged event"));
            SetAnimation(activeAnimation);
        }
        void Update()
        {
            driver.Update(state, listeners);
        }

        void SetSprite(Sprite sprite)
        {
            spriteRenderer.sprite = sprite;
        }

        public void SetAnimation(AnimationToken animation)
        {
            state.animation = animation;
        }

        //Animation Listener
        public void AddListener(AnimationEventListener listener, Action eventHandler)
        {
            listeners.AddListener(listener, eventHandler);
        }
        public void RemoveListener(AnimationEventListener listener)
        {
            listeners.RemoveListener(listener);
        }
        public void TriggerEvent(AnimationEventListener listener)
        {
            listeners.TriggerEvents(listener);
        }
        public void ClearListener()
        {
            listeners.ClearListener();
        }


    }


}
