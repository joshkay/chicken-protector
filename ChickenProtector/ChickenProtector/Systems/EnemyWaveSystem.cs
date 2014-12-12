namespace ChickenProtector.Systems
{
    using System;
    using System.Threading;
    using System.Collections.Generic;

    using Artemis;
    using Artemis.Attributes;
    using Artemis.Manager;
    using Artemis.System;

    using Microsoft.Xna.Framework.Graphics;

    using ChickenProtector.Components;
    using ChickenProtector.Templates;
    using ChickenProtector.Helper;


    [ArtemisEntitySystem(GameLoopType = GameLoopType.Update, Layer = 1)]
    class EnemyWaveSystem : IntervalEntitySystem
    {
        private Random random;
        private static int wave;
        private int spawnAmount;
        private int spawnedCount;
        private int spawnInterval;
        private bool finished;
        private int elapsed;
        private bool overtime;

        private EnemyFactory factory;

        private SpriteBatch spriteBatch;


        public EnemyWaveSystem()
            : base(new TimeSpan(0, 0, 0, 0, 500))
        {
            factory = new EnemyFactory();
            spawnInterval = BlackBoard.GetEntry<int>("WaveInterval") - 5000;
            spawnAmount = 5;
            wave = 1;
            finished = false;
            elapsed = 0;
            overtime = false;
        }


        //
        public override void LoadContent()
        {
            this.spriteBatch = BlackBoard.GetEntry<SpriteBatch>("SpriteBatch");
            this.random = new Random();
            factory.Init(spriteBatch, this.EntityWorld);
            //spawnAmount = 5;
            Console.WriteLine("Load Content - Wave");
        }


        protected override void ProcessEntities(IDictionary<int, Entity> entities)
        {
            //
            elapsed += 500; // change this to spawn the waves faster

            if (elapsed >= BlackBoard.GetEntry<int>("WaveInterval"))
            {
                finished = false;
                elapsed = 0;
                wave++;
                Console.WriteLine("Wave " + wave);
                spawnAmount = spawnAmount + (wave-1);
                spawnInterval = (BlackBoard.GetEntry<int>("WaveInterval") - 5000) / spawnAmount;
            }


            if (wave % 3 == 0 && !finished)
            {
                factory.Spawn(true);
                finished = true;
            }

            if (finished)
                return;



            if (overtime)
            {
                //
                if (elapsed >= spawnedCount * spawnInterval && spawnedCount < spawnAmount)
                {
                    factory.Spawn();
                    spawnedCount++;
                }
            }
            else
            {
                //
                for (spawnedCount = 0; spawnedCount < spawnAmount; spawnedCount++)
                {
                    factory.Spawn();
                    // DELAY by spawnInterval / spawnAmount
                }

                finished = true;
            }
        }

        public static int GetWave()
        {
            return wave;
        }
    }
}