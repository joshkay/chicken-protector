namespace ChickenProtector.Systems
{
    #region Using statements

    using System;

    using Artemis;
    using Artemis.Attributes;
    using Artemis.System;

    using ChickenProtector.Components;

    #endregion

    /// <summary>The expiration system.</summary>
    [ArtemisEntitySystem]
    public class ExpirationSystem : EntityProcessingSystem<ExpiresComponent>
    {
        /// <summary>Processes the specified entity.</summary>
        /// <param name="entity">The entity.</param>
        protected override void Process(Entity entity, ExpiresComponent expiresComponent)
        {
            if (expiresComponent != null)
            {
                float ms = TimeSpan.FromTicks(this.EntityWorld.Delta).Milliseconds;
                expiresComponent.ReduceLifeTime(ms);

                if (expiresComponent.IsExpired)
                {
                    entity.Delete();
                }
            }
        }
    }
}