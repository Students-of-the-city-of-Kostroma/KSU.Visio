using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Drawing2D;
using System.Drawing;

namespace KSU.Visio.Lib
{
    public class Dispatch_mess : Line
    {
        public Dispatch_mess(Point location, Size size)
            : base(location, size) { }
        public override void Draw(Graphics gr)
        {
            Pen Pe = (Pen)penDefault.Clone();
            Pe.EndCap = LineCap.Custom;
            Pe.CustomEndCap = new AdjustableArrowCap(3f, 3f);
            gr.DrawLine(penDefault, Location, Location+Size);
        }

    }
}
