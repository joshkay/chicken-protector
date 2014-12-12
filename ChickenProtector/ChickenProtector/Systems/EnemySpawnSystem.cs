//namespace ChickenProtector.Systems
//{
//    using System;
//    using System.Collections.Generic;

//    using Artemis;
//    using Artemis.Attributes;
//    using Artemis.Manager;
//    using Artemis.System;

//    using Microsoft.Xna.Framework.Graphics;

//    using ChickenProtector.Components;
//    using ChickenProtector.Templates;

//    [ArtemisEntitySystem(GameLoopType = GameLoopType.Update, Layer = 1)]
//    public class EnemySpawnSystem : IntervalEntitySystem
//    {
//        private Random random;

//        private SpriteBatch spriteBatch;

//        public EnemySpawnSystem()
//            : base(new TimeSpan(0, 0, 0, 0, BlackBoard.GetEntry<int>("EnemyInterval")))
//        {
//        }

//        public override void LoadContent()
//        {
//            this.spriteBatch = BlackBoard.GetEntry<SpriteBatch>("SpriteBatch");
//            this.random = new Random();
//        }

//        protected override void ProcessEntities(IDictionary<int, Entity> entities)
//        {
//            Entity entity = this.EntityWorld.CreateEntityFromTemplate(EnemyTemplate.Name);
//            int num = random.Next(3);

//            if (num == 0)
//            {
//                entity.GetComponent<TransformComponent>().X = -10;
//                entity.GetComponent<TransformComponent>().Y = this.random.Next(this.spriteBatch.GraphicsDevice.Viewport.Height) + 10;
//            }
//            else if (num == 1)
//            {
                
//                entity.GetComponent<TransformComponent>().X = this.random.Next(this.spriteBatch.GraphicsDevice.Viewport.Width);
//                entity.GetComponent<TransformComponent>().Y = -10;
                
//            }
//            else if (num == 2)
//            {
//                entity.GetComponent<TransformComponent>().X = (this.spriteBatch.GraphicsDevice.Viewport.Width) + 10;
//                entity.GetComponent<TransformComponent>().Y = this.random.Next(this.spriteBatch.GraphicsDevice.Viewport.Height);
//            }

//            entity.GetComponent<TransformComponent>().Width = 36;
//            entity.GetComponent<TransformComponent>().Height = 20;

//            entity.GetComponent<VelocityComponent>().Speed = 0.05f;

//            entity.GetComponent<FollowComponent>().Follow = this.EntityWorld.TagManager.GetEntity("BARN");
//        }
//    }
//}
