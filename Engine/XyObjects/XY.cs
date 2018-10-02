using System;
using System.Globalization;
using System.Text;

namespace Engine
{
    public class XY: ICloneable , IEquatable<XY>
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

        //by resharper
        public bool Equals(XY other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return X.Equals(other.X) && Y.Equals(other.Y);
        }

        //by resharper
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((XY) obj);
        }

        //by resharper
        public override int GetHashCode()
        {
            unchecked
            {
                return (X.GetHashCode() * 397) ^ Y.GetHashCode();
            }
        }
    }
}