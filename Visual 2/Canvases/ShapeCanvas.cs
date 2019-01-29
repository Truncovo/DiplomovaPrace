using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Engine.Shapes;
using Engine.Shapes.ShapeParts;
using Visual.Presets;

namespace Visual.Canvases
{
    public class ShapeCanvas : Canvas
    {
        public static double Scale { get; set; } = 30;
        private readonly IShape _shape;
        private readonly List<Line> _lines = new List<Line>();
        public ShapeCanvas(IShape shape, double zoomCoeficient)
        {
            _shape = shape;
            _shape.Edited += OnShapeChanged;
            RenderShape();
        }

        private void OnShapeChanged()
        {
               RenderShape();
        }

        private void RenderShape()
        {
            _lines.Clear();
            Children.Clear();

            if (_shape.PointShells.Count < 2)
                return;
            
            //put polyline on canvas (fill up shape with color)
            DisplayShape();

            //put line on canvas for each point on canvas
            for (int i = 0; i < _shape.PointShells.Count-1; i++)
                DisplayLine(_shape.PointShells[i], _shape.PointShells[i+1],_shape.EdgeShells[i]);

            //put last line between first and last point
            int lastIndex = _shape.PointShells.Count - 1;
            DisplayLine(_shape.PointShells[lastIndex], _shape.PointShells[0], _shape.EdgeShells[lastIndex]);
        }

        private void DisplayShape()
        {
            var p = new Polyline();
            p.StrokeThickness = 6;
            p.FillRule = FillRule.EvenOdd;

            if (_shape.State == ShapeStates.Basic)
                p.Fill = Brushes.LightSkyBlue;
            else
                p.Fill = Brushes.BurlyWood;

            foreach (var point in _shape.PointShells)
            {
                p.Points.Add(new Point(point.Point.X*Scale, point.Point.Y*Scale));
            }
            p.MouseDown += OnMouiseDownOnPolyline;
            
            Children.Add(p);

            var rValue = new PresetTextBlock("R = " + _shape.Skladba.Value + " " + Texts.RUnit);
            var topLeftPoint = _shape.FirstPoint();
            Canvas.SetTop(rValue, topLeftPoint.Y * Scale);
            Canvas.SetLeft(rValue, topLeftPoint.X * Scale);

            Children.Add(rValue);
        }

        private void DisplayLine(PointShell startPoint, PointShell endPoint, EdgeShell edgeParams)
        {
            var line = new Line();
            line.X1 = startPoint.Point.X * Scale;
            line.Y1 = startPoint.Point.Y * Scale;
            line.X2 = endPoint.Point.X * Scale;
            line.Y2 = endPoint.Point.Y * Scale;

            //set style of line
            line.StrokeThickness = 5;

            if (edgeParams.State == ShapeStates.Basic)
                line.Stroke = Brushes.Blue;
            else
                line.Stroke = Brushes.SaddleBrown;

            line.MouseDown += OnMouseDownOnLine;
            _lines.Add(line);
            Children.Add(line);
        }

        private EdgeShell GetEdgeParamsFromLine(Line line)
        {
            return _shape.EdgeShells[_lines.IndexOf(line)];
        }

        private void OnMouiseDownOnPolyline(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                foreach (var child in _shape.Childs)
                    child.State = _shape.State;
                return;
            }

            if (_shape.State == ShapeStates.Basic)
                _shape.State = ShapeStates.Selected;
            else if (_shape.State == ShapeStates.Selected)
                _shape.State = ShapeStates.Basic;
        }

        private void OnMouseDownOnLine(object sender, MouseButtonEventArgs e)
        {
            if (!(sender is Line line))
                return;

            var edgeParam = GetEdgeParamsFromLine(line);

            if (e.ClickCount == 2)
            {
                foreach (var child in _shape.Childs)
                    child.State = edgeParam.State;
                return;
            }

            if (edgeParam.State == ShapeStates.Basic)
                edgeParam.State = ShapeStates.Selected;
            else if (edgeParam.State == ShapeStates.Selected)
                edgeParam.State = ShapeStates.Basic;
        }
    }
}