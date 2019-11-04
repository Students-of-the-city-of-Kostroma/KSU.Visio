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

        public Condition SearchCondition(string name)
        {
            foreach (Condition condition in this.figures)
                if (condition.Name == name)
                    return condition;
                else
                {
                    Condition result = SearchCondition(name, condition);
                    if (result != null)
                        return result;
                }
            return null;
        }
        protected Condition SearchCondition(string name, Condition conditions)
        {
            foreach (Condition condition in conditions.Conditions)
                if (condition.Name == name)
                    return condition;
                else
                {
                    Condition result = SearchCondition(name, condition);
                    if (result != null)
                        return result;
                }
            return null;
        }
        public Transfer SearchTransfer(string name)
        {
            foreach (Condition condition in this.figures)
            {
                Transfer result = SearchTransfer(name, condition);
                if (result != null)
                    return result;
            }
            return null;
        }
        protected Transfer SearchTransfer(string name, Condition condition)
        {
            foreach (Transfer transfer in condition.Transfers)
                if (transfer.Name == name)
                    return transfer;
            foreach (Condition cond in condition.Conditions)
            {
                Transfer tr = SearchTransfer(name, cond);
                if (tr != null) 
                    return tr;
            }
            return null;
        }


        public override void UpdateCanvas()
        {
            base.UpdateCanvas();

            foreach (Condition figure in this.figures)
                figure.Draw(canvas);
        }
        public void ToParent(Condition condition)
        {
            if (condition != null && condition.Conditions.Count > 0)
            {
                figures.Clear();
                foreach (Condition cond in condition.Conditions)
                    AddFigure(cond);
                UpdatePositionFigures();
            }
        }
        public void ToOwner()
        {
            if (figures.Count > 0)
            {
                Condition owner = (figures[0] as Condition).Owner;
                figures.Clear();
                if (owner.Owner != null)
                    foreach (Condition condition in owner.Owner.Conditions)
                        figures.Add(condition);
                else
                    figures.Add(owner);
                UpdatePositionFigures();
                UpdateCanvas();
            }
        }
        XmlDocument xml;
        protected TestComplect testComplect;
        public Emulator(XmlDocument xml) : base(xml)
        {
            this.xml = xml;
            Init();
            IniXML();
            XmlNode emulatorXML = xml.DocumentElement;
            testComplect = new TestComplect(emulatorXML.SelectSingleNode("TestComplect"), this);
            UpdateCanvas();
        }
        private void IniXML()
        {
            figures.Clear();
            ActiveCondition.Clear();
            XmlNode emulatorXML = xml.DocumentElement;
            XmlNode figuresXML = emulatorXML.SelectSingleNode("Models");
            if (figuresXML != null)
            {
                XmlNodeList figuresXMLlist = figuresXML.ChildNodes;
                foreach (XmlNode figureXML in figuresXMLlist)
                    AddFigure(new Condition(figureXML, null));
            }
        }
        protected void GenerateInputsToCSV()
        {
            List<string> header = new List<string>();
            var testComplect = dict["TestComplect"] as List<List<Dictionary<string, object>>>;
            string text = "";
            foreach (List<Dictionary<string, object>> testCase in testComplect)
            {
                text += "TEST_CASE_START" + "\r\n";
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
                text += "TEST_CASE_END" + "\r\n";
            }
            text = string.Join(";", header) + "\r\n" + text;
            File.WriteAllText(DateTime.Now.ToString("yyyyMMdd") + ".csv", text);
        }
        void Init()
        {
            ActiveCondition = new List<Condition>();
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
            testComplect.Run();
            //foreach (Condition condition in figures)
            //    if (condition.Active)
            //    {
            //        condition.Dived = false;
            //        ActiveCondition.Add(condition);
            //    }

            while (ActiveCondition.Count > 0)
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
        internal void Run(Transfer transfer)
        {
            transfer.Run(dict);
            foreach (Condition start in transfer.Start)
            {
                start.Active = false;
                start.Dived = false;
                ActiveCondition.Remove(start);
            }
            foreach (Condition end in transfer.End)
            {
                end.Active = true;
                ActiveCondition.Add(end);
            }
        }
        protected void Run(Condition condition)
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
                    //списох доступных переходов
                    List<Transfer> tr = new List<Transfer>();
                    foreach (Transfer transfer in condition.Outputs)
                        if (transfer.CheckAllTransfer() && transfer.Start.Count > 0)
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
                        Run(tt);
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
