using System.Windows.Controls;
using Engine.ShapeColections;

namespace Visual.Panels.ColectionDisplayPanel
{
    //Display all informations about colection 
    //- most of it in text, all object are recreated when colection changed

    public class ShapeColectionDisplayPanel : StackPanel
    {
        private readonly ShapeColection _shapeColection;

        public ShapeColectionDisplayPanel(ShapeColection shapeColection)
        {
            Orientation = Orientation.Vertical;


            _shapeColection = shapeColection;
            _shapeColection.Edited += Refil;
            Refil();
        }

        private void Refil()
        {
            Children.Clear();
            Children.Add(new ShapeColectionInfoStackPanel(_shapeColection));
            Children.Add(new ShapesInBorder(_shapeColection));
        }
    }
}