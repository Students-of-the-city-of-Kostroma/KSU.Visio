using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Xml;

namespace KSU.Visio.Lib.StateDiagram
{
    public class Emulator : Canvas
    {
        protected Dictionary<string, object> dict = new Dictionary<string, object>();
        public Emulator(Size size) : base(size)
        {
            Init();
        }

        public override void UpdateCanvas()
        {
            base.UpdateCanvas();

            //foreach (Transfer transfer in this.Transfers)
            //    transfer.Draw(canvas);
        }
        public Emulator(XmlDocument xml) : base(xml)
        {
            Init();
            XmlNode emulatorXML = xml.DocumentElement;
            XmlNode figuresXML = emulatorXML.SelectSingleNode("Models");
            if (figuresXML != null)
            {
                XmlNodeList figuresXMLlist = figuresXML.ChildNodes;
                foreach (XmlNode figureXML in figuresXMLlist)
                {
                    Figure figure;
                    switch (figureXML.Name)
                    {
                        case "State": figure = new Condition(figureXML); break;
                        case "Synchronizer": figure = new Synchronizer(figureXML); break;
                        default: throw new NotImplementedException();
                    }
                    AddFigure(figure);
                }
            }
            //XmlNode transfersXML = emulatorXML.SelectSingleNode("Transfers");
            //if (transfersXML != null)
            //{
            //    XmlNodeList transfersXMLlist = transfersXML.ChildNodes;
            //    foreach (XmlNode transferXML in transfersXMLlist)
            //        Transfers.Add(new Transfer(transferXML, this));
            //}
            UpdateCanvas();
        }
        void Init()
        {
            
        }
        public override void ToXml(XmlDocument xml)
        {
            base.ToXml(xml);
            
            //XmlNode transfersXML = xml.CreateNode(XmlNodeType.Element, "Transfers", "");
            //foreach (Transfer transfer in Transfers)
            //    transfer.ToXml(xml, transfersXML);
            //XmlNode root = xml.DocumentElement;
            //root.AppendChild(transfersXML);
        }
        public void Run()
        {
            foreach (Condition condition in figures)
            {
                Run(condition);
            }
            SaveText();
        }

        private void SaveText()
        {
            string text = "";
            foreach (var key in dict.Keys)
            {
                text += key + " : " + dict[key].ToString() + "\r\n";
            }
            File.WriteAllText("text.txt", text);
        }
        Random rnd = new Random();
        private void Run(Condition condition)
        {
            if (!condition.Dived && condition.Conditions.Count > 0)
            {
                foreach (Condition condition2 in condition.Conditions)
                {
                    if (condition2.Starting)
                    {
                        condition.Active = false;
                        condition.Dived = true;
                        condition2.Active = true;
                        Run(condition2);
                    }
                }
            }
            else
            {
                if (condition.Ending) return;
                else
                {
                    List<Transfer> tr = new List<Transfer>();
                    foreach (Transfer transfer in condition.Outputs)
                    {
                        if (transfer.Start.Count > 0)
                        {
                            bool action = true;
                            foreach (Condition start in transfer.Start)
                            {
                                if (!start.Active)
                                {
                                    action = false;
                                    break;
                                }
                            }
                            if (action) tr.Add(transfer);
                        }
                        else continue;
                    }
                    if(tr.Count > 0)
                    {
                        double summ = 0;
                        foreach (Transfer transfer in tr)
                            summ += transfer.Probability;
                        double pro = rnd.NextDouble() * summ;
                        bool found = false;
                        foreach (Transfer transfer in tr)
                        {
                            summ -= transfer.Probability;
                            if(summ)
                        }
                            transfer.Run(dict);
                        foreach (Condition start in transfer.Start)
                        {
                            start.Active = false;
                            start.Dived = false;
                        }
                        foreach (Condition end in transfer.End)
                        {
                            end.Active = true;
                            Run(end);
                        }
                    }
                }
            }
        }

        public static Emulator LoadFromXMLFile(string path = "test.xml")
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(path);
            return new Emulator(xml);
        }
    }
}
