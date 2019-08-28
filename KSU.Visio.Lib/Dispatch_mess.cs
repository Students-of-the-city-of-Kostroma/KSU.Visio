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
        public Dispatch_mess(int FPx, int FPy, int SPx, int SPy)
            : base(FPx, FPy, SPx, SPy) { }
        public Dispatch_mess()
            : base(10, 10, 20, 20) { }
        public override void Draw(Graphics gr)
        {
            Pen Pe = new Pen(Line_color);
            Pe.Width = Line_width;
            Pe.EndCap = LineCap.Custom;
            Pe.CustomEndCap = new AdjustableArrowCap(3f, 3f);
            gr.DrawLine(Pe, LeftTop, RightBottom);
        }

    }
}
