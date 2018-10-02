using System.Collections.Generic;
using Engine.Shapes;

namespace Engine.ShapeColection
{
    public partial class ShapeColection : IShapeColection
    {
        //CTORS

        //PROPERTYS
        //todo Get rid of this
        public IShape SelectedShape
        {
            get => _selectedShape;
            set
            {
                //TODO 
                //if (!_storage.Contains(value))
                //    throw new Iso13370Exception("Selecting shape which is not in list");

                if (_selectedShape != null)
                    _selectedShape.ShapeEdited -= OnShapeColectionChanged;
                _selectedShape = value;
                value.ShapeEdited += OnShapeColectionChanged;
                OnShapeColectionChanged();
            }
        }

        //PUBLIC METHODS
        //todo Get rid of this
        public void SelectShape(IShape shape)
        {
            SelectedShape = shape;
        }

        public void Add(IShape shape)
        {
            _storage.Add(shape);
            shape.ShapeEdited += OnShapeColectionChanged;
            OnShapeColectionChanged();
        }

        public void Remove(IShape shape)
        {
           shape.ShapeEdited -= OnShapeColectionChanged;
            _storage.Remove(shape);
            if (shape == SelectedShape)
                _selectedShape = null;
            OnShapeColectionChanged();
        }

        public void Clear()
        {
            foreach (IShape shape in _storage)
                shape.ShapeEdited -= OnShapeColectionChanged;

            _storage.Clear();
            OnShapeColectionChanged();
        }

        //EVENT STUFF
        public event ShapeColectionEventHandler Changed;
        protected virtual void OnShapeColectionChanged()
        {
            Changed?.Invoke(this);
        }

        //PRIVATE PART
        private readonly List<IShape> _storage = new List<IShape>();
        //todo Get rid of this
        private IShape _selectedShape;

    }
}   