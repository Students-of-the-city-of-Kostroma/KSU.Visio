using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSU.Visio.Lib.Cap
{
    public class FoundMessageCap : LineCapBase
    {
        public FoundMessageCap() : base()
        {
           
            fillPath.AddLine(0, -2, -2, -5);
            fillPath.AddLine(-2, -5, 2, -5);
            fillPath.AddLine(2, -5, 0, -2);
            fillPath.AddEllipse(100, 100, 100, 100);
        }
    }
}
