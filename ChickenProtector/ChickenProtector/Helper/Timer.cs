using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChickenProtector.Helper
{
    class Timer
    {
        private float time;
        private float length;
        private bool completed;

        public bool Running { get; set; }
        public bool Completed { get { return completed; } }

        public Timer(float length)
        {
            this.length = length;
            time = 0;
            Running = false;
            completed = true;
        }

        public void update(float milliseconds)
        {
            if (Running && !Completed)
            {
                time += milliseconds;

                if (time >= length)
                {
                    completed = true;
                }
            }
        }

        public void reset()
        {
            time = 0;
            completed = false;
        }
    }
}
