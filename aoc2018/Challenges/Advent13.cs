using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using aoc2018.Util;

namespace aoc2018.Challenges
{
    class Advent13 : Challenge
    {
        public override string INPUT { get { return "C:/Dev/aoc2018/aoc2018/Input/12.txt"; } }

        public List<int> ParseInput(List<string> input)
        {
            return input.Select(int.Parse).ToList();
        }

        public override void A()
        {
            CaseName = "13A";
            Start();
            var input = Input.GetInputFromFile(INPUT);

            End();
        }

        public override void B()
        {
            CaseName = "13B";
            Start();

            var input = Input.GetInputFromFile(INPUT);

            End();
        }

    }
}
