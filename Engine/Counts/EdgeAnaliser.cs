using System;
using System.Collections.Generic;
using Engine.XyObjects;

namespace Engine.Counts
{
    public partial class EdgeAnaliser
    {
        public double Tolerance { get; set; } = 0.001d;
        private bool _useX;

        private PointMy[] _points;
        private bool[] _pointsUsed;

        private bool _onFirstEdge;
        private bool _onSecondEdge;

        private int _actualMin;
        private bool IsActualMinFromFirst => IsFromFirst(_actualMin);

        private EdgeAnaliserResult _result;

        public EdgeAnaliserResult Analize(PointMy a, PointMy b, PointMy c, PointMy d)
        {
            _points = new[] {a, b, c, d};
            _pointsUsed = new[] {false, false, false, false};
            _result = new EdgeAnaliserResult();
            _actualMin = -1;
          

            if (!CheckPrecondition())
                return _result;

            UpdateActualMin();
          

            _onFirstEdge = false;
            _onSecondEdge = false;

            From00To010();

            //should never throw exception 
            if (!(_onFirstEdge && !_onSecondEdge
                || !_onFirstEdge && _onSecondEdge))
                throw new Exception("Should be in 010 state");

          

            //should never throw exception - check if edger are layed on each-other
            if ((IsActualMinFromFirst && _onFirstEdge))
                throw new Exception("Edges not crossingA");
            if ((!IsActualMinFromFirst && _onSecondEdge))
                throw new Exception("Edges not crossingB");

           

            if (IsActualMinFromFirst)
                From01To11();
            else
                From10To11();

            //should never throw exception 
            if ( !_onFirstEdge&& !_onSecondEdge)
                throw new Exception("Should be in 11 state");

            if (IsActualMinFromFirst)
                From11To01();
            else
                From11To10();
          
            //should never throw exception 
            if (!(_onFirstEdge && !_onSecondEdge
                  || !_onFirstEdge && _onSecondEdge))
                throw new Exception("Should be in 010 state");


            //todo end - start switched edges
            if(GetValue(_points[0]) > GetValue(_points[1]))
                _result.ReverseFirst();


            if (GetValue(_points[2]) > GetValue(_points[3]))
                _result.ReverseSecond();

            _result.Status = -2;
            return _result;
        }

        private bool CheckPrecondition()
        {
            _result = new EdgeAnaliserResult();
            //check if edges are under same angle
            var angle = Math.Abs(CountAngle(_points[0], _points[1]));
            var angle2 = Math.Abs(CountAngle(_points[2], _points[3]));
            if (Math.Abs(angle - angle2) > Tolerance)
            {
                _result.Status = 7;
                return false;   
            }

            //decide which axle is better to compare
            if (Math.Abs(angle) > 0.785d && Math.Abs(angle) < 2.356d)
                _useX = false;
            else
                _useX = true;

            //check if edges have same axle crossing point
            if (_useX)
            {
                if (Math.Abs(CountYCrossing(_points[0], _points[1]) - CountYCrossing(_points[2], _points[3])) > Tolerance)
                {
                    _result.Status = -6;
                    return false;
                }
            }
            else
            {
                if (Math.Abs(CountXCrossing(_points[0], _points[1]) - CountXCrossing(_points[2], _points[3])) > Tolerance)
                {
                    _result.Status = 6;
                    return false;
                }
            }

            //check if edges are layed on each-other
            double minSecond = Math.Min(GetValue(_points[2]), GetValue(_points[3]));
            double maxSecond = Math.Max(GetValue(_points[2]), GetValue(_points[3]));
            if ((GetValue(_points[0]) <= minSecond && GetValue(_points[1]) <= minSecond) ||
                (GetValue(_points[0]) >= maxSecond && GetValue(_points[1]) >= maxSecond))
            {
                _result.Status = 5;
                return false;
            }

            //check if edges are same
            if (_points[0].Equals(_points[2]) && _points[1].Equals(_points[3]) ||
                _points[0].Equals(_points[3]) && _points[1].Equals(_points[2]))
            {
                _result.InContactFirst.Add(true);
                _result.InContactSecond.Add(true);
                _result.Status = 7;
                return false;
            }
            return true;
        }

        private double GetValue(PointMy point)
        {
            var n = _useX ? point.X : point.Y;
            return n;
        }

        private bool IsFromFirst(int index)
        {
            return index <= 1;
        }

    x    private void UpdateActualMin()
        {
            if(_actualMin != -1)
                _pointsUsed[_actualMin] = true;

            int minIndex = -1;
            double minValue = double.MaxValue;

            for (int i = 0; i < 4; i++)
            {
                if (_pointsUsed[i])
                    continue;

                if (GetValue(_points[i]) < minValue)
                {
                    minValue = GetValue(_points[i]);
                    minIndex = i;
                }
            }

            if(minIndex == -1)
                throw new Exception("all points done");
            _actualMin = minIndex;
        }
      
        public static double CountAngle(PointMy a, PointMy b)
        {
            double top = (b.Y - a.Y);
            double bot = (b.X - a.X);
            double res = Math.Atan(top / bot);
            return res;
        }


        public static double CountXCrossing(PointMy p1, PointMy p2)
        {
            var n = p1.X - (((p2.X - p1.X) / (p2.Y - p1.Y)) * p1.Y);

            //Console.WriteLine("X: " + n);

            return n;
        }

        public static double CountYCrossing(PointMy p1, PointMy p2)
        {
            var n = p1.Y - (((p2.Y - p1.Y) / (p2.X - p1.X)) * p1.X);

            //Console.WriteLine("Y: " + n);

            return n;

        }

        //tmp method for testing
        public static void FromMain()
        {
            var res = EdgeAnaliser.CountAngle(new PointMy(1, 1), new PointMy(2, 2));
        }

    }

}