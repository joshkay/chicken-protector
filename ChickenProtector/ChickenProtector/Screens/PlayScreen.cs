﻿namespace ChickenProtector.Screens
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
            EntitySystem.BlackBoard.SetEntry("EnemyInterval", 1000);

#if XBOX
            this.entityWorld.InitializeAll( System.Reflection.Assembly.GetExecutingAssembly());
#else
            this.entityWorld.InitializeAll(true);
#endif

            InitializeTileMap();
            InitializeBarn();
            InitializeChicken();
            InitializeMosquito();
        }

        public override void Show()
        {
            base.Show();
        }

        public override void Update(GameTime gameTime)
        {
            // check for game over
            if (!entityWorld.TagManager.GetEntity("PLAYER").GetComponent<HealthComponent>().IsAlive ||
                !entityWorld.TagManager.GetEntity("BARN").GetComponent<HealthComponent>().IsAlive)
            {
                ChickenGame chickenGame = (ChickenGame)this.game;
                chickenGame.QuitGame();
            }

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
            string delta = string.Format("Delta: {0}", gameTime.ElapsedGameTime);
#endif

            this.GraphicsDevice.Clear(Color.Black);

            this.entityWorld.Draw();
            this.spriteBatch.DrawString(this.font, fps, new Vector2(32, 32), Color.Yellow, 0, Vector2.Zero, 1, SpriteEffects.None, 1.0f);
#if DEBUG
            this.spriteBatch.DrawString(this.font, entityCount, new Vector2(32, 62), Color.Yellow, 0, Vector2.Zero, 1, SpriteEffects.None, 1.0f);
            this.spriteBatch.DrawString(this.font, removedEntityCount, new Vector2(32, 92), Color.Yellow, 0, Vector2.Zero, 1, SpriteEffects.None, 1.0f);
            this.spriteBatch.DrawString(this.font, totalEntityCount, new Vector2(32, 122), Color.Yellow, 0, Vector2.Zero, 1, SpriteEffects.None, 1.0f);
            this.spriteBatch.DrawString(this.font, delta, new Vector2(32, 152), Color.Yellow, 0, Vector2.Zero, 1, SpriteEffects.None, 1.0f);
#endif

            base.Draw(gameTime);
        }

        private void InitializeTileMap()
        {
            Entity entity = this.entityWorld.CreateEntityFromTemplate(TileMapTemplate.Name);
        }

        private void InitializeChicken()
        {
            Entity entity = this.entityWorld.CreateEntityFromTemplate(PlayerTemplate.Name);
            entity.GetComponent<TransformComponent>().X = this.GraphicsDevice.Viewport.Width * 0.5f + 100;
            entity.GetComponent<TransformComponent>().Y = this.GraphicsDevice.Viewport.Height - 50;
            entity.GetComponent<TransformComponent>().Width = 40;
            entity.GetComponent<TransformComponent>().Height = 64;
        }

        private void InitializeMosquito()
        {
            Entity entity = this.entityWorld.CreateEntityFromTemplate(MosquitoTemplate.Name);
            entity.GetComponent<TransformComponent>().X = this.GraphicsDevice.Viewport.Width * 0.5f;
            entity.GetComponent<TransformComponent>().Y = this.GraphicsDevice.Viewport.Height * 0.5f;
            //entity.GetComponent<VelocityComponent>().Speed = 0.0f;
            entity.GetComponent<TransformComponent>().Width = 30;
            entity.GetComponent<TransformComponent>().Height = 20;
        }

        private void InitializeBarn()
        {
            Entity entity = this.entityWorld.CreateEntityFromTemplate(BarnTemplate.Name);
            entity.GetComponent<TransformComponent>().X = this.GraphicsDevice.Viewport.Width * 0.5f;
            entity.GetComponent<TransformComponent>().Y = this.GraphicsDevice.Viewport.Height - 50;
            entity.GetComponent<TransformComponent>().Width = 70;
            entity.GetComponent<TransformComponent>().Height = 70;
        }
    }
}