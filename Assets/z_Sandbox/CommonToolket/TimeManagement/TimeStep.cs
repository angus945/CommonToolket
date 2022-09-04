namespace CommonToolket.TimeManagement
{
    [System.Serializable]
    public class TimeStep : Time
    {
        public TimeStep(TimeType type) : base(type)
        {

        }
        public new void Update()
        {
            base.Update();
        }
    }


}
