using System.Windows;
using System.Windows.Controls;
using Engine.Shapes;

namespace Visual.Panels.InfoPanelParts
{
    public sealed class PointsInfo : Border
    {
        public PointsInfo(IShape shape)
        {
            //set border
            BorderThickness = new Thickness(10);
            BorderBrush = shape.State == ShapeStates.Selected ?BrushesFI.ShapeSelected: BrushesFI.ShapeNotSelected;

            //stackPanel - filed with all points
            var stackPanel = new StackPanel();
            Child = stackPanel;

            for (int i = 0; i < shape.PointShells.Count; i++)
                stackPanel.Children.Add(new PointInfo(shape.PointShells[i], shape.EdgeShells[i]));
        }
    }
}