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
using Knights_Tour.BaseModels;
using System.Windows.Media.Imaging;

namespace Knights_Tour.Controls
{
    public class DynamicGrid : UniformGrid
    {

        public static readonly DependencyProperty RowsColumnsProperty =
            DependencyProperty.Register("RowsColumns", typeof(int), typeof(DynamicGrid), new PropertyMetadata(0, RowsColumnsChanged));

        public static readonly DependencyProperty CellCollectionProperty =
            DependencyProperty.Register("CellCollection", typeof(CellCollectionModel), typeof(DynamicGrid), new PropertyMetadata(null, CellCollectionChanged));

        public static readonly DependencyProperty KnightProperty =
            DependencyProperty.Register("Knight", typeof(KnightModel), typeof(DynamicGrid), new PropertyMetadata(null, KnightChanged));

        public static readonly DependencyProperty NeedsResetProperty =
            DependencyProperty.Register("NeedsReset", typeof(Boolean), typeof(DynamicGrid), new PropertyMetadata(true));

        private ImageBrush img;
        private Int16 count;
        public DynamicGrid()
        {
            img = new ImageBrush();
            img.ImageSource = new BitmapImage(new Uri("D:\\University\\TPA\\KnightsTour\\Knights_Tour\\Knights_Tour\\Resources\\KnightIcon.bmp"));
            img.Stretch = Stretch.Uniform;
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

        public KnightModel Knight
        {
            get { return (KnightModel)GetValue(KnightProperty); }
            set
            {
                SetValue(KnightProperty, value);
            }
        }

        public Boolean NeedsReset
        {
            get { return (Boolean)GetValue(NeedsResetProperty); }
            set
            {
                SetValue(NeedsResetProperty, value);
            }
        }

        private static void RowsColumnsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DynamicGrid grid = (DynamicGrid)d;
            grid.NeedsReset = true;
            grid.Rows = grid.RowsColumns + 1;
            grid.Columns = grid.RowsColumns + 1;
            grid.Children.Clear();
        }

        private static void SetGridColours(DependencyObject d)
        {
            DynamicGrid grid = (DynamicGrid)d;
            if (!grid.NeedsReset)
                return;
            grid.Children.Clear();
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
                            textBlock.Text = (i+1).ToString();
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
                        newRectangle.Stroke = new SolidColorBrush(Colors.Black);
                        newRectangle.StrokeThickness = 1;
                        grid.CellCollection.Cells[i, j].CellColour = cellColour.white;
                    }
                    else
                    {
                        newRectangle.Fill = new SolidColorBrush(Colors.Black);
                        newRectangle.Stroke = new SolidColorBrush(Colors.Black);
                        newRectangle.StrokeThickness = 1;
                        grid.CellCollection.Cells[i, j].CellColour = cellColour.black;
                    }
                    grid.CellCollection.Cells[i, j].CellState = cellState.notVisited;
                    if (grid.Knight.CurrentPosition.X == i && grid.Knight.CurrentPosition.Y == j)
                    {
                        grid.CellCollection.Cells[i, j].CellState = cellState.busy;
                        newRectangle.Fill = grid.img;
                        newRectangle.Stroke = new SolidColorBrush(Colors.Black);
                        newRectangle.StrokeThickness = 1;
                    }

                    //create grid for each cell and add rectangle
                    Grid newGrid = new Grid();
                    newGrid.Children.Add(newRectangle);
                    Grid.SetRow(newRectangle, 0);
                    Grid.SetColumn(newRectangle, 0);

                    //add grid to main grid
                    grid.Children.Add(newGrid);
                    Grid.SetRow(newGrid, i);
                    Grid.SetColumn(newGrid, j);
                }
            }
        }

        private static void CellCollectionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DynamicGrid grid = (DynamicGrid)d;

            if (grid.NeedsReset)
            {
                SetGridColours(d);
                grid.NeedsReset = false;
            }
            else
            {
                if (grid.CellCollection != null)
                {
                    int x = grid.Knight.CurrentPosition.X;
                    int y = grid.Knight.CurrentPosition.Y;
                    int px = grid.Knight.PreviousPosition.X;
                    int py = grid.Knight.PreviousPosition.Y;

                    for (int i = 0; i < grid.RowsColumns; i++)
                    {
                        for (int j = 0; j < grid.RowsColumns; j++)
                        {
                            if (x == i && y == j)
                            {
                                var element = grid.Children.Cast<UIElement>().
                                    FirstOrDefault(e => Grid.GetColumn(e) == j && Grid.GetRow(e) == i);

                                if (element != null)
                                {

                                    var rectangle = ((Grid)element).Children.Cast<UIElement>()
                                        .First(e => Grid.GetRow(e) == 0 && Grid.GetColumn(e) == 0);

                                    ((Rectangle)rectangle).Fill = grid.img;

                                }
                            }

                            if (px == i && py == j)
                            {
                                var element = grid.Children.Cast<UIElement>().
                                    FirstOrDefault(e => Grid.GetColumn(e) == j && Grid.GetRow(e) == i);

                                if (element != null)
                                {
                                    var rectangle = ((Grid)element).Children.Cast<UIElement>()
                                        .First(e => Grid.GetRow(e) == 0 && Grid.GetColumn(e) == 0);

                                    ((Rectangle)rectangle).Fill = new SolidColorBrush(Colors.GreenYellow);

                                    TextBlock newTextBlock = new TextBlock();
                                    newTextBlock.Text = grid.Knight.CellsCrossed.ToString();
                                    newTextBlock.Foreground = new SolidColorBrush(Colors.Black);
                                    newTextBlock.HorizontalAlignment = HorizontalAlignment.Center;
                                    newTextBlock.VerticalAlignment = VerticalAlignment.Center;
                                    ((Grid)element).Children.Add(newTextBlock);
                                    Grid.SetRow(element, 0);
                                    Grid.SetColumn(element, 0);

                                }
                            }
                        }
                    }
                }
            }
        }

        private static void KnightChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DynamicGrid grid = (DynamicGrid)d;
            if (grid.CellCollection != null)
            {
                if (!grid.Knight.IsMoving)
                    grid.NeedsReset = true;
                else
                {
                    grid.CellCollection.Cells[grid.Knight.CurrentPosition.X, grid.Knight.CurrentPosition.Y].CellState = cellState.busy;
                    grid.CellCollection.Cells[grid.Knight.PreviousPosition.X, grid.Knight.PreviousPosition.Y].CellState = cellState.visited;
                }
                CellCollectionChanged(grid, e);
            }
        }
    }
}
