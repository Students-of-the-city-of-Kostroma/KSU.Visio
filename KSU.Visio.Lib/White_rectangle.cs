using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace KSU.Visio.Lib
{
    public class White_rectangle : Figure
    {
        public White_rectangle(Point location, Size size) : base(location, size)
        {

        }

        public override Figure Clone()
        {
            Figure figure = new White_rectangle(Location, Size);
            figure.Selected = Selected;
            return figure;
        }

        public override void Draw(Graphics gr)
        {
            base.Draw(gr);

            Rectangle rect = new Rectangle(Location, Size);
            gr.FillRectangle(Brushes.White, rect);
            gr.DrawRectangle(pen, rect);
        }

        
       
    }
}
