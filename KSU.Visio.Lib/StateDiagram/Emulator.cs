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

        public Condition SearchCondition(string name)//V(G)=6
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
        public void ToOwner() //V(G) = 5
        {
            if (figures.Count > 0)
            {
                Condition owner = (figures[0] as Condition).Owner;
                if (owner != null)
                {
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
        public static string GenerateInputsToCSV(Dictionary<string, object> dict)
        {
            List<string> header = new List<string>();
            var testComplect = dict["TestComplect"] as List<List<Dictionary<string, object>>>;
            string text = "";
            foreach (List<Dictionary<string, object>> testCase in testComplect)
            {
                text += "TEST_CASE_START" + "\r\n";
                string ordersCSV = "";
                foreach (Dictionary<string, object> order in testCase)
                {
                    List<string> orderValues = new List<string>();
                    foreach (string orderKey in order.Keys)
                    {
                        int indColumn = header.IndexOf(orderKey);
                        if (indColumn == -1)
                        {
                            header.Add(orderKey);
                            indColumn = header.Count - 1;
                        }
                        while (indColumn >= orderValues.Count) orderValues.Add("");
                        orderValues[indColumn] = order[orderKey].ToString();
                    }
                    ordersCSV += string.Join(";", orderValues) + "\r\n";
                }
                text += string.Join(";", header) + "\r\n" + ordersCSV + "TEST_CASE_END" + "\r\n";
            }
            return text;
        }
        void Init()
        {
            ActiveCondition = new List<Condition>();
        }
        public override void ToXml(XmlDocument xml)
        {
            base.ToXml(xml);
        }
        public void Run()
        {
            foreach (Condition condition in figures)
                if (condition.Active)
                    if (ActiveCondition.IndexOf(condition) == -1)
                        ActiveCondition.Add(condition);


            for (int i = 0; ActiveCondition.Count > 0; i++)
                if (Run(ActiveCondition[i]))
                    i--;
                else
                {
                    Transfer transfer = ChooseTheBestTransfer(GetAListOfAvailableTransfer());
                    if (transfer == null) continue;

                    Run(transfer);
                    i = -1;
                }

            File.WriteAllText(DateTime.Now.ToString("yyyyMMdd") + ".csv", GenerateInputsToCSV(dict));
            SaveToXMLFile(DateTime.Now.ToString("yyyyMMdd") + ".xml");
        }


        Random rnd = new Random();
        private void Run(Transfer transfer)
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
                if (ActiveCondition.IndexOf(end) == -1)
                    ActiveCondition.Add(end);
            }
        }
        /// <summary>
        /// Получить доступные переходы для этого состояния
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        private List<Transfer> GetAListOfAvailableTransfer()
        {
            //списох доступных переходов
            List<Transfer> transfers = new List<Transfer>();
            foreach(Condition condition in ActiveCondition)
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
                        if (action) 
                            if(transfers.IndexOf(transfer) == -1)
                                transfers.Add(transfer);
                    }
                    else continue;
            return transfers;
 
        }
        /// <summary>
        /// Ищет наилучший переход
        /// </summary>
        /// <param name="transfers"></param>
        /// <returns></returns>
        private Transfer ChooseTheBestTransfer(List<Transfer> transfers)
        {
            int indT = transfers.IndexOf(testComplect.GetCurrentTransfer());
            if (indT != -1)
            {
                testComplect.NextTransfer();
                return transfers[indT];
            }

            if (transfers.Count > 0)
            {
                double summ = 0;
                foreach (Transfer t in transfers)
                    summ += t.Probability;
                double pro = rnd.NextDouble() * summ;
                Transfer transfer = null;
                summ = 0;
                foreach (Transfer t in transfers)
                {
                    summ += t.Probability;
                    if (pro < summ)
                    {
                        transfer = t;
                        break;
                    }
                }
                if (transfer == null) transfer = transfers[transfers.Count - 1];
                return transfer;
            }
            else return null;
        }
        /// <summary>
        /// выполняет один переход по уровням
        /// </summary>
        /// <param name="condition">вернет истину, если переход состоялся</param>
        private bool Run(Condition condition)
        {
            if (!condition.Dived && condition.Conditions.Count > 0)
            {
                ToLevelDown(condition);
                return true;
            }
            else if (condition.Ending)
            {
                ToLevelUp(condition);
                return true;
            }
            return false;
        }
        /// <summary>
        /// Перейти на уровень вверх
        /// </summary>
        /// <param name="condition"></param>
        private void ToLevelUp(Condition condition)
        {
            condition.Active = false;
            ActiveCondition.Remove(condition);
            if (condition.Owner != null)
            {
                condition.Owner.Active = true;
                if (ActiveCondition.IndexOf(condition.Owner) == -1)
                    ActiveCondition.Add(condition.Owner);
            }
        }
        /// <summary>
        /// Перейти на уровень вниз
        /// </summary>
        /// <param name="condition"></param>
        private void ToLevelDown(Condition condition)
        {
            //поиск стартовых позиций на нижнем уровне
            List<Condition> startingCondition = new List<Condition>();
            foreach (Condition condition2 in condition.Conditions)
                //поиска начальных состояний на нижнем уровне
                if (condition2.Starting)
                {
                    if (ActiveCondition.IndexOf(condition2) == -1)
                        ActiveCondition.Add(condition2);
                    ActiveCondition.Remove(condition);
                    condition.Active = false;
                    condition.Dived = true;
                    condition2.Active = true;
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
