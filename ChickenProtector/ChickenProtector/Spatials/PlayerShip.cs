namespace ChickenProtector.Spatials
{

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ChickenProtector.Components;

    internal static class PlayerShip
    {
        private static Texture2D ship;

        public static void Render(SpriteBatch spriteBatch, ContentManager contentManager, TransformComponent transformComponent)
        {
            if (ship == null)
            {
                ship = contentManager.Load<Texture2D>("player");
            }

            spriteBatch.Draw(ship, new Vector2(transformComponent.X - (ship.Width * 0.5f), transformComponent.Y - (ship.Height * 0.5f)), ship.Bounds, Color.White);
        }

    }
}
