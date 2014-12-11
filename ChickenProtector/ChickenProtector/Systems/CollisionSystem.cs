namespace ChickenProtector.Systems
{
#region Using statements

using System.Collections.Generic;

using Artemis;
using Artemis.Attributes;
using Artemis.Manager;
using Artemis.System;
using Artemis.Utils;

using Microsoft.Xna.Framework;

using ChickenProtector.Components;
using ChickenProtector.Templates;

#endregion

    [ArtemisEntitySystem(GameLoopType = GameLoopType.Update, Layer = 1)]
    internal class CollisionSystem : EntitySystem
    {
        public CollisionSystem()
            : base(Aspect.All(typeof(TransformComponent)))
        {
        }

        protected override void ProcessEntities(IDictionary<int, Entity> entities)
        {
            Bag<Entity> bullets = this.EntityWorld.GroupManager.GetEntities("BULLETS");
            Bag<Entity> ships = this.EntityWorld.GroupManager.GetEntities("ANIMALS");

            if (bullets != null && ships != null)
            {
                for (int shipIndex = 0; ships.Count > shipIndex; ++shipIndex)
                {
                    Entity animal = ships.Get(shipIndex);
                    for (int bulletIndex = 0; bullets.Count > bulletIndex; ++bulletIndex)
                    {
                        Entity egg = bullets.Get(bulletIndex);
                        ProjectileComponent bulletProjectile = egg.GetComponent<ProjectileComponent>();

                        // do not test collision between bullet and shooter
                        if (bulletProjectile.ShooterImmune)
                        {
                            if (bulletProjectile.ShooterTag == animal.Tag)
                                continue;
                        }

                        if (this.CollisionExists(egg, animal))
                        {
                            TransformComponent bulletTransform = egg.GetComponent<TransformComponent>();
                            Entity crackedEgg = this.EntityWorld.CreateEntityFromTemplate(EggExplosionTemplate.Name);
                            crackedEgg.GetComponent<TransformComponent>().Position = bulletTransform.Position;
                            crackedEgg.Refresh();
                            egg.Delete();

                            HealthComponent healthComponent = animal.GetComponent<HealthComponent>();
                            healthComponent.AddDamage(4);

                            if (!healthComponent.IsAlive)
                            {
                                TransformComponent shipTransform = animal.GetComponent<TransformComponent>();
                                Entity deadAnimal = this.EntityWorld.CreateEntityFromTemplate(AnimalDeathTemplate.Name);
                                deadAnimal.GetComponent<TransformComponent>().Position = shipTransform.Position;
                                deadAnimal.Refresh();
                                animal.Delete();
                                break;
                            }
                        }
                    }
                }
            }
        }

        private bool CollisionExists(Entity entity1, Entity entity2)
        {
            return Vector2.Distance(entity1.GetComponent<TransformComponent>().Position, entity2.GetComponent<TransformComponent>().Position) < 20;
        }
    }
}