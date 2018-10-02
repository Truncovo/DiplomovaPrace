using System.Collections.Generic;
using Engine.XyObjects;

namespace Engine.Shapes
{
    public delegate void ShapeEventHandler();

    public class Polygon : IShape
    {
        public event ShapeEventHandler ShapeEdited;

        internal readonly List<PointMy> _points = new List<PointMy>();

        

        public Polygon(Skladba skladba = null)
        {
            Skladba = skladba;
        }

        public virtual void Add(PointMy pointMy)
        {
            _points.Add(pointMy);
            OnShapeEdited();
        }

      

        public IReadOnlyCollection<PointMy> GetPoints()
        {
            return _points.AsReadOnly();
        }

        public ShapeStates State { get; set; }

        public Skladba Skladba { get; set; }

        public virtual void Clear()
        {
            _points.Clear();
            OnShapeEdited();
        }

        protected virtual void OnShapeEdited()
        {
            ShapeEdited?.Invoke();
        }

        public override string ToString()
        {
            if (_points.Count == 0)
                return "No Points, its wierd";
            return "Point: " + _points[0] + " pocet: " + _points.Count;
        }
    }
}