using System.Windows;
using System.Windows.Controls;

namespace Visual
{
   
    class ToolBarPanel : Grid
    {
        private readonly PresetButton _rectangleButton;
        private readonly PresetButton _polylineButton;

       
        public ToolBarPanel()
        {
            ColumnDefinitions.Add(Settings.GetColumnDefinitionStar(0.5));
            ColumnDefinitions.Add(Settings.GetColumnDefinitionStar(0.5));

            _rectangleButton = new PresetButton("nový čtverec");
            _rectangleButton.Click += ButtonClick;
            _polylineButton = new PresetButton("nový víceuhelnik");
            _polylineButton.Click += ButtonClick;

            Settings.PlaceInGridAndAdd(_rectangleButton,this,0,0);
            Settings.PlaceInGridAndAdd(_polylineButton, this, 0, 1);

            _rectangleButton.IsEnabled = false;
            _polylineButton.IsEnabled = true;

        }

        public void EnableBothButtons()
        {
            _rectangleButton.IsEnabled = true;
            _polylineButton.IsEnabled = true;
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            _rectangleButton.IsEnabled = false;
            _polylineButton.IsEnabled = false;
            if (_rectangleButton == sender)
            { 
                OnToolSwitched(ToolStates.NewRectangle);
                _polylineButton.IsEnabled = true;

            }
            if (_polylineButton == sender)
            { 
                OnToolSwitched(ToolStates.NewShape);
                _rectangleButton.IsEnabled = true;

            }
        }

        public event StateSwitcherEventHandler ToolSwitched;


        protected virtual void OnToolSwitched(ToolStates state)
        {
            ToolSwitched?.Invoke(state);
        }
    }
}