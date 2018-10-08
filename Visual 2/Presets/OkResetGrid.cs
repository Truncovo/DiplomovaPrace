using System.Windows;
using System.Windows.Controls;

namespace Visual2
{
    public class OkResetGrid: Grid
    {
        public OkResetGrid()
        {
            ColumnDefinitions.Add(Settings.GetColumnDefinitionStar(0.5));
            ColumnDefinitions.Add(Settings.GetColumnDefinitionStar(0.5));


            PresetButton resetButton = new PresetButton("reset");
            resetButton.Click += OnResetButtonClick;
            Settings.PlaceInGridAndAdd(resetButton, this, 0, 0);

            PresetButton okButton = new PresetButton("OK");
            okButton.Click += OnOkButtonClick;
            Settings.PlaceInGridAndAdd(okButton,this,0,1);


        }


        public event RoutedEventHandler OkButtonClicked;
        public event RoutedEventHandler ResetButtonClicked;


        protected virtual void OnOkButtonClick(object sender, RoutedEventArgs e)
        {
            OkButtonClicked?.Invoke(sender, e);

        }

        protected virtual void OnResetButtonClick(object sender, RoutedEventArgs e)
        {
            ResetButtonClicked?.Invoke(sender, e);
        }


    }
}