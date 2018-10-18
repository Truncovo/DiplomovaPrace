using System.Windows;
using System.Windows.Controls;

namespace Visual.Presets
{
    public class PresetTextBlock: TextBlock
    {
        public PresetTextBlock(string text)
        {
            Text = text;
            Margin = new Thickness(4);
        }
    }
}