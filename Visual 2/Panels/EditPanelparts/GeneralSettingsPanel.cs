using System.Windows.Controls;
using Engine.ShapeColections;

namespace Visual.Panels.EditPanelparts
{
    public class GeneralSettingsPanel : StackPanel
    {
        public GeneralSettingsPanel(ShapeColection shapeColection)
        {
            Children.Add(new EditLambdaPanel(shapeColection));
        }
    }
}