using System;
using System.Windows;
using System.Windows.Controls;
using ShapeColection = Engine.ShapeColections.ShapeColection;

namespace Visual
{

    class MainApp
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            Window shapeWindow = new MainWindow();
            app.Run(shapeWindow);
        }
    }

    class MainWindow : Window
    {
        private readonly DockPanel _dockPanel;
        private readonly ShapeColection _shapeColection;
        private readonly ToolBarPanel _toolBarPanel;
        private readonly StackPanel _toolStackPanel;

        
        private readonly ShapeListPanel _shapeList;

        private IShapeInput _shapeInput;
        public MainWindow()
        {
            _shapeColection = new ShapeColection();

            _dockPanel = new DockPanel();
            Content = _dockPanel;

            //only to fill top
            ToolStackPanel toolStackPanel = new ToolStackPanel();
            DockPanel.SetDock(toolStackPanel,Dock.Top);
            _dockPanel.Children.Add(toolStackPanel);


            _toolStackPanel = new StackPanel();
            DockPanel.SetDock(_toolStackPanel, Dock.Left);
            _dockPanel.Children.Add(_toolStackPanel);

            _toolBarPanel = new ToolBarPanel();
            _toolBarPanel.ToolSwitched += OnToolSwitched;
            _toolStackPanel.Children.Add(_toolBarPanel);

            _shapeInput = new NewSquarePanel(_shapeColection);
            _toolStackPanel.Children.Add((NewSquarePanel) _shapeInput);

            _shapeList = new ShapeListPanel(_shapeColection);
            _shapeList.ToolSwitched += OnToolSwitched;
            _toolStackPanel.Children.Add(_shapeList);

            _dockPanel.Children.Add((new MainCanvas(_shapeColection)));



        }

        public void OnToolSwitched(ToolStates toolState)
        {

            _toolStackPanel.Children.Clear();
            _toolStackPanel.Children.Add(_toolBarPanel);

            switch (toolState)
            {
                case ToolStates.NewShape:
                    _shapeInput = new NewShapePanel(_shapeColection);
                    _toolStackPanel.Children.Add((NewShapePanel) _shapeInput);
                    break;
                case ToolStates.NewRectangle:
                    _shapeInput = new NewSquarePanel(_shapeColection);
                    _toolStackPanel.Children.Add((NewSquarePanel)_shapeInput);
                    break;
                case ToolStates.SelectedRectangle:
                    _toolBarPanel.EnableBothButtons();
                    _toolStackPanel.Children.Add(new EditSquarePanel(_shapeColection));

                    break;
                case ToolStates.SelectedShape:
                    _toolStackPanel.Children.Add(new EditShapePanel(_shapeColection));
                    _toolBarPanel.EnableBothButtons();
                    break;
                default:
                    throw new ArgumentOutOfRangeException("there is somthing other than p olyline and rectangle");
                    

            }

            _toolStackPanel.Children.Add(_shapeList);

        }
    }

    public enum ToolStates
    {
        NewShape, NewRectangle, SelectedRectangle, SelectedShape
    }

    public delegate void StateSwitcherEventHandler(ToolStates state );

  
}
