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
        public void SetStateToAllChilds(ShapeStates state)
        {
            State = state;
        }

        public IEnumerable<INode> Childs => new List<INode>();
        public INode Parent => ShapeParent;
        public IShape ShapeParent { get; }

        public EdgeValues EdgeValues { get; set; }
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
        private double _edgeValue; 
        public double EdgeValue
        {
            get => _edgeValue;
            set
            {
                _edgeValue = value;
                OnEdited();
            }
        }

        //PUBLIC FIELDS
        public void DeleteYourself()
        {
            throw new System.NotImplementedException();
        }

        public override string ToString()
        {
            return "EV: " + EdgeValue;
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