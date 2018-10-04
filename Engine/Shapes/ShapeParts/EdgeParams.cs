using System.Collections.Generic;

namespace Engine.Shapes.ShapeParts
{
    public class EdgeParams : INode
    {
        //CTORS
        public EdgeParams(IShape parent)
        {
            _state = ShapeStates.Basic;
            ShapeParent = parent;
        }

        //PROPERTY - tree parts
        public IEnumerable<INode> Childs => new List<INode>();
        public INode Parent => ShapeParent;
        public IShape ShapeParent { get; }

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

        //TODO create real settings
        private int _edgeValue;
        public int EdgeValue
        {
            get => _edgeValue;
            set
            {
                _edgeValue = value;
                OnEdited();
            }
        }

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
    }
}