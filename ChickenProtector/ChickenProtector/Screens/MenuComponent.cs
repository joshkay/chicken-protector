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
    public class MenuComponent : Microsoft.Xna.Framework.DrawableGameComponent
    {
        string[] menuItems;
        int selectedIndex;

        Color normal = Color.White;

        KeyboardState keyboardState;
        KeyboardState oldKeyboardState;

        GamePadState gamePadState;
        GamePadState oldGamePadState;

        SpriteBatch spriteBatch;
        SpriteFont spriteFont;
        
        Vector2 position;
        
        float width = 0f;
        float height = 0f;

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        public float Width
        {
            get { return width; }
        }
        public float Height
        {
            get { return height; }
        }
        public Color FontColor
        {
            get { return normal; }
            set { normal = value; }
        }

        public int SelectedIndex
        {
            get { return selectedIndex; }
            set
            {
                selectedIndex = value;
                if (selectedIndex < 0)
                    selectedIndex = 0;
                if (selectedIndex >= menuItems.Length)
                    selectedIndex = menuItems.Length - 1;
            }
        }

        public MenuComponent(Game game, SpriteBatch spriteBatch, SpriteFont spriteFont, string[] menuItems)
            : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.spriteFont = spriteFont;
            this.menuItems = menuItems;
            MeasureMenu();
        }

        private void MeasureMenu()
        {
            height = 0;
            width = 0;
            foreach (string item in menuItems)
            {
                Vector2 size = spriteFont.MeasureString(item);
                if (size.X > width)
                    width = size.X;
                height += spriteFont.LineSpacing + 5;
            }
            position = new Vector2(
            (Game.Window.ClientBounds.Width - width) / 2,
            (Game.Window.ClientBounds.Height - height) / 2);
        }

        public override void Initialize()
        {
            base.Initialize();
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

        public override void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();
            gamePadState = GamePad.GetState(PlayerIndex.One);

            if (CheckKey(Keys.Down) || CheckButton(Buttons.DPadDown))
            {
                selectedIndex++;
                if (selectedIndex == menuItems.Length)
                    selectedIndex = 0;
            }
            if (CheckKey(Keys.Up) || CheckButton(Buttons.DPadUp))
            {
                selectedIndex--;
                if (selectedIndex < 0)
                    selectedIndex = menuItems.Length - 1;
            }

            base.Update(gameTime);
            oldKeyboardState = keyboardState;
            oldGamePadState = gamePadState;
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            Vector2 location = position;
            
            float pointerSize = spriteFont.MeasureString("> ").X;
            float offset;
            string item;
            
            for (int i = 0; i < menuItems.Length; i++)
            {
                item = menuItems[i];
                if (i == selectedIndex)
                {
                    item = "> " + item;
                    offset = pointerSize;
                }
                else
                    offset = 0;

                spriteBatch.DrawString(
                    spriteFont,
                    item,
                    location - new Vector2(offset, 0),
                    normal);

                location.Y += spriteFont.LineSpacing + 5;
            }
        }
    }
}
