using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;
using Engine.ShapeColections;
using Engine.Shapes;
using Visual.Panels.EditPanelparts;

namespace Visual.Panels
{
    public sealed class EditPanel : Border
    {
        private readonly ShapeColection _shapeColection;

        private Grid _grid;
        private int positionInGrid;
        public EditPanel(ShapeColection shapeColection)
        {
            _grid = new Grid();
            Child = _grid;
            positionInGrid = 0;

            _shapeColection = shapeColection;

            _grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(60, GridUnitType.Auto) });
            Settings.PlaceInGridAndAdd(new GrayBorder(
                new PanelBox(
                    new EngineSettingsPanel(_shapeColection), 
                    shapeColection,
                    () => true,
                    "Hlavní nastavení"
                    )), _grid, positionInGrid++, 0);

            _grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(40+250, GridUnitType.Auto) });
            Settings.PlaceInGridAndAdd(new GrayBorder(
                new PanelBox(
                    new EditShapePanel(_shapeColection),
                    shapeColection,
                    () => (shapeColection.GetShapes(ShapeStates.Selected).Count() == 1), 
                    "Vlastnosti jedné části",
                    "Vyberte pouze jeden dvar"
                )), _grid, positionInGrid++, 0);

            _grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(40, GridUnitType.Auto) });
            Settings.PlaceInGridAndAdd(new GrayBorder(
                new PanelBox(
                    new EditShapesPanel(_shapeColection),
                    shapeColection,
                    () => (shapeColection.GetShapes(ShapeStates.Selected).Any()),
                    "Vlastnosti více částí",
                    "Vyberte alespoň jeden tvar"
                    )), _grid, positionInGrid++, 0);


            _grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(250, GridUnitType.Auto) });
            Settings.PlaceInGridAndAdd(new GrayBorder(
                new PanelBox(
                    new EditEdgesPanel(_shapeColection),
                    _shapeColection,
                    () => _shapeColection.GetEdges(ShapeStates.Selected).Any(),
                    "Vlastnosti více hran",
                    "Vyberte alespoň jendu hranu"
                )), _grid, positionInGrid++, 0);

            _grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(250, GridUnitType.Star) });
            var rectangle = new Rectangle();
            rectangle.Fill = GrayBorder.Bg;
            Settings.PlaceInGridAndAdd(rectangle,_grid, positionInGrid++, 0);
        }

        private bool showed = false;
        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            if(Keyboard.IsKeyDown(Key.P))
                if (Keyboard.IsKeyDown(Key.R))
                    if (Keyboard.IsKeyDown(Key.E))
                        if (!showed)

                        {
                            _grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(70, GridUnitType.Auto) });
                            Settings.PlaceInGridAndAdd(new GrayBorder(new PresetPanel(_shapeColection)), _grid, positionInGrid++, 0);
                            showed = true;
                        }

            base.OnPreviewKeyDown(e);
        }
    }
}