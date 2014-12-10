namespace ChickenProtector
{
    #region Using statements

    using System;

    using Artemis;
    using Artemis.System;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;

    using ChickenProtector.Components;
    //using ChickenProtector.Templates;

    #endregion

    /// <summary>This is the main type for Star Warrior.</summary>
    public class ChickenProtectorGame : Game
    {
        /// <summary>The one second.</summary>
        private static readonly TimeSpan OneSecond = TimeSpan.FromSeconds(1);

        /// <summary>The graphics.</summary>
        private readonly GraphicsDeviceManager graphics;

        /// <summary>The elapsed time.</summary>
        private TimeSpan elapsedTime;

        /// <summary>The font.</summary>
        private SpriteFont font;

        /// <summary>The frame counter.</summary>
        private int frameCounter;

        /// <summary>The frame rate.</summary>
        private int frameRate;

        /// <summary>The sprite batch.</summary>
        private SpriteBatch spriteBatch;

        /// <summary>The entityWorld.</summary>
        private EntityWorld entityWorld;

        /// <summary>Initializes a new instance of the <see cref="StarWarriorGame" /> class.</summary>
        public ChickenProtectorGame()
        {
            this.elapsedTime = TimeSpan.Zero;

            this.graphics = new GraphicsDeviceManager(this)
            {
                IsFullScreen = false,
                PreferredBackBufferHeight = 720,
                PreferredBackBufferWidth = 1280,
                PreferredBackBufferFormat = SurfaceFormat.Color,
                PreferMultiSampling = false,
                PreferredDepthStencilFormat = DepthFormat.None
            };
#if DEBUG
            this.graphics.SynchronizeWithVerticalRetrace = false;
#else
            this.graphics.SynchronizeWithVerticalRetrace = true;
#endif
            this.IsFixedTimeStep = false; //this wont work when vsync is ON
            this.Content.RootDirectory = "Content";
        }

        /// <summary>This is called when the game should draw itself.</summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            string fps = string.Format("fps: {0}", this.frameRate);
#if DEBUG
            string entityCount = string.Format("Active entities: {0}", this.entityWorld.EntityManager.ActiveEntities.Count);
            string removedEntityCount = string.Format("Removed entities: {0}", this.entityWorld.EntityManager.TotalRemoved);
            string totalEntityCount = string.Format("Total entities: {0}", this.entityWorld.EntityManager.TotalCreated);
#endif

            this.GraphicsDevice.Clear(Color.Black);
            this.spriteBatch.Begin();
            this.entityWorld.Draw();
            this.spriteBatch.DrawString(this.font, fps, new Vector2(32, 32), Color.Yellow);
#if DEBUG
            this.spriteBatch.DrawString(this.font, entityCount, new Vector2(32, 62), Color.Yellow);
            this.spriteBatch.DrawString(this.font, removedEntityCount, new Vector2(32, 92), Color.Yellow);
            this.spriteBatch.DrawString(this.font, totalEntityCount, new Vector2(32, 122), Color.Yellow);
#endif
            this.spriteBatch.End();
        }

        /// <summary>Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.</summary>
        protected override void Initialize()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            this.spriteBatch = new SpriteBatch(this.GraphicsDevice);
            this.font = this.Content.Load<SpriteFont>("myFont");

            this.entityWorld = new EntityWorld();

            EntitySystem.BlackBoard.SetEntry("ContentManager", this.Content);
            EntitySystem.BlackBoard.SetEntry("GraphicsDevice", this.GraphicsDevice);
            EntitySystem.BlackBoard.SetEntry("SpriteBatch", this.spriteBatch);
            EntitySystem.BlackBoard.SetEntry("SpriteFont", this.font);
            EntitySystem.BlackBoard.SetEntry("EnemyInterval", 500);

#if XBOX
            this.entityWorld.InitializeAll( System.Reflection.Assembly.GetExecutingAssembly());
#else
            this.entityWorld.InitializeAll(true);
#endif
            this.InitializePlayerShip();
            base.Initialize();
        }

        /// <summary>Allows the game to run logic such as updating the entityWorld,
        /// checking for collisions, gathering input, and playing audio.</summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape) ||
                GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.Back))
            {
                this.Exit();
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
        private void InitializePlayerShip()
        {
            Entity entity = this.entityWorld.CreateEntity();
            entity.Group = "SHIPS";

            entity.AddComponentFromPool<TransformComponent>();
            entity.AddComponent(new SpatialFormComponent("PlayerShip"));
            entity.AddComponent(new HealthComponent(30));

            entity.GetComponent<TransformComponent>().X = this.GraphicsDevice.Viewport.Width * 0.5f;
            entity.GetComponent<TransformComponent>().Y = this.GraphicsDevice.Viewport.Height - 50;
            entity.Tag = "PLAYER";
        }
    }
}