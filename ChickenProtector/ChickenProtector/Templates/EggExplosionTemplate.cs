namespace ChickenProtector.Templates
{
    #region Using statements

    using Artemis;
    using Artemis.Attributes;
    using Artemis.Interface;

    using ChickenProtector.Components;

    #endregion

    [ArtemisEntityTemplate(Name)]
    public class EggExplosionTemplate : IEntityTemplate
    {
        public const string Name = "EggExplosionTemplate";

        public Entity BuildEntity(Entity entity, EntityWorld entityWorld, params object[] args)
        {
            entity.Group = "EFFECTS";

            entity.AddComponentFromPool<TransformComponent>();
            entity.AddComponent(new SpatialFormComponent("CrackedEgg"));
            entity.AddComponent(new ExpiresComponent(1000));

            return entity;
        }
    }
}
