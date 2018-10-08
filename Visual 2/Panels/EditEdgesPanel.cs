using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Engine.ShapeColections;
using Engine.Shapes;
using Engine.Shapes.ShapeParts;
using Visual2;

namespace Visual.Panels
{
    public class EditEdgesPanel : StackPanel
    {
        private readonly ShapeColection _shapeColection;
        private readonly DoubleBox _doubleBox;
        public EditEdgesPanel(ShapeColection shapeColection)
        {
            _shapeColection = shapeColection;
            _shapeColection.Edited += OnShapeColectionChanged;

            Orientation = Orientation.Horizontal;

            Children.Add(new PresetTextBlock("Edge value:"));

            _doubleBox = new DoubleBox();
            Children.Add(_doubleBox);

            var okButton = new PresetButton("OK");
            okButton.Click += OnOkButtonClicked;
            Children.Add(okButton);
            OnShapeColectionChanged();
                
        }

        private void OnOkButtonClicked(object sender, RoutedEventArgs e)
        {
            var num = _doubleBox.Number;
            foreach (var edgeParams in _shapeColection.GetEdges(ShapeStates.Selected))
            {
                Console.WriteLine(edgeParams);
                edgeParams.EdgeValue = num;

            }
        }

      

        private void OnShapeColectionChanged()
        {

            if (_shapeColection.CountOfEdges(ShapeStates.Selected) == 0)
            {
                Console.WriteLine("1");
                _doubleBox.IsEnabled = false;
                _doubleBox.Text = "";
                return;
            }
            _doubleBox.IsEnabled = true;
            _doubleBox.Text = ValueToBox();
        }

        private string ValueToBox()
        {
            bool firstIteration = true;
            double value = -1;
            
            foreach (var edgeParams in _shapeColection.GetEdges(ShapeStates.Selected))
            {
                if (firstIteration)
                {
                    value = edgeParams.EdgeValue;
                    firstIteration = false;
                    continue;
                }

                if (Math.Abs(value - edgeParams.EdgeValue) > 0.001)
                    return "";
            }
            return value.ToString();
        }
    }
}