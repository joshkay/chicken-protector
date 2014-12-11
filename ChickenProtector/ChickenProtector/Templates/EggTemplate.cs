namespace ChickenProtector.Templates
{
    #region Using statements

    using Artemis;
    using Artemis.Attributes;
    using Artemis.Interface;

    using ChickenProtector.Components;

    #endregion

    [ArtemisEntityTemplate(Name)]
    public class EggTemplate : IEntityTemplate
    {
        public const string Name = "EggTemplate";

        public Entity BuildEntity(Entity entity, EntityWorld entityWorld, params object[] args)
        {
            entity.Group = "BULLETS";

            entity.AddComponentFromPool<TransformComponent>();
            entity.AddComponent(new SpatialFormComponent("Egg"));
            entity.AddComponent(new VelocityComponent());
            entity.AddComponent(new ExpiresComponent(2500));
            entity.AddComponent(new ProjectileComponent());
            entity.AddComponent(new DamageComponent(4));

            entity.GetComponent<TransformComponent>().Width = 20;
            entity.GetComponent<TransformComponent>().Height = 25;

            return entity;
        }
    }
}
