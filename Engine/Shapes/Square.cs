using System.Collections.Generic;
using System.Text;
using Engine.XyObjects;

namespace Engine.Shapes
{
    public class Square: IShape
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
                OnShapeEdited();
            }
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
        public IReadOnlyCollection<PointMy> GetPoints()
        {
            var res = new List<PointMy>();
            res.Clear();
            res.Add(new PointMy(_point.X, _point.Y));
            res.Add(new PointMy(_point.X, _point.Y + _size.Y));
            res.Add(new PointMy(_point.X + _size.X, _point.Y + _size.Y));
            res.Add(new PointMy(_point.X + _size.X, _point.Y));
            return res;
        }

        public void Clear()
        {
            _point = new PointMy(0, 0);
            _size = new SizeMy(0, 0);
            OnShapeEdited();
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
        public event ShapeEventHandler ShapeEdited;
        protected virtual void OnShapeEdited()
        {
            ShapeEdited?.Invoke();
        }

        //PRIVATE PART
        private SizeMy _size;
        private PointMy _point;

    }

   
}