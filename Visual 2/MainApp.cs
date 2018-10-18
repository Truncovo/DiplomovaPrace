using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Engine.Shapes;
using Visual.Canvases;
using Visual.Panels;
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

    class MainWindow : Window
    {

        public MainWindow()
        {
            var shapeColection = new ShapeColection();

            //some content to shape colection for testing
            var polygon = new Polygon(shapeColection);
            polygon.Add(0,0);
            polygon.Add(0, 200);
            polygon.Add(200, 200);
            polygon.Add(200, 0);

            polygon = new Polygon(shapeColection);
            polygon.Add(200, 200);
            polygon.Add(200, 400);
            polygon.Add(400, 400);
            polygon.Add(400, 200);

            
            var mainGrid = new Grid();
            Content = mainGrid;
            mainGrid.ColumnDefinitions.Add(new ColumnDefinition{Width = new GridLength(900)});
            mainGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(300) });
            mainGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(250) });

            //create border for main canvas
            var border = new Border();
            border.Child = new MainCanvas(shapeColection);
            border.BorderThickness = new Thickness(20);
            border.BorderBrush = Brushes.BlanchedAlmond;
            mainGrid.Children.Add(border);

            var mainEditPanel = new EditPanel(shapeColection);
            mainGrid.Children.Add(mainEditPanel);
            Grid.SetColumn(mainEditPanel,1);

            var shapeColectionDisplayStackPanel = new InfoPanel(shapeColection);     
            mainGrid.Children.Add(shapeColectionDisplayStackPanel);
            Grid.SetColumn(shapeColectionDisplayStackPanel, 3);



        }

    }
}
