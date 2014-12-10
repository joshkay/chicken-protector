namespace ChickenProtector.Screens
{
    #region Using statements

    using System;

    using Artemis;
    using Artemis.System;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;

    using ChickenProtector.Components;
    using ChickenProtector.Templates;

    #endregion

    class PlayScreen : GameScreen
    {
        /// <summary>The one second.</summary>
        private static readonly TimeSpan OneSecond = TimeSpan.FromSeconds(1);

        /// <summary>The elapsed time.</summary>
        private TimeSpan elapsedTime;

        /// <summary>The font.</summary>
        private SpriteFont font;

        /// <summary>The frame counter.</summary>
        private int frameCounter;

        /// <summary>The frame rate.</summary>
        private int frameRate;

        /// <summary>The entityWorld.</summary>
        private EntityWorld entityWorld;

        public PlayScreen(Game game, SpriteBatch spriteBatch)
            : base(game, spriteBatch)
        {
            
        }

        public override void Initialize()
        {
            base.Initialize();
            this.font = this.game.Content.Load<SpriteFont>("myFont");

            this.entityWorld = new EntityWorld();

            EntitySystem.BlackBoard.SetEntry("ContentManager", this.game.Content);
            EntitySystem.BlackBoard.SetEntry("GraphicsDevice", this.GraphicsDevice);
            EntitySystem.BlackBoard.SetEntry("SpriteBatch", this.spriteBatch);
            EntitySystem.BlackBoard.SetEntry("SpriteFont", this.font);
            EntitySystem.BlackBoard.SetEntry("EnemyInterval", 500);

#if XBOX
            this.entityWorld.InitializeAll( System.Reflection.Assembly.GetExecutingAssembly());
#else
            this.entityWorld.InitializeAll(true);
#endif

            InitializeBarn();
            InitializeChicken();
           // InitializeSpider();
        }

        public override void Show()
        {
            base.Show();
        }

        public override void Update(GameTime gameTime)
        {
            this.entityWorld.Update();

            ++this.frameCounter;
            this.elapsedTime += gameTime.ElapsedGameTime;
            if (this.elapsedTime > OneSecond)
            {
                this.elapsedTime -= OneSecond;
                this.frameRate = this.frameCounter;
                this.frameCounter = 0;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            string fps = string.Format("fps: {0}", this.frameRate);
#if DEBUG
            string entityCount = string.Format("Active entities: {0}", this.entityWorld.EntityManager.ActiveEntities.Count);
            string removedEntityCount = string.Format("Removed entities: {0}", this.entityWorld.EntityManager.TotalRemoved);
            string totalEntityCount = string.Format("Total entities: {0}", this.entityWorld.EntityManager.TotalCreated);
#endif

            this.GraphicsDevice.Clear(Color.Black);
            //this.spriteBatch.Begin();
            this.entityWorld.Draw();
            this.spriteBatch.DrawString(this.font, fps, new Vector2(32, 32), Color.Yellow);
#if DEBUG
            this.spriteBatch.DrawString(this.font, entityCount, new Vector2(32, 62), Color.Yellow);
            this.spriteBatch.DrawString(this.font, removedEntityCount, new Vector2(32, 92), Color.Yellow);
            this.spriteBatch.DrawString(this.font, totalEntityCount, new Vector2(32, 122), Color.Yellow);
#endif
            //this.spriteBatch.End();

            base.Draw(gameTime);
        }

        private void InitializeChicken()
        {
            Entity entity = this.entityWorld.CreateEntityFromTemplate(PlayerTemplate.Name);
            entity.GetComponent<TransformComponent>().X = this.GraphicsDevice.Viewport.Width * 0.5f;
            entity.GetComponent<TransformComponent>().Y = this.GraphicsDevice.Viewport.Height - 50;
        }

        private void InitializeBarn()
        {
            Entity entity = this.entityWorld.CreateEntityFromTemplate(BarnTemplate.Name);
            entity.GetComponent<TransformComponent>().X = this.GraphicsDevice.Viewport.Width * 0.5f;
            entity.GetComponent<TransformComponent>().Y = this.GraphicsDevice.Viewport.Height - 50;
            EntitySystem.BlackBoard.SetEntry("Barn", entity);
        }
        /*private void InitializeSpider()
        {
            Random random = new Random();
            for (int index = 0; 2 > index; ++index)
            {

                Entity entity = this.entityWorld.CreateEntityFromTemplate(EnemyTemplate.Name);
                entity.GetComponent<TransformComponent>().X = random.Next(this.GraphicsDevice.Viewport.Width - 500) + 50;
                entity.GetComponent<TransformComponent>().Y = random.Next((int)((this.GraphicsDevice.Viewport.Height * 0.75) + 0.5)) + 50;
                entity.GetComponent<VelocityComponent>().Speed = 0.05f;
                entity.GetComponent<VelocityComponent>().Angle = random.Next() % 2 == 0 ? 0 : 180;
                
            }
        }*/
    }
}
