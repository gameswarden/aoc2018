using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using aoc2018.Util;

namespace aoc2018.Challenges
{
    class Advent3 : Challenge
    {
        public override string INPUT { get { return "C:/Dev/aoc2018/aoc2018/Input/3.txt"; } }


        public override void Execute()
        {
            A();
            B();
        }

        private void A()
        {
            CaseName = "3A";
            Start();
            var input = Input.GetInputFromFile(INPUT);

            var claims = new HashSet<Claim>();

            //var map = new Dictionary<Point, List<Claim>>();

            var overlap = new HashSet<Point>();

            foreach (string i in input)
            {
                var c = Claim.ParseClaimString(i);

                claims.Add(c);
            }

            var overlappingClaims = new List<Claim>();

            foreach (var claim in claims)
            {
                foreach (var c in claims)
                {
                    if (c.Id == claim.Id)
                        continue;

                    if (claim.Rect.Overlap(c.Rect))
                    {
                        foreach (var p in claim.Rect.GetOverlapArea(c.Rect))
                        {
                            overlap.Add(p);
                        }
                    }
                }
            }

            Console.WriteLine(overlap.Count);
            End();
        }

        private void B()
        {
            CaseName = "3B";
            Start();
            var input = Input.GetInputFromFile(INPUT);

            var claims = new List<Claim>();

            foreach (string i in input)
            {
                var c = Claim.ParseClaimString(i);

                claims.Add(c);
            }

            foreach (var claim in claims)
            {
                var unique = true;
                foreach (var c in claims)
                {
                    if (c.Id == claim.Id)
                        continue;

                    if (claim.Rect.Overlap(c.Rect))
                    {
                        unique = false;
                        break;
                    }
                        
                }

                if (unique)
                {
                    Console.WriteLine(claim.Id);
                    End();
                    break;
                }
            }

        }
    }
}
