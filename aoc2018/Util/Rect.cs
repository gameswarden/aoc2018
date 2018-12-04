using System.Collections.Generic;
using System;
using static aoc2018.Util.Vector;

namespace aoc2018.Util
{
    public class Rect
    {
        public Point Origin { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public Rect(Point origin, int width, int height)
        {
            Origin = origin;
            Width = width;
            Height = height;
        }

        public bool Overlap(Rect r)
        {
            return VectorOverlap(Origin.x, r.Origin.x, Width, r.Width) && VectorOverlap(Origin.y, r.Origin.y, Height, r.Height);
        }

        public HashSet<Point> GetOverlapArea(Rect r)
        {
            var result = new HashSet<Point>();

            var overlapOrigin = new Point(OverlapOrigin(Origin.x, r.Origin.x), OverlapOrigin(Origin.y, r.Origin.y));
            var overlapWidth = OverlapVector(Origin.x, r.Origin.x, Width, r.Width);
            var overlapHeight = OverlapVector(Origin.y, r.Origin.y, Height, r.Height);

            for (int y = 0; y < overlapHeight; y++)
            {
                for (int x = 0; x < overlapWidth; x++)
                {
                    result.Add(new Point(overlapOrigin.x + x, overlapOrigin.y + y));
                }
            }

            return result;

        }
    }
}
