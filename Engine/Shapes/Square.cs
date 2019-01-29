using System;
using System.Text;
using Engine.ShapeColections;
using Engine.Shapes.ShapeParts;
using Engine.XyObjects;

namespace Engine.Shapes
{
    public class Square : Shape
    {
        //CTORS
        public Square()
        {
            _point = new PointMy(0, 0);
            _size = new SizeMy(0, 0);
            RecreatePointsAndEdges();
        }
        public Square(IShapeColection parent):base(parent)
        {
            _point = new PointMy(0,0);
            _size = new SizeMy(0,0);
            RecreatePointsAndEdges();
        }

        //Added propertyes and methods
        public virtual PointMy Point
        {
            get => _point;
            set
            {
                _point = value;
                RecreatePointValues();
                OnEdited();
            } 
        }
        public virtual SizeMy Size
        {
            get => _size;
            set
            {
                _size = value;
                RecreatePointValues();
                OnEdited();
            }
        }

        public virtual Polygon ConvertToPolygon()
        {
            Polygon polygon;

            if(Parent == null)
                polygon = new Polygon();
            else
                polygon = new Polygon(_parent, false);

            polygon.Skladba = new Skladba(Skladba.Value);

            for (int i = 0; i < _pointsShells.Count; i++)
                polygon.Add(_pointsShells[i].Point, _edgeShells[i]);

            if (Parent != null)
                _parent.ChangeShapes(this, polygon);

            //RecreatePointsAndEdges();

            return polygon;
        }

        //PUBLIC METHODS - from shape
        public override void DeleteEdge(EdgeShell nodePoint)
        {
            //todo convert to polygon before base call
            throw new NotImplementedException();
            base.DeleteEdge(nodePoint);
        }

        public override object Clone(ShapeColection sc = null)
        {
            var res = new Square(sc);
            res.Size = new SizeMy(Size.X,Size.Y);
            res.Point = new PointMy(Point.X,Point.Y);
            res.RecreatePointsAndEdges();
            res.Skladba = new Skladba(Skladba.Value);
            for (int i = 0; i < EdgeShells.Count; i++)
                res.EdgeShells[i].EdgeValues = EdgeShells[i].EdgeValues.GetCopy();
            return res;
        }

        public override void DeletePoint(PointShell nodePoint)
        {
            throw new NotImplementedException();
        }

        public override void Optimize()
        {
            if(Math.Abs(Size.X) < 0.0001d || Math.Abs(Size.Y) < 0.0001d)
                DeleteYourself();
        }

        //private part
        private SizeMy _size;
        private PointMy _point;

        private void RecreatePointsAndEdges()
        {
            _edgeShells.Clear();
            for (int i = 0; i < 4; i++)
            {
                var point = new PointShell(this);
                _pointsShells.Add(point);
                point.Edited += OnEdited;

                var edge = new EdgeShell(this);    
                _edgeShells.Add(edge);
                edge.Edited += OnEdited;
            }
            RecreatePointValues();
            base.OnEdited();
        }
        private void RecreatePointValues()
        {
            _pointsShells[0].Point = _point;
            _pointsShells[1].Point = new PointMy(_point.X + _size.X, _point.Y);
            _pointsShells[2].Point = new PointMy(_point.X + _size.X, _point.Y + _size.Y);
            _pointsShells[3].Point = new PointMy(_point.X, _point.Y + _size.Y);

            while (_pointsShells.Count > 4)
                _pointsShells.RemoveAt(_pointsShells.Count -1);

            while (_edgeShells.Count > 4)
                _edgeShells.RemoveAt(_edgeShells.Count - 1);
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder(40);
            stringBuilder.Append("Square P: ");
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

