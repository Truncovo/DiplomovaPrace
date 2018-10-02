using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Engine;
using Engine.ShapeColection;
using Engine.Shapes;

namespace Visual
{
    public class ShapeVisualLine : StackPanel
    {
        private IShape _shape;
        private ShapeColection _shapeColection;
        public ShapeVisualLine(IShape shape, ShapeColection shapeColection)
        {
            _shapeColection = shapeColection;
            Orientation = Orientation.Horizontal;
            _shape = shape;
            var text = new PresetTextBlock(shape.ToString());     //TODO only squares 
            Children.Add(text);
            var deleteButton = new PresetButton("Delete");
            deleteButton.Click += OnDeleteButtonClick;
            Children.Add(deleteButton);

            if (_shapeColection.SelectedShape == _shape)
                Background = Brushes.Aquamarine;

        }

        private void OnDeleteButtonClick(object sender, RoutedEventArgs e)
        {
            _shapeColection.Remove(_shape);

        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            e.Handled = true;
            ShapeSelected?.Invoke(_shape);
            base.OnMouseDown(e);
        }

        public delegate void ShapeVisualLineEventHandler(IShape shape);

        public event ShapeVisualLineEventHandler ShapeSelected;
    }
}