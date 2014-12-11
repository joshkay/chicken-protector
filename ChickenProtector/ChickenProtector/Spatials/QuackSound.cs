namespace ChickenProtector.Spatials
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using ChickenProtector.Components;

    using Microsoft.Xna.Framework.Audio;

    internal static class QuackSound
    {
        private static SoundEffect quack;

        public static void PlaySound(ContentManager contentManager)
        {
            if (quack == null)
            {
                quack = contentManager.Load<SoundEffect>("sound/quack");
            }

            quack.Play(0.01f, 0, 0);
        }
    }
}
