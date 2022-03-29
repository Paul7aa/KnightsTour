using System;
using System.Collections.Generic;
using System.Windows.Shapes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using Knights_Tour.Models;

namespace Knights_Tour.Controls
{
    public class DynamicGrid : UniformGrid
    {

        public static readonly DependencyProperty RowsColumnsProperty =
            DependencyProperty.Register("RowsColumns", typeof(int), typeof(DynamicGrid), new PropertyMetadata(0, RowsColumnsChanged));

        public static readonly DependencyProperty CellCollectionProperty =
            DependencyProperty.Register("CellCollection", typeof(CellCollectionModel), typeof(DynamicGrid), new PropertyMetadata(new CellCollectionModel(8), CellCollectionChanged));

        public DynamicGrid()
        {
            CellCollection = new CellCollectionModel(8);
        }

        public int RowsColumns
        {
            get { return (int)GetValue(RowsColumnsProperty); }
            set
            {
                SetValue(RowsColumnsProperty, value);
            }
        }

        public CellCollectionModel CellCollection
        {
            get { return (CellCollectionModel)GetValue(CellCollectionProperty); }
            set
            {
                SetValue(CellCollectionProperty, value);
            }
        }

        private static void RowsColumnsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DynamicGrid grid = (DynamicGrid)d;
            if (grid.RowsColumns > 11)
            {
                grid.Width = grid.RowsColumns * 32.5;
                grid.Height = grid.RowsColumns * 32.5;
            }
            else
            {
                grid.Width = grid.RowsColumns * 50;
                grid.Height = grid.RowsColumns * 50;
            }
            grid.Rows = grid.RowsColumns + 1;
            grid.Columns = grid.RowsColumns + 1;
            grid.Children.Clear();
            grid.CellCollection = new CellCollectionModel(grid.RowsColumns);
            SetGridColours(d);
        }

        private static void SetGridColours(DependencyObject d){
            DynamicGrid grid = (DynamicGrid)d;
            for (int i = 0; i < grid.Rows; i++)
            {
                for (int j = 0; j < grid.Columns; j++)
                {
                    if ((i == grid.Rows - 1 && j != grid.Columns - 1) || (i != grid.Rows - 1 && j == grid.Columns - 1))
                    {
                        TextBlock textBlock = new TextBlock();
                        textBlock.FontSize = 20;
                        if (i == grid.Rows - 1)
                            textBlock.Text = ((char)(j + 65)).ToString();
                        else
                            textBlock.Text = (grid.RowsColumns - i).ToString();
                        textBlock.TextAlignment = TextAlignment.Center;
                        textBlock.VerticalAlignment = VerticalAlignment.Center;
                        grid.Children.Add(textBlock);
                        Grid.SetRow(textBlock, i);
                        Grid.SetColumn(textBlock, j);
                        continue;
                    }

                    if (i == grid.Rows - 1 && j == grid.Columns - 1)
                        continue;

                    Rectangle newRectangle = new Rectangle();
                    if (i % 2 != 0 && j % 2 != 0 || i % 2 == 0 && j % 2 == 0)
                    {
                        newRectangle.Fill = new SolidColorBrush(Colors.White);
                        grid.CellCollection.Cells[i,j].CellColour = cellColour.white;
                    }
                    else
                    {
                        newRectangle.Fill = new SolidColorBrush(Colors.Black);
                        grid.CellCollection.Cells[i,j].CellColour = cellColour.black;
                    }
                    grid.CellCollection.Cells[i, j].CellState = cellState.notVisited;
                    newRectangle.Stroke = new SolidColorBrush(Colors.Black);
                    newRectangle.StrokeThickness = 2;
                    grid.Children.Add(newRectangle);
                    Grid.SetRow(newRectangle, i);
                    Grid.SetColumn(newRectangle, j);
                }
            }
        }

        private static void CellCollectionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DynamicGrid grid = (DynamicGrid)d;
            for (int i = 0; i < grid.RowsColumns-1; i++)
            {
                for (int j = 0; j < grid.RowsColumns-1; j++)
                {
                    if (grid.CellCollection.Cells[i, j].CellState == cellState.visited)
                    {
                        Rectangle newRectangle = new Rectangle();
                        newRectangle.Fill = new SolidColorBrush(Colors.GreenYellow);
                        grid.Children.Add(newRectangle);
                        Grid.SetRow(newRectangle, i);
                        Grid.SetColumn(newRectangle, i);
                    }
                    else
                    {
                        var element = grid.Children.Cast<UIElement>().
                            FirstOrDefault(e => Grid.GetColumn(e) == i && Grid.GetRow(e) == j);
                        if (element != null)
                            if (((SolidColorBrush)((Rectangle)element).Fill).Color == Colors.GreenYellow)
                            {
                                Rectangle newRectangle = new Rectangle();
                                switch (grid.CellCollection.Cells[i, j].CellColour)
                                {
                                    case cellColour.black:
                                        newRectangle.Fill = new SolidColorBrush(Colors.Black);
                                        grid.Children.Add(newRectangle);
                                        Grid.SetRow(newRectangle, i);
                                        Grid.SetColumn(newRectangle, i);
                                        break;
                                    case cellColour.white:
                                        newRectangle.Fill = new SolidColorBrush(Colors.White);
                                        grid.Children.Add(newRectangle);
                                        Grid.SetRow(newRectangle, i);
                                        Grid.SetColumn(newRectangle, i);
                                        break;
                                }
                            }
                    }
                }
            }
        }
    }
}
