using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Engine.Shapes;
using Visual.Panels;
using Visual.Panels.ColectionDisplayPanel;
using ShapeColection = Engine.ShapeColections.ShapeColection;

namespace Visual2
{

    class MainApp
    {
        [STAThread]
        public static void Main()
        {
            var app = new Application();
            Window shapeWindow = new MainWindow();
            app.Run(shapeWindow);
        }
    }

    class MainWindow : Window
    {

        private readonly ShapeColection _shapeColection;
        private Grid _mainGrid;

        //tmp to visualise


        public MainWindow()
        {
            _shapeColection = new ShapeColection();

            var polygon = new Polygon(_shapeColection);
            polygon.Add(0,0);
            polygon.Add(0, 200);
            polygon.Add(200, 200);
            polygon.Add(200, 0);

            polygon = new Polygon(_shapeColection);
            polygon.Add(200, 200);
            polygon.Add(200, 400);
            polygon.Add(400, 400);
            polygon.Add(400, 200);

            _mainGrid = new Grid();
            Content = _mainGrid;
            _mainGrid.ColumnDefinitions.Add(new ColumnDefinition{Width = new GridLength(1300)});
            _mainGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(250) });
            _mainGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(250) });


            var border = new Border();
            border.Child = new MainCanvas(_shapeColection);
            border.BorderThickness = new Thickness(30);
            border.BorderBrush = Brushes.BlanchedAlmond;
            _mainGrid.Children.Add(border);

            var mainEditPanel = new MainEditPanel(_shapeColection);
            _mainGrid.Children.Add(mainEditPanel);
            Grid.SetColumn(mainEditPanel,1);

            var shapeColectionDisplayStackPanel = new ShapeColectionDisplayPanel(_shapeColection);     
            _mainGrid.Children.Add(shapeColectionDisplayStackPanel);
            Grid.SetColumn(shapeColectionDisplayStackPanel, 3);



        }

    }
}
