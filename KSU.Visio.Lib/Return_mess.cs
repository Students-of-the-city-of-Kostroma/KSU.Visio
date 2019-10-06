using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Drawing2D;
using System.Drawing;

namespace KSU.Visio.Lib
{
  public  class Return_mess : Line
    {
        public Return_mess(Point location, Size size)
            : base(location, size) { }
        public override void Draw(Graphics gr)
        {
            Pen Pe = (Pen)penDefault.Clone();
            Pe.DashStyle = DashStyle.Dash;
            Pe.EndCap =LineCap.Custom;
            Pe.CustomEndCap = new AdjustableArrowCap(3f, 3f, false);
            gr.DrawLine(penDefault, Location, Location+Size);
        }
    }
}
