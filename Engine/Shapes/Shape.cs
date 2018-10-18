using System.Collections.Generic;
using System.Linq;
using Engine.ShapeColections;
using Engine.Shapes.ShapeParts;

namespace Engine.Shapes
{
    public abstract class Shape : IShape
    {
        protected Shape()
        {
            _skladba = new Skladba(2);
            _skladba.Changed += OnEdited;
        }

        protected Shape(IShapeColection parent) : this()
        {
            _parent = parent;
            _parent.AddShape(this);
        }
        public virtual void DeletePoint(NodePoint nodePoint)
        {
            int index = _points.IndexOf(nodePoint);
            _points.RemoveAt(index);
            _edgeParams.RemoveAt(index);
            OnEdited();
        }
        public virtual void DeleteEdge(EdgeParams nodePoint)
        {
            int index = _edgeParams.IndexOf(nodePoint);
            _points.RemoveAt(index);
            _edgeParams.RemoveAt(index);
            OnEdited();
        }

        public virtual int EdgesCount(ShapeStates state)
        {
            return EdgeParams.Aggregate(0, (i, e) => e.State == state ? i + 1 : i);
        }
        public virtual void SetSelectedEdges(EdgeValues values)
        {
            foreach (var ep in _edgeParams)
            {
                if (ep.State == ShapeStates.Selected)
                    ep.EdgeValues = values;
            }
            OnEdited();
        }

        public virtual void SetStateToAllChilds(ShapeStates state)
        {
            State = state;
            _points.ForEach(p => p.State = state);
            _edgeParams.ForEach(p => p.State = state);
        }

        //EVENT STUFF
        public void DeleteYourself()
        {
            ShapeColectionParent.RemoveShape(this);
        }

        public virtual event NoAtributeEventHandler Edited;

        protected virtual void OnEdited()
        {
            Edited?.Invoke();
        }

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


        public virtual IEnumerable<INode> Childs => ((IReadOnlyList<INode>)Points).Concat(EdgeParams);
        public virtual IEnumerable<INode> AllChilds => Childs;

        public virtual INode Parent => _parent;

        public virtual IShapeColection ShapeColectionParent => _parent;

        public IReadOnlyList<NodePoint> Points => _points.AsReadOnly();
        public IReadOnlyList<EdgeParams> EdgeParams => _edgeParams.AsReadOnly();

        public virtual Skladba Skladba
        {
            get => _skladba;
            set
            {
                _skladba.Changed -= OnEdited;
                _skladba = value;
                _skladba.Changed += OnEdited;
                OnEdited();
            }
        }

        public virtual void Clear()
        {
            _points.Clear();
            OnEdited();
        }

        //PRIVATE PART
        protected IShapeColection _parent;
        protected readonly List<NodePoint> _points = new List<NodePoint>();
        protected readonly List<EdgeParams> _edgeParams = new List<EdgeParams>();
        protected Skladba _skladba;
    }
}