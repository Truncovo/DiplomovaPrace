using System;
using System.Globalization;
using System.Text;

namespace Engine
{

    public class XY: ICloneable
    {
        public double X { get; }
        public double Y { get; }
        public XY(double x, double y)
        {
            X = x;
            Y = y;
        }

        public virtual object Clone()
        {
            return new XY(X, Y);
        }

        public override string ToString()
        {
            StringBuilder B = new StringBuilder(10);
            B.Append("[ ");
            B.Append(X.ToString(CultureInfo.InvariantCulture));
            B.Append(" , ");
            B.Append(Y.ToString(CultureInfo.InvariantCulture));
            B.Append(" ]");

            return B.ToString();

        }
    }
}