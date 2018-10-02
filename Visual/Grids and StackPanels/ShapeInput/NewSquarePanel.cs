 using System.Threading;
using System.Windows;
using System.Windows.Controls;
using Engine;
 using Engine.ShapeColection;
 using Engine.Shapes;

namespace Visual
{
  


    public class NewSquarePanel : StackPanel, IShapeInput
    {
        private readonly ShapeColection _shapeColection;

        private Square _square
        {
            get => _shapeColection.SelectedShape as Square;
            set => _shapeColection.SelectedShape = value;
        }

        private readonly GetXyLine _pointLine = new GetXyLine(Texts.SqareWindow_Point);
        private readonly GetXyLine _sizeLine = new GetXyLine(Texts.SquareWindow_Size);

        public NewSquarePanel(ShapeColection shapeColection)
        {
            _shapeColection = shapeColection;
            _square = new Square();
            //Ad Title
            Children.Add(new PresetTextBlock(Texts.SquareWindow_Title));
           
            //adding point and size line
            Children.Add(_pointLine);
            Children.Add(_sizeLine);

            //subscribe to event, to be able to invoke ValuesChanged
            _pointLine.ValuesChanged += OnValuesChanged;
            _sizeLine.ValuesChanged += OnValuesChanged;

            //create ok and reset button
            OkResetGrid okResetGrid = new OkResetGrid();
            Children.Add(okResetGrid);
            okResetGrid.OkButtonClicked += OnOkButtonClick;
            okResetGrid.ResetButtonClicked += OnResetButtonClick;
        }

        protected virtual void OnValuesChanged(object sender)
        {
            _square.Point = _pointLine.Point;
            _square.Size = _sizeLine.Point.ToSize();
        }

        private void OnOkButtonClick(object sender, RoutedEventArgs e)
        {
            _shapeColection.Add(_square);
            _square = new Square();
            Reset();
        }

        private void OnResetButtonClick(object sender, RoutedEventArgs e)
        {
            Reset();
        }

        public void Reset()
        {
            _square = new Square();
            _pointLine.Reset();
            _sizeLine.Reset();
            OnValuesChanged(this);
        }



      

        
    }
}
