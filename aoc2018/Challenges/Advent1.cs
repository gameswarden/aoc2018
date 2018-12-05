using System;
using System.Collections.Generic;
using System.Linq;
using aoc2018.Util;

namespace aoc2018.Challenges
{
    class Advent1 : Challenge
    {
        public override string INPUT { get { return "C:/Dev/aoc2018/aoc2018/Input/1a.txt"; } }

        public override void Execute()
        {
            A();
            B();
        }

        public List<int> ParseInput(List<string> input)
        {
            return input.Select(int.Parse).ToList();
        }

        public void A()
        {
            CaseName = "1A";
            Start();
            var input = Input.GetInputFromFile(INPUT);

            int result = 0;

            foreach (string i in input)
            {
                result += int.Parse(i);
            }
            Console.WriteLine(result);
            End();
        }

        public void B()
        {
            CaseName = "1B";
            Start();

            var input = Input.GetInputFromFile(INPUT).Select(int.Parse);

            var frequencies = new HashSet<int>();

            var result = 0;

            frequencies.Add(result);

            var x = 0;
            while (true)
            {
                x++;
                foreach (int i in input)
                {
                    result += i;
                    if (frequencies.Contains(result))
                    {
                        Console.WriteLine(result);
                        Console.WriteLine(x);
                        End();
                        return;
                    }
                    frequencies.Add(result);
                }
            }
        }
    }
}
