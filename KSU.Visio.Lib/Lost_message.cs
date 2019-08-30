using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Drawing2D;
using System.Drawing;

namespace KSU.Visio.Lib
{
    public class Lost_message : Line
    {
        public Lost_message(Point location, Size size)
            : base(location, size) { }

        public override void Draw(Graphics gr)
        {
            Pen Pe = (Pen)penDefault.Clone();
            Pe.EndCap = LineCap.Custom;
            Pe.CustomEndCap = new AdjustableArrowCap(3f, 3f);
            Pen Pe2 = (Pen)penDefault.Clone();
            Pe2.EndCap = LineCap.RoundAnchor;


            gr.DrawLine(penDefault, Location, Location+Size);
            gr.DrawLine(Pe2, Location, Location+Size);
        }


    }
}
