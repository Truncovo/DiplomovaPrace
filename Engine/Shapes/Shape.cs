using System.Collections.Generic;
using System.Linq;
using Engine.Counts;
using Engine.ShapeColections;
using Engine.Shapes.ShapeParts;
using Engine.XyObjects;

namespace Engine.Shapes
{
    public abstract partial class Shape : IShape
    {
        protected Shape()
        {
            _skladba = new Skladba(2);
            _skladba.Changed += OnEdited;
        }
        protected Shape(IShapeColection parent, bool add = true) : this()
        {
            _parent = parent;
            if(add)
                _parent.AddShape(this);
        }
        
        //IShape - inherited methods
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
        public virtual IShapeColection ShapeColectionParent => _parent;

        public virtual IReadOnlyList<PointShell> PointShells => _pointsShells.AsReadOnly();
        public virtual IReadOnlyList<EdgeShell> EdgeShells => _edgeShells.AsReadOnly();

        public virtual int EdgesCount() => _edgeShells.Count;
        public virtual int EdgesCount(ShapeStates state)
        {
            return EdgeShells.Aggregate(0, (i, e) => e.State == state ? i + 1 : i);
        }

        public virtual EdgeShell GetEdgeShell(int index) => _edgeShells[index];
        public virtual PointShell GetPointShell(int index) => _pointsShells[index];

        public virtual void SetAllEdgesToNotInContact()
        {
            foreach (var edgeParam in EdgeShells)
                edgeParam.EdgeValues.InContact = false;
        }

        public virtual void SplitEdgeAsSecondFromResult(int index, List<bool> edgesInContact, List<PointMy> pointsToAdd)
        {
            if (_edgeShells[index].EdgeValues.InContact)
                return;

            int number = 1;
            foreach (var point in pointsToAdd)
            {
                var pointShell = new PointShell(this, point);
                pointShell.Edited += OnEdited;
                _pointsShells.Insert(index + number++, pointShell);
            }

            var tmp = _edgeShells[index];

            if (edgesInContact.Count != 0)
                _edgeShells[index].EdgeValues.InContact = edgesInContact[0];

            for (int i = 1; i < edgesInContact.Count; i++)
            {
                var tmp2 = tmp.GetCopy();
                tmp2.EdgeValues.InContact = edgesInContact[i];
                tmp2.Edited += OnEdited;
                _edgeShells.Insert(index + i, tmp2);
            }

            OnEdited();
        }

        public virtual void Clear()
        {
            foreach (var edgeShell in _edgeShells)
                edgeShell.Edited -= OnEdited;

            foreach (var pointsShell in _pointsShells)
                pointsShell.Edited -= OnEdited;

            _pointsShells.Clear();
            _edgeShells.Clear();
            OnEdited();
        }
        public virtual void DeletePoint(PointShell nodePoint)
        {
            int index = _pointsShells.IndexOf(nodePoint);
            _pointsShells.RemoveAt(index);
            _edgeShells.RemoveAt(index);
            OnEdited();
        }
        public virtual void DeleteEdge(EdgeShell nodePoint)
        {
            int index = _edgeShells.IndexOf(nodePoint);
            _pointsShells.RemoveAt(index);
            _edgeShells.RemoveAt(index);
            OnEdited();
        }

        public PointMy FirstPoint()
        {
            return _pointsShells[0].Point;
        }

        public virtual void DeleteYourself()
        {
            ShapeColectionParent.RemoveShape(this);
        }

        public abstract void Optimize();
        public virtual int SplitEdges(IShape shape)
        {
            var edgeAnaliser = new EdgeAnaliser();
            int counter = 0;
            for (int i = 0; i < _pointsShells.Count; i++)
            for (int j = 0; j < shape.EdgesCount(); j++)
            {
                PointMy p1, p2, p3, p4;
                counter++;
                //get right points for two edges
                p1 = _pointsShells[i].Point;
                if (i < _pointsShells.Count - 1)
                    p2 = _pointsShells[i + 1].Point;
                else
                    p2 = _pointsShells[0].Point;

                p3 = shape.GetPointShell(j).Point;
                if (j < shape.EdgesCount() - 1)
                    p4 = shape.GetPointShell(j + 1).Point;
                else
                    p4 = shape.GetPointShell(0).Point;

                //calculate result
                var result = edgeAnaliser.Analize(p1, p2, p3, p4);

                SplitEdgeAsSecondFromResult(i, result.InContactFirst, result.AddFirst);
                shape.SplitEdgeAsSecondFromResult(j, result.InContactSecond, result.AddSecond);
            }
            return counter;
        }

        public virtual PointMy MaxLeftMaxTopPoint()
        {
            double maxX = 0;
            double maxY = 0;

            foreach (var pointsShell in _pointsShells)
            {
                if (pointsShell.Point.X > maxX)
                    maxX = pointsShell.Point.X;
                if (pointsShell.Point.Y > maxY)
                    maxY = pointsShell.Point.Y;
            }
            return new PointMy(maxX,maxY);
        }

        
        //INode - inheried methods
        public ShapeStates State
        {
            get => _state;
            set
            {
                _state = value;
                OnEdited();
            }
        }

        public virtual void SetStateToAllChilds(ShapeStates state)
        {
            State = state;
            _pointsShells.ForEach(p => p.State = state);
            _edgeShells.ForEach(p => p.State = state);
        }
        public virtual IEnumerable<INode> Childs => ((IReadOnlyList<INode>)PointShells).Concat(EdgeShells);
        public virtual INode Parent => _parent;

        //EVENT STUFF
        public virtual event NoAtributeEventHandler Edited;
        protected virtual void OnEdited()
        {
            if (InvokeEdtited)
                Edited?.Invoke();
        }

        //PRIVATE PART
        protected ShapeStates _state = ShapeStates.Basic;
        protected IShapeColection _parent;
        protected readonly List<PointShell> _pointsShells = new List<PointShell>();
        protected readonly List<EdgeShell> _edgeShells = new List<EdgeShell>();
        protected Skladba _skladba;
        private bool _invokeEdited = true;
        public bool InvokeEdtited
        {
            get => _invokeEdited;
            set
            {
                _invokeEdited = value;
                OnEdited();
            }
        }

        public abstract object Clone(ShapeColection sc = null);
    }
}