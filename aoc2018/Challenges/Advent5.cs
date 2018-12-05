using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using aoc2018.Util;

namespace aoc2018.Challenges
{
    class Advent5 : Challenge
    {
        public override string INPUT { get { return "C:/Dev/aoc2018/aoc2018/Input/5.txt"; } }


        public override void Execute()
        {
            A();
            B();
        }

        private void A()
        {
            CaseName = "5A";
            Start();
            var input = Input.GetInputFromFile(INPUT);

            var polymer = input[0];

            while (CanReact(polymer))
            {
                polymer = React(polymer);
            }

            Console.WriteLine(polymer.Length);

            End();
        }

        private void B()
        {
            CaseName = "5B";
            Start();
            var input = Input.GetInputFromFile(INPUT);

            var polymers = new Dictionary<char, int>();

            for (int i = 'a'; i <= 'z'; i++)
            {
                var polymer = input[0];
                var c = (Char)i;

                polymer = Regex.Match(polymer, String.Format("[{0}]+", GetReactionRegex(c))).Value;

                while (CanReact(polymer))
                {
                    polymer = React(polymer);
                }

                polymers[c] = polymer.Length;
            }

            Console.WriteLine(polymers.Min(p => p.Value));

            End();

        }

        private string React(string polymer)
        {
            foreach (var reaction in Reactions)
            {
                if (polymer.Contains(reaction))
                {
                    polymer = polymer.Replace(reaction, string.Empty);
                }
            }

            return polymer;
        }

        private bool CanReact(string polymer)
        {
            foreach (var reaction in Reactions)
            {
                if (polymer.Contains(reaction))
                {
                    return true;
                }
            }
            return false;
        }

        private bool WillReact(char a, char b)
        {
            return (a != b && Char.ToLower(a) == Char.ToLower(b));
        }

        private List<string> _reactions;

        private List<string> Reactions
        {
            get
            {
                if (_reactions == null)
                {
                    var result = new List<string>();
                    for (int i = 'a'; i <= 'z'; i++)
                    {
                        result.Add(String.Format("{0}{1}", (char)i, Char.ToUpper((char)i)));
                        result.Add(String.Format("{0}{1}", Char.ToUpper((char)i), (char)i));
                    }

                    _reactions = result;
                }


                return _reactions;
            }
        }

        private string GetReactionRegex(Char c)
        {
            var s = string.Empty;

            foreach(var r in Reactions)
            {
                if (!r.StartsWith(c))
                    s += r;
            }

            return s;
        }
    }
}

