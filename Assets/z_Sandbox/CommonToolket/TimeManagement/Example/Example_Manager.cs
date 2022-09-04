using UnityEngine;

namespace CommonToolket.TimeManagement
{
    public class Example_Manager : MonoBehaviour
    {
        TimeStep timeStep;
        public Time time { get => timeStep; }

        void Awake()
        {
            timeStep = new TimeStep(TimeType.BaseTime);

        }
        void Update()
        {
            timeStep.Update();
            //time.Update(); //保護層級

            //time.tim
        }
    }


}
