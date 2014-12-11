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

                    transformComponent.X += (float)(velocityComponent.Velocity.X * ms);
                    transformComponent.Y += (float)(velocityComponent.Velocity.Y * ms);
                }
            }
        }
    }
}
