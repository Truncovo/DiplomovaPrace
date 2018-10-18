using System.Windows.Controls;
using Engine.Shapes;
using Engine.Shapes.ShapeParts;

namespace Visual.Panels.InfoPanelParts
{
    public class PointInfo : StackPanel
    {
        public PointInfo(NodePoint point, EdgeParams @params)
        {
            Children.Add(new TextBlock { Text = "[ " + point.Point.X + " , " + point.Point.Y + " ]"  + @params});
            Background = @params.State == ShapeStates.Selected ? BrushesFI.PointSelected : BrushesFI.PointNotSelected;
        }
    }
}