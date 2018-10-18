using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Visual.Canvases
{
    public class MainCanvasBoxing : ScrollViewer
    {
        public ShapeCanvas Canvass { get; }
        public MainCanvasBoxing(ShapeCanvas mainCanvas)
        {
            Canvass = mainCanvas;
            Content = Canvass;

            this.HorizontalAlignment = HorizontalAlignment.Left;
            this.VerticalAlignment = VerticalAlignment.Top;

            this.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            this.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            this.IsEnabled = true;

            Canvass.Background = Brushes.Aqua;
        }
    }
}