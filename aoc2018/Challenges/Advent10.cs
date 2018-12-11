using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using aoc2018.Util;

namespace aoc2018.Challenges
{
    class Advent10 : Challenge
    {
        public override string INPUT { get { return "C:/Dev/aoc2018/aoc2018/Input/10.txt"; } }

        public override void A()
        {
            CaseName = "10A";
            Start();
            var input = Input.GetInputFromFile(INPUT);

            var points = new List<MovingPoint>();

            foreach(var i in input)
            {
                var matches = Regex.Matches(i, "[-\\d]+");
                var point = new MovingPoint { Position = new Point(int.Parse(matches[0].Value), int.Parse(matches[1].Value)), Velocity = new Point(int.Parse(matches[2].Value), int.Parse(matches[3].Value)) };
                points.Add(point);
            }

            var minDiff = points.Count;

            var minX = 0;
            var maxX = 0;
            var minY = 0;
            var maxY = 0;

            var lastDiff = int.MaxValue;
            var diff = int.MaxValue;
            while (diff <= lastDiff)
            {
                lastDiff = diff;

                foreach (var p in points)
                {
                    p.Position = new Point(p.Position.x + p.Velocity.x, p.Position.y + p.Velocity.y);
                }

                minX = points.OrderBy(p => p.Position.x).First().Position.x;
                maxX = points.OrderBy(p => p.Position.x).Last().Position.x;
                minY = points.OrderBy(p => p.Position.y).First().Position.y;
                maxY = points.OrderBy(p => p.Position.y).Last().Position.y;

                var diffX = maxX - minX;
                var diffY = maxY - minY;

                diff = diffX + diffY;

                if (diff < 150)
                {
                    for (int y = minY; y <= maxY; y++)
                    {
                        var s = string.Empty;
                        for (int x = minX; x <= maxX; x++)
                        {
                            if (points.Any(p => p.Position.x == x && p.Position.y == y))
                                s += '#';
                            else
                                s += '.';

                        }
                        Console.WriteLine(s);
                    }
                }
            }

            minX = points.OrderBy(p => p.Position.x).First().Position.x;
            maxX = points.OrderBy(p => p.Position.x).Last().Position.x;
            minY = points.OrderBy(p => p.Position.y).First().Position.y;
            maxY = points.OrderBy(p => p.Position.y).Last().Position.y;

            for (int y = minY; y <= maxY; y++)
            {
                var s = string.Empty;
                for (int x = minX; x <= maxX; x++)
                {
                    if (points.Any(p => p.Position.x == x && p.Position.y == y))
                        s += '#';
                    else
                        s += '.';

                }
                Console.WriteLine(s);
            }

            End();
        }

        public Dictionary<int, long> Players { get; set; }
        public int CurrentPlayer { get; set; }

        public override void B()
        {
            CaseName = "10B";
            Start();

            var input = Input.GetInputFromFile(INPUT);

            var points = new List<MovingPoint>();

            foreach (var i in input)
            {
                var matches = Regex.Matches(i, "[-\\d]+");
                var point = new MovingPoint { Position = new Point(int.Parse(matches[0].Value), int.Parse(matches[1].Value)), Velocity = new Point(int.Parse(matches[2].Value), int.Parse(matches[3].Value)) };
                points.Add(point);
            }

            var minDiff = points.Count;

            var minX = 0;
            var maxX = 0;
            var minY = 0;
            var maxY = 0;

            var lastDiff = int.MaxValue;
            var diff = int.MaxValue;

            var count = 0;
            while (diff <= lastDiff)
            {
                count++;
                lastDiff = diff;

                foreach (var p in points)
                {
                    p.Position = new Point(p.Position.x + p.Velocity.x, p.Position.y + p.Velocity.y);
                }

                minX = points.OrderBy(p => p.Position.x).First().Position.x;
                maxX = points.OrderBy(p => p.Position.x).Last().Position.x;
                minY = points.OrderBy(p => p.Position.y).First().Position.y;
                maxY = points.OrderBy(p => p.Position.y).Last().Position.y;

                var diffX = maxX - minX;
                var diffY = maxY - minY;

                diff = diffX + diffY;

                if (diff < 150)
                {
                    Console.WriteLine();
                    Console.WriteLine(count);
                    for (int y = minY; y <= maxY; y++)
                    {
                        var s = string.Empty;
                        for (int x = minX; x <= maxX; x++)
                        {
                            if (points.Any(p => p.Position.x == x && p.Position.y == y))
                                s += '#';
                            else
                                s += '.';

                        }
                        Console.WriteLine(s);
                    }
                }
            }

            minX = points.OrderBy(p => p.Position.x).First().Position.x;
            maxX = points.OrderBy(p => p.Position.x).Last().Position.x;
            minY = points.OrderBy(p => p.Position.y).First().Position.y;
            maxY = points.OrderBy(p => p.Position.y).Last().Position.y;

            for (int y = minY; y <= maxY; y++)
            {
                var s = string.Empty;
                for (int x = minX; x <= maxX; x++)
                {
                    if (points.Any(p => p.Position.x == x && p.Position.y == y))
                        s += '#';
                    else
                        s += '.';

                }
                Console.WriteLine(s);
            }

            End();

        }
    }

    public class MovingPoint
    {
        public Point Position { get; set; }
        public Point Velocity { get; set; }
    }
}
