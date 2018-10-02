using System.Collections.Generic;
using Engine.XyObjects;

namespace Engine.Shapes
{
    public delegate void ShapeEventHandler();

    public class Polygon : IShape
    {
        //CTORS
        public Polygon(Skladba skladba = null)
        {
            Skladba = skladba;
        }

        //PROPERTYS
        public ShapeStates State { get; set; } = ShapeStates.Basic;
        public Skladba Skladba { get; set; }

        //PUBLIC METHODS
        public IReadOnlyCollection<PointMy> GetPoints()
        {
            return _points.AsReadOnly();
        }

        public virtual void Add(PointMy pointMy)
        {
            _points.Add(pointMy);
            OnShapeEdited();
        }

        public virtual void Clear()
        {
            _points.Clear();
            OnShapeEdited();
        }

        public override string ToString()
        {
            if (_points.Count == 0)
                return "No Points, its wierd";
            return "Point: " + _points[0] + " pocet: " + _points.Count;
        }

        //EVENT STUFF
        public event ShapeEventHandler ShapeEdited;
        protected virtual void OnShapeEdited()
        {
            ShapeEdited?.Invoke();
        }

        //PRIVATE PART
        private readonly List<PointMy> _points = new List<PointMy>();


        

        
    }
}