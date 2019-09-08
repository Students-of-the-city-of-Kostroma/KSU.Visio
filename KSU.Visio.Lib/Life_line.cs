using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Drawing2D;
using System.Drawing;

namespace KSU.Visio.Lib
{
    public class Life_line : Figure
    {
        public Life_line(Point location, Size size) : base(location, size)
        {

        }

        public override Figure Clone()
        {
            Figure figure = new Life_line(Location, Size);
            figure.Selected = Selected;
            return figure;
        }

        public override void Draw(Graphics gr)
        {
            base.Draw(gr);
            Pen Pe = (Pen)pen.Clone();
            gr.DrawRectangle(pen, Location.X, Location.Y, Location.X + Size.Width - Location.X, Location.Y + Size.Width - Location.Y);
            Pen.DashStyle = DashStyle.Dash;
            Pen.Color = Pe.Color;
            SortLine();
            gr.DrawLine(pen, Line_ends[0], Line_ends[1]);
        }

        int Line_length = 100;
        static Pen Pen = new Pen(Brushes.Black, 3);
        Point[] Line_ends = new Point[2];
        bool Edit_line = false;//Определает, нужно ли изменять длинну линии



        private void SortLine()
        {
            int mid = (Location.X + Location.X + Size.Width) / 2;//середина нижней линии прямоугольника (из этой точки выходит линия жизни) 
            Line_ends[0] = new Point(mid, Location.Y+Size.Width);
            Line_ends[1] = new Point(mid, Location.Y+Size.Width + Line_length);
        }

    }
}
