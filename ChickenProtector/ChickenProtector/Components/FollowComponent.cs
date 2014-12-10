namespace ChickenProtector.Components
{
    using System;
    using Artemis.Interface;
    using Artemis;

    public class FollowComponent : IComponent
    {
        public FollowComponent()
            : this(null)
        {
        }
        
        public FollowComponent(Entity entity)
        {
            this.Follow = entity;
        }

        public Entity Follow { get; set; }
    }
}
