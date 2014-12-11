namespace ChickenProtector.Spatials
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using ChickenProtector.Components;

    using Microsoft.Xna.Framework.Audio;

    internal static class PoopSound
    {
        private static SoundEffect poop;

        public static void PlaySound(ContentManager contentManager)
        {
            if (poop == null)
            {
                poop = contentManager.Load<SoundEffect>("sound/poop");
            }

            poop.Play(0.01f, 0, 0);
        }
    }
}
