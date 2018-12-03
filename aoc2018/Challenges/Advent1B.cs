using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using aoc2018.Util;

namespace aoc2018.Challenges
{
    class Advent1B : Challenge
    {
        public override string INPUT { get { return "C:/Dev/aoc2018/aoc2018/Input/1a.txt"; } }

        public override void Execute()
        {
            var input = Input.GetInputFromFile(INPUT);

            var frequencies = new List<int>();

            var result = 0;

            frequencies.Add(result);

            while (true)
            {
                foreach (string i in input)
                {
                    result += int.Parse(i);
                    if (frequencies.Contains(result))
                    {
                        Console.WriteLine(result);
                        return;
                    }
                    frequencies.Add(result);
                }
            }
        }
    }
}
