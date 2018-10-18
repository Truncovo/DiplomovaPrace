using System.Windows.Controls;
using Engine.ShapeColections;
using Visual.Panels.InfoPanelParts;

namespace Visual.Panels
{
    //Display all informations about colection 
    //- most of it in text, all object are recreated when colection changed

    public class InfoPanel: StackPanel
    {
        private readonly ShapeColection _shapeColection;

        public InfoPanel(ShapeColection shapeColection)
        {
            _shapeColection = shapeColection;
            _shapeColection.Edited += Refil;

            Orientation = Orientation.Vertical;
            Refil();
        }

        private void Refil()
        {
            Children.Clear();
            Children.Add(new ShapeColectionHeadInfo(_shapeColection));
            Children.Add(new AllShapesInfo(_shapeColection));
        }
    }
}