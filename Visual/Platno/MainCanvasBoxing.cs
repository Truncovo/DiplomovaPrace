using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Visual
{
    public class MainCanvasBoxing : ScrollViewer
    {
        public MainCanvas Canvass { get; }
        public MainCanvasBoxing(MainCanvas mainCanvas)
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