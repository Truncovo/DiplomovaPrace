using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Engine.ShapeColections;
using Engine.Shapes;
using Engine.Shapes.ShapeParts;
using Visual.Presets;

namespace Visual.Panels.EditPanelparts
{
    public class EditEdgesPanel : StackPanel
    {
        public EditEdgesPanel(ShapeColection shapeColection)
        {
            Children.Add(new EditEdgeValuePanel(shapeColection,EdgeValuesPropertyes.Psi));
            Children.Add(new EditEdgeValuePanel(shapeColection, EdgeValuesPropertyes.PsiEdge));
            Children.Add(new EditEdgeValuePanel(shapeColection, EdgeValuesPropertyes.WallThickness));
            Children.Add(new IsEdgeInContact(shapeColection));
        }
    }

    public class IsEdgeInContact : Grid
    {
        private readonly ShapeColection _shapeColection;
        private TripleSwitch _checkBox;

        public IsEdgeInContact(ShapeColection shapeColection)
        {
            _shapeColection = shapeColection;
            _shapeColection.Edited += OnShapeColectionChanged;
            _checkBox = new TripleSwitch(shapeColection);
            _checkBox.Margin = new Thickness(10,3,0,0);

            ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(600, GridUnitType.Star) });
            ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(300, GridUnitType.Star) });
            ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(300, GridUnitType.Star) });
            ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(200, GridUnitType.Star) });

            Settings.PlaceInGridAndAdd(new PresetTextBlock("V kontaktu s\nvytápěným prostorem:"), this, 0, 0);
            Settings.PlaceInGridAndAdd(_checkBox, this, 0, 1,1,3);

        }

        private void OnShapeColectionChanged()
        {
            bool first = true;
            bool? value = null;
            foreach (var edgeShell in _shapeColection.GetEdges(ShapeStates.Selected))
            {
                if (first)
                {
                    first = false;
                    value = edgeShell.EdgeValues.InContact;
                    continue;
                }

                if (value != edgeShell.EdgeValues.InContact)
                {
                    value = null;
                    break;
                }
            }
        }

        public class TripleSwitch : Grid
        {
            private List<TripleSwitchButton> buttons = new List<TripleSwitchButton>();
            private ShapeColection _shapeColection;
            public TripleSwitch(ShapeColection shapeColection)
            {
                _shapeColection = shapeColection;
                _shapeColection.Edited += OnShapeColectionEdited;
                var texts = new string[] { "NE", "mix", "ANO" };

                int size = 50;
                this.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(size + 5, GridUnitType.Pixel) });
                var btn = new TripleSwitchButton(_shapeColection, texts[0], false,this);
                buttons.Add(btn);
                Settings.PlaceInGridAndAdd(btn, this, 0, 0);

                this.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(size -5, GridUnitType.Pixel) });
                btn = new TripleSwitchButton(_shapeColection, texts[1], null,this);
                buttons.Add(btn);
                Settings.PlaceInGridAndAdd(btn, this, 0, 1);

                this.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(size + 5, GridUnitType.Pixel) });
                btn = new TripleSwitchButton(_shapeColection, texts[2], true,this);
                buttons.Add(btn);
                Settings.PlaceInGridAndAdd(btn, this, 0, 2);
            }

            public void OnShapeColectionEdited()
            {
                bool first = true;
                bool? value = null;
                foreach (var edgeShell in _shapeColection.GetEdges(ShapeStates.Selected))
                {
                    if (first)
                    {
                        first = false;
                        value = edgeShell.EdgeValues.InContact;
                        continue;
                    }

                    if (value != edgeShell.EdgeValues.InContact)
                    {
                        value = null;
                        break;
                    }
                }

                if (value == true)
                    Select(2);
                else if (value == false)
                    Select(0);
                else
                    Select(1);
            }

            private void Select(int i)
            {
                foreach (var btn in buttons)
                    btn.Selected = false;

                buttons[i].Selected = true;
            }
        }

        public class TripleSwitchButton : Border
        {
            public static Brush SelectedBrush = Brushes.BurlyWood;
            //public static Brush UnslectedBrush = Brushes.Aquamarine;
            public static Brush UnslectedBrush = Brushes.LightSkyBlue;

            private bool _selected = false;
            public bool Selected
            {
                get => _selected;
                set { 
                _selected = value;
                if(_selected)
                        FormatAsSelected();
                else
                    FormatAsNotSelected();
                }
            }

            private PresetTextBlock _textBlock;
            private ShapeColection _shapeColection;
            private bool? _value;
            private TripleSwitch _parent;
            public TripleSwitchButton(ShapeColection shapeColection,string text,bool? value, TripleSwitch parent)
            {
                _parent = parent;
                _value = value;
                _shapeColection = shapeColection;
                _textBlock =new PresetTextBlock(text);
                _textBlock.MinWidth = 60;
                _textBlock.MouseLeftButtonUp += OnMouseButtonUp;
                _textBlock.MaxHeight = 20;
                //MaxHeight = 20;
                Padding = new Thickness(0);

                Child = _textBlock;
                BorderBrush = Brushes.Black;

                int Bt= 2;
                int Marg = 5;
                if (_value == null)
                {
                    BorderThickness = new Thickness(0, Bt, 0, Bt);
                    Margin = new Thickness(0, 0, 0, Marg);

                }
                else if(_value == true)
                { 
                    BorderThickness = new Thickness(0, Bt, Bt, Bt);
                    Margin = new Thickness(0,0,Marg,Marg);

                }
                else
                {
                    BorderThickness = new Thickness(Bt, Bt, 0,Bt);
                    Margin = new Thickness(Marg,0,0,Marg);


                }
            }

            private void OnMouseButtonUp(object sender, MouseButtonEventArgs e)
            {
                if (_value == null)
                    return;
                bool tmp;
                if (_value == true)
                    tmp = true;
                else
                {
                    tmp = false;
                }
                foreach (var shape in _shapeColection.GetEdges(ShapeStates.Selected))
                {
                    shape.EdgeValues.InContact = tmp;
                }
                _parent.OnShapeColectionEdited();
            }

            private void FormatAsSelected()
            {
                _textBlock.Background = SelectedBrush;
                Background = SelectedBrush;
                _textBlock.Foreground = Brushes.Black;
            }

            private void FormatAsNotSelected()
            {
                if (_value == null)
                {
                    FormatAsNotSelectedNotClickable();
                    return;
                }
                _textBlock.Background = UnslectedBrush;
                Background = UnslectedBrush;
            }

            private void FormatAsNotSelectedNotClickable()
            {
                _textBlock.Background = UnslectedBrush;
                Background = UnslectedBrush;
                _textBlock.Foreground = UnslectedBrush;
            }
        }
    }
}