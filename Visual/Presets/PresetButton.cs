using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Visual
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

