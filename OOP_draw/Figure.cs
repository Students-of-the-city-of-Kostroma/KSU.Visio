using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace OOP_drow
{
    public abstract class Figure
    {
        public Point[] Basic_points = new Point[2];

        public int Line_width = 2;//ширина линии

        public Color Line_color { get; set; }//цвет

        public abstract void Draw(Graphics gr);

        public abstract bool Hit_testing(Figure Figureigure, Point Point);

        public abstract void Shift(Point Point);
    }
}
