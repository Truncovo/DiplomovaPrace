using System.Windows;
using System.Windows.Controls;
using Engine.Shapes;
using Engine.Shapes.ShapeParts;
using Engine.XyObjects;

namespace Visual.Presets
{
    public class SquarePointLine : GetXyLine
    {
        private readonly Square _square;

        public SquarePointLine(Square square) : base(Texts.SquarePointLine)
        {

            ColumnDefinitions.Clear();
            ColumnDefinitions.Add(Settings.GetColumnDefinitionStar(0.6));
            ColumnDefinitions.Add(Settings.GetColumnDefinitionStar(0.3));
            ColumnDefinitions.Add(Settings.GetColumnDefinitionStar(0.8));
            ColumnDefinitions.Add(Settings.GetColumnDefinitionStar(0.3));
            ColumnDefinitions.Add(Settings.GetColumnDefinitionStar(0.8));

            _square = square;
            _pointX.Number = _square.Point.X;
            _pointY.Number = _square.Point.Y;


            _pointX.TextChanged += OnTextChanged;
            _pointY.TextChanged += OnTextChanged;
    }


        

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            _square.Point = new PointMy(_pointX.Number, _pointY.Number);
        }
    }
}