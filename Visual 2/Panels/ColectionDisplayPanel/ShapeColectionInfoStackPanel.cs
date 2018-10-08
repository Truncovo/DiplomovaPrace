using System.Linq;
using System.Windows.Controls;
using Engine.ShapeColections;
using Engine.Shapes;

namespace Visual.Panels.ColectionDisplayPanel
{
    public class ShapeColectionInfoStackPanel : StackPanel
    {
        public ShapeColectionInfoStackPanel(ShapeColection shapeColection)
        {

            Orientation = Orientation.Vertical;

            Children.Add(new TextBlock{Text = "SHAPE COLECTION"});
            Children.Add(new TextBlock {
                Text = "shapes: " +
                       shapeColection.CountOfShapes(ShapeStates.Selected) +
                       "/"  +
                       shapeColection.GetColection().Count()
            });
            Children.Add(new TextBlock
            {
                Text = "edges: " +
                       shapeColection.CountOfEdges(ShapeStates.Selected) +
                       "/" +
                       (shapeColection.CountOfEdges(ShapeStates.Selected) + shapeColection.CountOfEdges(ShapeStates.Basic) + shapeColection.CountOfEdges(ShapeStates.Temporary))
            });


            Background = BrushesFtd.Colection;

        }
    }
}