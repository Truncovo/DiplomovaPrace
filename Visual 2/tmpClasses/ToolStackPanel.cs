using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Visual2
{
    class ToolStackPanel : StackPanel
    {
        public ToolStackPanel()
        {
            Children.Add(new Rectangle
            {
                Fill =  Brushes.BlueViolet,
                Height = 80
                
            });
        }
    }
}