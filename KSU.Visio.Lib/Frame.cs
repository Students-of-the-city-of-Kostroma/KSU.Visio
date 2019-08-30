using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace KSU.Visio.Lib
{
    public class Frame : Rectangle_object
    {
        Point[] Corner = new Point[4];//хранит точки, отвечающие за угол фрейма

        public Frame(Point location, Size size)
            : base(location, size)
        {
            int k1x = (int)(Location.X + Size.Width + 0.2 * (Location.X - Location.X + Size.Width));//координата Х первой точки (которая лежит на верхней границе) и той, что под ней
            int k2y = (int)(Location.Y + 0.15 * (Location.Y - Location.Y));//координата У второй  точки, которая лежит под первой
            int k3x = (int)(Location.X + Size.Width + 0.15 * (Location.X - Location.X + Size.Width));//координата х третей точки, которая лежит левее и ниже второй точки
            int k3y = (int)(Location.Y + 0.2 * (Location.Y - Location.Y));//координата у третей точки, а так же четвертой точки (которая лежит на левой границе)
            Corner[0] = new Point(k1x, Location.Y);
            Corner[1] = new Point(k1x, k2y);
            Corner[2] = new Point(k3x, k3y);
            Corner[3] = new Point(Location.X + Size.Width, k3y);
        }
        override public void Draw(Graphics gr)
        {
            gr.DrawRectangle(penDefault, Location.X+Size.Width, Location.Y, Location.X - Location.X+Size.Width, Location.Y - Location.Y);

            gr.DrawLine(penDefault, Corner[0], Corner[1]);
            gr.DrawLine(penDefault, Corner[1], Corner[2]);
            gr.DrawLine(penDefault, Corner[2], Corner[3]);
        }

    }
}
