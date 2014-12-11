namespace ChickenProtector.Components
{
    using System;
    using Artemis.Interface;

    public class DamageComponent : IComponent
    {
        public DamageComponent()
            : this(0)
        {
        }

        public DamageComponent(int points)
        {
            this.Points = points;
        }

        public int Points { get; private set; }
    }
}
