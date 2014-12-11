using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ChickenProtector.Screens
{
    class StartScreen : GameScreen
    {
        MenuComponent menuComponent;
        Texture2D image;
        Rectangle imageRectangle;

        public int SelectedIndex
        {
            get { return menuComponent.SelectedIndex; }
            set { menuComponent.SelectedIndex = value; }
        }

        public StartScreen(Game game, SpriteBatch spriteBatch, SpriteFont spriteFont, Texture2D image)
            : base(game, spriteBatch)
        {
            string[] menuItems = { "Start Game", "End Game" };
            menuComponent = new MenuComponent(game,
                spriteBatch,
                spriteFont,
                menuItems
            );
            menuComponent.Position = new Vector2(50, Game.Window.ClientBounds.Height - Game.Window.ClientBounds.Height / 4);

            Components.Add(menuComponent);

            this.image = image;
            imageRectangle = new Rectangle(
                0,
                0,
                Game.Window.ClientBounds.Width,
                Game.Window.ClientBounds.Height
            );
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            this.GraphicsDevice.Clear(Color.Black);
            spriteBatch.Draw(image, imageRectangle, image.Bounds, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.0f);
            base.Draw(gameTime);
        }
    }
}
