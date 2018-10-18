using System.Windows.Controls;
using Engine.ShapeColections;
using Engine.Shapes.ShapeParts;

namespace Visual.Panels.EditPanelparts
{
    public class EditEdgesPanel : StackPanel
    {
        public EditEdgesPanel(ShapeColection shapeColection)
        {
            Children.Add(new EditEdgeValuePanel(shapeColection,EdgeValuesPropertyes.Psi));
            Children.Add(new EditEdgeValuePanel(shapeColection, EdgeValuesPropertyes.PsiEdge));
            Children.Add(new EditEdgeValuePanel(shapeColection, EdgeValuesPropertyes.WallThickness));
        }

    }
}