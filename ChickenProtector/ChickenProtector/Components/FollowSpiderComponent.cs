namespace ChickenProtector.Components
{
    using System;
    using Artemis.Interface;
    using Artemis;

    public class FollowSpiderComponent : IComponent
    {
        public FollowSpiderComponent()
            : this(null)
        {
        }

        public FollowSpiderComponent(Entity entity)
        {
            this.FollowSpider = entity;
        }

        public Entity FollowSpider { get; set; }
    }
}
