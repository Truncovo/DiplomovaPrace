
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Engine.Shapes;
using Engine.XyObjects;
using ShapeColection = Engine.ShapeColections.ShapeColection;

namespace Visual
{
  

    public class MainCanvas : Canvas
    {
        private readonly ShapeColection _shapeColection;

        public enum ShapeStyle
        {
            Clasic, Selected, NotConfirmed
        }


        private void InsertShape(IShape shape, ShapeStyle shapeStyle)
        {
            if (shape == null)
                return;

            Polyline polyline = new Polyline();
            foreach (PointMy point in shape.Points)
                polyline.Points.Add(new Point(point.X,point.Y));
            foreach (PointMy point in shape.Points)
            { 
                polyline.Points.Add(new Point(point.X, point.Y));
                break;
            }
            polyline.StrokeThickness = 6;
            switch (shapeStyle)
            {
                case ShapeStyle.Clasic:
                    polyline.Fill = Brushes.Blue;
                    polyline.Stroke = Brushes.Black;
                    break;
                
                case ShapeStyle.Selected:
                    polyline.Fill = Brushes.Blue;
                    polyline.Stroke = Brushes.DarkOrange;
                    break;

                case ShapeStyle.NotConfirmed:
                    polyline.Stroke = Brushes.DarkOrange;
                    polyline.StrokeDashArray = new DoubleCollection { 3, 3 };
                    break;
            }

            Children.Add(polyline);

        }

        public MainCanvas(ShapeColection shapeColection)
        {
            _shapeColection = shapeColection;
            shapeColection.Changed += OnShapeColectionChanged;

            this.Background = Brushes.Aqua;
            this.HorizontalAlignment = HorizontalAlignment.Left;
            this.VerticalAlignment = VerticalAlignment.Top;
        }

        private void OnShapeColectionChanged(object source)
        {
            Children.Clear();
            foreach (IShape shape in _shapeColection.GetColection())
                    InsertShape(shape,shape == _shapeColection.SelectedShape?ShapeStyle.Selected:ShapeStyle.Clasic);

            InsertShape(_shapeColection.SelectedShape,ShapeStyle.NotConfirmed);

        }

        private double scale = 1;
        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            e.Handled = true;
            base.OnMouseWheel(e);
            Scale(e.Delta > 0 ? 0.9 : 1.1);

        }
        protected override Size MeasureOverride(Size constraint)

        {
            Console.WriteLine(constraint);
            Size availableSize = new Size(double.PositiveInfinity, double.PositiveInfinity);
            double maxHeight = 0;
            double maxWidth = 0;

            foreach (UIElement element in base.InternalChildren)
            {
                if (element != null)
                {
                    element.Measure(availableSize);
                    double left = Canvas.GetLeft(element);
                    if (Double.IsNaN(left))
                        left = 0;
                    double top = Canvas.GetTop(element);
                    if (Double.IsNaN(top))
                        top = 0;

                    left += element.DesiredSize.Width;
                    top += element.DesiredSize.Height;

                    maxWidth = maxWidth < left ? left : maxWidth;
                    maxHeight = maxHeight < top ? top : maxHeight;
                }
            }

            Height = maxHeight;
            Width = maxWidth;
            
            return new Size { Height = maxHeight, Width = maxWidth };
        }


        private void Scale(double i)
        {
            
            ScaleTransform myScaleTransform = new ScaleTransform();
            scale *= i;
            myScaleTransform.ScaleX = scale;
            myScaleTransform.ScaleY = scale;
            myScaleTransform.CenterX = 0;

            TransformGroup transformGroup = new TransformGroup();
            transformGroup.Children.Add(myScaleTransform);

            RenderTransform = transformGroup;
        }
    }
}