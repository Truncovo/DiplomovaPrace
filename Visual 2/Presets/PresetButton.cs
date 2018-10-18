using System.Windows;
using System.Windows.Controls;

namespace Visual.Presets
{
    public class PresetButton : Button
    {
        public PresetButton(string text)
        {
            Content = text;
            Margin = new Thickness(4);
            IsDefault = true;
        }
    }
}

