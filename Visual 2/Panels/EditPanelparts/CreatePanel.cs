using System;
using System.Windows;
using System.Windows.Controls;
using Engine.ShapeColections;
using Engine.Shapes;
using Visual.Presets;

namespace Visual.Panels.EditPanelparts

{
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
            polygon.Add(0, 0);
            polygon.Add(0, 0);
            polygon.SetStateToAllChilds(ShapeStates.Selected);


        }

        private void OnSquareButtonClick(object sender, RoutedEventArgs e)
        {
            _shapeColection.SetStateToAllChilds(ShapeStates.Basic);
            var square = new Square(_shapeColection) { State = ShapeStates.Selected };
            square.SetStateToAllChilds(ShapeStates.Selected);
        }
    }
}
