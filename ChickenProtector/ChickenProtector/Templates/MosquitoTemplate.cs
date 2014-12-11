namespace ChickenProtector.Templates
{
    #region Using statements

    using Artemis;
    using Artemis.Attributes;
    using Artemis.Interface;

    using ChickenProtector.Components;

    #endregion
    [ArtemisEntityTemplate(Name)]
    public class MosquitoTemplate : IEntityTemplate
    {
        public const string Name = "MosquitoTemplate";

        public Entity BuildEntity(Entity entity, EntityWorld entityWorld, params object[] args)
        {
            entity.Tag = "MOSQUITO";

            entity.AddComponentFromPool<TransformComponent>();
            entity.AddComponent(new SpatialFormComponent("Mosquito"));
            entity.AddComponent(new VelocityComponent());
            entity.AddComponent(new FollowComponent());
            entity.AddComponent(new DamageComponent(0.01f));

            return entity;
        }
    }
}
