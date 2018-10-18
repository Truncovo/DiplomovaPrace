using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Visual.Presets
{
    public class DoubleBox : TextBox
    {
        public DoubleBox()
        {
            Width = 50;
            Margin = new Thickness(4);

        }

        public double Number
        {
            get
            {
                Double.TryParse(Text,out var res);
                return res;
            }
            set => Text = value.ToString(CultureInfo.InvariantCulture);
        }

        protected override void OnPreviewTextInput(TextCompositionEventArgs e)
        {
            base.OnPreviewTextInput(e);
            e.Handled = Regex.IsMatch(e.Text, "[^0-9,.,-]+");
            

        }
    }
}