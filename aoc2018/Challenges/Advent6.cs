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

            Coordinates = ParseInput(input).ToHashSet();

            FiniteRegions = new Dictionary<Point, HashSet<Point>>();
            InfiniteRegions = new HashSet<Point>();

            foreach(var c in Coordinates)
            {
                CheckRegion(c, c, null);
            }

            var biggestArea = FiniteRegions.OrderByDescending(a => a.Value.Count()).First();
            Console.WriteLine("{0}: {1}", input.IndexOf(string.Format("{0}, {1}",biggestArea.Key.x, biggestArea.Key.y)), biggestArea.Value.Count());
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

        private HashSet<Point> InfiniteRegions { get; set; }
        private Dictionary<Point, HashSet<Point>> FiniteRegions { get; set; }

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

        public bool CheckRegion(Point origin, Point p, char? direction)
        {
            if (p.x == 0 || p.y == 0 || p.x == Coordinates.Max(c => c.x) || p.y == Coordinates.Max(c => c.y))
            {
                if (FiniteRegions.ContainsKey(origin))
                    FiniteRegions.Remove(origin);
                return false;
            }

            var originDist = Math.Abs(Vector.Dist(origin.x, p.x)) + Math.Abs(Vector.Dist(origin.y, p.y));
            var nearestPoint = Coordinates.Where(c => c.x != origin.x && c.y != origin.y)
                .OrderBy(c => Math.Abs(Vector.Dist(c.x, p.x)) + Math.Abs(Vector.Dist(c.y, p.y))).First();
            var dist = Math.Abs(Vector.Dist(nearestPoint.x, p.x)) + Math.Abs(Vector.Dist(nearestPoint.y, p.y));

            if (originDist < dist)
            {
                if (!FiniteRegions.ContainsKey(origin))
                    FiniteRegions[origin] = new HashSet<Point>();

                FiniteRegions[origin].Add(p);

                if (direction == 'l')
                {
                    if (!CheckRegion(origin, new Point(p.x, p.y - 1), 'u'))
                        return false;
                    if (!CheckRegion(origin, new Point(p.x, p.y + 1), 'd'))
                        return false;
                    if (!CheckRegion(origin, new Point(p.x - 1, p.y), 'l'))
                        return false;
                }
                else if (direction == 'r')
                {
                    if (!CheckRegion(origin, new Point(p.x, p.y - 1), 'u'))
                        return false;
                    if (!CheckRegion(origin, new Point(p.x, p.y + 1), 'd'))
                        return false;
                    if (!CheckRegion(origin, new Point(p.x + 1, p.y), 'r'))
                        return false;
                }
                else if (direction == 'u')
                {
                    if (!CheckRegion(origin, new Point(p.x, p.y - 1), 'u'))
                        return false;
                }
                else if (direction == 'd')
                {
                    if (!CheckRegion(origin, new Point(p.x, p.y + 1), 'd'))
                        return false;
                }
                else
                {
                    if (!CheckRegion(origin, new Point(p.x - 1, p.y), 'l'))
                        return false;
                    if (!CheckRegion(origin, new Point(p.x + 1, p.y), 'r'))
                        return false;
                    if (!CheckRegion(origin, new Point(p.x, p.y - 1), 'u'))
                        return false;
                    if (!CheckRegion(origin, new Point(p.x, p.y + 1), 'd'))
                        return false;
                }
            }
            return true;
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

