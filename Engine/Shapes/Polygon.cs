using System.Collections.Generic;
using Engine.ShapeColections;
using Engine.Shapes.ShapeParts;
using Engine.XyObjects;

namespace Engine.Shapes
{

    public class Polygon : IShape
    {
        //CTORS
        public Polygon(ShapeColection parent,Skladba skladba = null)
        {
            Skladba = skladba;
            ShapeColectionParent = parent;
        }

        //PROPERTYS
        private ShapeStates _state = ShapeStates.Basic;
        public ShapeStates State
        {
            get => _state;
            set
            {
                _state = value;
                OnEdited();
            }
        }

        public IEnumerable<INode> Childs { get; }
        public ShapeColection ShapeColectionParent { get; }
        public INode Parent => ShapeColectionParent;

        public Skladba Skladba { get; set; }

        //PUBLIC METHODS
        public IReadOnlyCollection<PointMy> Points => _points.AsReadOnly();
        public IReadOnlyCollection<EdgeParams> EdgeParams=> _edgeParams.AsReadOnly();


        public virtual void Add(PointMy pointMy)
        {
            _points.Add(pointMy);
            _edgeParams.Add(new EdgeParams(this));
            OnEdited();
        }

        public virtual void Clear()
        {
            _points.Clear();
            OnEdited();
        }

        public override string ToString()
        {
            if (_points.Count == 0)
                return "No Points, its wierd";
            return "Point: " + _points[0] + " pocet: " + _points.Count;
        }

        //EVENT STUFF
        public void Delete()
        {
            throw new System.NotImplementedException();
        }

        public event NoAtributeEventHandler Edited;
        protected virtual void OnEdited()
        {
            Edited?.Invoke();
        }

        //PRIVATE PART
        private readonly List<PointMy> _points = new List<PointMy>();
        private readonly List<EdgeParams> _edgeParams = new List<EdgeParams>();

    }
}