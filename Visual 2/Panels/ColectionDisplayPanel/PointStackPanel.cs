using System.Windows.Controls;
using Engine.Shapes;
using Engine.Shapes.ShapeParts;

namespace Visual.Panels.ColectionDisplayPanel
{
    public class PointStackPanel : StackPanel
    {
        public PointStackPanel(NodePoint point, EdgeParams @params)
        {
            Children.Add(new TextBlock { Text = "[ " + point.Point.X + " , " + point.Point.Y + " ]"  + @params});
            Background = @params.State == ShapeStates.Selected ? BrushesFtd.PointSelected : BrushesFtd.PointNotSelected;
        }
    }
}