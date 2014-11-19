namespace ChickenProtector
{
    #region Using statements

    using System;

    using Artemis;
    using Artemis.System;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;

    //using ChickenProtector.Components;
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
            
        }

        /// <summary>This is called when the game should draw itself.</summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
           
        }

        /// <summary>Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.</summary>
        protected override void Initialize()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            //this.spriteBatch = new SpriteBatch(this.GraphicsDevice);
            //this.font = this.Content.Load<SpriteFont>("myFont");

            base.Initialize();
        }

        /// <summary>Allows the game to run logic such as updating the entityWorld,
        /// checking for collisions, gathering input, and playing audio.</summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
        }      
    }
}