using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using aoc2018.Util;

namespace aoc2018.Challenges
{
    class Advent2A : Challenge
    {
        public override string INPUT { get { return "C:/Dev/aoc2018/aoc2018/Input/2.txt"; } }

        public override void Execute()
        {
            var input = Input.GetInputFromFile(INPUT);

            var twos = 0;
            var threes = 0;

            int result = 0;

            foreach(string i in input)
            {
                var frequencies = new Dictionary<char, int>();

                foreach (char c in i)
                {
                    if (!frequencies.ContainsKey(c))
                        frequencies.Add(c, 0);

                    frequencies[c]++;
                }
                if (frequencies.ContainsValue(2))
                    twos++;
                if (frequencies.ContainsValue(3))
                    threes++;
            }

            Console.WriteLine(twos * threes);
        }
    }
}
