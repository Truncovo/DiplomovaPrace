using System.Collections.Generic;
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
            shape.Edited += OnShapeColectionChanged;
            OnShapeColectionChanged();
        }

        public void Remove(IShape shape)
        {
           shape.Edited -= OnShapeColectionChanged;
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

        //EVENT STUFF
        public event ShapeColectionEventHandler Changed;
        protected virtual void OnShapeColectionChanged()
        {
            Changed?.Invoke(this);
        }

        //PRIVATE PART

        private readonly List<IShape> _storage = new List<IShape>();

    }
}   