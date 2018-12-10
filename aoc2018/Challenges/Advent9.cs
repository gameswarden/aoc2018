using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using aoc2018.Util;

namespace aoc2018.Challenges
{
    class Advent9 : Challenge
    {
        public override string INPUT { get { return "C:/Dev/aoc2018/aoc2018/Input/9.txt"; } }

        public override void A()
        {
            CaseName = "9A";
            Start();
            var input = Input.GetInputFromFile(INPUT);
            var matches = Regex.Matches(input[0], "([\\d]+)");
            var numPlayers = int.Parse(matches[0].Value);
            var lastScore = int.Parse(matches[1].Value);
            Players = new Dictionary<int, long>();

            for (var i = 0; i < numPlayers; i++)
            {
                Players.Add(i,0);
            }

            Marbles = new List<long>();
            CurrentMarble = 0;
            CurrentPlayer = 0;

            AddMarble(0);
            for (long i = 1; i < lastScore; i++)
            {
                if (i % 23 == 0 && i != 0)
                {
                    Players[CurrentPlayer] += i;
                    RemoveMarble();
                }
                else
                    AddMarble(i);
                if (CurrentPlayer < Players.Count -1)
                    CurrentPlayer = CurrentPlayer + 1;
                else
                    CurrentPlayer = 0;
            }

            Console.WriteLine(Players.OrderByDescending(p => p.Value).First().Value);

            End();
        }

        public Dictionary<int, long> Players { get; set; }
        public List<long> Marbles { get; set; }
        public long CurrentMarble { get; set; }
        public int CurrentPlayer { get; set; }

        public void AddMarble(long marble)
        {
            int pos = 0;
            if (Marbles.Count != 0)
            {
                pos = (int)CurrentMarble + 1;
                pos %= Marbles.Count;
            }

            if (pos + 1 == Marbles.Count || Marbles.Count == 0)
                Marbles.Add(marble);
            else
                Marbles.Insert(pos + 1, marble);
            CurrentMarble = pos + 1;
        }

        public void RemoveMarble()
        {
            var pos = CurrentMarble - 7;
            CurrentMarble = Math.Abs(pos % Marbles.Count);
            Players[CurrentPlayer] += Marbles[(int)CurrentMarble];
            Marbles.RemoveAt((int)CurrentMarble);
        }

        public override void B()
        {
            CaseName = "9B";
            Start();

            var input = Input.GetInputFromFile(INPUT);
            var matches = Regex.Matches(input[0], "([\\d]+)");
            var numPlayers = int.Parse(matches[0].Value);
            var lastScore = long.Parse(matches[1].Value);
            Players = new Dictionary<int, long>();

            for (var i = 0; i < numPlayers; i++)
            {
                Players.Add(i, 0);
            }

            Marbles = new List<long>();
            CurrentMarble = 0;
            CurrentPlayer = 0;

            AddMarble(0);
            for (int i = 1; i < lastScore * 100; i++)
            {
                if (i % 100000 == 0)
                    Console.WriteLine(i);

                if (i % 23 == 0 && i != 0)
                {
                    Players[CurrentPlayer] += i;
                    RemoveMarble();
                }
                else
                    AddMarble(i);
                if (CurrentPlayer < Players.Count - 1)
                    CurrentPlayer = CurrentPlayer + 1;
                else
                    CurrentPlayer = 0;
            }

            Console.WriteLine(Players.OrderByDescending(p => p.Value).First().Value);

            End();

        }

    }
}
