using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace KSU.Visio.Lib
{
    public class Continuation : Figure
    {
        public Continuation(Point location, Size size) : base(location, size)
        {

        }

        public override Figure Clone()
        {
            Figure figure = new Continuation(Location, Size);
            figure.Selected = Selected;
            return figure;
        }

        public override void Draw(Graphics gr)
        {
            base.Draw(gr);
            gr.DrawEllipse(pen, Location.X + Size.Width, Location.Y, Location.X - Location.X + Size.Width, Location.Y - Location.Y);
        }

    }
}
