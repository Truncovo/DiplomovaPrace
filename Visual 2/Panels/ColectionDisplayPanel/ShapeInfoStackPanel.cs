using System.Windows.Controls;
using Engine.Shapes;

namespace Visual.Panels.ColectionDisplayPanel
{
    public class ShapeInfoStackPanel : StackPanel
    {
        public ShapeInfoStackPanel(IShape shape)
        {
            Orientation = Orientation.Vertical;
            

            Children.Add(new TextBlock { Text = "SHAPE:POLYGON V:" + shape.Skladba.Value });

            if(shape.Points.Count != shape.EdgeParams.Count)
                Children.Add(new TextBlock
                {
                    Text = "POINTS: " + shape.Points.Count + " " + shape.EdgeParams.Count,
                });
            else
                Children.Add(new TextBlock { Text = "POINTS: " + shape.Points.Count });

            Children.Add(new TextBlock { Text = "POINTS: " + shape.Skladba });

            Children.Add(new PointsBorder(shape));


            Background = shape.State == ShapeStates.Selected ? BrushesFtd.ShapeSelected: BrushesFtd.ShapeNotSelected;
        }

    }
}