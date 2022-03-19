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

namespace Knights_Tour.Controls
{
    public class DynamicGrid : UniformGrid
    {
        public int RowsColumns
        {
            get { return (int)GetValue(RowsColumnsProperty); }
            set 
            { 
                SetValue(RowsColumnsProperty, value);
            }
        }

        // Using a DependencyProperty as the backing store for RowsColumns.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RowsColumnsProperty =
            DependencyProperty.Register("RowsColumns", typeof(int), typeof(DynamicGrid), new PropertyMetadata(0, RowsColumnsChanged));


        private static void RowsColumnsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DynamicGrid grid = (DynamicGrid)d;
            if (grid.RowsColumns > 10)
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
            for (int i = 0; i < grid.Rows; i++)
            {
                for (int j = 0; j < grid.Columns; j++)
                {
                    if((i == grid.Rows - 1 && j != grid.Columns-1) || (i != grid.Rows - 1 && j == grid.Columns - 1))
                    {
                        TextBlock textBlock = new TextBlock();
                        textBlock.FontSize = 20;
                        if (i == grid.Rows - 1)
                            textBlock.Text = ((char)(j + 65)).ToString();
                        else
                            textBlock.Text = (grid.RowsColumns-i).ToString();
                        textBlock.TextAlignment = TextAlignment.Center;
                        textBlock.VerticalAlignment = VerticalAlignment.Center; 
                        grid.Children.Add(textBlock);
                        Grid.SetRow(textBlock,i);
                        Grid.SetColumn(textBlock, j);
                        continue;
                    }

                    if (i == grid.Rows - 1 && j == grid.Columns - 1)
                        continue;

                    Rectangle newRectangle = new Rectangle();
                    if (i % 2 != 0 && j % 2 != 0 || i % 2 == 0 && j % 2 == 0)
                        newRectangle.Fill = new SolidColorBrush(Colors.White);
                    else
                        newRectangle.Fill = new SolidColorBrush(Colors.Black);
                    newRectangle.Stroke = new SolidColorBrush(Colors.Black);
                    newRectangle.StrokeThickness = 2;
                    grid.Children.Add(newRectangle);
                    Grid.SetRow(newRectangle, i);
                    Grid.SetColumn(newRectangle, j);
                }
            }
        }
    }
}
