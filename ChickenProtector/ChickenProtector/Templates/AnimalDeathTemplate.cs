namespace ChickenProtector.Templates
{
    #region Using statements

    using Artemis;
    using Artemis.Attributes;
    using Artemis.Interface;

    using ChickenProtector.Components;

    #endregion

    [ArtemisEntityTemplate(Name)]
    public class AnimalDeathTemplate : IEntityTemplate
    {
        public const string Name = "AnimalDeathTemplate";

        public Entity BuildEntity(Entity entity, EntityWorld entityWorld, params object[] args)
        {
            entity.Group = "EFFECTS";

            entity.AddComponentFromPool<TransformComponent>();
            entity.AddComponent(new SpatialFormComponent("BloodPool"));
            entity.AddComponent(new ExpiresComponent(1500));

            return entity;
        }
    }
}
