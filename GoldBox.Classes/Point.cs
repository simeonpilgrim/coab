using System;

namespace GoldBox.Classes
{
    public struct Point
    {
        public int x;
        public int y;

        public const int MapMaxX = 50;
        public const int MapMaxY = 25;
        public const int MapMinX = 0;
        public const int MapMinY = 0;

        public const int ScreenMaxX = 6;
        public const int ScreenMaxY = 6;
        public const int ScreenHalfX = ScreenMaxX / 2;
        public const int ScreenHalfY = ScreenMaxY / 2;
        public static readonly Point ScreenCenter = new Point(ScreenHalfX, ScreenHalfY);

        public Point(int _x, int _y)
        {
            x = _x;
            y = _y;
        }

        public Point(Point old)
        {
            x = old.x;
            y = old.y;
        }

        public static Point operator +(Point a, Point b)
        {
            return new Point(a.x + b.x, a.y + b.y);
        }

        public static Point operator -(Point a, Point b)
        {
            return new Point(a.x - b.x, a.y - b.y);
        }

        public static Point operator *(Point a, int b)
        {
            return new Point(a.x * b, a.y * b);
        }

        public static Point operator /(Point a, int b)
        {
            return new Point(a.x / b, a.y / b);
        }

        public static bool operator ==(Point a, Point b)
        {
            return a.x == b.x && a.y == b.y;
        }

        public static bool operator !=(Point a, Point b)
        {
            return a.x != b.x || a.y != b.y;
        }

        public void MapBoundaryTrunc()
        {
            x = Math.Max(Math.Min(x, MapMaxX - 1), MapMinX);
            y = Math.Max(Math.Min(y, MapMaxY - 1), MapMinY);
        }

        public bool MapInBounds()
        {
            return x < MapMaxX && x >= MapMinX && y < MapMaxY && y >= MapMinY;
        }

        public override string ToString()
        {
            return string.Format("x: {0} y: {1}", x, y);
        }
    }
}
