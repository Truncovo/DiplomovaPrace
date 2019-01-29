using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Engine;
using Engine.Logger;
using Engine.ShapeColections;
using Engine.Shapes;
using EngineUnitTests.ShapeColections;
using Visual.Presets;

namespace Visual.Panels.EditPanelparts
{
    public class PresetPanel : Grid
    {

        private readonly ShapeColection _shapeColection;

        public PresetPanel(ShapeColection shapeColection)
        {
            _shapeColection = shapeColection;
            for (int i = 1; i < 7; i++)
            {
                var button = new PresetButton("P: " + i);
                button.Click += OnPresetClick;
                this.ColumnDefinitions.Add(new ColumnDefinition{Width = new GridLength(2,GridUnitType.Star)});
                Settings.PlaceInGridAndAdd(button,this,0,i-1);
            }
        }

        private void OnPresetClick(object sender, RoutedEventArgs e)
        {
            int number;
            if (sender is Button button && button.Content is string str)
            {
                int.TryParse(str.Substring(3, 1), out number);
                switch (number)
                {
                    case 1:
                        ShapeColectionPresets.Example1(_shapeColection);
                        break;
                    case 2:
                        ShapeColectionPresets.Example2(_shapeColection);
                        break;
                    case 3:
                        ShapeColectionPresets.Example3(_shapeColection);
                        break;
                    case 4:
                        ShapeColectionPresets.Example4(_shapeColection);
                        break;
                    case 5:
                        ShapeColectionPresets.Example5(_shapeColection);
                        break;
                    case 6:
                        ShapeColectionPresets.Example6(_shapeColection);
                        break;
                }
            }
        }
    }

    public class EngineSettingsPanel : Grid
    {
        private readonly ShapeColection _shapeColection;

        public EngineSettingsPanel(ShapeColection shapeColection)
        {
            _shapeColection = shapeColection;

            ColumnDefinitions.Add(Settings.GetColumnDefinitionStar(50));
            ColumnDefinitions.Add(Settings.GetColumnDefinitionStar(50));
            RowDefinitions.Add(Settings.GetRowDefinitionStar(50));
            RowDefinitions.Add(Settings.GetRowDefinitionStar(50));
            RowDefinitions.Add(Settings.GetRowDefinitionStar(50));
            RowDefinitions.Add(Settings.GetRowDefinitionStar(50));

            var button2 = new PresetButton("Protokol");
            button2.Click += OnCalculationClick;
            Settings.PlaceInGridAndAdd(button2, this, 0, 0);

            var button = new PresetButton("Nápověda");
            button.Click += OnHelpButtonClick;
            Settings.PlaceInGridAndAdd(button, this, 0, 1);

            var _squareButton = new PresetButton(Texts.NewSquare);
            Settings.PlaceInGridAndAdd(_squareButton, this, 1, 0);
            _squareButton.Click += OnSquareButtonClick;

            var _polygonButton = new PresetButton(Texts.NewPolygon);
            _polygonButton.Click += OnPolygonButtonClick;
            Settings.PlaceInGridAndAdd(_polygonButton, this, 1, 1);

            var button3 = new PresetButton("Smazat");
            button3.Click += OnDeleteClick;
            Settings.PlaceInGridAndAdd(button3, this, 2, 0);

            var button4 = new PresetButton("Smazat vše");
            button4.Click += OnDeleteAllClick;
            Settings.PlaceInGridAndAdd(button4, this, 2, 1);

            Settings.PlaceInGridAndAdd(new EditLambdaPanel(shapeColection),this,3,0,1,2);
        }

        private void OnPolygonButtonClick(object sender, RoutedEventArgs e)
        {
            _shapeColection.SetStateToAllChilds(ShapeStates.Basic);
            var polygon = new Polygon(_shapeColection) { State = ShapeStates.Selected };
            polygon.Add(0, 0);
            polygon.Add(5, 0);
            polygon.Add(0, 5);
            polygon.SetStateToAllChilds(ShapeStates.Selected);
        }

        private void OnSquareButtonClick(object sender, RoutedEventArgs e)
        {
            _shapeColection.SetStateToAllChilds(ShapeStates.Basic);
            var square = new Square(_shapeColection);
            square.Size = new SizeMy(5, 5);
            square.State = ShapeStates.Selected;
            square.SetStateToAllChilds(ShapeStates.Selected);
        }

        private void OnDeleteAllClick(object sender, RoutedEventArgs e)
        {
            _shapeColection.Clear();
        }

        private void OnDeleteClick(object sender, RoutedEventArgs e)
        {
            var shapes = new List<IShape>();
            foreach (var shape in _shapeColection.GetShapes(ShapeStates.Selected))
            {
                shapes.Add(shape);
            }

            foreach (var shape in shapes)
            {
                shape.DeleteYourself();
            }
        }


        private void OnCalculationClick(object sender, RoutedEventArgs e)
        {
            try
            {
                ShapeColection calculatedShapeColection = (ShapeColection)_shapeColection.Clone();

                calculatedShapeColection.SplitEdgesForCalculation();
                Console.WriteLine("Edges splitted");

                var loger = new CalculationPdfLogger();

                loger.ShowPdf(calculatedShapeColection);

                CalculationLogger.ConsoleWriteShapeColectionLog(calculatedShapeColection);

            }
            catch (LambdaGround0Exception exception)
            {
                MessageBox.Show("Lambda nemůže být 0");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                MessageBox.Show("Části zadané podlahy musí tvořit pouze jeden víceúhelník a nesmí se překrývat");
            }
        }

        private void OnHelpButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                ;
                Process.Start(("Nápověda.pdf"));

            }
            catch (Exception exception)
            {
                MessageBox.Show("Nápověda není k dispozici.");
            }
        }
    }
}