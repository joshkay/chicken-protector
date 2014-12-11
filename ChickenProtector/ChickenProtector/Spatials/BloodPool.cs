namespace ChickenProtector.Spatials
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using ChickenProtector.Components;

    internal static class BloodPool
    {
        private static Texture2D pool;

        public static void Render(SpriteBatch spriteBatch, ContentManager contentManager, TransformComponent transformComponent)
        {
            if (pool == null)
            {
                pool = contentManager.Load<Texture2D>("blood");
            }

            spriteBatch.Draw(pool, new Vector2(transformComponent.X - (pool.Width * 0.5f), transformComponent.Y - (pool.Height * 0.5f)), pool.Bounds, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0.55f);
        }
    }
}
