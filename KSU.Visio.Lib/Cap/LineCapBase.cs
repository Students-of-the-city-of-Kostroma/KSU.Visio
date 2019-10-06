using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSU.Visio.Lib.Cap
{
    public class LineCapBase
    {
        protected GraphicsPath fillPath = new GraphicsPath();
        protected GraphicsPath strokePath = new GraphicsPath();

        public CustomLineCap GetCustomLineCap()
        {
            return new CustomLineCap(fillPath, strokePath);
        }
    }
}
