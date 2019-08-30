using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace KSU.Visio.Lib
{
  public class Rectangle_object : Figure
    {

        public Rectangle_object(Point location, Size size)
            : base(location, size) { }

        public override object Clone()
        {
            return new Rectangle_object(Location, Size);
        }

        public override void Draw(Graphics gr)
        {
            gr.DrawRectangle(penDefault, Location.X+Size.Width, Location.Y, Location.X - Location.X+Size.Width, Location.Y - Location.Y);
        }
    }
}
