using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Engine.ShapeColections;
using Visual.Panels.EditPanelparts;

namespace Visual.Panels
{
    public sealed class EditPanel : Border
    {
        private readonly ShapeColection _shapeColection;


        public EditPanel(ShapeColection shapeColection)
        {
            BorderBrush = Brushes.BlanchedAlmond;
            BorderThickness = new Thickness(0,20,20,20);

            var grid = new Grid();
            Child = grid;
            int positionInGrid = 0;

            _shapeColection = shapeColection;
            _shapeColection.Edited += OnShapeColectionEdited;

            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(40) });
            Settings.PlaceInGridAndAdd(new EngineSettingsPanel(_shapeColection), grid, positionInGrid++, 0);


            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(20) });
            Settings.PlaceInGridAndAdd(new Rectangle { Fill = Brushes.BlanchedAlmond }, grid, positionInGrid++, 0);


            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(40) });
            Settings.PlaceInGridAndAdd(new GeneralSettingsPanel(_shapeColection), grid, positionInGrid++, 0);

            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(20) });
            Settings.PlaceInGridAndAdd(new Rectangle { Fill = Brushes.BlanchedAlmond }, grid, positionInGrid++, 0);

            //todo join to one panel
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(40) });
            Settings.PlaceInGridAndAdd(new CreatePanel(_shapeColection), grid, positionInGrid++, 0);
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(250) });
            Settings.PlaceInGridAndAdd(new EditShapePanel(_shapeColection), grid, positionInGrid++, 0);

            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(20) });
            Settings.PlaceInGridAndAdd(new Rectangle { Fill = Brushes.BlanchedAlmond }, grid, positionInGrid++, 0);

            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(28) });
            Settings.PlaceInGridAndAdd(new EditShapesPanel(_shapeColection), grid, positionInGrid++, 0);

            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(20) });
            Settings.PlaceInGridAndAdd(new Rectangle { Fill = Brushes.BlanchedAlmond }, grid, positionInGrid++, 0);


            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(250) });
            Settings.PlaceInGridAndAdd(new EditEdgesPanel(_shapeColection), grid, positionInGrid++, 0);
        }

        private void OnShapeColectionEdited()
        {

        }
    }
}