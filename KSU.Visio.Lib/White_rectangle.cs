using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace KSU.Visio.Lib
{
    public class White_rectangle : Rectangle_object
    {
        SolidBrush Brush = new SolidBrush(Color.White);
        public White_rectangle(Point location, Size size)
            : base(location, size) { }
        override public void Draw(Graphics gr)
        {
            gr.FillRectangle(Brush, Location.X+Size.Width, Location.Y, Location.X - Location.X+Size.Width, Location.Y - Location.Y);
            gr.DrawRectangle(penDefault, Location.X+Size.Width, Location.Y, Location.X - Location.X+Size.Width, Location.Y - Location.Y);

        }

    }
}
