using System;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Engine.ShapeColections;
using Engine.Shapes;
using Engine.Shapes.ShapeParts;
using Visual2;

namespace Visual.Panels
{
    public class EditShapePanel : StackPanel
    {
        private readonly IShapeColection _shapeShapeColection;
        private IShape _selectedShape;

        public EditShapePanel(IShapeColection shpaShapeColection)
        {
            _shapeShapeColection = shpaShapeColection;
            _shapeShapeColection.Edited += OnShapeColectionEdited;


        }

        private void OnShapeColectionEdited()
        {
            int count = _shapeShapeColection.CountOfShapes(ShapeStates.Selected);

            if(count == 1)
            { 
                OneShapeSelected(_shapeShapeColection.GetColection(ShapeStates.Selected).First());
                return;
            }

            NotOneShapesSelected();
        }

        private void NotOneShapesSelected()
        {
            _selectedShape = null;
            Children.Clear();
            Children.Add(new TextBlock {Text = Texts.SelectOnlyOneShape});
        }

        private void OneShapeSelected(IShape shape)
        {
            if (ReferenceEquals(shape, _selectedShape) && shape.Points.Count == (Children.Count -2) )
                return;
            Children.Clear();

            _selectedShape = shape;
            if (shape is Polygon polygon)
                OnePolygonSelected(polygon);
                
            if(shape is Square square)
                OneSquareSelected(square);
        }

        private PresetButton _addPointButton;
        private void OnePolygonSelected(Polygon polygon)
        {
            var title = new StackPanel();
            title.Orientation = Orientation.Horizontal;
            title.Children.Add(new TextBlock {Text = Texts.PolygonSelected});
            var deleteButton = new PresetButton("Delete");

            deleteButton.Click += OnDeleteButtonClick;
            title.Children.Add(deleteButton);
            Children.Add(title);
            foreach (var point in polygon.Points)
            {
                Children.Add(new NodePointLine(point, "Bod "));
            }
            _addPointButton = new PresetButton(Texts.AddPoint);
            _addPointButton.Click += OnAddPointButtonClick;
            Children.Add(_addPointButton);
        }

        private void OnDeleteButtonClick(object sender, RoutedEventArgs e)
        {
            _selectedShape?.DeleteYourself();
        }

        private void OnAddPointButtonClick(object sender, RoutedEventArgs e)
        {
            var nodePoint = new NodePoint(_selectedShape, 0, 0);

            (_selectedShape as Polygon).Add(nodePoint);
        }


        private void OneSquareSelected(Square polygon)
        {
            throw new NotImplementedException();
        }
    }
}