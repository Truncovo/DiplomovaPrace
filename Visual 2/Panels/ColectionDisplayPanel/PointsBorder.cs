using System.Windows;
using System.Windows.Controls;
using Engine.Shapes;

namespace Visual.Panels.ColectionDisplayPanel
{
    public class PointsBorder : Border
    {
        public PointsBorder(IShape shape)
        {

            BorderThickness = new Thickness(10);
            BorderBrush = shape.State == ShapeStates.Selected ?BrushesFtd.ShapeSelected: BrushesFtd.ShapeNotSelected;
            var sp = new StackPanel();
            Child = sp;

            for (int i = 0; i < shape.Points.Count; i++)
                sp.Children.Add(new PointStackPanel(shape.Points[i], shape.EdgeParams[i]));
        }
    }
}