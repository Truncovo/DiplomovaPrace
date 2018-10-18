using System.Linq;
using System.Windows.Controls;
using Engine.ShapeColections;
using Engine.Shapes;

namespace Visual.Panels.InfoPanelParts
{
    //Panel - shows general info about colection
    public class ShapeColectionHeadInfo : StackPanel
    {
        public ShapeColectionHeadInfo(ShapeColection shapeColection)
        {
            Orientation = Orientation.Vertical;

            Children.Add(new TextBlock{Text = "SHAPE COLECTION"});

            //add info about shapes count
            Children.Add(new TextBlock {
                Text = "shapes: " +
                       shapeColection.CountOfShapes(ShapeStates.Selected) +
                       "/"  +
                       shapeColection.GetShapes().Count()
            });

            //add info about edges count
            Children.Add(new TextBlock
            {
                Text = "edges: " +
                       shapeColection.CountOfEdges(ShapeStates.Selected) +
                       "/" +
                       (shapeColection.CountOfEdges(ShapeStates.Selected) + shapeColection.CountOfEdges(ShapeStates.Basic) )
            });


            Background = BrushesFI.Colection;

        }
    }
}