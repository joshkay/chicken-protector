namespace ChickenProtector.Systems
{
    using Artemis;
    using Artemis.Attributes;
    using Artemis.Manager;
    using Artemis.System;
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    using ChickenProtector.Components;

    [ArtemisEntitySystem(GameLoopType = GameLoopType.Update, Layer = 1)]
    public class FollowSystem : EntityProcessingSystem<FollowComponent, TransformComponent, VelocityComponent>
    {
        protected override void Process(Entity entity, FollowComponent followComponent, TransformComponent transformComponent, VelocityComponent velocityComponent)
        {
            if (followComponent != null && followComponent.Follow != null && followComponent.Follow.IsActive)
            {
                if (transformComponent != null)
                {
                    if (velocityComponent != null)
                    {
                        Vector2 follow = new Vector2(followComponent.Follow.GetComponent<TransformComponent>().X, followComponent.Follow.GetComponent<TransformComponent>().Y);
                        Vector2 pos = new Vector2(transformComponent.X, transformComponent.Y);
                        float angle = (float)Math.Atan2(follow.Y - pos.Y, follow.X - pos.X);
                        velocityComponent.Angle = MathHelper.ToDegrees(angle);
                    }
                }
            }
        }
        
    }
}
