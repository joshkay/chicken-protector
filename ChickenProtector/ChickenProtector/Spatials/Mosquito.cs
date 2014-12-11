namespace ChickenProtector.Spatials
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using ChickenProtector.Components;

    internal static class Mosquito
    {
        private static Texture2D mosquito;

        public static void Render(SpriteBatch spriteBatch, ContentManager contentManager, TransformComponent transformComponent)
        {
            if (mosquito == null)
            {
                mosquito = contentManager.Load<Texture2D>("mosquito");
            }

            spriteBatch.Draw(mosquito, new Vector2(transformComponent.X - (mosquito.Width * 0.5f), transformComponent.Y - (mosquito.Height * 0.5f)), mosquito.Bounds, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0.85f);
        }
    }
}
