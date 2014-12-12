namespace ChickenProtector.Helper
{
    using System;
    using System.Collections.Generic;

    using Artemis;
    using Artemis.Attributes;
    using Artemis.Manager;
    using Artemis.System;

    using Microsoft.Xna.Framework.Graphics;

    using ChickenProtector.Components;
    using ChickenProtector.Templates;


    class EnemyFactory
    {
        private Random random;

        private EntityWorld entityWorld;
        private SpriteBatch spriteBatch;


        public EnemyFactory()
        {
            /*entityWorld = null;
            spriteBatch = null;
            random = null;*/
        }

        public void Init(SpriteBatch sb, EntityWorld ew)
        {
            this.random = new Random();
            this.entityWorld = ew;
            this.spriteBatch = sb;
        }


        public void /*Entity*/ Spawn(bool boss = false)
        {
            Console.WriteLine("Spawning");
            Entity entity = null;

            int num = 0;

            if (boss)
            {
                //
                entity = entityWorld.CreateEntityFromTemplate(HawkTemplate.Name);
                num = 1;
            }
            else
            {
                //
                if (random.Next(101) >= 50)
                    entity = entityWorld.CreateEntityFromTemplate(SpiderTemplate.Name);
                else
                    entity = entityWorld.CreateEntityFromTemplate(EnemyTemplate.Name);

                num = random.Next(3);
            }
            
            if (num == 0)
            {
                entity.GetComponent<TransformComponent>().X = -10;
                entity.GetComponent<TransformComponent>().Y = this.random.Next(this.spriteBatch.GraphicsDevice.Viewport.Height) + 10;
            }
            else if (num == 1)
            {

                entity.GetComponent<TransformComponent>().X = this.random.Next(this.spriteBatch.GraphicsDevice.Viewport.Width);
                entity.GetComponent<TransformComponent>().Y = -10;

            }
            else if (num == 2)
            {
                entity.GetComponent<TransformComponent>().X = (this.spriteBatch.GraphicsDevice.Viewport.Width) + 10;
                entity.GetComponent<TransformComponent>().Y = this.random.Next(this.spriteBatch.GraphicsDevice.Viewport.Height);
            }


            if (boss)
            {
                entity.GetComponent<TransformComponent>().Width = 120;
                entity.GetComponent<TransformComponent>().Height = 135;
                entity.GetComponent<VelocityComponent>().Speed = 0.1f;
            }
            else
            {
                entity.GetComponent<TransformComponent>().Width = 36;
                entity.GetComponent<TransformComponent>().Height = 20;
                entity.GetComponent<VelocityComponent>().Speed = 0.05f;
            }


            entity.GetComponent<FollowComponent>().Follow = entityWorld.TagManager.GetEntity("BARN");

            //return entity;
        }
    }
}
