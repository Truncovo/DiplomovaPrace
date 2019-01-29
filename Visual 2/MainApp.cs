using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Engine;
using Engine.Shapes;
using Engine.XyObjects;
using Visual.Canvases;
using Visual.Panels;
using Border = System.Windows.Controls.Border;
using ShapeColection = Engine.ShapeColections.ShapeColection;

namespace Visual
{
    class MainApp
    {
        [STAThread]
        public static void Main()
        {
            var app = new Application();
            Window shapeWindow = new MainWindow();
            app.MainWindow = shapeWindow;
            app.Run(shapeWindow);
        }
    }

    public class  ScrollingCanvas: ScrollViewer
    {
        private MainCanvas _c;
        public ScrollingCanvas(MainCanvas canvas)
        {
            HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            _c = canvas;

            Content = new GridToScroll(canvas);
        }
        

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftAlt) || Keyboard.IsKeyDown(Key.RightAlt))
            {
                _c.WheelSpinedWhileAltPressed(e);
                _c.CalculateSize();

                e.Handled = true;
            }

            base.OnMouseWheel(e);
        }
    }

    public class GridToScroll : Grid
    {
        private MainCanvas _c;

        public GridToScroll(MainCanvas canvas)
        {
            this.Margin = new Thickness(5);
            _c = canvas;
            Children.Add(canvas);
            this.VerticalAlignment = VerticalAlignment.Top;
            this.HorizontalAlignment = HorizontalAlignment.Left;
        }
    }

    public class GrayBorder : Border
    {
        public static Brush Line = Brushes.Black;
        public static Brush Bg = Brushes.Gray;
        public static Brush TableBg = Brushes.Cornsilk;

        public GrayBorder(UIElement element, bool line = true)
        {
            BorderBrush = Bg;
            BorderThickness = new Thickness(5);
            var secBorder = new Border();

            secBorder.Child = element;
            Child = secBorder;
            secBorder.BorderBrush = Line;
            if(line)
                secBorder.BorderThickness = new Thickness(1);
        }
    }

    class MainWindow : Window
    {

        public MainWindow()
        {
            this.Title = "JTDP 2019 - Jakub Truneček diplomová práce";

            var shapeColection = new ShapeColection();

            var square = new Square(shapeColection);
            square.Point = new PointMy(0,0);
            square.Size = new SizeMy(20,20);

            var mainGrid = new Grid();
            Content = new GrayBorder( mainGrid, false);
            mainGrid.ColumnDefinitions.Add(new ColumnDefinition{ Width = new GridLength(100,GridUnitType.Star) });
            mainGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(300) });

            //create border for main canvas
            var border = new Border();
            var canvas = new MainCanvas(shapeColection);

            mainGrid.Children.Add(new GrayBorder(new ScrollingCanvas(canvas)));

            var mainEditPanel = (new EditPanel(shapeColection));
            mainGrid.Children.Add(mainEditPanel);
            Grid.SetColumn(mainEditPanel,1);
        }
    }
}
