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
    /// Синхронизатор
    /// </summary>
    public class Synchronizer : SDBase
    {
        public Synchronizer(XmlNode synchronizerXML) : base(synchronizerXML)
        {
        }

        public Synchronizer(Point location, Size size) : base(location, size)
        {
        }

        public override Figure Clone()
        {
            Figure figure = new Synchronizer(Location, Size)
            {
                Selected = Selected,
            };
            return figure;
        }

        public override void Draw(Graphics gr)
        {
            base.Draw(gr);

            gr.FillRectangle(brush, new Rectangle(location, size));
        }
    }
}
