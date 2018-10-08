using System.Collections.Generic;
using System.Text;
using Engine.ShapeColections;
using Engine.Shapes.ShapeParts;
using Engine.XyObjects;

namespace Engine.Shapes
{
    public class Square : IShape
    {
        public ShapeStates State { get; set; }
        public void SetStateToAllChilds(ShapeStates state)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<INode> Childs { get; }
        public INode Parent { get; }
        public void DeleteYourself()
        {
            throw new System.NotImplementedException();
        }

        public event NoAtributeEventHandler Edited;
        public Skladba Skladba { get; set; }
        public IShapeColection ShapeColectionParent { get; }
        public int EdgesCount(ShapeStates state)
        {
            throw new System.NotImplementedException();
        }

        public void SetSelectedEdges(EdgeValues values)
        {
            throw new System.NotImplementedException();
        }

        public IReadOnlyList<NodePoint> Points { get; }
        public IReadOnlyList<EdgeParams> EdgeParams { get; }
        public void Clear()
        {
            throw new System.NotImplementedException();
        }

        public void DeletePoint(NodePoint nodePoint)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteEdge(EdgeParams nodePoint)
        {
            throw new System.NotImplementedException();
        }
    }

    /*
     * 
    public class Square: IShape, INode
    {
        //CTORS
        //PROPERTYS
        //PUBLIC METHODS
        //EVENT STUFF
        //PRIVATE PART

        //CTORS
        public Square()
        {
            _point = new PointMy(0,0);
            _size = new SizeMy(0,0);
        }
        public Square(PointMy point, SizeMy size, Skladba skladba = null)
        {
            _size = size;
            _point = point;
        }

        public PointMy Point
        {
            get => _point;
            set
            {
                _point = value;
                RefreshPoints();
                OnShapeEdited();
            }
        }

        private void RefreshPoints()
        {
            _points.Clear();
            _points.Add(new PointMy(_point.X, _point.Y));
            _points.Add(new PointMy(_point.X, _point.Y + _size.Y));
            _points.Add(new PointMy(_point.X + _size.X, _point.Y + _size.Y));
            _points.Add(new PointMy(_point.X + _size.X, _point.Y));
        }

        public SizeMy Size
        {
            get => _size;
            set
            {
                _size = value;
                OnShapeEdited();
            }
        }

        //PROPERTYS
        public Skladba Skladba { get; set; }
        public ShapeStates State { get; set; } = ShapeStates.Basic;

        //PUBLIC METHODS
        public IReadOnlyCollection<PointMy> Points => _points;

        public IReadOnlyCollection<EdgeParams> EdgeParams => _edgeParams;


        public void Clear()
        {
            _point = new PointMy(0, 0);
            _size = new SizeMy(0, 0);
            RefreshPoints();
            ResetEdgeParams();
            OnShapeEdited();
        }

        private void ResetEdgeParams()
        {
            _edgeParams.Clear();

            for (int i = 0; i < 4; i++)
                _edgeParams.Add(Shapes.EdgeParams.Empty);

        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder(30);
            stringBuilder.Append("Point: [ ");
            stringBuilder.Append(Point.X.ToString());
            stringBuilder.Append(" , ");
            stringBuilder.Append(Point.Y.ToString());
            stringBuilder.Append(" ] size: ");
            stringBuilder.Append(Size.X.ToString());
            stringBuilder.Append(" X ");
            stringBuilder.Append(Size.Y.ToString());

            return stringBuilder.ToString();
        }

        //EVENT STUFF
        public void Delete()
        {
            throw new System.NotImplementedException();
        }

        public event NoAtributeEventHandler Edited;

        protected virtual void OnShapeEdited()
        {
            Edited?.Invoke();
        }

        //PRIVATE PART
        private SizeMy _size;
        private PointMy _point;
        private readonly List<PointMy> _points = new List<PointMy>();
        private readonly List<EdgeParams> _edgeParams = new List<EdgeParams>();
    }

   */
}