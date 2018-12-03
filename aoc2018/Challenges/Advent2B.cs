using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using aoc2018.Util;

namespace aoc2018.Challenges
{
    class Advent2B : Challenge
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
                foreach(string j in input)
                {
                    if (Match(i, j) == (i.Length - 1))
                    {
                        Console.WriteLine(Union(i, j));
                        return;
                    }
                }
            }

            Console.WriteLine(twos * threes);
        }

        private int Match(string a, string b)
        {
            var result = 0;
            for(int i =0; i < a.Length; i++)
            {
                if (a[i] == b[i])
                {
                    result++;
                }
            }

            return result;
        }

        private string Union(string a, string b)
        {
            var result = string.Empty;

            for(int i = 0; i < a.Length; i++)
            {
                if (a[i] == b[i])
                {
                    result += a[i];
                }
            }
            return result;
        }
    }
}
