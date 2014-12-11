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
            if (followComponent != null)
            {
                if (transformComponent != null)
                {
                    if (velocityComponent != null)
                    {
                        if (followComponent.Follow != null)
                        {
                            Vector2 barn = new Vector2(followComponent.Follow.GetComponent<TransformComponent>().X, followComponent.Follow.GetComponent<TransformComponent>().Y);
                            Vector2 spider = new Vector2(transformComponent.X, transformComponent.Y);
                            float angle = (float)Math.Atan2(barn.Y - spider.Y, barn.X - spider.X);
                            velocityComponent.Angle = MathHelper.ToDegrees(angle);
                        }
                    }
                }
            }
        }
        
    }
}
