using System.Windows.Controls;
using Engine;
using Engine.XyObjects;

namespace Visual
{
    public class GetXyLine : Grid
    {
        public PointMy Point => new PointMy(_pointX.Number, _pointY.Number);
        private readonly DoubleBox _pointX;
        private readonly DoubleBox _pointY;

        public GetXyLine(string text,double x, double y):this(text)
        {
            _pointX.Text = x.ToString();
            _pointY.Text = y.ToString();
        }

        public GetXyLine(string text)
        {
            ColumnDefinitions.Add(Settings.GetColumnDefinitionStar(0.5));
            ColumnDefinitions.Add(Settings.GetColumnDefinitionStar(0.5));
            ColumnDefinitions.Add(Settings.GetColumnDefinitionStar(0.5));
            ColumnDefinitions.Add(Settings.GetColumnDefinitionStar(0.5));
            ColumnDefinitions.Add(Settings.GetColumnDefinitionStar(0.5));

            int row = 0;
            var textBlock = new PresetTextBlock(text);
            Settings.PlaceInGridAndAdd(textBlock, this, row, 0);

            textBlock = new PresetTextBlock("X:");
            Settings.PlaceInGridAndAdd(textBlock, this, row, 1);

            _pointX = new DoubleBox();
            _pointX.TextChanged += OnTextChanged;
            Settings.PlaceInGridAndAdd(_pointX, this, row, 2);

            textBlock = new PresetTextBlock("Y:");
            Settings.PlaceInGridAndAdd(textBlock, this, row, 3);
            _pointY = new DoubleBox();

            _pointY.TextChanged += OnTextChanged;
            Settings.PlaceInGridAndAdd(_pointY, this, row, 4);
        }

        

        public delegate void GetXyLineEventHandler(object sender);
        public event GetXyLineEventHandler ValuesChanged;
        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            ValuesChanged?.Invoke(this);
        }


        public void Reset()
        {
            _pointY.Text = "";
            _pointX.Text = "";
        }
    }

}