using System.Windows;
using System.Windows.Controls;
using Engine.ShapeColections;
using Engine.Shapes;

namespace Visual.Panels.InfoPanelParts
{
    //bordered stackPanel - displays info about all shapes
    public sealed class AllShapesInfo : Border
    {
        public AllShapesInfo(ShapeColection shapeColection)
        {
            //set border
            BorderThickness = new Thickness(5);
            BorderBrush = BrushesFI.Colection;

            //create and set stack panel for all shapes
            var stackPanel = new StackPanel();
            Child = stackPanel;

            //fillup stackPanel
            foreach (var shape in shapeColection.GetShapes())
                stackPanel.Children.Add(new ShapeInfo (shape));
        }
    }
}