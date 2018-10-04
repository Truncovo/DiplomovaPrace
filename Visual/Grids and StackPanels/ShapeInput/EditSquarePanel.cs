using System.Windows;
using System.Windows.Controls;
using Engine;
using Engine.Shapes;
using ShapeColection = Engine.ShapeColections.ShapeColection;

namespace Visual
{
    internal class EditSquarePanel : StackPanel
    {
        private readonly ShapeColection _shapeColection;
        private Square _square
        {
            get => _shapeColection.SelectedShape as Square;
            set => _shapeColection.SelectedShape = value;
        }

        private readonly GetXyLine _pointLine;
        private readonly GetXyLine _sizeLine;

        public EditSquarePanel(ShapeColection shapeColection)
        {
            _shapeColection = shapeColection;
            //Ad Title
            Children.Add(new PresetTextBlock("Edit square"));

            //adding point and size line
            _pointLine = new GetXyLine(Texts.SqareWindow_Point, _square.Point.X, _square.Point.Y);
            Children.Add(_pointLine);
            _sizeLine = new GetXyLine(Texts.SquareWindow_Size, _square.Size.X, _square.Size.Y);
            Children.Add(_sizeLine);

            //subscribe to event, to be able to invoke ValuesChanged
            _pointLine.ValuesChanged += OnValuesChanged;
            _sizeLine.ValuesChanged += OnValuesChanged;

            //convert to polyline

            PresetButton button = new PresetButton("převeď na mnohouhelnik");
            button.Click += OnConvertButtonClicked;
            button.IsEnabled = false; //todo Enable this button
            Children.Add(button);
        }

        private void OnConvertButtonClicked(object sender, RoutedEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void OnValuesChanged(object sender)
        {
            _square.Point = _pointLine.Point;
            _square.Size = _sizeLine.Point.ToSize();
        }


    }
}