using System.Collections.Generic;
using System.Text;
using Engine.XyObjects;

namespace Engine.Shapes
{
    public class Square: IShape
    {
        private readonly List<PointMy> _points = new List<PointMy>();

        private PointMy _point;
        public PointMy Point
        {
            get => _point;
            set
            {
                _point = value;
                ResetSqarePoints();
                OnShapeEdited();

            }
        }

        private SizeMy _size;
        public SizeMy Size
        {
            get => _size;
            set
            {
                _size = value;
                ResetSqarePoints();
                OnShapeEdited();

            }
        }

        //Empty ctor
        public Square()
        {
            _point = new PointMy(0,0);
            _size = new SizeMy(0,0);
        }

        public Square(PointMy point, SizeMy size, Skladba skladba = null)
        {
            _size = size;
            _point = point;

            ResetSqarePoints();

        }

        

        public event ShapeEventHandler ShapeEdited;

        public IReadOnlyCollection<PointMy> GetPoints()
        {
            return _points.AsReadOnly();
        }

        public ShapeStates State { get; set; }

        public Skladba Skladba { get; set; }

        public void Clear()
        {
            _point = new PointMy(0, 0);
            _size = new SizeMy(0, 0);
            ResetSqarePoints();
            OnShapeEdited();
        }
        protected virtual void OnShapeEdited()
        {
            ShapeEdited?.Invoke();
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

        private void ResetSqarePoints()
        {
            _points.Clear();
            _points.Add(new PointMy(_point.X, _point.Y));
            _points.Add(new PointMy(_point.X, _point.Y + _size.Y));
            _points.Add(new PointMy(_point.X + _size.X, _point.Y + _size.Y));
            _points.Add(new PointMy(_point.X + _size.X, _point.Y));
        }

    }

   
}