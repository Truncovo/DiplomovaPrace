using System;
using System.Runtime.InteropServices;

namespace Engine.XyObjects
{
    public class PointMy : XY
    {
        public PointMy(double x, double y) : base(x, y)
        {
        }

        public override object Clone()
        {
            return new PointMy(X, Y);
        }

        public  PointMy Copy()
        {
            return (PointMy)this.Clone();

        }
        public SizeMy ToSize()
        {
            return new SizeMy(X,Y);
        }

        public static double LengthOfLine(PointMy first, PointMy second)
        {
            double x = first.X - second.X;
            double y = first.Y - second.Y;
            var x2 = x * x;
            var y2 = y * y;
            var res = Math.Sqrt(x2 + y2);
            return res;
        }
    }
}