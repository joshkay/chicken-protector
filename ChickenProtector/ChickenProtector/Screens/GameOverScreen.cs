using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ChickenProtector.Screens
{
    class GameOverScreen : GameScreen
    {
        MenuComponent menuComponent;
        Texture2D image;
        Rectangle imageRectangle;

        public int SelectedIndex
        {
            get { return menuComponent.SelectedIndex; }
            set { menuComponent.SelectedIndex = value; }
        }

        public GameOverScreen(Game game, SpriteBatch spriteBatch, SpriteFont spriteFont, Texture2D image)
            : base(game, spriteBatch)
        {
            string[] menuItems = { "Back to Menu" };
            menuComponent = new MenuComponent(game,
                spriteBatch,
                spriteFont,
                menuItems
            );
            Components.Add(menuComponent);

            this.image = image;
            imageRectangle = new Rectangle(
                (Game.Window.ClientBounds.Width - this.image.Width) / 2,
                (Game.Window.ClientBounds.Height - this.image.Height) / 2,
                this.image.Width,
                this.image.Height
            );
            menuComponent.Position = new Vector2(
                imageRectangle.X + imageRectangle.Width / 2 - menuComponent.Width / 2,
                imageRectangle.Y + imageRectangle.Height / 2 - menuComponent.Height / 2
            );
            menuComponent.FontColor = Color.Black;
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
            spriteBatch.Draw(image, imageRectangle, image.Bounds, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.95f);
            base.Draw(gameTime);
        }
    }
}
