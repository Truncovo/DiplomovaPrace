using System;
using System.Collections.Generic;
using System.Linq;
using Engine.Shapes;

namespace Engine.ShapeColections
{
    public partial class ShapeColection : IShapeColection 
    {
        public ShapeColection()
        {
            _colectionValues = new ColectionValues();
            _colectionValues.Edited += OnShapeColectionEdited;
        }

        public void AddShape(IShape shape)
        {
            _shapes.Add(shape);
            shape.Edited += OnShapeColectionEdited;
            OnShapeColectionChanged();
        }
        public void RemoveShape(IShape shape)
        {
           shape.Edited -= OnShapeColectionEdited;
            _shapes.Remove(shape);
            OnShapeColectionChanged();
        }
        public void Clear()
        {
            foreach (IShape shape in _shapes)
                shape.Edited -= OnShapeColectionChanged;

            _shapes.Clear();
            OnShapeColectionChanged();
        }

        public void Optimize()
        {
            int count;
            do
            {
                count = _shapes.Count;
                try
                {
                    for (int i = 0; i < _shapes.Count; i++)
                        _shapes[i].Optimize();
                }
                catch (ArgumentOutOfRangeException)
                {
                }
            } while (count != _shapes.Count);


        }

        public ColectionValues ColectionValues
        {
            get => _colectionValues;
            set
            {
                _colectionValues.Edited -= OnShapeColectionChanged;
                _colectionValues = value;
                _colectionValues.Edited += OnShapeColectionChanged;
                OnShapeColectionChanged();
            }
        }

        public void SetSelectedShapes(Skladba skladba)
        {
            foreach (var shape in _shapes)
                if (shape.State == ShapeStates.Selected)
                    shape.Skladba = skladba;
        }

        public void SplitEdgesForCalculation()
        {
            var startTime = DateTime.UtcNow;
            foreach (var shape in _shapes)
                shape.InvokeEdtited = false;

            foreach (var shape in _shapes)
                shape.SetAllEdgesToNotInContact();
            int counter = 0;
            for (int i = 0; i < _shapes.Count; i++)
            for (int x = i+1; x < _shapes.Count; x++)
                counter +=_shapes[i].SplitEdges(_shapes[x]);


            Optimize();

            foreach (var shape in _shapes)
                shape.InvokeEdtited = true;
            OnShapeColectionEdited();
            var Time = DateTime.UtcNow - startTime;
            Console.WriteLine("edges splitted {0} times time: {1}", counter,Time);

        }

        public int IndexOf(IShape shape) => _shapes.IndexOf(shape);
        

        public void ChangeShapes(IShape toDelete, IShape toAdd)
        {
            var index = _shapes.IndexOf(toDelete);
            ChangeShapes(index, toAdd);
        }

        public void ChangeShapes(int index, IShape toAdd)
        {
            _shapes[index].Edited -= OnShapeColectionEdited;
            var shapeStates = _shapes[index].State;
            _shapes[index] = toAdd;
            _shapes[index].Edited += OnShapeColectionEdited;
            _shapes[index].State = shapeStates;
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


        private readonly List<IShape> _shapes = new List<IShape>();
        private ColectionValues _colectionValues;

    }
}   