using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Engine.ShapeColections;
using Engine.Shapes;
using Visual2;

namespace Visual.Panels
{
    public class CreatePanel : StackPanel
    {
        private readonly IShapeColection _shapeColection;
        private PresetButton _squareButton;
        private PresetButton _polygonButton;

        public CreatePanel(IShapeColection shapeColection)
        {
            _shapeColection = shapeColection;
            _shapeColection.Edited += OnShapeColectionEdited;

            Orientation = Orientation.Horizontal;

            _squareButton = new PresetButton(Texts.NewSquare);
            _squareButton.Click += OnSquareButtonClick;
            _squareButton.IsEnabled = false;
            Children.Add(_squareButton);

            _polygonButton = new PresetButton(Texts.NewPolygon);
            _polygonButton.Click += OnPolygonButtonClick;
            Children.Add(_polygonButton);


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

        }

        private void OnSquareButtonClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
