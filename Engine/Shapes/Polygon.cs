using System;
using System.Linq;
using System.Text;
using Engine.Counts;
using Engine.ShapeColections;
using Engine.Shapes.ShapeParts;
using Engine.XyObjects;

namespace Engine.Shapes
{
    public class Polygon : Shape
    {
        //CTORS
        public Polygon(){}
        public Polygon(IShapeColection parent, bool add = true) : base(parent,add){}

        //PUBLIC METHODS - polygon
        public virtual void Add(int x, int y, EdgeShell edgeShell = null)
        {
            Add(new PointMy(x,y),edgeShell);
        }
        public virtual void Add(PointMy pointMy , EdgeShell edgeShell = null)
        {
            var nodePoint = new PointShell(this, pointMy);
            nodePoint.Edited += OnEdited;
           Add(nodePoint, edgeShell);
        }
        public virtual void Add(PointShell nodePoint, EdgeShell edgeShell = null)
        {
            _pointsShells.Add(nodePoint);
            nodePoint.Edited += OnEdited;

            if (edgeShell == null)
                edgeShell = new EdgeShell(this);

            edgeShell.Edited += OnEdited;
            _edgeShells.Add(edgeShell);
            OnEdited();
        }

        //PUBLIC METHODS - overrided

        public override void Optimize()
        {
            if(_pointsShells.Count < 3)
                DeleteYourself();


            var prevPoint = PointShells.FirstOrDefault();

            for (var i = 1; i < _pointsShells.Count; i++)
            {
                if (prevPoint != null && prevPoint.Point.Equals(_pointsShells[i].Point))
                { 
                    _pointsShells.RemoveAt(i);
                    _edgeShells.RemoveAt(i-1);
                    OnEdited();
                    i--;
                }
                prevPoint = _pointsShells[i];
            }

            if (_pointsShells.Count < 3)
                DeleteYourself();
        }
        public override PointMy MaxLeftMaxTopPoint()
        {
            //todo not working right
            return _pointsShells[0].Point;
        }
        public override string ToString()
        {
            var stringBuilder = new StringBuilder(40);
            stringBuilder.Append("POLYGON P: ");
            stringBuilder.Append(_pointsShells.Count);
            stringBuilder.Append(" EP:  ");
            stringBuilder.Append(_edgeShells.Count);
            stringBuilder.Append("\n");
            foreach (var nodePoint in _pointsShells)
            { 
                stringBuilder.Append(nodePoint);
                stringBuilder.Append("\n");
            }
            foreach (var edgeParamse in _edgeShells)
            {
                stringBuilder.Append(edgeParamse);
                stringBuilder.Append("\n");

            }

            return stringBuilder.ToString();

        }
    }
}