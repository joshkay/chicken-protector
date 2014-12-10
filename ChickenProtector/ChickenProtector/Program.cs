using System;

#if WINDOWS || XBOX
namespace ChickenProtector
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (GameStates game = new GameStates())
            {
                game.Run();
            }
        }
    }
}
#endif