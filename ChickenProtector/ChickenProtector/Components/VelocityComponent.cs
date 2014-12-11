namespace ChickenProtector.Components
{
    using System;
    using Artemis.Interface;
    using Microsoft.Xna.Framework;

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

        public Vector2 Velocity 
        { 
            get
            {
                return new Vector2(
                    (float)Math.Cos(AngleAsRadians) * Speed,
                    (float)Math.Sin(AngleAsRadians) * Speed
                );
            }
        }

        public float Speed { get; set; }

        public void AddAngle(float angle)
        {
            this.Angle = (this.AngleAsRadians + angle) % 360;
        }
    }
}
