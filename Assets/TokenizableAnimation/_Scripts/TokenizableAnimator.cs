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

    public class AnimationListener
    {
        Dictionary<AnimationEventToken, Action> listeners;

        public void AddListener(AnimationEventToken token, Action eventHandler)
        {
            if(listeners.ContainsKey(token))
            {
                listeners[token] += eventHandler;
            }
            else listeners.Add(token, eventHandler);
        }
        public void RemoveListener(AnimationEventToken token)
        {
            listeners.Remove(token);
        }
        public void ClearListener()
        {
            listeners.Clear();
        }

        public void TriggerEvents(AnimationEventToken token)
        {
            if(listeners.ContainsKey(token))
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
        [SerializeField] AnimationEventToken eventToken;

        AnimationState state;
        AnimationListener listener;

        void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();

            state = new AnimationState(SetSprite, () => Time.deltaTime);
            listener = new AnimationListener();
        }
        void Start()
        {
            //debug
            state.animation = activeAnimation;
            listener.AddListener(eventToken, () => Debug.Log("Trigged event"));
        }
        void Update()
        {
            //spriteRenderer.sprite = activeAnimation.Update(ref time, Time.deltaTime, 20);

            driver.Update(state, listener);
        }

        public void AddListener(AnimationEventToken eventToken, Action eventHandler)
        {

        }

        void SetSprite(Sprite sprite)
        {
            spriteRenderer.sprite = sprite;
        }

    }


}
