using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace KSU.Visio.Lib.StateDiagram
{
    /// <summary>
    /// Базовый класс для фигур
    /// </summary>
    public class SDBase : Figure
    {
        public SDBase(XmlNode sdbaseXML) : base(sdbaseXML)
        {

        }

        public SDBase(Point location, Size size) : base(location, size)
        {

        }


        public override Figure Clone()
        {
            Figure figure = new SDBase(Location, Size)
            {
                Selected = Selected
            };
            return figure;
        }
    }
}
