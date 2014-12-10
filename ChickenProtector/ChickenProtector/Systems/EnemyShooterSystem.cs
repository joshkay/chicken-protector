namespace ChickenProtector.Systems
{
    #region Using statements

    using System;

    using Artemis;
    using Artemis.Attributes;
    using Artemis.Manager;
    using Artemis.System;
    using Artemis.Utils;

    using ChickenProtector.Components;
    using ChickenProtector.Templates;

    #endregion

    [ArtemisEntitySystem(GameLoopType = GameLoopType.Update, Layer = 1)]
    class EnemyShooterSystem : EntityProcessingSystem<TransformComponent,WeaponComponent, EnemyComponent>
    {
        private static readonly long TwoSecondsTicks = TimeSpan.FromSeconds(2).Ticks;

        public override void Process(Entity entity, TransformComponent transformComponent, WeaponComponent weaponComponent, EnemyComponent enemyComponent)
        {
            if (weaponComponent != null)
            {
                if ((weaponComponent.ShotAt + TwoSecondsTicks) < FastDateTime.Now.Ticks)
                {
                    Entity missle = this.EntityWorld.CreateEntityFromTemplate(MissleTemplate.Name);

                    missle.GetComponent<TransformComponent>().X = transformComponent.X;
                    missle.GetComponent<TransformComponent>().Y = transformComponent.Y + 20;

                    missle.GetComponent<VelocityComponent>().Speed = -0.5f;
                    missle.GetComponent<VelocityComponent>().Angle = 270;
                    weaponComponent.ShotAt = FastDateTime.Now.Ticks;
                }
            }
        
        }
    }
}
