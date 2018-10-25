using System.Collections.Generic;
using Engine.XyObjects;

namespace Engine.Shapes.ShapeParts
{
    public class PointShell : INode
    {
        //CTORS
        public PointShell(IShape parent)
        {
            ShapeParent = parent;
            _state = ShapeStates.Basic;
            
        }
        public PointShell(IShape parent, int x, int y):this(parent)
        {
            _point = new PointMy(x,y);
        }
        public PointShell(IShape parent, PointMy point) : this(parent)
        {
            _point = point;
        }

        //PROPERTY - tree parts
        public void SetStateToAllChilds(ShapeStates state)
        {
            State = state;
        }

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
        public void DeleteYourself()
        {
            ShapeParent.DeletePoint(this);
        }

        public override string ToString()
        {
            return "NODE POINT " + Point + " ST:" + State;
        }

        //EVENT PARTS
        public event NoAtributeEventHandler Edited;
        protected virtual void OnEdited()
        {
            Edited?.Invoke();
        }
        
        //PRIVATE FIELDS
        private ShapeStates _state;
        private PointMy _point = new PointMy(0,0);

    }
}