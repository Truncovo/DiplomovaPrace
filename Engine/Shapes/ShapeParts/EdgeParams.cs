using System.Collections.Generic;

namespace Engine.Shapes.ShapeParts
{
    public class EdgeParams : INode
    {
        //CTORS
        public EdgeParams(IShape parent)
        {
            _edgeValues = new EdgeValues();
            _edgeValues.Edited += OnEdited;

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

        private EdgeValues _edgeValues;
        public EdgeValues EdgeValues
        {
            get => _edgeValues;
            set
            {
                if(_edgeValues != null)
                    _edgeValues.Edited -= OnEdited;
                _edgeValues = value;
                _edgeValues.Edited += OnEdited;
                OnEdited();
            }
        }
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
    

        //PUBLIC FIELDS
        public void DeleteYourself()
        {
            throw new System.NotImplementedException();
        }

        public override string ToString()
        {
            return "EP: " + _edgeValues ;
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