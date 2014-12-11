namespace ChickenProtector.Components
{
    using System;
    using Artemis.Interface;

    public class DamageComponent : IComponent
    {
        public DamageComponent()
            : this(0.0f)
        {
        }

        public DamageComponent(float points)
        {
            this.Points = points;
        }

        public float Points { get; private set; }
    }
}
