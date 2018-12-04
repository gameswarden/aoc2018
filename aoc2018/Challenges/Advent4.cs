using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using aoc2018.Util;

namespace aoc2018.Challenges
{
    class Advent4 : Challenge
    {
        public override string INPUT { get { return "C:/Dev/aoc2018/aoc2018/Input/3.txt"; } }


        public override void Execute()
        {
            A();
            B();
        }

        private void A()
        {
            CaseName = "4A";
            Start();
            var input = Input.GetInputFromFile(INPUT);


            End();
        }

        private void B()
        {
            CaseName = "4B";
            Start();
            var input = Input.GetInputFromFile(INPUT);

            End();

        }
    }
}
