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
    public class Condition : SDBase
    {
        public Condition Owner { get; set; }
        /// <summary>
        /// Хранит сведения о переходе на нижний слой после перехода в это состяние
        /// </summary>
        public bool Dived { get; set; }
        /// <summary>
        /// список входящих переходов
        /// </summary>
        public List<Transfer> Inputs { get; set; }
        /// <summary>
        /// список исходящих переходов
        /// </summary>
        public List<Transfer> Outputs { get; set; }
        /// <summary>
        /// В этом состянии находится модель
        /// </summary>
        public bool Active { get; set; }
        /// <summary>
        /// Является начальной позицией на этом слое модели
        /// </summary>
        public bool Starting { get; set; }
        /// <summary>
        /// является конечной позицей на этом слое модели
        /// </summary>
        public bool Ending { get; set; }
        /// <summary>
        /// состояния входящие в подслой этого слоя
        /// </summary>
        public List<Condition> Conditions { get; set; }
        /// <summary>
        /// переходя входящие в подслой этого состояния
        /// </summary>
        public List<Transfer> Transfers { get; set; }
        public Condition(XmlNode stateXML, Condition owner) : base (stateXML)
        {
            Init();

            Owner = owner;

            XmlAttribute attr = stateXML.Attributes["active"];
            Active = (attr != null) ? bool.Parse(attr.Value) : false;

            attr = stateXML.Attributes["dived"];
            Dived = (attr != null) ? bool.Parse(attr.Value) : false;

            attr = stateXML.Attributes["starting"];
            Starting = (attr != null) ? bool.Parse(attr.Value) : false;

            attr = stateXML.Attributes["ending"];
            Ending = (attr != null) ? bool.Parse(attr.Value) : false;

            foreach (XmlNode conditionXML in stateXML.SelectNodes("Condition"))
                Conditions.Add(new Condition(conditionXML, this));

            foreach (XmlNode transferXML in stateXML.SelectNodes("Transfer"))
            {
                Transfer tr = new Transfer();
                tr.Expression = transferXML.SelectSingleNode("Expression").InnerText;
                tr.Probability = double.Parse(transferXML.Attributes["probability"].Value.Replace('.',','));
                tr.Name =transferXML.Attributes["name"].Value;
                if(transferXML.Attributes["allTransfers"]!=null)
                    tr.AllTransfers = bool.Parse(transferXML.Attributes["allTransfers"].Value);
                foreach (XmlNode startXML in transferXML.SelectNodes("Start"))
                {
                    string nameStart = startXML.Attributes["name"].Value;
                    foreach (Condition conditions in Conditions)
                    {
                        if(conditions.Name == nameStart)
                        {
                            tr.Start.Add(conditions);
                            conditions.Outputs.Add(tr);
                            break;
                        }
                    }
                }
                foreach (XmlNode startXML in transferXML.SelectNodes("End"))
                {
                    string nameStart = startXML.Attributes["name"].Value;
                    foreach (Condition conditions in Conditions)
                    {
                        if (conditions.Name == nameStart)
                        {
                            tr.End.Add(conditions);
                            conditions.Inputs.Add(tr);
                            break;
                        }
                    }
                }
                Transfers.Add(tr);

            }

        }
        void Init()
        {
            Conditions = new List<Condition>();
            Transfers = new List<Transfer>();
            Inputs = new List<Transfer>();
            Outputs = new List<Transfer>();
        }
        public Condition(Point location, Size size) : base(location, size)
        {
            Init();
        }


        //public Condition Owner { get; set; }

        

        public override XmlNode ToXml(XmlDocument xml, XmlNode stateXML)
        {
            XmlNode figureXml = base.ToXml(xml, stateXML);

            XmlAttribute attr = xml.CreateAttribute("active");
            attr.Value = Active.ToString();
            figureXml.Attributes.Append(attr);

            attr = xml.CreateAttribute("dived");
            attr.Value = this.Dived.ToString();
            figureXml.Attributes.Append(attr);

            attr = xml.CreateAttribute("starting");
            attr.Value = this.Starting.ToString();
            figureXml.Attributes.Append(attr);

            attr = xml.CreateAttribute("ending");
            attr.Value = this.Ending.ToString();
            figureXml.Attributes.Append(attr);

            foreach (Condition state in Conditions)
                state.ToXml(xml, figureXml);

            foreach (Transfer transfer in Transfers)
                transfer.ToXml(xml, figureXml);

            return figureXml;
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

