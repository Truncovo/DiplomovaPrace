using System.Collections.Generic;
using Engine.XyObjects;

namespace Engine.Shapes.ShapeParts
{
    public class NodePoint : INode
    {
        //CTORS
        private NodePoint(IShape parent)
        {
            ShapeParent = parent;
            _state = ShapeStates.Basic;
        }
        public NodePoint(IShape parent, int x, int y):this(parent)
        {
            _point = new PointMy(x,y);
        }
        public NodePoint(IShape parent, PointMy point) : this(parent)
        {
            _point = point;
        }

        //PROPERTY - tree parts
        public IEnumerable<INode> Childs => new List<INode>();
        public IShape ShapeParent { get; }
        public INode Parent => ShapeParent;
        
        //PROPERTY - has private field
        public ShapeStates State
        {
            get => _state;
            set
            {
                _state = value;
                OnEdited();
            }
        }
        public PointMy Point
        {
            get => _point;
            set
            {
                _point = value;
                OnEdited();
            } }

        //PUBLIC FIELDS
        public void Delete()
        {
            throw new System.NotImplementedException();
        }

        //EVENT PARTS
        public event NoAtributeEventHandler Edited;
        protected virtual void OnEdited()
        {
            Edited?.Invoke();
        }
        
        //PRIVATE FIELDS
        private ShapeStates _state;
        private PointMy _point;

    }
}