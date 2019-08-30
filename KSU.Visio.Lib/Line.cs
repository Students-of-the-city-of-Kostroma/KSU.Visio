using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace KSU.Visio.Lib
{
    public class Line : Figure
    {
        public Line(Point location, Size size) 
            : base(location, size) { }

        public override object Clone()
        {
            return new Line(Location, Size);
        }

        public override void Draw(Graphics gr)
        {
            gr.DrawLine(penDefault, location, location+size);
        }

    }
}
