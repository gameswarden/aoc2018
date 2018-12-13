using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using aoc2018.Util;

namespace aoc2018.Challenges
{
    class Advent12 : Challenge
    {
        public override string INPUT { get { return "C:/Dev/aoc2018/aoc2018/Input/12.txt"; } }

        public List<int> ParseInput(List<string> input)
        {
            return input.Select(int.Parse).ToList();
        }

        public override void A()
        {
            CaseName = "12A";
            Start();
            var input = Input.GetInputFromFile(INPUT);

            var pots = Regex.Match(input[0], "[#.]+").Value;
            var potOffset = 0;

            var rules = new Dictionary<string,string> ();
            var generations = new List<string>();
            var offsets = new List<int>();

            for(int i = 2; i < input.Count; i++)
            {
                var ruleMatches = Regex.Matches(input[i], "[#.]+");
                rules[ruleMatches[0].Value] = ruleMatches[1].Value;
            }

            generations.Add(pots);
            offsets.Add(potOffset);

            for (var g = 0; g < 1000; g++)
            {
                var nextGen = string.Empty;
                for (int i = -3; i <= pots.Length + 3; i++)
                {
                    if (i == -3)
                    {
                        var s = pots.Substring(0, 5 + i).PadLeft(5, '.');
                        if (rules.ContainsKey(s) && rules[s] == "#")
                        {
                            nextGen += rules[s];
                            potOffset++;
                        }
                    }
                    else if (i < 0)
                    {
                        var s = pots.Substring(0, 5 + i).PadLeft(5, '.');
                        if (rules.ContainsKey(s))
                        {
                            nextGen += rules[s];
                        }
                        else
                            nextGen += '.';
                    }
                    else if (i > pots.Length - 5 && i <= pots.Length - 1)
                    {
                        var s = pots.Substring(i, pots.Length - i).PadRight(5,'.');
                        if (rules.ContainsKey(s))
                        {
                            nextGen += rules[s];
                        }
                        else
                        {
                            nextGen += '.';
                        }
                    }
                    else if (i > pots.Length - 1)
                    {
                        var s = ".....";
                        if (rules.ContainsKey(s))
                        {
                            nextGen += rules[s];
                        }
                    }
                    else
                    {
                        var s = pots.Substring(i, 5);
                        if (rules.ContainsKey(s))
                            nextGen += rules[s];
                        else
                            nextGen += '.';
                    }
                }
                pots = nextGen;
                generations.Add(pots);
                offsets.Add(potOffset);

            }

            var lastResult = 0;
            for(int i = 0; i < generations.Count; i++)
            {
                var result = 0;
                for (int j = 0; j < generations[i].Length; j++)
                {
                    if (generations[i][j] == '#')
                        result += (j - potOffset);


                }

                Console.WriteLine((result).ToString().PadRight(5, ' ') + ' ' + (result - lastResult).ToString().PadRight(5, ' '));
                //Console.WriteLine((result).ToString().PadRight(5, ' ') + ' ' + (result - lastResult).ToString().PadRight(5, ' ') + string.Empty.PadLeft(potOffset - offsets[i], '.') + generations[i]);
                lastResult = result;
            }

            End();
        }

        public override void B()
        {
            CaseName = "12B";
            Start();

            var input = Input.GetInputFromFile(INPUT);

            var pots = Regex.Match(input[0], "[#.]+").Value;
            var potOffset = 0;

            var rules = new Dictionary<string, string>();
            var generations = new List<string>();
            var offsets = new List<int>();

            for (int i = 2; i < input.Count; i++)
            {
                var ruleMatches = Regex.Matches(input[i], "[#.]+");
                rules[ruleMatches[0].Value] = ruleMatches[1].Value;
            }

            generations.Add(pots);
            offsets.Add(potOffset);

            for (var g = 0; g < 250; g++)
            {
                var nextGen = string.Empty;
                for (int i = -3; i <= pots.Length + 3; i++)
                {
                    if (i == -3)
                    {
                        var s = pots.Substring(0, 5 + i).PadLeft(5, '.');
                        if (rules.ContainsKey(s) && rules[s] == "#")
                        {
                            nextGen += rules[s];
                            potOffset++;
                        }
                    }
                    else if (i < 0)
                    {
                        var s = pots.Substring(0, 5 + i).PadLeft(5, '.');
                        if (rules.ContainsKey(s))
                        {
                            nextGen += rules[s];
                        }
                        else
                            nextGen += '.';
                    }
                    else if (i > pots.Length - 5 && i <= pots.Length - 1)
                    {
                        var s = pots.Substring(i, pots.Length - i).PadRight(5, '.');
                        if (rules.ContainsKey(s))
                        {
                            nextGen += rules[s];
                        }
                        else
                        {
                            nextGen += '.';
                        }
                    }
                    else if (i > pots.Length - 1)
                    {
                        var s = ".....";
                        if (rules.ContainsKey(s))
                        {
                            nextGen += rules[s];
                        }
                    }
                    else
                    {
                        var s = pots.Substring(i, 5);
                        if (rules.ContainsKey(s))
                            nextGen += rules[s];
                        else
                            nextGen += '.';
                    }
                }
                pots = nextGen;
                generations.Add(pots);
                offsets.Add(potOffset);

            }

            var lastResult = 0;
            var delta = 0;
            var lastDelta = -1;
            var convergence = -1;
            for (int i = 0; i < generations.Count; i++)
            {
                var result = 0;
                for (int j = 0; j < generations[i].Length; j++)
                {
                    if (generations[i][j] == '#')
                        result += (j - offsets[i]);
                }

                //Console.WriteLine((result).ToString().PadRight(5, ' ') + ' ' + (result - lastResult).ToString().PadRight(5, ' '));
                //Console.WriteLine((result).ToString().PadRight(5, ' ') + ' ' + (result - lastResult).ToString().PadRight(5, ' ') + string.Empty.PadLeft(potOffset - offsets[i], '.') + generations[i]);
                delta = result - lastResult;
                if (delta != lastDelta)
                {
                    lastResult = result;
                    lastDelta = delta;
                    Console.WriteLine("{0}: {1} {2}", i, delta, result);
                }
                else
                {
                    lastResult = result;
                    Console.WriteLine("Convergence at {0}: {1} {2}", i, delta, result);
                    convergence = i;
                    break;
                }
            }

            var genCount = 50000000000;
            var answer = lastResult + lastDelta * (genCount - convergence);

            Console.WriteLine(answer);

            End();
        }

    }
}
