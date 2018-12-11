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

            CurrentPlayer = 0;

            Add(0);
            for (int i = 1; i < lastScore; i++)
            {
                if (i % 23 == 0 && i != 0)
                {
                    Players[CurrentPlayer] += i;
                    Remove();
                }
                else
                    Add(i);
                if (CurrentPlayer < Players.Count -1)
                    CurrentPlayer = CurrentPlayer + 1;
                else
                    CurrentPlayer = 0;
            }

            Console.WriteLine(Players.OrderByDescending(p => p.Value).First().Value);

            End();
        }

        public Dictionary<int, long> Players { get; set; }
        public int CurrentPlayer { get; set; }

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

            CurrentPlayer = 0;

            Add(0);
            for (int i = 1; i < lastScore * 100; i++)
            {
                if (i % 23 == 0 && i != 0)
                {
                    Players[CurrentPlayer] += i;
                    Remove();
                }
                else
                    Add(i);
                if (CurrentPlayer < Players.Count - 1)
                    CurrentPlayer = CurrentPlayer + 1;
                else
                    CurrentPlayer = 0;
            }

            Console.WriteLine(Players.OrderByDescending(p => p.Value).First().Value);

            End();

        }

        public void Add(int i)
        {
            if (i > 0)
            {
                LastMarble = LastMarble.Clockwise(1);
                var newMarble = new Marble { Id = i };
                newMarble.Right = LastMarble.Right;
                newMarble.Left = LastMarble;
                newMarble.Left.Right = newMarble;
                newMarble.Right.Left = newMarble;
                LastMarble = newMarble;
            }
            else
            {
                LastMarble = new Marble {Id = 0};
                LastMarble.Left = LastMarble;
                LastMarble.Right = LastMarble;
            }
        }

        public void Remove()
        {
            LastMarble = LastMarble.CounterClockwise(7);
            LastMarble.Left.Right = LastMarble.Right;
            LastMarble.Right.Left = LastMarble.Left;
            Players[CurrentPlayer] += LastMarble.Id;
            LastMarble = LastMarble.Right;
        }

        public Marble LastMarble { get; set; }
    }

    public class Marble
    {
        public int Id { get; set; }
        public Marble Left { get; set; }
        public Marble Right { get; set; }

        public Marble Clockwise(int i)
        {
            return i == 0 ? this : Right.Clockwise(i - 1);
        }

        public Marble CounterClockwise(int i)
        {
            return i == 0 ? this : Left.CounterClockwise(i - 1);
        }
    }
}
