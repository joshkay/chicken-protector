namespace ChickenProtector.Spatials
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using ChickenProtector.Components;

    internal static class Spider
    {
        private static Texture2D spider;

        public static void Render(SpriteBatch spriteBatch, ContentManager contentManager, TransformComponent transformComponent)
        {
            if (spider == null)
            {
                spider = contentManager.Load<Texture2D>("spider");
            }

            spriteBatch.Draw(spider, new Vector2(transformComponent.X - (spider.Width * 0.5f), transformComponent.Y - (spider.Height * 0.5f)), spider.Bounds, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0.8f);
        }
    }
}