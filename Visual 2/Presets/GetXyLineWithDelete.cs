using System.Windows;

namespace Visual2
{
    public class GetXyLineWithDelete : GetXyLine
    {
        public event GetXyLineEventHandler DeleteButtonClicked;
        private  PresetButton _deleteButton;
        public bool ActiceDeleteButton
        {
            get => _deleteButton.IsEnabled;
            set => _deleteButton.IsEnabled = value;
        }    
            
        public GetXyLineWithDelete(string text) : base(text)
        {
            CTOROnly();
        }

        public GetXyLineWithDelete(string text, double x, double y) : base(text, x, y)
        {
            CTOROnly();
        }

        private void CTOROnly()
        {
            ColumnDefinitions.Add(Settings.GetColumnDefinitionStar(0.5));
            _deleteButton = new PresetButton(Texts.DeleteButtonText);
            Settings.PlaceInGridAndAdd(_deleteButton, this, 0, 5);

            _deleteButton.Click += OnDeleteButtonClick;
        }
        protected virtual void OnDeleteButtonClick(object sender, RoutedEventArgs routedEventArgs)
        {
            DeleteButtonClicked?.Invoke(this);
        }
    }
}