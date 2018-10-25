using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Windows.Navigation;
using Engine.XyObjects;

namespace Engine.Counts
{
    public class EdgeAnaliserResult : IEquatable<EdgeAnaliserResult>
    {
        public int Status { get; set; } = -1;

        public List<PointMy> AddFirst { get; set; } = new List<PointMy>();
        public List<PointMy> AddSecond { get; set; } = new List<PointMy>();

        public List<bool> InContactFirst { get; set; } = new List<bool>();
        public List<bool> InContactSecond { get; set; }= new List<bool>();

        public void ReverseFirst()
        {
             AddFirst.Reverse();
            InContactFirst.Reverse();
        }

        public void ReverseSecond()
        {
            AddSecond.Reverse();
            InContactSecond.Reverse();
        }


        public bool Equals(EdgeAnaliserResult other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Status == other.Status
                   && AddFirst.SequenceEqual(other.AddFirst)
                   && AddSecond.SequenceEqual(other.AddSecond)
                   && InContactFirst.SequenceEqual(other.InContactFirst)
                   && InContactSecond.SequenceEqual(other.InContactSecond);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((EdgeAnaliserResult) obj);
        }


        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Status;
                hashCode = (hashCode * 397) ^ (AddFirst != null ? AddFirst.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (AddSecond != null ? AddSecond.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (InContactFirst != null ? InContactFirst.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (InContactSecond != null ? InContactSecond.GetHashCode() : 0);
                return hashCode;
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("S: " + Status + "\n");

            sb.Append("AddFirst: ");
            foreach (var v in AddFirst)
                sb.Append(v);
            sb.Append("\n");

            sb.Append("AddSecond: ");
            foreach (var v in AddSecond)
                sb.Append(v);
            sb.Append("\n");


            sb.Append("ContactFirst: ");
            foreach (var v in InContactFirst)
                sb.Append(v + " ");
            sb.Append("\n");

            sb.Append("ContactSecond: ");
            foreach (var v in InContactSecond)
                sb.Append(v + " ");

            return sb.ToString();
        }
    }
}