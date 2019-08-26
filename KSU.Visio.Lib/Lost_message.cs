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

        public override void Draw(Graphics gr)
        {
            Pen Pe = new Pen(Line_color);
            Pe.Width = Line_width;
            Pe.EndCap = LineCap.Custom;
            Pe.CustomEndCap = new AdjustableArrowCap(3f, 3f);
            Pen Pe2 = new Pen(Line_color);
            Pe2.Width = Line_width;
            Pe2.EndCap = LineCap.RoundAnchor;


            gr.DrawLine(Pe, LeftTop, RightBottom);
            gr.DrawLine(Pe2, LeftTop, RightBottom);
        }


    }
}
