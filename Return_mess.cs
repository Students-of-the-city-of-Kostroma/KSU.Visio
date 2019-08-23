﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Drawing2D;
using System.Drawing;

namespace OOP_drow
{
  public  class Return_mess : Line
    {
        public override void Draw(Graphics gr)
        {
            Pen Pe = new Pen(Line_color);
            Pe.DashStyle = DashStyle.Dash;
            Pe.Width = Line_width;
            Pe.EndCap = System.Drawing.Drawing2D.LineCap.Custom;
            Pe.CustomEndCap = new AdjustableArrowCap(3f, 3f, false);
            gr.DrawLine(Pe, Basic_points[0], Basic_points[1]);
        }
    }
}