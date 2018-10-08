using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using Engine.Shapes;

namespace Engine.ShapeColections
{
    public partial class ShapeColection : IShapeColection 
    {
        //CTORS

        //PROPERTYS

        public void Add(IShape shape)
        {
            _storage.Add(shape);
            shape.Edited += OnShapeColectionEdited;
            OnShapeColectionChanged();
        }

        public void Remove(IShape shape)
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

        public int CountOfShapes(ShapeStates state)
        {
            return _storage.Aggregate(0, (i, s) => i + (s.State == state?1:0));
        }

        public int CountOfEdges(ShapeStates state)
        {
            return _storage.Aggregate(0, (i, s) => i + s.EdgesCount(state));
        }

        public void SetSelectedShapes(Skladba skladba)
        {
            foreach (var shape in _storage)
                if (shape.State == ShapeStates.Selected)
                    shape.Skladba = skladba;
        }

        public void DeleteShape(IShape shape)
        {
            _storage.Remove(shape);
            OnShapeColectionChanged();
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