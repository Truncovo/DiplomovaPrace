using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using Engine;
using Engine.ShapeColections;
using Engine.Shapes;
using Visual;
using Visual.Presets;

namespace Visual.Panels.EditPanelparts

{
    public class PanelBox : Grid
    {
        private bool _isHiden = false;
        private bool _isDisabled = false;
        public bool IsDisabled
        {
            get { return _isDisabled; }
            set
            {
                _isDisabled = value;
                ShowRightThing();


            }
        }

        private Panel _panel;
        private TextBlock _disableMessage;
        private TextBlock _colapseButton;
        private ShapeColection _shapeColection;
        private Func<bool> _shouldBeDisabled;
        public PanelBox(Panel panel,ShapeColection shapeColection, Func<bool> f ,string title = "empty title", string disbleMessage = "Window is disabled")
        {
            _panel = panel;
            _shapeColection = shapeColection;
            _shapeColection.Edited += OnColectionChanged;
            _shouldBeDisabled = f;

            ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(100, GridUnitType.Star) });
            ColumnDefinitions.Add(new ColumnDefinition{Width = new GridLength(25)});
            RowDefinitions.Add(new RowDefinition{Height = new GridLength(25)});
            RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });


            //title
            var textBlock = new TextBlock();
            textBlock.Background = Brushes.BurlyWood;
            textBlock.Text = title;
            textBlock.FontWeight = FontWeights.Bold;
            textBlock.FontSize = 12;
            textBlock.Padding = new Thickness(3);
            Settings.PlaceInGridAndAdd(textBlock, this,0,0);

            //colapse button
            _colapseButton = new TextBlock();
            _colapseButton.MouseLeftButtonUp += OnColapseButtonClick;
            _colapseButton.Background = Brushes.LightSkyBlue;
            _colapseButton.Text = "-";
            _colapseButton.FontWeight = FontWeights.Bold;
            _colapseButton.FontSize = 16;
            _colapseButton.TextAlignment = TextAlignment.Center;
            Settings.PlaceInGridAndAdd(_colapseButton, this,0,1);

            //disable message
            _disableMessage = new TextBlock();
            _disableMessage.Text = disbleMessage;
            _disableMessage.FontWeight = FontWeights.Bold;
            _disableMessage.FontSize = 12;
            _disableMessage.Padding = new Thickness(3,5,3,5);


            //panel settings
            _panel.Background = new SolidColorBrush(Color.FromRgb(200,200,200));
            Settings.PlaceInGridAndAdd(_panel,this,1,0,1,2);

            _isDisabled = _shouldBeDisabled();
            ShowRightThing();
        }

   

        private void OnColectionChanged()
        {
            _isDisabled = _shouldBeDisabled();
            ShowRightThing();
        }

        private void OnColapseButtonClick(object sender, MouseButtonEventArgs e)
        {
            _isHiden = !_isHiden;
            if(_isHiden)
            { 
                _colapseButton.Text = "+";
            }
            else
            { 
                _colapseButton.Text = "-";
            }

            ShowRightThing();
        }

        private void ShowRightThing()
        {
            Children.Remove(_panel);
            Children.Remove(_disableMessage);
            if (_isHiden)
                return;
            if(!_isDisabled)
                Settings.PlaceInGridAndAdd(_disableMessage, this, 1, 0, 1, 2);
            else
                Settings.PlaceInGridAndAdd(_panel, this, 1, 0, 1, 2);
        }

    }
}

    


    public class CreatePanel : Grid
    {
        private readonly IShapeColection _shapeColection;
        private PresetButton _squareButton;
        private PresetButton _polygonButton;

        public CreatePanel(IShapeColection shapeColection)
        {
            _shapeColection = shapeColection;
            _shapeColection.Edited += OnShapeColectionEdited;

            ColumnDefinitions.Add(new ColumnDefinition());
            ColumnDefinitions.Add(new ColumnDefinition());

            _squareButton = new PresetButton(Texts.NewSquare);
            Settings.PlaceInGridAndAdd(_squareButton,this,0,0);
            _squareButton.Click += OnSquareButtonClick;

            _polygonButton = new PresetButton(Texts.NewPolygon);
            _polygonButton.Click += OnPolygonButtonClick;
            Settings.PlaceInGridAndAdd(_polygonButton,this,0,1);

        }

        private void OnShapeColectionEdited()
        {

        }

        private void OnPolygonButtonClick(object sender, RoutedEventArgs e)
        {
            _shapeColection.SetStateToAllChilds(ShapeStates.Basic);
            var polygon = new Polygon(_shapeColection){State = ShapeStates.Selected};
            polygon.Add(0,0);
            polygon.Add(0, 5);
            polygon.Add(5, 5);
            polygon.SetStateToAllChilds(ShapeStates.Selected);


        }

        private void OnSquareButtonClick(object sender, RoutedEventArgs e)
        {
            _shapeColection.SetStateToAllChilds(ShapeStates.Basic);
            var square = new Square(_shapeColection) { State = ShapeStates.Selected };
            square.Size = new SizeMy(5,5);
            square.SetStateToAllChilds(ShapeStates.Selected);
        }
    }

