using System;
using UnityEngine;

namespace CommonToolket.TimeManagement
{
    [System.Serializable]
    public class Time 
    {
        public readonly TimeType type;
        public float time { get; private set; }
        public float deltaTime { get; private set; }
        public float timeScale { get; set; } = 1;

        Func<float> deltaHandler;

        public Time(TimeType type)
        {
            this.type = type;

            deltaHandler = DeltaTimeHandler();
        }
        protected void Update()
        {
            deltaTime = deltaHandler.Invoke();

            time += deltaTime;
        }

        Func<float> DeltaTimeHandler()
        {
            switch (type)
            {
                case TimeType.BaseTime:
                    return () => UnityEngine.Time.deltaTime * timeScale;

                case TimeType.Independent:
                    return () => UnityEngine.Time.unscaledDeltaTime * timeScale;

                case TimeType.Unscale:
                    return () => UnityEngine.Time.unscaledDeltaTime;

                case TimeType.BaseFixed:
                    return () => UnityEngine.Time.fixedDeltaTime * timeScale;

                case TimeType.IndependentFixed:
                    return () => UnityEngine.Time.fixedUnscaledDeltaTime * timeScale;

                case TimeType.UnscaleFixed:
                    return () => UnityEngine.Time.fixedUnscaledDeltaTime;

                default:
                    Debug.LogError($"TimeType \"{type}\" has undefine");
                    return () => 0;
            }
        }
    }


}
