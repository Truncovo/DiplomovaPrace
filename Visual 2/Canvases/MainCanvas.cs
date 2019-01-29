using System.Windows.Controls;
using System.Windows.Input;
using Engine.ShapeColections;

namespace Visual.Canvases
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
            ShapeCanvas.Scale = ZoomCoeficient; 
            foreach (var shape in _shapeColection.GetShapes())
                Children.Add(new ShapeCanvas(shape,ZoomCoeficient));
            CalculateSize();
        }

        public void CalculateSize()
        {
            var point = _shapeColection.MaxXY();
            Height = point.Y* ZoomCoeficient;
            Width = point.X* ZoomCoeficient;
        }

        private double ZoomCoeficient = 30;
        public void WheelSpinedWhileAltPressed(MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
                ZoomCoeficient *= 1.1d;
            else
                ZoomCoeficient /= 1.1d;
            RenderShapes();
        }
    }
}