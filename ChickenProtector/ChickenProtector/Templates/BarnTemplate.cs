namespace ChickenProtector.Templates
{
    #region Using statements

    using Artemis;
    using Artemis.Attributes;
    using Artemis.Interface;

    using ChickenProtector.Components;

    #endregion

    [ArtemisEntityTemplate(Name)]
    public class BarnTemplate : IEntityTemplate
    {
        public const string Name = "BarnTemplate";

        public Entity BuildEntity(Entity entity, EntityWorld entityWorld, params object[] args)
        {
            entity.AddComponentFromPool<TransformComponent>();
            entity.AddComponent(new SpatialFormComponent("Barn"));
            entity.AddComponent(new HealthComponent(100));
            entity.Tag = "BARN";

            return entity;
        }
    }
}
