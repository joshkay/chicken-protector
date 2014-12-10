namespace ChickenProtector.Templates
{
    #region Using statements

    using Artemis;
    using Artemis.Attributes;
    using Artemis.Interface;

    using ChickenProtector.Components;

    #endregion

    [ArtemisEntityTemplate(Name)]
    public class EnemyTemplate : IEntityTemplate
    {
        public const string Name = "EnemyTemplate";

        public Entity BuildEntity(Entity entity, EntityWorld entityWorld, params object[] args)
        {
            entity.Group = "ANIMALS";

            entity.AddComponentFromPool<TransformComponent>();
            entity.AddComponent(new SpatialFormComponent("Spider"));
            entity.AddComponent(new HealthComponent(10));
            entity.AddComponent(new FollowComponent());
            //entity.AddComponent(new WeaponComponent());
          // entity.AddComponent(new EnemyComponent());
           entity.AddComponent(new VelocityComponent());
            
            return entity;
        }
    }
}
