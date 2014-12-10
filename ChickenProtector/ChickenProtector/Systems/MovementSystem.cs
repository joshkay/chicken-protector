namespace ChickenProtector.Systems
{
#region Using statements

using System;

using Artemis;
using Artemis.Attributes;
using Artemis.Manager;
using Artemis.System;

using ChickenProtector.Components;

#endregion

    [ArtemisEntitySystem(GameLoopType = GameLoopType.Update, Layer = 1)]
    public class MovementSystem : EntityProcessingSystem<TransformComponent, VelocityComponent>
    {
        protected override void Process(Entity entity, TransformComponent transformComponent, VelocityComponent velocityComponent)
        {
            if (velocityComponent != null)
            {
                if (transformComponent != null)
                {
                    float ms = TimeSpan.FromTicks(this.EntityWorld.Delta).Milliseconds;

                    transformComponent.X += (float)(Math.Cos(velocityComponent.AngleAsRadians) * velocityComponent.Speed * ms);
                    transformComponent.Y += (float)(Math.Sin(velocityComponent.AngleAsRadians) * velocityComponent.Speed * ms);
                }
            }
        }
    }
}
