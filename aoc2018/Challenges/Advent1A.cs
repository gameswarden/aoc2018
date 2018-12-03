using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using aoc2018.Util;

namespace aoc2018.Challenges
{
    class Advent1A : Challenge
    {
        public override string INPUT { get { return "C:/Dev/aoc2018/aoc2018/Input/1a.txt"; } }

        public override void Execute()
        {
            var input = Input.GetInputFromFile(INPUT);

            int result = 0;

            foreach(string i in input)
            {
                result += int.Parse(i);
            }

            Console.WriteLine(result);
        }
    }
}
