using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Xml;

namespace KSU.Visio.Lib.StateDiagram
{
    public class Emulator : Canvas
    {
        public List<Condition> ActiveCondition { get; set; }
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
                    AddFigure(new Condition(figureXML,null));

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
        protected void GenerateInputsToCSV()
        {
            List<string> header = new List<string>();
            var testCase = (List<Dictionary<string, object>>)dict["TestCase"];
            string text = "";
            foreach (Dictionary<string, object> order in testCase)
            {
                List<string> line = new List<string>();
                foreach (string orderKey in order.Keys)
                {
                    int indColumn = header.IndexOf(orderKey);
                    if (indColumn == -1)
                    {
                        header.Add(orderKey);
                        indColumn = header.Count - 1;
                    }
                    while (indColumn >= line.Count) line.Add("");
                    line[indColumn] = order[orderKey].ToString();
                }
                text += string.Join(";", line) + "\r\n";
            }
            text = "TEST_CASE_START" + "\r\n" + string.Join(";", header) + "\r\n" + text + "TEST_CASE_END";
            File.WriteAllText(DateTime.Now.ToString("yyyyMMdd") + ".csv", text);
        }
        void Init()
        {
            ActiveCondition = new List<Condition>();
            dict.Add("TestCase", new List<Dictionary<string, object>>());
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
                if (condition.Active)
                    ActiveCondition.Add(condition);

            while (ActiveCondition.Count > 0 )
            {
                Run(ActiveCondition[0]);
            }

            SaveText();
            GenerateInputsToCSV();
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
        private void  Run(Condition condition)
        {
            //если перехода на нижний уровень не осуществлялся и есть нижний уровень
            if (!condition.Dived && condition.Conditions.Count > 0)
            {
                //поиск стартовых позиций на нижнем уровне
                List<Condition> startingCondition = new List<Condition>();
                foreach (Condition condition2 in condition.Conditions)
                    //поиска начальных состояний на нижнем уровне
                    if (condition2.Starting)
                    {
                        ActiveCondition.Add(condition2);
                        ActiveCondition.Remove(condition);
                        condition.Active = false;
                        condition.Dived = true;
                        condition2.Active = true;
                    }                
            }
            else
            {
                //переход на уровень выше
                if (condition.Ending)
                {
                    condition.Active = false;
                    ActiveCondition.Remove(condition);
                    if (condition.Owner != null)
                    {
                        condition.Owner.Active = true;
                        ActiveCondition.Add(condition.Owner);
                    }
                }
                else
                {
                    List<Transfer> tr = new List<Transfer>();
                    foreach (Transfer transfer in condition.Outputs)
                        if (transfer.Start.Count > 0)
                        {
                            bool action = true;
                            foreach (Condition start in transfer.Start)
                                if (!start.Active)
                                {
                                    action = false;
                                    break;
                                }
                            if (action) tr.Add(transfer);
                        }
                        else continue;

                    if (tr.Count > 0)
                    {
                        double summ = 0;
                        foreach (Transfer t in tr)
                            summ += t.Probability;
                        double pro = rnd.NextDouble() * summ;
                        Transfer tt = null;
                        summ = 0;
                        foreach (Transfer t in tr)
                        {
                            summ += t.Probability;
                            if (pro < summ)
                            {
                                tt = t;
                                break;
                            }
                        }
                        if (tt == null) tt = tr[tr.Count - 1];
                        tt.Run(dict);
                        foreach (Condition start in tt.Start)
                        {
                            start.Active = false;
                            start.Dived = false;
                        }
                        foreach (Condition end in tt.End)
                        {
                            end.Active = true;
                            ActiveCondition.Add(end);
                            ActiveCondition.Remove(condition);
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
