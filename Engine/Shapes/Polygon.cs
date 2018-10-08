using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Navigation;
using Engine.ShapeColections;
using Engine.Shapes.ShapeParts;
using Engine.XyObjects;

namespace Engine.Shapes
{

    public class Polygon : IShape
    {
        //CTORS
        public Polygon(IShapeColection parent,Skladba skladba = null)
        {
            if(skladba != null)
                Skladba = skladba;
            else
             _skladba = new Skladba(2);

            _skladba.Changed += OnEdited;
            ShapeColectionParent = parent;
            ShapeColectionParent.Add(this);
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

        public void SetStateToAllChilds(ShapeStates state)
        {
            State = state;
            _points.ForEach(p => p.State = state);
            _edgeParams.ForEach(p => p.State = state);
        }

        public IEnumerable<INode> Childs => ((IReadOnlyList<INode>)Points).Concat(EdgeParams);

        public IEnumerable<INode> AllChilds => Childs;

        public IShapeColection ShapeColectionParent { get; }

        public int EdgesCount(ShapeStates state)
        {
            return EdgeParams.Aggregate(0, (i, e) => e.State == state ? i + 1 : i);
        }

        public void SetSelectedEdges(EdgeValues values)
        {
            foreach (var ep in _edgeParams)
            {
                if (ep.State == ShapeStates.Selected)
                    ep.EdgeValues = values;
            }
            OnEdited();
        }

        public INode Parent => ShapeColectionParent;

        private Skladba _skladba;
        public Skladba Skladba
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

        //PUBLIC METHODS
        public IReadOnlyList<NodePoint> Points => _points.AsReadOnly();
        public IReadOnlyList<EdgeParams> EdgeParams=> _edgeParams.AsReadOnly();


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

        public virtual void Clear()
        {
            _points.Clear();
            OnEdited();
        }



        public void DeletePoint(NodePoint nodePoint)
        {
            int index = _points.IndexOf(nodePoint);
            _points.RemoveAt(index);
            _edgeParams.RemoveAt(index);
            OnEdited();
        }

        public void DeleteEdge(EdgeParams nodePoint)
        {
            int index = _edgeParams.IndexOf(nodePoint);
            _points.RemoveAt(index);
            _edgeParams.RemoveAt(index);
            OnEdited();
        }

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

        //EVENT STUFF
        public void DeleteYourself()
        {
            ShapeColectionParent.DeleteShape(this);
        }

        public event NoAtributeEventHandler Edited;
        protected virtual void OnEdited()
        {
            Edited?.Invoke();
        }

        //PRIVATE PART
        private readonly List<NodePoint> _points = new List<NodePoint>();
        private readonly List<EdgeParams> _edgeParams = new List<EdgeParams>();

    }
}