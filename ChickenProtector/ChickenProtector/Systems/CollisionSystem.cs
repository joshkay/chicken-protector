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
            Bag<Entity> animals = this.EntityWorld.GroupManager.GetEntities("ANIMALS");
            Entity barn = this.EntityWorld.TagManager.GetEntity("BARN");
            Entity player = this.EntityWorld.TagManager.GetEntity("PLAYER");
            Entity mosquito = this.EntityWorld.TagManager.GetEntity("MOSQUITO");
            
            if (bullets != null && animals != null)
            {
                for (int animalIndex = 0; animals.Count > animalIndex; ++animalIndex)
                {
                    Entity animal = animals.Get(animalIndex);
                    
                    // barn and animal
                    if (this.CollisionExists(animal, barn))
                    {
                        SuicideIntoEntity(animal, barn);
                    }

                    // player and animal
                    if (this.CollisionExists(animal, player))
                    {
                        SuicideIntoEntity(animal, player);
                    }

                    // player and mosquito
                    if (this.CollisionExists(animal, mosquito))
                    {
                        HurtAnimal(mosquito, animal);
                    }

                    // collision between entities
                    for (int animalIndex2 = 0; animals.Count > animalIndex2; ++animalIndex2)
                    {
                        if (animalIndex == animalIndex2)
                            continue;

                        Entity animal2 = animals.Get(animalIndex2);

                        if (this.CollisionExists(animal, animal2))
                        {
                            AnimalAndAnimal(animal, animal2);
                        }
                    }

                    // collision between projectiles and entities
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
                            EggAndAnimal(egg, animal);
                        }
                    }
                }
            }
        }

        private bool CollisionExists(Entity entity1, Entity entity2)
        {
            Rectangle first = entity1.GetComponent<TransformComponent>().Bounds;
            Rectangle second = entity2.GetComponent<TransformComponent>().Bounds;

            if (first.Intersects(second))
                return true;

            return false;
        }

        private void EggAndAnimal(Entity egg, Entity animal)
        {
            TransformComponent bulletTransform = egg.GetComponent<TransformComponent>();
            Entity crackedEgg = this.EntityWorld.CreateEntityFromTemplate(EggExplosionTemplate.Name);
            crackedEgg.GetComponent<TransformComponent>().Position = bulletTransform.Position;
            crackedEgg.Refresh();
            egg.Delete();

            HurtAnimal(egg, animal);
        }

        private void HurtAnimal(Entity attacker, Entity animal)
        {
            HealthComponent healthComponent = animal.GetComponent<HealthComponent>();
            healthComponent.AddDamage(attacker.GetComponent<DamageComponent>().Points);

            if (!healthComponent.IsAlive)
            {
                TransformComponent animalTransform = animal.GetComponent<TransformComponent>();
                Entity deadAnimal = this.EntityWorld.CreateEntityFromTemplate(AnimalDeathTemplate.Name);
                deadAnimal.GetComponent<TransformComponent>().Position = animalTransform.Position;
                deadAnimal.Refresh();
                animal.Delete();
            }
        }

        private void AnimalAndAnimal(Entity animal1, Entity animal2)
        {
            //Vector2 newPosition = animal1.GetComponent<TransformComponent>().Position;
            //Vector2 velocity = animal1.GetComponent<VelocityComponent>().Velocity;

            //if (velocity.X > 0)
            //    newPosition.X = animal2.GetComponent<TransformComponent>().Bounds.Left - animal2.GetComponent<TransformComponent>().Width / 2;
            //else if (velocity.X < 0)
            //    newPosition.X = animal2.GetComponent<TransformComponent>().Bounds.Right + animal2.GetComponent<TransformComponent>().Width / 2;
            //if (velocity.Y > 0)
            //    newPosition.Y = animal2.GetComponent<TransformComponent>().Bounds.Top - animal2.GetComponent<TransformComponent>().Height / 2;
            //else if (velocity.Y < 0)
            //    newPosition.Y = animal2.GetComponent<TransformComponent>().Bounds.Bottom + animal2.GetComponent<TransformComponent>().Height / 2;

            //animal1.GetComponent<TransformComponent>().Position = newPosition;
        }

        private void SuicideIntoEntity(Entity animal, Entity e)
        {
            //animal.GetComponent<VelocityComponent>().Speed = 0;

            HealthComponent healthComponent = e.GetComponent<HealthComponent>();
            healthComponent.AddDamage(animal.GetComponent<DamageComponent>().Points);

            TransformComponent animalTransform = animal.GetComponent<TransformComponent>();
            Entity deadAnimal = this.EntityWorld.CreateEntityFromTemplate(AnimalDeathTemplate.Name);
            deadAnimal.GetComponent<TransformComponent>().Position = animalTransform.Position;
            deadAnimal.Refresh();

            animal.Delete();
        }
    }
}