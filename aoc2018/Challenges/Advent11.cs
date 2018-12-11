using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;
using aoc2018.Util;

namespace aoc2018.Challenges
{
    class Advent11 : Challenge
    {
        public override string INPUT { get { return "2187"; } }

        public override void A()
        {
            CaseName = "11A";
            Start();

            var serial = int.Parse(INPUT);

            var fuelCells = new Dictionary<Point, int>();

            for (var y = 0; y < 300; y++)
            {
                for (var x = 0; x < 300; x++)
                {
                    var rackId = x + 10;
                    var powerLevelStart = rackId * y;
                    var powerLevel = ((((powerLevelStart + serial) * rackId) % 1000) / 100) - 5;
                    fuelCells[new Point(x, y)] = powerLevel;
                }
            }

            var maxSquare = new Point();
            var maxValue = 0;
            for (var y = 0; y < 300 - 2; y++)
            {

                for (var x = 0; x < 300 - 2; x++)
                {
                    var squareValue = 0;
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            squareValue += fuelCells[new Point(x + j, y + i)];
                        }
                    }

                    if (squareValue > maxValue)
                    {
                        maxSquare = new Point(x, y);
                        maxValue = squareValue;
                    }

                }
            }

            Console.WriteLine("Square starting at ({0},{1}) has the highest charge at {2}.", maxSquare.x, maxSquare.y, maxValue);
            End();
        }

        public override void B()
        {
            CaseName = "11B";
            Start();

            var serial = int.Parse(INPUT);

            var fuelCells = new Dictionary<Point, int>();

            for (var y = 0; y < 300; y++)
            {
                var s = string.Empty;
                for (var x = 0; x < 300; x++)
                {
                    var rackId = x + 10;
                    var powerLevelStart = rackId * y;
                    var powerLevel = ((((powerLevelStart + serial) * rackId) % 1000) / 100) - 5;
                    fuelCells[new Point(x, y)] = powerLevel;
                    //if (powerLevel > 0)
                    //    s += "+";
                    //else if (powerLevel < 0)
                    //    s += "-";
                    //else
                    //    s += ".";
                }

                //Console.WriteLine(s);
            }

            

            Console.WriteLine("{0}: {1}",regions.OrderByDescending(r => r.Value).First().Key, regions.OrderByDescending(r => r.Value).First().Value);
            End();
        }

        public Dictionary<Point, int> ShrinkRegion(Dictionary<Point, int> region)
        {
            var minX = region.Min(p => p.Key.x);
            var maxX = region.Max(p => p.Key.x);
            var minY = region.Min(p => p.Key.y);
            var maxY = region.Max(p => p.Key.y);

            var topEdge = region.Where(p => p.Key.x == minY);
            var bottomEdge = region.Where(p => p.Key.y == maxY);
            var leftEdge = region.Where(p => p.Key.x == minX);
            var rightEdge = region.Where(p => p.Key.x == maxX);

            var topRight = topEdge.Union(rightEdge).Sum(c => c.Value);
            var topLeft = topEdge.Union(leftEdge).Sum(c => c.Value);
            var bottomRight = bottomEdge.Union(rightEdge).Sum(c => c.Value);
            var bottomLeft = bottomEdge.Union(leftEdge).Sum(c => c.Value);

            
        }
    }
}
