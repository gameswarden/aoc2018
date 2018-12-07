using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using aoc2018.Util;

namespace aoc2018.Challenges
{
    class Advent6 : Challenge
    {
        public override string INPUT { get { return "C:/Dev/aoc2018/aoc2018/Input/6.txt"; } }

        public override void A()
        {
            CaseName = "6A";
            Start();
            var input = Input.GetInputFromFile(INPUT);

            var coords = ParseInput(input);

            var map = new Dictionary<Point, int>();

            var maxX = coords.Max(c => c.x);
            var maxY = coords.Max(c => c.y);

            for (var y = 0; y < maxY + 1; y++)
            {
                for (var x = 0; x < maxX + 1; x++)
                {
                    var p = new Point(x, y);
                    var minDist = -1;
                    var coordId = -1;
                    for (int i = 0; i < coords.Count; i++)
                    {
                        var c = coords[i];
                        var distX = Vector.Dist(c.x, p.x);
                        var distY = Vector.Dist(c.y, p.y);
                        var dist = Math.Abs(distX) + Math.Abs(distY);
                        if ((dist) == 0)
                        {
                            minDist = 0;
                            coordId = i;
                        }
                        else if (minDist == -1 || minDist > dist)
                        {
                            minDist = dist;
                            coordId = i;
                        }
                        else if (minDist == dist)
                        {
                            coordId = -1;
                        }
                    }

                    map[p] = coordId;
                }
            }

            var infiniteAreas =
                map.Where(c => (c.Key.x == 0 || c.Key.y == 0 || c.Key.x == maxX || c.Key.y == maxY) && c.Value != -1).Select(c => c.Value).Distinct();

            var finiteAreaIds = new List<int>();

            for (int i = 0; i < input.Count; i++)
            {
                if (!infiniteAreas.Contains(i))
                    finiteAreaIds.Add(i);
            }

            var finiteAreas = map.Where(c => finiteAreaIds.Contains(c.Value)).ToDictionary(c => c.Key, c => c.Value);

            //DrawMap(coords, finiteAreas);

            var biggestArea = finiteAreas.GroupBy(a => a.Value).OrderByDescending(a => a.Count()).First();
            Console.WriteLine("{0}: {1}", biggestArea.Key, biggestArea.Count());

            End();
        }

        public override void B()
        {
            CaseName = "6B";
            Start();
            var input = Input.GetInputFromFile(INPUT);

            Coordinates = ParseInput(input).ToHashSet();
            WithinDistance = new HashSet<Point>();

            var region = new HashSet<Point>();

            var avg = new Point((int) Coordinates.Average(c => c.x), (int) Coordinates.Average(c => c.y));

            CheckDistance(avg, null);

            Console.WriteLine(WithinDistance.Count);

            End();

        }

        public List<Point> ParseInput(List<string> input)
        {
            var result = new List<Point>();
            foreach (var i in input)
            {
                var coords = Regex.Matches(i, "([\\d]+)");
                var p = new Point(int.Parse(coords[0].Value),int.Parse(coords[1].Value));
                result.Add(p);
            }

            return result;
        }

        private HashSet<Point> WithinDistance { get; set; }

        private HashSet<Point> Coordinates { get; set; }

        private static void DrawMap(List<Point> coords, Dictionary<Point, int> finiteAreas)
        {
            var finiteMinX = finiteAreas.Min(c => c.Key.x);
            var finiteMinY = finiteAreas.Min(c => c.Key.y);
            var finiteMaxX = finiteAreas.Max(c => c.Key.x);
            var finiteMaxY = finiteAreas.Max(c => c.Key.y);

            for (int y = finiteMinY; y <= finiteMaxY; y++)
            {
                var s = string.Empty;
                for (int x = finiteMinX; x <= finiteMaxX; x++)
                {
                    var p = new Point(x, y);

                    if (coords.Contains(p))
                        s += ' ';
                    else if (!finiteAreas.ContainsKey(p))
                        s += '.';
                    else if (finiteAreas[p] == -1)
                        s += '|';
                    else
                    {
                        s += (char)(finiteAreas[p] + 33);
                    }
                }

                Console.WriteLine(s);
            }
        }

        public void CheckDistance(Point p, char? direction)
        {
            var withinDistance = Coordinates.Sum(c => Math.Abs(Vector.Dist(c.x, p.x)) + Math.Abs(Vector.Dist(c.y, p.y))) < 10000;

            if (withinDistance)
            {
                WithinDistance.Add(p);

                if (direction == 'l')
                {
                    CheckDistance(new Point(p.x, p.y - 1), 'u');
                    CheckDistance(new Point(p.x, p.y + 1), 'd');
                    CheckDistance(new Point(p.x-1, p.y), 'l');
                }
                else if (direction == 'r')
                {
                    CheckDistance(new Point(p.x, p.y - 1), 'u');
                    CheckDistance(new Point(p.x, p.y + 1), 'd');
                    CheckDistance(new Point(p.x+1, p.y), 'r');
                }
                else if (direction == 'u')
                {
                    CheckDistance(new Point(p.x, p.y-1), 'u');
                }
                else if (direction == 'd')
                {
                    CheckDistance(new Point(p.x, p.y+1), 'd');
                }
                else
                {
                    CheckDistance(new Point(p.x - 1, p.y), 'l');
                    CheckDistance(new Point(p.x + 1, p.y), 'r');
                    CheckDistance(new Point(p.x, p.y - 1), 'u');
                    CheckDistance(new Point(p.x, p.y + 1), 'd');
                }
            }
        }
    }
}

