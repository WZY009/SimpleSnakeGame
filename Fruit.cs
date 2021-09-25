using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows;
using System.Windows.Controls;

namespace SimpleSnakeGame
{
    public class Fruit
    {
        // if you forget get() set() method, please check this page:https://blog.csdn.net/alzzw/article/details/112859568
        public Point _pos { get; set; }
        public Ellipse _ellipse { get; set; }
        public Canvas _canvas { get; set; }
        public Fruit(Point point, Canvas canvas)
        {
            _pos = point;
            _canvas = canvas;
            _ellipse = new Ellipse
            {
                Width = 20,
                Height = 20,
                Fill = Brushes.Red
            };
            _ellipse.SetValue(Canvas.LeftProperty, _pos.X * 20);
            _ellipse.SetValue(Canvas.TopProperty, _pos.Y * 20);
            _canvas.Children.Add(_ellipse);
        }

        public void SetPostion(Point pos)
        {
            _pos = pos;
            _ellipse.SetValue(Canvas.LeftProperty, _pos.X * 20);
            _ellipse.SetValue(Canvas.TopProperty, _pos.Y * 20);
        }
    }
}
