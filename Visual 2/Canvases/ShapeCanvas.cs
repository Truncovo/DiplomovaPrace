﻿using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Engine.Shapes;
using Engine.Shapes.ShapeParts;

namespace Visual.Canvases
{
    public class ShapeCanvas : Canvas
    {
        private readonly IShape _shape;
        private readonly List<Line> _lines = new List<Line>();
        public ShapeCanvas(IShape shape)
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

            if (_shape.Points.Count < 2)
                return;
            
            //put polyline on canvas (fill up shape with color)
            DisplayShape();

            //put line on canvas for each point on canvas
            for (int i = 0; i < _shape.Points.Count-1; i++)
                DisplayLine(_shape.Points[i], _shape.Points[i+1],_shape.EdgeParams[i]);

            //put last line between first and last point
            int lastIndex = _shape.Points.Count - 1;
            DisplayLine(_shape.Points[lastIndex], _shape.Points[0], _shape.EdgeParams[lastIndex]);
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

            foreach (var point in _shape.Points)
            {
                p.Points.Add(new Point(point.Point.X, point.Point.Y));
            }
            p.MouseDown += OnMouiseDownOnPolyline;
            
            Children.Add(p);
        }

        private void DisplayLine(NodePoint startPoint, NodePoint endPoint, EdgeParams edgeParams)
        {
            var line = new Line();
            line.X1 = startPoint.Point.X;
            line.Y1 = startPoint.Point.Y;
            line.X2 = endPoint.Point.X;
            line.Y2 = endPoint.Point.Y;

            //set style of line
            line.StrokeThickness = 6;

            if (edgeParams.State == ShapeStates.Basic)
                line.Stroke = Brushes.Blue;
            else
                line.Stroke = Brushes.SaddleBrown;

            line.MouseDown += OnMouseDownOnLine;
            _lines.Add(line);
            Children.Add(line);
        }

        private EdgeParams GetEdgeParamsFromLine(Line line)
        {
            return _shape.EdgeParams[_lines.IndexOf(line)];
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