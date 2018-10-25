using System;

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
    }
}