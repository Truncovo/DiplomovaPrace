using System.Windows;
using System.Windows.Controls;
using Engine;
using Engine.ShapeColections;
using Engine.Shapes;
using Visual.Presets;

namespace Visual.Panels.EditPanelparts
{
    public class EditShapesPanel : Grid
    {
        private readonly ShapeColection _shapeColection;
        private readonly DoubleBox _doubleBox;

        public EditShapesPanel(ShapeColection shapeColection)
        {

            _shapeColection = shapeColection;
            _shapeColection.Edited += OnShapeColectionEdited;

            ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(600, GridUnitType.Star) });
            ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(300, GridUnitType.Star) });
            ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(300, GridUnitType.Star) });
            ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(200, GridUnitType.Star) });

            Settings.PlaceInGridAndAdd(new PresetTextBlock(Texts.RValue), this, 0, 0);
            _doubleBox = new DoubleBox();
            Settings.PlaceInGridAndAdd(_doubleBox, this, 0, 1);

            Settings.PlaceInGridAndAdd(new PresetTextBlock(Texts.RUnit), this, 0, 2);

            var okButton = new PresetButton("OK");
            okButton.Click += OnOkButtonClicked;
            Settings.PlaceInGridAndAdd(okButton, this, 0, 3);
            OnShapeColectionEdited();
        }

        private void OnOkButtonClicked(object sender, RoutedEventArgs e)
        {
            var skladbaValue = _doubleBox.Number;
            foreach (var shape in _shapeColection.GetShapes(ShapeStates.Selected))
            { 
                shape.Skladba.Value = skladbaValue;
            }
        }

        private void OnShapeColectionEdited()
        {
            if (_shapeColection.CountOfShapes(ShapeStates.Selected) == 0)
            {
                _doubleBox.IsEnabled = false;
                _doubleBox.Text = "";
                return;
            }
            _doubleBox.IsEnabled = true;

            bool firstIteration = true;
            Skladba skladba = new Skladba();
            foreach (var shape in _shapeColection.GetShapes(ShapeStates.Selected))
            {
                if (firstIteration)
                {
                    skladba = shape.Skladba;
                    firstIteration = false;
                }
                if (!shape.Skladba.Equals(skladba))
                { 
                    _doubleBox.Text = "";
                    return;
                }
            }
            if (firstIteration)
            {
                _doubleBox.Text = "";
                return;
            }
            _doubleBox.Number = skladba.Value;
        }
    }
}