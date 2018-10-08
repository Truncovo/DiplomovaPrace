using System.Windows.Controls;
using Engine.ShapeColections;

namespace Visual.Panels
{
    public class MainEditPanel : StackPanel
    {
        private readonly ShapeColection _shapeColection;


        public MainEditPanel(ShapeColection shapeColection)
        {
            _shapeColection = shapeColection;
            _shapeColection.Edited += OnShapeColectionEdited;

            this.Orientation = Orientation.Vertical;

            Children.Add(new CreatePanel(_shapeColection));
            Children.Add(new EditShapePanel(_shapeColection));
            Children.Add(new EditShapesPanel(_shapeColection));
            Children.Add(new EditEdgesPanel(_shapeColection));

        }

        private void OnShapeColectionEdited()
        {

        }
    }
}