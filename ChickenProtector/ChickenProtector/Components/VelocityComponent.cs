namespace ChickenProtector.Components
{
using System;
using Artemis.Interface;


    public class VelocityComponent : IComponent
    {
        private const float ToRadians = (float)(Math.PI / 180.0);

        public VelocityComponent()
            : this(0.0f, 0.0f)
        {
        }

        public VelocityComponent(float velocity)
            : this(velocity, 0.0f)
        {
        }

        public VelocityComponent(float velocity, float angle)
        {
            this.Speed = velocity;
            this.Angle = angle;
        }

        public float Angle { get; set; }

        public float AngleAsRadians
        {
            get
            {
                return this.Angle * ToRadians;
            }
        }

        public float Speed { get; set; }

        public void AddAngle(float angle)
        {
            this.Angle = (this.AngleAsRadians + angle) % 360;
        }
    }
}
