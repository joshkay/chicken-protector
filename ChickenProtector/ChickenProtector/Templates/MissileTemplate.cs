﻿namespace ChickenProtector.Templates
{
#region Using statements

using Artemis;
using Artemis.Attributes;
using Artemis.Interface;

using ChickenProtector.Components;

#endregion

    [ArtemisEntityTemplate(Name)]
    public class MissileTemplate : IEntityTemplate
    {
        public const string Name = "MissleTemplate";

        public Entity BuildEntity(Entity entity, EntityWorld entityWorld, params object[] args)
        {
            entity.Group = "BULLETS";

            entity.AddComponentFromPool<TransformComponent>();
            entity.AddComponent(new SpatialFormComponent("Egg"));
            entity.AddComponent(new VelocityComponent());
            entity.AddComponent(new ExpiresComponent(2000));

            return entity;
        }
    }
}
