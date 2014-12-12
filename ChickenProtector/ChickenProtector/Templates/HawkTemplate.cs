namespace ChickenProtector.Templates
{
    #region Using statements

    using Artemis;
    using Artemis.Attributes;
    using Artemis.Interface;

    using ChickenProtector.Components;

    #endregion

    [ArtemisEntityTemplate(Name)]
    public class HawkTemplate : IEntityTemplate
    {
        public const string Name = "HawkTemplate";

        public Entity BuildEntity(Entity entity, EntityWorld entityWorld, params object[] args)
        {
            entity.Group = "ANIMALS";

            entity.AddComponentFromPool<TransformComponent>();
            entity.AddComponent(new SpatialFormComponent("Hawk"));
            entity.AddComponent(new HealthComponent(75));
            entity.AddComponent(new FollowComponent());
            //entity.AddComponent(new WeaponComponent());
            // entity.AddComponent(new EnemyComponent());
            entity.AddComponent(new VelocityComponent());
            entity.AddComponent(new BossComponent());
            entity.AddComponent(new DamageComponent(3));

            return entity;
        }
    }
}
