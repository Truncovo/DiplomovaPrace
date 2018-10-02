using System.Windows.Controls;
using Engine;
using Engine.ShapeColection;
using Engine.Shapes;

namespace Visual
{
    class ShapeListPanel: StackPanel
    {
        private readonly ShapeColection _shapeColection;
        public ShapeListPanel(ShapeColection shapeColection)
        {
            _shapeColection = shapeColection;
            shapeColection.ShapeColectionChanged += OnShapeColectionChanged;
        }



        public void OnShapeColectionChanged(object source)
        {
            var enumerable = _shapeColection.GetColection();
            Children.Clear();
            foreach (IShape shape in enumerable)
            {
                ShapeVisualLine shapeVisual = new ShapeVisualLine(shape, _shapeColection);
                shapeVisual.ShapeSelected += OnShapeSelected;
                Children.Add(shapeVisual);
            }
        }

        private void OnShapeSelected(IShape shapees)
        {
            if (shapees is Square square)
            {
                _shapeColection.SelectedShape = square;
                OnToolSwitched(ToolStates.SelectedRectangle);
                return;
            }

            if (shapees is Polygon shape)
            {
                _shapeColection.SelectedShape = shape;
                OnToolSwitched(ToolStates.SelectedShape);
            }
        }

        public event StateSwitcherEventHandler ToolSwitched;



        protected virtual void OnToolSwitched(ToolStates state)
        {
            ToolSwitched?.Invoke(state);
        }
    }
}