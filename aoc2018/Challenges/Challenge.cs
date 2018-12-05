using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace aoc2018.Challenges
{
    abstract class Challenge
    {
        public string CaseName { get; set; }
        public Stopwatch Timer { get; set; }
        public abstract string INPUT { get; }

        public void Execute()
        {
            A();
            B();
        }
        public abstract void A();
        public abstract void B();

        public void Start()
        {
            Timer = new Stopwatch();
            Timer.Start();
            Console.WriteLine("{0} starting", CaseName);
        }

        public void End()
        {
            Timer.Stop();
            Console.WriteLine("{0} execution time was {1}.", CaseName, Timer.Elapsed);
        }
    }
}
