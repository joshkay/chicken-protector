namespace ChickenProtector.Spatials
{

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using ChickenProtector.Components;

    internal static class Egg
    {
        private static Texture2D egg;

        public static void Render(SpriteBatch spriteBatch, ContentManager contentManager, TransformComponent transformComponent)
        {
            if (egg == null)
            {
                egg = contentManager.Load<Texture2D>("egg");
            }

            spriteBatch.Draw(egg, new Vector2(transformComponent.X - (egg.Width * 0.5f), transformComponent.Y - (egg.Height * 0.5f)), egg.Bounds, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0.8f);
        }
    }
}
