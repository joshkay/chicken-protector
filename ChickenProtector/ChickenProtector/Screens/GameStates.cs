using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace ChickenProtector
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class GameStates : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        KeyboardState keyboardState;
        KeyboardState oldKeyboardState;

        GamePadState gamePadState;
        GamePadState oldGamePadState;

        Screens.GameScreen activeScreen;
        Screens.StartScreen startScreen;
        Screens.PlayScreen playScreen;

        public GameStates()
        {
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

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
            playScreen.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            startScreen = new Screens.StartScreen(
                this,
                spriteBatch,
                Content.Load<SpriteFont>("menuFont"),
                Content.Load<Texture2D>("alienmetal")
            );

            Components.Add(startScreen);
            startScreen.Hide();

            playScreen = new Screens.PlayScreen(
                this,
                spriteBatch,
                Content.Load<Texture2D>("greenmetal")
            );
            Components.Add(playScreen);
            playScreen.Hide();

            activeScreen = startScreen;
            activeScreen.Show();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();
            gamePadState = GamePad.GetState(PlayerIndex.One);
            if (CheckButton(Buttons.Back))
                this.Exit();

            if (activeScreen == startScreen)
            {
                if (CheckKey(Keys.Enter) || CheckButton(Buttons.A))
                {
                    if (startScreen.SelectedIndex == 0)
                    {
                        activeScreen.Hide();
                        activeScreen = playScreen;
                        activeScreen.Show();
                    }
                    if (startScreen.SelectedIndex == 1)
                    {
                        this.Exit();
                    }
                }
            }
            base.Update(gameTime);

            oldKeyboardState = keyboardState;
            oldGamePadState = gamePadState;
        }

        private bool CheckKey(Keys theKey)
        {
            return keyboardState.IsKeyUp(theKey) &&
                oldKeyboardState.IsKeyDown(theKey);
        }

        private bool CheckButton(Buttons button)
        {
            return gamePadState.IsButtonUp(button) &&
                oldGamePadState.IsButtonDown(button);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            base.Draw(gameTime);

            spriteBatch.End();
        }
    }
}
