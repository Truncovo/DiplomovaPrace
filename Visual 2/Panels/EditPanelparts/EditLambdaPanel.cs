using System.Windows;
using System.Windows.Controls;
using Engine.ShapeColections;
using Visual.Presets;

namespace Visual.Panels.EditPanelparts
{

    public class EditLambdaPanel : Grid
    {
        private readonly ShapeColection _shapeColection;

        public EditLambdaPanel(ShapeColection shapeColection)
        {
            _shapeColection = shapeColection;

            int columnPosition = 0;

            ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(600, GridUnitType.Star) });
            ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(300, GridUnitType.Star) });
            ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(300, GridUnitType.Star) });
            ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(200, GridUnitType.Star) });


            Settings.PlaceInGridAndAdd(new PresetTextBlock(Texts.Lambda),this,0,columnPosition++ );

            var doubleBox = new DoubleBox();
            doubleBox.TextChanged += OnTextChanged;
            Settings.PlaceInGridAndAdd(doubleBox,this,0,columnPosition++);

            Settings.PlaceInGridAndAdd(new PresetTextBlock(Texts.LambdaUnit), this, 0, columnPosition++);
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is DoubleBox doubleBox)
                _shapeColection.ColectionData.LamdaGround = doubleBox.Number;
        }
    }
}