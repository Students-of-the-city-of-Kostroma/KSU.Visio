using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace KSU.Visio.Lib
{
    public class Continuation : Rectangle_object
    {
        public Continuation(Point location, Size size)
            : base(location, size) { }
        override public void Draw(Graphics gr)
        {

            gr.DrawEllipse(pen, Location.X+Size.Width, Location.Y, Location.X - Location.X+Size.Width, Location.Y - Location.Y);
        }

    }
}
