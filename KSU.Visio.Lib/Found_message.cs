using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Drawing2D;
using System.Drawing;

namespace KSU.Visio.Lib
{
    public class Found_message : Line
    {
        public Found_message(int FPx = 10, int FPy = 10, int SPx = 20, int SPy = 20)
            : base(FPx, FPy, SPx, SPy) { }
        public override void Draw(Graphics gr)
        {
            Pen Pe = new Pen(Line_color);
            Pe.Width = Line_width;
            Pe.EndCap = LineCap.Custom;
            Pe.CustomEndCap = new AdjustableArrowCap(3f, 3f);
            Pe.StartCap = LineCap.RoundAnchor;
            gr.DrawLine(Pe, LeftTop, RightBottom);
        }
    }
}
