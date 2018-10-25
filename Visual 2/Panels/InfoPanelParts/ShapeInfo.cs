using System;
using System.Windows.Controls;
using Engine.Shapes;

namespace Visual.Panels.InfoPanelParts
{
    public class ShapeInfo : StackPanel
    {
        public ShapeInfo(IShape shape)
        {
            Orientation = Orientation.Vertical;

            
            //title + info about Skladba
            if (shape is Square sq)
                Children.Add(new TextBlock { Text = "SQUARE Sk: " + shape.Skladba.Value + "\n" });
            else
                Children.Add(new TextBlock { Text = "POLYGON Sk: " + shape.Skladba.Value});


            //count of points and edgeParams
            if (shape.PointShells.Count != shape.EdgeShells.Count)
                Children.Add(new TextBlock
                {
                    Text = "POINTS: " + shape.PointShells.Count + " " + shape.EdgeShells.Count,
                });
            else
                Children.Add(new TextBlock { Text = "POINTS: " + shape.PointShells.Count });

            //add info about all points
            Children.Add(new PointsInfo(shape));

            //set background - based on shape state
            Background = shape.State == ShapeStates.Selected ? BrushesFI.ShapeSelected: BrushesFI.ShapeNotSelected;
        }

    }
}