using System.Windows.Controls;
using Engine.ShapeColections;

namespace Visual2
{
    public class MainCanvas : Canvas
    {
        private readonly IShapeColection _shapeColection;
        public MainCanvas(IShapeColection shapeColection)
        {
            _shapeColection = shapeColection;
            _shapeColection.Edited += OnShapeColectionEdited;

            RenderShapes();
        }

        private void OnShapeColectionEdited()
        {
            RenderShapes();
        }

        private void RenderShapes()
        {
            Children.Clear();
            foreach (var shape in _shapeColection.GetColection())
                Children.Add(new ShapeCanvas(shape));
        }

    }
}