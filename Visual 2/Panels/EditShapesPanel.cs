using System.Windows;
using System.Windows.Controls;
using Engine;
using Engine.ShapeColections;
using Engine.Shapes;
using Visual2;

namespace Visual.Panels
{
    public class EditShapesPanel : StackPanel
    {
        private readonly ShapeColection _shapeColection;
        private readonly DoubleBox _doubleBox;

        public EditShapesPanel(ShapeColection shapeColection)
        {
            Orientation = Orientation.Horizontal;

            _shapeColection = shapeColection;
            _shapeColection.Edited += OnShapeColectionEdited;

            _doubleBox = new DoubleBox();
            Children.Add(_doubleBox);

            var okButton = new PresetButton("OK");
            okButton.Click += OnOkButtonClicked;
            Children.Add(okButton);
            OnShapeColectionEdited();
        }

        private void OnOkButtonClicked(object sender, RoutedEventArgs e)
        {
            var skladbaValue = _doubleBox.Number;
            foreach (var shape in _shapeColection.GetColection(ShapeStates.Selected))
            { 
                shape.Skladba.Value = skladbaValue;
            }
        }

        private void OnShapeColectionEdited()
        {
            bool firstIteration = true;
            Skladba skladba = new Skladba();
            foreach (var shape in _shapeColection.GetColection(ShapeStates.Selected))
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