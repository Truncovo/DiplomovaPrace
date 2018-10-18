using System.Windows;
using System.Windows.Controls;

namespace Visual
{
    public static class Settings
    {
        public static RowDefinition GetRowDefinitionStar(double size)
        {
            RowDefinition rowDefinition = new RowDefinition();
            rowDefinition.Height = new GridLength(size, GridUnitType.Star);
            return rowDefinition;
        }

        public static ColumnDefinition GetColumnDefinitionStar(double size)
        {
            ColumnDefinition columnDefinition = new ColumnDefinition();
            columnDefinition.Width = new GridLength(size, GridUnitType.Star);
            return columnDefinition;
        }

        public static void PlaceInGrid(UIElement obj, int setRow, int setColumn, int setRowSpan = 1,
            int setColumnSpan = 1)
        {
            Grid.SetRow(obj, setRow);
            Grid.SetColumn(obj, setColumn);
            Grid.SetRowSpan(obj, setRowSpan);
            Grid.SetColumnSpan(obj, setColumnSpan);
        }

        public static void PlaceInGridAndAdd(UIElement obj, Grid grid, int setRow, int setColumn, int setRowSpan = 1,
            int setColumnSpan = 1)
        {
            grid.Children.Add(obj);
            PlaceInGrid(obj, setRow, setColumn, setRowSpan, setColumnSpan);
        }
    }
}