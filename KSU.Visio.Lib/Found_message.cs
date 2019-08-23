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
        public override void Draw(Graphics gr)
        {
            Pen Pe = new Pen(Line_color);
            Pe.Width = Line_width;
            Pe.EndCap = LineCap.Custom;
            Pe.CustomEndCap = new AdjustableArrowCap(3f, 3f);
            Pe.StartCap = LineCap.RoundAnchor;
            gr.DrawLine(Pe, Basic_points[0], Basic_points[1]);
        }
    }
}
