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
    public class ChickenGame : Microsoft.Xna.Framework.Game
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
        Screens.PopUpScreen quitScreen;

        public ChickenGame()
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
            startScreen.Initialize();
            playScreen.Initialize();
            quitScreen.Initialize();
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
                Content.Load<Texture2D>("chickenbarn")
            );

            Components.Add(startScreen);
            startScreen.Hide();

            playScreen = new Screens.PlayScreen(
                this,
                spriteBatch
            );
            Components.Add(playScreen);
            playScreen.Hide();

            quitScreen = new Screens.PopUpScreen(
                this,
                spriteBatch,
                Content.Load<SpriteFont>("menufont"),
                Content.Load<Texture2D>("popup")
            );
            Components.Add(quitScreen);
            quitScreen.Hide();

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

            if (activeScreen == startScreen)
            {
                HandleStartScreen();
            }
            else if (activeScreen == playScreen)
            {
                HandlePlayScreen();
            }
            else if (activeScreen == quitScreen)
            {
                HandleQuitScreen();
            }

            base.Update(gameTime);

            oldKeyboardState = keyboardState;
            oldGamePadState = gamePadState;
        }

        private void HandleStartScreen()
        {
            if (CheckKey(Keys.Enter) || CheckButton(Buttons.A))
            {
                if (startScreen.SelectedIndex == 0)
                {
                    activeScreen.Hide();
                    activeScreen = playScreen;

                    activeScreen.Initialize();
                    activeScreen.Show();
                }
                if (startScreen.SelectedIndex == 1)
                {
                    this.Exit();
                }
            }
        }
        
        private void HandlePlayScreen()
        {
            if (CheckButton(Buttons.Back) || CheckKey(Keys.Escape))
            {
                activeScreen.Enabled = false;
                activeScreen = quitScreen;
                activeScreen.Show();
            }
        }

        private void HandleQuitScreen()
        {
            if (CheckKey(Keys.Enter) || CheckButton(Buttons.A))
            {
                if (quitScreen.SelectedIndex == 0)
                {
                    activeScreen.Hide();
                    activeScreen = playScreen;
                    activeScreen.Show();
                }
                if (quitScreen.SelectedIndex == 1)
                {
                    activeScreen.Hide();
                    activeScreen = startScreen;
                    activeScreen.Show();
                }
            }
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

            if (activeScreen == quitScreen)
                playScreen.Draw(gameTime);
            activeScreen.Draw(gameTime);
            
            spriteBatch.End();
        }
    }
}
