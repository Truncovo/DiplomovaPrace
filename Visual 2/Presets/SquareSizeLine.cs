using System.Windows.Controls;
using Engine;
using Engine.Shapes;

namespace Visual.Presets
{
    public class SquareSizeLine : GetXyLine
    {
        private readonly Square _square;

        public SquareSizeLine(Square square) : base(Texts.SquareSizeLine)
        {
            ColumnDefinitions.Clear();
            ColumnDefinitions.Add(Settings.GetColumnDefinitionStar(0.6));
            ColumnDefinitions.Add(Settings.GetColumnDefinitionStar(0.3));
            ColumnDefinitions.Add(Settings.GetColumnDefinitionStar(0.8));
            ColumnDefinitions.Add(Settings.GetColumnDefinitionStar(0.3));
            ColumnDefinitions.Add(Settings.GetColumnDefinitionStar(0.8));

            _square = square;
            _pointX.Number = _square.Size.X;
            _pointY.Number = _square.Size.Y;


            _pointX.TextChanged += OnTextChanged;
            _pointY.TextChanged += OnTextChanged;
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            _square.Size = new SizeMy(_pointX.Number, _pointY.Number);
        }
    }
}