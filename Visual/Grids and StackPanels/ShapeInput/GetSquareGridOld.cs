using System.Windows;
using System.Windows.Controls;
using Engine;
using Engine.Shapes;
using Engine.XyObjects;

namespace Visual
{
    public class GetSquareGridOld : Grid
    {
        private DoubleBox _pointX;
        private DoubleBox _pointY;
        private DoubleBox _sizeX;
        private DoubleBox _sizeY;

        public Square GetSqare()
        {
            PointMy point = new PointMy(_pointX.Number,_pointY.Number);
            SizeMy size = new SizeMy(_sizeX.Number,_sizeY.Number);
            return new Square(point, size);
        }

        public void Reset()
        {
            _pointY.Text = "";
            _pointX.Text = "";
            _sizeX.Text = "";
            _sizeY.Text = "";
        }

        public GetSquareGridOld()
        {
            ShowGridLines = true;

            RowDefinitions.Add(Settings.GetRowDefinitionStar(0.5));
            RowDefinitions.Add(Settings.GetRowDefinitionStar(0.5));
            RowDefinitions.Add(Settings.GetRowDefinitionStar(0.5));
            RowDefinitions.Add(Settings.GetRowDefinitionStar(0.5));

            ColumnDefinitions.Add(Settings.GetColumnDefinitionStar(0.5));
            ColumnDefinitions.Add(Settings.GetColumnDefinitionStar(0.5));
            ColumnDefinitions.Add(Settings.GetColumnDefinitionStar(0.5));
            ColumnDefinitions.Add(Settings.GetColumnDefinitionStar(0.5));
            ColumnDefinitions.Add(Settings.GetColumnDefinitionStar(0.5));

            //Title text
            var title = new PresetTextBlock(Texts.SquareWindow_Title);
            Settings.PlaceInGridAndAdd(title,this,0,0,1,4);

            int row = 1;
            var text = new PresetTextBlock(Texts.SqareWindow_Point);
            Settings.PlaceInGridAndAdd(text,this,row,0);

            text = new PresetTextBlock("X:");
            Settings.PlaceInGridAndAdd(text, this, row, 1);

            _pointX = new DoubleBox();
            Settings.PlaceInGridAndAdd(_pointX,this,row,2);

            text = new PresetTextBlock("Y:");
            Settings.PlaceInGridAndAdd(text, this, row, 3);


            _pointY = new DoubleBox();
            Settings.PlaceInGridAndAdd(_pointY, this, row, 4);

            row = 2;
            text = new PresetTextBlock(Texts.SquareWindow_Size);
            Settings.PlaceInGridAndAdd(text, this, row, 0);

            text = new PresetTextBlock("X:");
            Settings.PlaceInGridAndAdd(text, this, row, 1);

            _sizeX = new DoubleBox();
            Settings.PlaceInGridAndAdd(_sizeX, this, row, 2);

            text = new PresetTextBlock("Y:");
            Settings.PlaceInGridAndAdd(text, this, row, 3);

            _sizeY= new DoubleBox();
            Settings.PlaceInGridAndAdd(_sizeY, this, row, 4);


            //Button stack panel and buttons
            StackPanel buttonStackPanel = new StackPanel();
            buttonStackPanel.Orientation = Orientation.Horizontal;
            buttonStackPanel.HorizontalAlignment = HorizontalAlignment.Stretch;
            buttonStackPanel.VerticalAlignment= VerticalAlignment.Stretch;

            Settings.PlaceInGridAndAdd(buttonStackPanel,this,3,0,1,4);

            PresetButton okButton = new PresetButton("OK");
            okButton.Click += OnOkButtonClick;
            buttonStackPanel.Children.Add(okButton);

            PresetButton resetButton = new PresetButton("reset");
            resetButton.Click += OnResetButtonClick;
            buttonStackPanel.Children.Add(resetButton);
        }

        public delegate void ShapeWindowEventDelegate(object sender);
        public event ShapeWindowEventDelegate OkButtonClick;
        protected virtual void OnOkButtonClick()
        {
            OkButtonClick?.Invoke(this);
        }

        private void OnOkButtonClick(object sender, RoutedEventArgs e)
        {
            OnOkButtonClick();
        }

        private void OnResetButtonClick(object sender, RoutedEventArgs e)
        {
            Reset();
        }

        
    }
}