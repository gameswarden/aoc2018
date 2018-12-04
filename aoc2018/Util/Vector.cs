using System;

namespace aoc2018.Util
{
    public class Vector
    {
        public static bool VectorOverlap(int x, int y, int v1, int v2)
        {
            var dist = Dist(x,y);

            if (dist > 0)
                return x <= y + v2;
            else if (dist < 0)
                return y <= x + v1;

            return true;
        }

        public static int Dist(int x, int y)
        {
            return x - y;
        }

        public static int OverlapOrigin(int x, int y)
        {
            var dist = Dist(x, y);
            if (dist > 0)
                return x;

            return y;
        }

        public static bool PointBetween(int x, int a, int b)
        {
            return x >= a && x <= b;
        }

        public static int OverlapVector(int x, int y, int v1, int v2)
        {

            var vectorStart = 0;
            var vectorEnd = 0;

            if (PointBetween(y, x, x + v1)) // y between x and x'
            {
                vectorStart = y;
            }
            else
                vectorStart = x;

            if (PointBetween(x + v1, y, y + v2)) // x' between y and y'
            {
                vectorEnd = x + v1;
            }
            else
                vectorEnd = y + v2;

            return vectorEnd - vectorStart;
        }
    }
}
