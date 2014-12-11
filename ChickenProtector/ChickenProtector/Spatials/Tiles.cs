namespace ChickenProtector.Spatials
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using ChickenProtector.Components;

    internal static class GrassTile
    {
        private static Texture2D tile;

        public static void Render(SpriteBatch spriteBatch, ContentManager contentManager, TransformComponent transformComponent)
        {
            if (tile == null)
            {
                tile = contentManager.Load<Texture2D>("tiles/grasstile");
            }

            spriteBatch.Draw(tile, new Vector2(transformComponent.X - (tile.Width * 0.5f), transformComponent.Y - (tile.Height * 0.5f)), tile.Bounds, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0.0f);
        }
    }

    internal static class DirtTile
    {
        private static Texture2D tile;

        public static void Render(SpriteBatch spriteBatch, ContentManager contentManager, TransformComponent transformComponent)
        {
            if (tile == null)
            {
                tile = contentManager.Load<Texture2D>("tiles/dirttile");
            }

            spriteBatch.Draw(tile, new Vector2(transformComponent.X - (tile.Width * 0.5f), transformComponent.Y - (tile.Height * 0.5f)), tile.Bounds, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0.0f);
        }
    }

    internal static class WaterTile
    {
        private static Texture2D tile;

        public static void Render(SpriteBatch spriteBatch, ContentManager contentManager, TransformComponent transformComponent)
        {
            if (tile == null)
            {
                tile = contentManager.Load<Texture2D>("tiles/watertile");
            }

            spriteBatch.Draw(tile, new Vector2(transformComponent.X - (tile.Width * 0.5f), transformComponent.Y - (tile.Height * 0.5f)), tile.Bounds, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0.0f);
        }
    }

    internal static class SnowTile
    {
        private static Texture2D tile;

        public static void Render(SpriteBatch spriteBatch, ContentManager contentManager, TransformComponent transformComponent)
        {
            if (tile == null)
            {
                tile = contentManager.Load<Texture2D>("tiles/snowtile");
            }

            spriteBatch.Draw(tile, new Vector2(transformComponent.X - (tile.Width * 0.5f), transformComponent.Y - (tile.Height * 0.5f)), tile.Bounds, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0.0f);
        }
    }
}
