using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Engine.ShapeColections;
using Engine.Shapes;
using Engine.Shapes.ShapeParts;
using Visual.Presets;

namespace Visual.Panels.EditPanelparts
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
                OneShapeSelected(_shapeShapeColection.GetShapes(ShapeStates.Selected).First());
                return;
            }

            NotOneShapesSelected();
        }

        private void NotOneShapesSelected()
        {
            _selectedShape = null;
            Children.Clear();
            Children.Add(GetTextBlock("Select"));
            Children.Add(GetTextBlock("one shape"));
            Children.Add(GetTextBlock("for shape editing"));

        }

        private TextBlock GetTextBlock(string text)
        {
            return new TextBlock
            {
                Text = text,
                FontSize = 25,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };
        }

        private void OneShapeSelected(IShape shape)
        {
            if (ReferenceEquals(shape, _selectedShape) && shape.PointShells.Count == (Children.Count -2) )
                return;

            if (ReferenceEquals(shape, _selectedShape) && _selectedShape is Square)
                return;

            Children.Clear();

            _selectedShape = shape;
            switch (shape)
            {
                case Square square:
                    OneSquareSelected(square);
                    break;
                case Polygon polygon:
                    OnePolygonSelected(polygon);
                    break;
            
            }
        }

        private PresetButton _addPointButton;
        private void OnePolygonSelected(Polygon polygon)
        {
            CreateTitleWithDeleteButton(Texts.PolygonSelected);
            
            foreach (var point in polygon.PointShells)
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
            var nodePoint = new PointShell(_selectedShape, 0, 0);

            (_selectedShape as Polygon).Add(nodePoint);
        }


        private void OneSquareSelected(Square square)
        {
            CreateTitleWithDeleteButton(Texts.SquareSelected);

            _selectedShape = square;

            Children.Add(new SquarePointLine(square));
            Children.Add(new SquareSizeLine(square));

            var button = new PresetButton("Convert to polygon");
            button.Click += OnConvertToPolygonClick;
            Children.Add(button);

        }

        private void OnConvertToPolygonClick(object sender, RoutedEventArgs e)
        {
            if(_selectedShape is Square square)
                square.ConvertToPolygon();
        }

        private void CreateTitleWithDeleteButton(string text)
        {
            var grid = new Grid();
            Children.Add(grid);

            grid.ColumnDefinitions.Add(Settings.GetColumnDefinitionStar(0.7));
            grid.ColumnDefinitions.Add(Settings.GetColumnDefinitionStar(0.3));

            var textBlock = new TextBlock {Text = text};
            textBlock.FontSize = 15;
            Settings.PlaceInGridAndAdd(textBlock, grid,0,0);
            var deleteButton = new PresetButton("Delete");
            deleteButton.Click += OnDeleteButtonClick;
            Settings.PlaceInGridAndAdd(deleteButton,grid,0,1);
        }

    }
}