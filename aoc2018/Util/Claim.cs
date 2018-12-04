using System.Collections.Generic;
namespace aoc2018.Util
{
    public class Claim
    {
        public int Id { get; set; }
        public Point Offset { get; set; }
        public Rect Rect { get; set; }

        public static Claim ParseClaimString(string i)
        {
            var result = new Claim();

            var str = i.Replace(" @ ", " ");
            str = str.Replace(": ", " ");

            var s = str.Split(" ");

            result.Id = int.Parse(s[0].Replace("#", string.Empty));
            var offsetString = s[1].Split(",");
            result.Offset = new Point(int.Parse(offsetString[0]), int.Parse(offsetString[1]));
            var dimensionString = s[2].Split("x");
            result.Rect = new Rect(result.Offset, int.Parse(dimensionString[0]), int.Parse(dimensionString[1]));

            return result;
        }
    }

    public struct Point
    {
        public int x;
        public int y;

        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
