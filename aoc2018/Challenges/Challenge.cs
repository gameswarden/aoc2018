using System;
using System.Collections.Generic;
using System.Text;

namespace aoc2018.Challenges
{
    abstract class Challenge
    {
        public abstract string INPUT { get; }

        public abstract void Execute();
    }
}
