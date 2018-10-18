using System.Collections.Generic;
using System.Security.AccessControl;
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
            CtorOnly();
        }
        public Square(IShapeColection parent):base(parent)
        {
            CtorOnly();
        }
        
        //todo - edit this

        private SizeMy _size;
        private PointMy _point;

        public PointMy Point
        {
            get => _point;
            set
            {
                _point = value;
                RecreatePoints();
                OnEdited();
            } 
        }

        public SizeMy Size
        {
            get => _size;
            set
            {
                _size = value;
                RecreatePoints();
                OnEdited();
            }
        }

        //PUBLIC METHODS
        public override event NoAtributeEventHandler Edited;

        protected override void OnEdited()
        {
            Edited?.Invoke();
            base.OnEdited();
        }

        private void CtorOnly()
        {
            for (int i = 0; i < 4; i++)
            {
                var point = new NodePoint(this);
                _points.Add(point);
                point.Edited += OnEdited;

                var edge = new EdgeParams(this);    
                _edgeParams.Add(edge);
                edge.Edited += OnEdited;
            }
            _point = new PointMy(400,400);
            _size = new SizeMy(100,100);
            RecreatePoints();
            base.OnEdited();
        }
        private void RecreatePoints()
        {
            _points[0].Point = _point;
            _points[1].Point = new PointMy(_point.X, _point.Y + _size.Y );
            _points[2].Point = new PointMy(_point.X + _size.X, _point.Y + _size.Y);
            _points[3].Point = new PointMy(_point.X + _size.X, _point.Y);
        }
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
