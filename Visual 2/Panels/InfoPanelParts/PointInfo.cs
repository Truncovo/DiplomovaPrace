using System.Windows.Controls;
using Engine.Shapes;
using Engine.Shapes.ShapeParts;

namespace Visual.Panels.InfoPanelParts
{
    public class PointInfo : StackPanel
    {
        public PointInfo(PointShell point, EdgeShell edgeShell)
        {
            Children.Add(new TextBlock { Text = "[ " + point.Point.X + " , " + point.Point.Y + " ]"  + edgeShell });
            Background = edgeShell.State == ShapeStates.Selected ? BrushesFI.PointSelected : BrushesFI.PointNotSelected;
        }
    }
}