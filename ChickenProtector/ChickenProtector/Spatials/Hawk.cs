namespace ChickenProtector.Spatials
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using ChickenProtector.Components;

    internal static class Hawk
    {
        private static Texture2D hawk;

        public static void Render(SpriteBatch spriteBatch, ContentManager contentManager, TransformComponent transformComponent)
        {
            if (hawk == null)
            {
                hawk = contentManager.Load<Texture2D>("hawk");
            }

            spriteBatch.Draw(hawk, new Vector2(transformComponent.X - (hawk.Width * 0.5f), transformComponent.Y - (hawk.Height * 0.5f)), hawk.Bounds, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0.8f);
        }
    }
}