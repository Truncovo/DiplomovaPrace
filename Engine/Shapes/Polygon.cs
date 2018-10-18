using System;
using System.Text;
using Engine.ShapeColections;
using Engine.Shapes.ShapeParts;
using Engine.XyObjects;

namespace Engine.Shapes
{
    //----------------------------------------------------------
    //----------------------------------------------------------

    public class Polygon : Shape
    {
        //CTORS
        public Polygon()
        {
        }
        public Polygon(IShapeColection parent) : base(parent)
        {
        }

        //PUBLIC METHODS
       

        public virtual void Add(int x, int y)
        {
            Add(new PointMy(x,y));
        }
        public virtual void Add(PointMy pointMy)
        {
            var nodePoint = new NodePoint(this, pointMy);   
           Add(nodePoint);
        }
        public virtual void Add(NodePoint nodePoint)
        {
            _points.Add(nodePoint);
            nodePoint.Edited += Edited;

            var edgeParams = new EdgeParams(this);
            edgeParams.Edited += Edited;
            _edgeParams.Add(edgeParams);
            OnEdited();

        }

        protected override void OnEdited()
        {
            Edited?.Invoke();
            base.OnEdited();
        }

        public override event NoAtributeEventHandler Edited;


        public override string ToString()
        {
            var stringBuilder = new StringBuilder(40);
            stringBuilder.Append("POLYGON P: ");
            stringBuilder.Append(_points.Count);
            stringBuilder.Append(" EP:  ");
            stringBuilder.Append(_edgeParams.Count);
            stringBuilder.Append("\n");
            foreach (var nodePoint in _points)
            { 
                stringBuilder.Append(nodePoint);
                stringBuilder.Append("\n");
            }
            foreach (var edgeParamse in _edgeParams)
            {
                stringBuilder.Append(edgeParamse);
                stringBuilder.Append("\n");

            }

            return stringBuilder.ToString();

        }





    }
}