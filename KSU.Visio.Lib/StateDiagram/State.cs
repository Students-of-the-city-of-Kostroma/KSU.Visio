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
    /// Состояние
    /// </summary>
    public class State : SDBase
    {
        public State(XmlNode stateXML) : base (stateXML)
        {
            Name = stateXML.Attributes["name"].Value;
        }

        public State(Point location, Size size) : base(location, size)
        {
        }

        public string Name { get; set; }
        public State Owner { get; set; }
        public bool Status { get; set; }

        public override Figure Clone()
        {
            Figure figure = new State(Location, Size)
            {
                Selected = Selected,
                Name = Name
            };
            return figure;
        }

        public override XmlNode ToXml(XmlDocument xml)
        {
            XmlNode stateXML = base.ToXml(xml);

            XmlAttribute stateXMLname = xml.CreateAttribute("name");
            stateXMLname.Value = Name;
            stateXML.Attributes.InsertBefore(stateXMLname,(XmlAttribute)stateXML.Attributes.Item(0));

            return stateXML;
        }

        public override void Draw(Graphics gr)
        {
            base.Draw(gr);

            ///диаметр
            int r = ((Size.Width < Size.Height) ? Size.Width : Size.Height) / 10;
            r = (r < 5) ? 5 : r;

            ///h = horisontal, v - vertical
            ///l - left, r - right
            ///t - top, b - bottom

            Point hlt = new Point(location.X + r, location.Y);
            Point hrt = new Point(location.X + size.Width - r, hlt.Y);

            Point hlb = new Point(hlt.X, location.Y + size.Height);
            Point hrb = new Point(hrt.X, hlb.Y);

            Point vlt = new Point(location.X, location.Y + r);
            Point vlb = new Point(vlt.X, location.Y + size.Height - r);

            Point vrt = new Point(location.X + size.Width, vlt.Y);
            Point vrb = new Point(vrt.X, vlb.Y);


            gr.DrawLine(pen, hlt, hrt);
            gr.DrawLine(pen, hlb, hrb);

            gr.DrawLine(pen, vlt, vlb);
            gr.DrawLine(pen, vrt, vrb);

            int d = r * 2;

            Point locLT = location;
            Point locLB = new Point(locLT.X, vlb.Y - r);
            Point locRT = new Point(hrt.X - r, locLT.Y);
            Point locRB = new Point(locRT.X, locLB.Y);
            Size sizeArc = new Size(d, d);

            gr.DrawArc(pen, new Rectangle(locLT, sizeArc), 180f, 90f);  //LT
            gr.DrawArc(pen, new Rectangle(locLB, sizeArc), 90f, 90f);   //LB
            gr.DrawArc(pen, new Rectangle(locRT, sizeArc), 270f, 90f);  //RT
            gr.DrawArc(pen, new Rectangle(locRB, sizeArc), 0f, 90f);    //RB

            RectangleF recF = new RectangleF(location, size);
            StringFormat drawFormat = new StringFormat();

            gr.DrawString(Name, font, brush, recF, drawFormat);
        }
    }
}

