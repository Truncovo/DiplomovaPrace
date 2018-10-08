using System.Windows;
using System.Windows.Controls;
using Engine.ShapeColections;
using Engine.Shapes;

namespace Visual.Panels.ColectionDisplayPanel
{
    public class ShapesInBorder : Border
    {
        private ShapeColection _shapeColection;

        public ShapesInBorder(ShapeColection shapeColection)
        {
            _shapeColection = shapeColection;
            BorderThickness = new Thickness(10);
            BorderBrush = BrushesFtd.Colection;
            var sp = new StackPanel();
            Child = sp;
            foreach (var shape in _shapeColection.GetColection())
            {
                sp.Children.Add(new ShapeInfoStackPanel((Polygon)shape));
            }
        }
    }
}