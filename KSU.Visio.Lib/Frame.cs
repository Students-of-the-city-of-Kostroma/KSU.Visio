using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace KSU.Visio.Lib
{
    public class Frame : Figure
    {
        public Frame(Point location, Size size)
            : base(location, size)
        {

        }

        public override Figure Clone()
        {
            Figure figure = new Frame(Location, Size);
            figure.Selected = Selected;
            return figure;
        }

        override public void Draw(Graphics gr)
        {
            base.Draw(gr);

            gr.DrawRectangle(pen, new Rectangle(Location, Size));
        }

    }
}
