using System.Windows;
using System.Windows.Controls;
using Engine.ShapeColections;
using Visual.Presets;

namespace Visual.Panels.EditPanelparts
{
    public class EngineSettingsPanel : Grid
    {
        private readonly ShapeColection _shapeColection;

        public EngineSettingsPanel(ShapeColection shapeColection)
        {
            _shapeColection = shapeColection;

            ColumnDefinitions.Add(Settings.GetColumnDefinitionStar(50));
            ColumnDefinitions.Add(Settings.GetColumnDefinitionStar(50));

            var button = new PresetButton("Optimize");
            button.Click += OnOptimaliseButtonClick;
            Settings.PlaceInGridAndAdd(button, this, 0, 0);

            var button2 = new PresetButton("Calculation");
            button2.Click += OnCalculationClick;
            Settings.PlaceInGridAndAdd(button2, this, 0, 1);

        }

        private void OnCalculationClick(object sender, RoutedEventArgs e)
        {
            _shapeColection.SplitEdgesForCalculation();
        }

        private void OnOptimaliseButtonClick(object sender, RoutedEventArgs e)
        {
            _shapeColection.Optimize();
        }
    }
}