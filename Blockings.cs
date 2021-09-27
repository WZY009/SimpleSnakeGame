using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;

namespace SimpleSnakeGame
{
    class Blockings
    {
        public Point _pos { get; set; }
        public Rectangle _rect { get; set; }
        public Canvas _canvas { get; set; }
        public Blockings(Point point,Canvas canvas)
        {
            _canvas = canvas;
            _pos = point;
            _rect = new Rectangle
            {
                Width = 20,
                Height = 20,
                Stroke = new SolidColorBrush(Colors.DodgerBlue),
                StrokeThickness = 1,
                Fill = Brushes.Red
            };
            _rect.SetValue(Canvas.LeftProperty, _pos.X * 20);
            _rect.SetValue(Canvas.TopProperty, _pos.Y * 20);
            _canvas.Children.Add(_rect);
        }
    }
}
