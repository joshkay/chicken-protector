namespace ChickenProtector.Spatials
{

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using ChickenProtector.Components;

    internal static class Barn
    {
        private static Texture2D barn;

        public static void Render(SpriteBatch spriteBatch, ContentManager contentManager, TransformComponent transformComponent)
        {
            if (barn == null)
            {
                barn = contentManager.Load<Texture2D>("barn");
            }

            spriteBatch.Draw(barn, new Vector2(transformComponent.X - (barn.Width * 0.5f), transformComponent.Y - (barn.Height * 0.5f)), barn.Bounds, Color.White);
        }
    }
}
