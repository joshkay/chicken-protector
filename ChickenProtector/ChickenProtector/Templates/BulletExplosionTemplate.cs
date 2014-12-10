namespace ChickenProtector.Templates
{
    #region Using statements

    using Artemis;
    using Artemis.Attributes;
    using Artemis.Interface;

    using ChickenProtector.Components;

    #endregion

    [ArtemisEntityTemplate(Name)]
    public class BulletExplosionTemplate : IEntityTemplate
    {
        public const string Name = "BulletExplosionTemplate";

        public Entity BuildEntity(Entity entity, EntityWorld entityWorld, params object[] args)
        {
            entity.Group = "EFFECTS";

            entity.AddComponentFromPool<TransformComponent>();
            entity.AddComponent(new SpatialFormComponent("BulletExplosion"));
            entity.AddComponent(new ExpiresComponent(1000));

            return entity;
        }
    }
}
