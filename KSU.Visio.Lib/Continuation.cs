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
            gr.DrawEllipse(pen, Location.X, Location.Y, Location.X + Size.Width - Location.X, Location.Y + Size.Width - Location.Y);
           
        }

    }
}
