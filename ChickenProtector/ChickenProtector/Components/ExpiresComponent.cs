namespace ChickenProtector.Components
{
    using Artemis.Interface;


    public class ExpiresComponent : IComponent
    {
        public ExpiresComponent()
            : this(0.0f)
        {
        }

        public ExpiresComponent(float lifeTime)
        {
            this.LifeTime = lifeTime;   
        }
        public bool IsExpired
        {
            get
            {
                return this.LifeTime <= 0;
            }
        }

        public float LifeTime { get; set; }

        public void ReduceLifeTime(float lifeTimeDelta)
        {
            this.LifeTime -= lifeTimeDelta;
            if (this.LifeTime < 0)
            {
                this.LifeTime = 0;
            }
        }
    }
}
