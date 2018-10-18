using System.Collections.Generic;
using System.Linq;
using Engine.Shapes;

namespace Engine.ShapeColections
{
    public partial class ShapeColection : IShapeColection 
    {
        public ShapeColection()
        {
            _colectionData = new ColectionData();
            _colectionData.Edited += OnShapeColectionEdited;
        }

        public void AddShape(IShape shape)
        {
            _storage.Add(shape);
            shape.Edited += OnShapeColectionEdited;
            OnShapeColectionChanged();
        }
        public void RemoveShape(IShape shape)
        {
           shape.Edited -= OnShapeColectionEdited;
            _storage.Remove(shape);
            OnShapeColectionChanged();
        }
        public void Clear()
        {
            foreach (IShape shape in _storage)
                shape.Edited -= OnShapeColectionChanged;

            _storage.Clear();
            OnShapeColectionChanged();
        }

        private ColectionData _colectionData;
        public ColectionData ColectionData
        {
            get => _colectionData;
            set
            {
                _colectionData.Edited -= OnShapeColectionChanged;
                _colectionData = value;
                _colectionData.Edited += OnShapeColectionChanged;
                OnShapeColectionChanged();
            }
        }

        public void SetSelectedShapes(Skladba skladba)
        {
            foreach (var shape in _storage)
                if (shape.State == ShapeStates.Selected)
                    shape.Skladba = skladba;
        }

        //EVENT STUFF
        public event ShapeColectionEventHandler ColectionChanged;

        protected virtual void OnShapeColectionEdited()
        {
            Edited?.Invoke();
        }
        protected virtual void OnShapeColectionChanged()
        {
            ColectionChanged?.Invoke(this);
            OnShapeColectionEdited();
        }

        //PRIVATE PART

        private readonly List<IShape> _storage = new List<IShape>();

    }
}   