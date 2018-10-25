using System;
using System.Windows;
using System.Windows.Controls;
using Engine.ShapeColections;
using Engine.Shapes;
using Engine.Shapes.ShapeParts;
using Visual.Presets;

namespace Visual.Panels.EditPanelparts
{
    public class EditEdgeValuePanel : Grid
    {
        private readonly ShapeColection _shapeColection;
        private readonly DoubleBox _doubleBox;
        private readonly EdgeValuesPropertyes _type;

        public EditEdgeValuePanel(ShapeColection shapeColection, EdgeValuesPropertyes type)
        {
            _type = type;

            _shapeColection = shapeColection;
            _shapeColection.Edited += OnShapeColectionChanged;

            ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(600, GridUnitType.Star) });
            ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(300, GridUnitType.Star) });
            ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(300, GridUnitType.Star) });
            ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(200, GridUnitType.Star) });
      
            Settings.PlaceInGridAndAdd(new PresetTextBlock(GetValueText()), this, 0, 0);

            _doubleBox = new DoubleBox();
            Settings.PlaceInGridAndAdd(_doubleBox,this,0,1);

            Settings.PlaceInGridAndAdd(new PresetTextBlock(GetUnitText()), this, 0, 2);


            var okButton = new PresetButton("OK");
            okButton.Click += OnOkButtonClicked;
            Settings.PlaceInGridAndAdd(okButton, this, 0, 3);
            OnShapeColectionChanged();

            

                
        }

        private void OnOkButtonClicked(object sender, RoutedEventArgs e)
        {
            var num = _doubleBox.Number;
            foreach (var edgeParams in _shapeColection.GetEdges(ShapeStates.Selected))
                SetValue(edgeParams,num);
        }

        private void OnShapeColectionChanged()
        {

            if (_shapeColection.CountOfEdges(ShapeStates.Selected) == 0)
            {
                _doubleBox.IsEnabled = false;
                _doubleBox.Text = "";
                return;
            }
            _doubleBox.IsEnabled = true;
            _doubleBox.Text = ValueToBox();
        }


        private string ValueToBox()
        {
            bool firstIteration = true;
            double value = -1;
            
            foreach (var edgeParams in _shapeColection.GetEdges(ShapeStates.Selected))
            {
                if (firstIteration)
                {
                    value = GetValue(edgeParams);
                    firstIteration = false;
                    continue;
                }

                if (Math.Abs(value - GetValue(edgeParams)) > 0.001)
                    return "";
            }
            return value.ToString();
        }

        //switch methods

        private string GetUnitText()
        {
            switch (_type)  
            {
                case EdgeValuesPropertyes.WallThickness:
                    return Texts.WallThicknessUnit;
                case EdgeValuesPropertyes.Psi:
                case EdgeValuesPropertyes.PsiEdge:
                    return Texts.PsiUnit;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private string GetValueText()
        {
            switch (_type)
            {
                case EdgeValuesPropertyes.WallThickness:
                    return Texts.WallThickness;
                case EdgeValuesPropertyes.Psi:
                    return Texts.Psi;
                case EdgeValuesPropertyes.PsiEdge:
                    return Texts.PsiEdge;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }


        private void SetValue(EdgeShell edgeParams, double value)
        {
            switch (_type)
            {
                case EdgeValuesPropertyes.WallThickness:
                    edgeParams.EdgeValues.WallThickness = value;
                    break;
                case EdgeValuesPropertyes.Psi:
                    edgeParams.EdgeValues.Psi = value;
                    break;
                case EdgeValuesPropertyes.PsiEdge:
                    edgeParams.EdgeValues.PsiEdge = value;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private double GetValue(EdgeShell edgeParams)
        {
            switch (_type)
            {
                case EdgeValuesPropertyes.WallThickness:
                    return edgeParams.EdgeValues.WallThickness;
                case EdgeValuesPropertyes.Psi:
                    return edgeParams.EdgeValues.Psi;
                case EdgeValuesPropertyes.PsiEdge:
                    return edgeParams.EdgeValues.PsiEdge;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}