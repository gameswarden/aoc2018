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

            var fuelCells = new int [300,300];
            var sums = new int [300,300];

            for (var y = 0; y < 300; y++)
            {
                var s = string.Empty;
                for (var x = 0; x < 300; x++)
                {
                    var rackId = x + 10;
                    var powerLevelStart = rackId * y;
                    var powerLevel = ((((powerLevelStart + serial) * rackId) % 1000) / 100) - 5;
                    fuelCells[x,y] = powerLevel;

                    //if (powerLevel > 0)
                    //    s += "+";
                    //else if (powerLevel < 0)
                    //    s += "-";
                    //else
                    //    s += ".";
                }

                //Console.WriteLine(s);
            }

            for (int i = 0; i < fuelCells.GetLength(0); i++)
            {
                for (int j = 0; j < fuelCells.GetLength(1); j++)
                {
                    if (i == 0 && j == 0)
                        sums[i, j] = fuelCells[i, j];
                    else if (j == 0)
                        sums[i, j] = sums[i - 1, j] + fuelCells[i, j];
                    else if (i == 0)
                        sums[i, j] = sums[i, j - 1] + fuelCells[i, j];
                    else
                        sums[i, j] = sums[i - 1, j] + sums[i, j - 1] - sums[i - 1, j - 1] + fuelCells[i, j];
                }
            }

            var maxArea = int.MinValue;

            for (int rowStart = 0; rowStart < sums.GetLength(0); rowStart++)
            {
                for (int rowEnd = rowStart; rowEnd < sums.GetLength(0); rowEnd++)
                {
                    for (int colStart = 0; colStart < sums.GetLength(1); colStart++)
                    {
                        for (int colEnd = colStart; colEnd < sums.GetLength(1); colEnd++)
                        {
                            if (colEnd - colStart == rowEnd - rowStart)
                            {
                                var area = ComputeSum(sums, rowStart, rowEnd, colStart, colEnd);
                                if (area > maxArea)
                                {
                                    maxArea = area;
                                    Console.WriteLine("{0}x{0} grid at ({1},{2}) - Max Sum: {3}", rowEnd - rowStart + 1, rowStart, colStart, area);
                                }
                            }   
                        }
                    }
                }
            }

            //Console.WriteLine("{0}: {1}",regions.OrderByDescending(r => r.Value).First().Key, regions.OrderByDescending(r => r.Value).First().Value);
            End();
        }

        public int ComputeSum(int[,] sums, int i1, int i2, int j1, int j2)
        {
            if (i1 == 0 && j1 == 0)
                return sums[i2, j2];
            else if (i1 == 0)
                return sums[i2, j2] - sums[i2, j1 - 1];
            else if (j1 == 0)
                return sums[i2, j2] - sums[i1 - 1, j2];
            else
                return sums[i2, j2] - sums[i2, j1 - 1] - sums[i1 - 1, j2] + sums[i1 - 1, j1 - 1];
        }
    }
}
