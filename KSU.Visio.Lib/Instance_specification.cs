using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace KSU.Visio.Lib
{
    public class Instance_specification : Figure
    {
        public Instance_specification(Point location, Size size)
           : base(location, size)
        {

        }

        public override Figure Clone()
        {
            Figure figure = new Instance_specification(Location, Size);
            figure.Selected = Selected;
            return figure;
        }

        override public void Draw(Graphics gr)
        {
            base.Draw(gr);

            gr.DrawRectangle(pen, new Rectangle(Location, Size));
            int k1 = Location.X + Size.Width + 20; //левая граница линии
            int k2 = (int)(Location.Y + 0.3 * (Location.Y - Location.Y));//расстояние межу верхней границы прямоугольника и линией
            int k3 = Location.X - 20;//правая граница линии
            Internal_line[0] = new Point(k1, k2);
            Internal_line[1] = new Point(k3, k2);
            gr.DrawLine(pen, Internal_line[0], Internal_line[1]);
        }

        Point[] Internal_line = new Point[2];
    }
}
