using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Dynamic;
using System.Security.AccessControl;
using System.Text;
using Engine.Counts;
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
            RecreatePointsAndEdges();
        }
        public Square(IShapeColection parent):base(parent)
        {
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
            var polygon = new Polygon(_parent, false);

            for (int i = 0; i < _pointsShells.Count; i++)
                polygon.Add(_pointsShells[i].Point, _edgeShells[i]);

            _parent.ChangeShapes(this, polygon);

            RecreatePointsAndEdges();

            return polygon;
        }

        //PUBLIC METHODS - from shape

   

       
        public override void DeleteEdge(EdgeShell nodePoint)
        {
            //todo convert to polygon before base call
            throw new NotImplementedException();
            base.DeleteEdge(nodePoint);
        }
        public override void DeletePoint(PointShell nodePoint)
        {
            //todo convert to polygon before base call
            throw new NotImplementedException();
            base.DeletePoint(nodePoint);
        }

        public override void Optimize()
        {
            if(Math.Abs(Size.X) < 0.0001d || Math.Abs(Size.Y) < 0.0001d)
                DeleteYourself();
        }
        public override PointMy MaxLeftMaxTopPoint()
        {
            return _point;
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
            _point = new PointMy(400,400);
            _size = new SizeMy(100,100);
            RecreatePointValues();
            base.OnEdited();
        }
        private void RecreatePointValues()
        {
            _pointsShells[0].Point = _point;
            _pointsShells[1].Point = new PointMy(_point.X, _point.Y + _size.Y );
            _pointsShells[2].Point = new PointMy(_point.X + _size.X, _point.Y + _size.Y);
            _pointsShells[3].Point = new PointMy(_point.X + _size.X, _point.Y);

            while (_pointsShells.Count > 4)
                _pointsShells.RemoveAt(_pointsShells.Count -1);

            while (_edgeShells.Count > 4)
                _edgeShells.RemoveAt(_edgeShells.Count - 1);
        }
    }


}

