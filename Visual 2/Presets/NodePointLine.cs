using System.Windows;
using System.Windows.Controls;
using Engine.Shapes.ShapeParts;
using Engine.XyObjects;

namespace Visual.Presets
{
    public class NodePointLine : GetXyLine
    {

        private PresetButton _deleteButton;

        private PointShell _nodePoint;

        public bool ActiceDeleteButton
        {
            get => _deleteButton.IsEnabled;
            set => _deleteButton.IsEnabled = value;
        }

        public NodePointLine(PointShell nodePoint, string text) : base(text)
        {
            CTOROnly(nodePoint);
        }

        public NodePointLine(PointShell nodePoint,string text, double x, double y) : base(text, x, y)
        {
            CTOROnly(nodePoint);
        }

        private void CTOROnly(PointShell nodePoint)
        {
            _nodePoint = nodePoint;
            _pointX.Number = nodePoint.Point.X;
            _pointY.Number = nodePoint.Point.Y;

            ColumnDefinitions.Add(Settings.GetColumnDefinitionStar(0.5));
            _deleteButton = new PresetButton(Texts.DeleteButtonText);
            Settings.PlaceInGridAndAdd(_deleteButton, this, 0, 5);

            _deleteButton.Click += OnDeleteButtonClick;
            _pointX.TextChanged += OnTextChanged;
            _pointY.TextChanged += OnTextChanged;
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var point = new PointMy(_pointX.Number, _pointY.Number);
            _nodePoint.Point = point;
        }


        protected virtual void OnDeleteButtonClick(object sender, RoutedEventArgs routedEventArgs)
        {
            _nodePoint.DeleteYourself();
        }
    }
}