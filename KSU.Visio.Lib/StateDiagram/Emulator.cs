using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml;

namespace KSU.Visio.Lib.StateDiagram
{
    public class Emulator : Canvas
    {
        public List<Transfer> Transfers {get; set;}
        public Emulator(Size size) : base(size)
        {
            Init();
        }

        public override void UpdateCanvas()
        {
            base.UpdateCanvas();

            foreach (Transfer transfer in this.Transfers)
                transfer.Draw(canvas);
        }
        public Emulator(XmlDocument xml) : base(xml)
        {
            Init();
            XmlNode emulatorXML = xml.DocumentElement;
            XmlNode figuresXML = emulatorXML.SelectSingleNode("Figures");
            if (figuresXML != null)
            {
                XmlNodeList figuresXMLlist = figuresXML.ChildNodes;
                foreach (XmlNode figureXML in figuresXMLlist)
                {
                    Figure figure;
                    switch (figureXML.Name)
                    {
                        case "State": figure = new State(figureXML); break;
                        case "Synchronizer": figure = new Synchronizer(figureXML); break;
                        default: throw new NotImplementedException();
                    }
                    AddFigure(figure);
                }
            }
            XmlNode transfersXML = emulatorXML.SelectSingleNode("Transfers");
            if (transfersXML != null)
            {
                XmlNodeList transfersXMLlist = transfersXML.ChildNodes;
                foreach (XmlNode transferXML in transfersXMLlist)
                    Transfers.Add(new Transfer(transferXML, this));
            }
            UpdateCanvas();
        }
        void Init()
        {
            Transfers = new List<Transfer>();
        }
        public override void ToXml(XmlDocument xml)
        {
            base.ToXml(xml);
            
            XmlNode transfersXML = xml.CreateNode(XmlNodeType.Element, "Transfers", "");
            foreach (Transfer transfer in Transfers)
                transfersXML.AppendChild(transfer.ToXml(xml));
            XmlNode root = xml.DocumentElement;
            root.AppendChild(transfersXML);
        }


        public static Emulator LoadFromXMLFile(string path = "test.xml")
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(path);
            return new Emulator(xml);
        }
    }
}
