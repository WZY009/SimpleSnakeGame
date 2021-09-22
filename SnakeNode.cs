using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SimpleSnakeGame
{
    public class SnakeNode
    {
        public Point _pos { get; set; }
        public Rectangle _rect { get; set; }
        public SnakeNode(Point point)
        {
            _pos = point;
            _rect = new Rectangle
            {
                Width = 20,
                Height = 20,
                Stroke = new SolidColorBrush(Colors.DodgerBlue),
                StrokeThickness = 3,
                Fill = Brushes.SkyBlue
            };
            _rect.SetValue(Canvas.LeftProperty, _pos.X * 20);
            _rect.SetValue(Canvas.TopProperty, _pos.Y * 20);
        }
    }
}
