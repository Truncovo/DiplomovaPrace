using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Engine;
using Engine.Shapes;
using ShapeColection = Engine.ShapeColections.ShapeColection;

namespace Visual
{
    public class NewShapePanel : StackPanel,IShapeInput
    {
        private ShapeColection _shapeColection;

        private Polygon _shape
        {
            get => _shapeColection.SelectedShape as Polygon;
            set => _shapeColection.SelectedShape = value;
        }
    

        private readonly List<GetXyLineWithDelete> _lineList = new List<GetXyLineWithDelete>();

        private readonly PresetButton _addButton;
        private readonly OkResetGrid _okResetGrid;

        public NewShapePanel(ShapeColection shapeColection)
        {
            _shapeColection = shapeColection;
            _shape = new Polygon();

            //add title
            Children.Add(new PresetTextBlock(Texts.ShapeWindow_Title));

            //adding 3 get point lines
            FillWithDefault3Lines();

            //adding button to add line
            _addButton = new PresetButton("add new point");
            _addButton.Click += OnAddButtonClick;
            Children.Add(_addButton);

            //create ok and reset button
            _okResetGrid = new OkResetGrid();
            Children.Add(_okResetGrid);
            _okResetGrid.OkButtonClicked += OnOkButtonClicked;
            _okResetGrid.ResetButtonClicked += OnResetButtonClick;

            OnValuesChanged(this);

        }

        private void OnOkButtonClicked(object sender, RoutedEventArgs e)
        {
            _shapeColection.Add(_shape);
            _shape= new Polygon();
            Reset();
        }

        private void OnResetButtonClick(object sender, RoutedEventArgs e)
        {
            Reset();
        }
        public void Reset()
        {
            Children.Clear();
            _lineList.Clear();

            Children.Add(new PresetTextBlock(Texts.ShapeWindow_Title));
            FillWithDefault3Lines();
            Children.Add(_addButton);
            Children.Add(_okResetGrid);

            OnValuesChanged(this);
        }



        private void OnAddButtonClick(object sender, RoutedEventArgs e)
        {
            Children.Remove(_addButton);
            Children.Remove(_okResetGrid);

            GetXyLineWithDelete getPointsLine = new GetXyLineWithDelete(Texts.ShapeWindow_point);
            getPointsLine.DeleteButtonClicked += OnDeleteButtonClick;
            getPointsLine.ValuesChanged += OnValuesChanged;
            Children.Add(getPointsLine);
            _lineList.Add(getPointsLine);
            Children.Add(_addButton);
            Children.Add(_okResetGrid);

            OnValuesChanged(this);

        }


        private void OnDeleteButtonClick(object sender)
        {
            Children.Remove(sender as GetXyLineWithDelete);
            _lineList.Remove(sender as GetXyLineWithDelete);
            OnValuesChanged(this);
        }

        private void OnValuesChanged(object sender)
        {
            bool isMoreThen3 = _lineList.Count > 3;

            foreach (GetXyLineWithDelete line in _lineList)
                line.ActiceDeleteButton = isMoreThen3;

            SetShapeeRight();

        }

        private void SetShapeeRight()
        {
            _shape.Clear();
            foreach (GetXyLineWithDelete line in _lineList)
            {
                _shape.Add(line.Point);
            }
        }

        private void FillWithDefault3Lines()
        {
            for (int i = 0; i < 3; i++)
            {
                GetXyLineWithDelete getPointsLine = new GetXyLineWithDelete(Texts.ShapeWindow_point);
                getPointsLine.DeleteButtonClicked += OnDeleteButtonClick;
                Children.Add(getPointsLine);
                _lineList.Add(getPointsLine);
                getPointsLine.ValuesChanged += OnValuesChanged;
            }
        }

        
    }
}