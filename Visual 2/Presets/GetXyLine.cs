using System.Windows.Controls;
using Engine.XyObjects;

namespace Visual.Presets
{
    public class GetXyLine : Grid
    {
        public PointMy Point => new PointMy(_pointX.Number, _pointY.Number);
        protected readonly DoubleBox _pointX;
        protected readonly DoubleBox _pointY;

        public GetXyLine(string text,double x, double y):this(text)
        {
            _pointX.Text = x.ToString();
            _pointY.Text = y.ToString();
        }

        public GetXyLine(string text)
        {
            ColumnDefinitions.Add(Settings.GetColumnDefinitionStar(0.5));
            ColumnDefinitions.Add(Settings.GetColumnDefinitionStar(0.3));
            ColumnDefinitions.Add(Settings.GetColumnDefinitionStar(0.8));
            ColumnDefinitions.Add(Settings.GetColumnDefinitionStar(0.3));
            ColumnDefinitions.Add(Settings.GetColumnDefinitionStar(0.8));

            int row = 0;
            var textBlock = new PresetTextBlock(text);
            Settings.PlaceInGridAndAdd(textBlock, this, row, 0);

            textBlock = new PresetTextBlock("X:");
            Settings.PlaceInGridAndAdd(textBlock, this, row, 1);

            _pointX = new DoubleBox();
            Settings.PlaceInGridAndAdd(_pointX, this, row, 2);

            textBlock = new PresetTextBlock("Y:");
            Settings.PlaceInGridAndAdd(textBlock, this, row, 3);
            _pointY = new DoubleBox();

            Settings.PlaceInGridAndAdd(_pointY, this, row, 4);
        }

        

        public delegate void GetXyLineEventHandler(object sender);


        public void Reset()
        {
            _pointY.Text = "";
            _pointX.Text = "";
        }
    }

}