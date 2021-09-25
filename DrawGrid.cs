using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Shapes;
using System.Windows.Media;

namespace SimpleSnakeGame
{
    static class DrawGrid
    {
        const int CellSize = 20;                // size of the cell
        const int SnakeHead = 0;                // the position of the snake's head(always on the starting of the list)
        const int CellWidth = 640 / CellSize;    // the number of the cell in the wide side
        const int CellHeight = 480 / CellSize;    

        static public Path MyDrawGrid()
        {
            Path gridPath = new Path();
            gridPath.Stroke = new System.Windows.Media.SolidColorBrush(Color.FromArgb(255, 50, 50, 50));
            gridPath.StrokeThickness = 1;

            StringBuilder data = new StringBuilder();

            for (int x = 0; x < 640; x += CellSize)
            {
                data.Append($"M{x},0 L{x},480 ");
            }

            for (int y = 0; y < 480; y += CellSize)
            {
                data.Append($"M0,{y} L640,{y} ");
            }

            gridPath.Data = Geometry.Parse(data.ToString());
            return gridPath;
        }
    }
}
