using KSU.Visio.Lib.Cap;
using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Reflection;
using System.Xml;

namespace KSU.Visio.Lib.StateDiagram
{
    /// <summary>
    /// Перемещение
    /// </summary>
    public class Transfer 
    {
        /// <summary>
        /// вероятность перехода
        /// </summary>
        public double Probability { get; set; }
        public string Name { get; set; }
        protected static string snamespace = "KSU.Visio.Lib.StateDiagram";
        protected static string sclass = "Expression";
        protected static string smehod = "Run";
        protected static Dictionary<string, string> providerOptions = new Dictionary<string, string> { { "CompilerVersion", "v4.0" } };
        protected static CSharpCodeProvider provider = new CSharpCodeProvider(providerOptions);
        protected static CompilerParameters compilerParams = new CompilerParameters
        {
            GenerateInMemory = true,
            GenerateExecutable = false, 
        };
        protected string sourceBegin = "" +
            "using System.Collections.Generic;" +
            "using System;" +
            "using System.IO;" +
            "using KSU.Visio.Lib.StateDiagram;" +
            "\n" +
            "namespace " + snamespace + "{" +
            "public class " + sclass + "{" +
            "public static string GenerateInputsToCSV(Dictionary<string, object> dict) { " +
            "List<string> header = new List<string>(); " +
            "var testComplect = dict[\"TestComplect\"] as List<List<Dictionary<string, object>>>; " +
            "string text = \"\"; " +
            "foreach (List<Dictionary<string, object>> testCase in testComplect) { " +
            "text += \"TEST_CASE_START\" + \"\\r\\n\"; " +
            "foreach (Dictionary<string, object> order in testCase) { " +
            "List<string> line = new List<string>(); " +
            "foreach (string orderKey in order.Keys) { " +
            "int indColumn = header.IndexOf(orderKey); " +
            "if (indColumn == -1) { header.Add(orderKey); indColumn = header.Count - 1; } " +
            "while (indColumn >= line.Count) line.Add(\"\"); " +
            "line[indColumn] = order[orderKey].ToString(); } " +
            "text += string.Join(\";\", line) + \"\\r\\n\"; } " +
            "text += \"TEST_CASE_END\" + \"\\r\\n\"; } " +
            "text = string.Join(\";\", header) + \"\\r\\n\" + text; return text; }" +
            ""+
            "public void " + smehod + "(object obj){ " +
            "Dictionary<string, object> dict = obj as Dictionary<string, object>;" +
            "try{";
        protected string sourceEnd = "}catch (Exception ex){" +
            "using (FileStream fs = File.Open(DateTime.Now.ToString(\"yyyyMMdd\") + \".log\", FileMode.OpenOrCreate, FileAccess.Write))" +
            "{" +
                "using (StreamWriter sw = new StreamWriter(fs))" +
                "{" +
                    "sw.BaseStream.Position = sw.BaseStream.Length;" +
                    "sw.WriteLine(\"DEBUG|\" + DateTime.Now.ToString() + \"|\" + ex.ToString()" +
            "+\"\\n\"+Emulator.GenerateInputsToCSV(dict));" +
                "}" +
            "}}}}}";
        /// <summary>
        /// Описание конца линии
        /// </summary>
        protected LineCapBase EndLineCap { get; set; }

        public Transfer()
        {
            Init();
            Probability = 1;
            EndLineCap = new AsynchronousMessageCap();
            Start = new List<Condition>();
            End = new List<Condition>();
        }
        public Transfer(XmlNode root, Condition owner)
        {
            Init();
            Expression = root.SelectSingleNode("Expression").InnerText;
            Probability = double.Parse(root.Attributes["probability"].Value);
            Name = root.Attributes["name"].Value;
            if (root.Attributes["allTransfers"] != null)
                AllTransfers = bool.Parse(root.Attributes["allTransfers"].Value);
            foreach (XmlNode startXML in root.SelectNodes("Start"))
            {
                string nameStart = startXML.Attributes["name"].Value;

                foreach (Condition conditions in owner.Conditions)
                {
                    if (conditions.Name == nameStart)
                    {
                        Start.Add(conditions);
                        conditions.Outputs.Add(this);
                        break;
                    }
                }
            }
            foreach (XmlNode startXML in root.SelectNodes("End"))
            {
                string nameStart = startXML.Attributes["name"].Value;
                foreach (Condition conditions in owner.Conditions)
                {
                    if (conditions.Name == nameStart)
                    {
                        End.Add(conditions);
                        conditions.Inputs.Add(this);
                        break;
                    }
                }
            }
            owner.Transfers.Add(this);
        }
        public static void SetLink(Condition c1, Transfer t1, Condition c2)
        {
            t1.Start.Add(c1);
            t1.End.Add(c2);
            c1.Outputs.Add(t1);
            c1.Inputs.Add(t1);
        }

        public void Draw(Graphics gr)
        {
            CustomLineCap e = EndLineCap.GetCustomLineCap();

            Pen pen = new Pen(Color.Black);
            pen.CustomEndCap = e;

            //gr.DrawLine(pen, Start.Location, End.Location);

            e.Dispose();
        }
        void Init()
        {
            compilerParams.ReferencedAssemblies.Add("KSU.Visio.Lib.dll");
        }
        /// <summary>
        /// Истина, если переход пройден
        /// </summary>
        public bool Status { get; set; }
        /// <summary>
        /// Если истина, то переход не должен быть выполнен если не пройдены все преджыдущие маршруты
        /// </summary>
        public bool AllTransfers { get; set; }
        /// <summary>
        /// Проверяет все ли переходы были пройдены
        /// </summary>
        /// <returns>Если параметр AllTransfers равен ложь, то метод будет всегда возвращать истину</returns>
        public bool CheckAllTransfer()
        {
            List<string> transfers = new List<string>();
            if (!AllTransfers) return true;
            foreach (Condition condition in this.Start)
            {
                if (!CheckAllTransfer(condition, transfers)) 
                    return false;
            }
            return true;
        }

        protected bool CheckAllTransfer(Condition condition, List<string> transfers)
        {
            foreach (Transfer transfer in condition.Inputs)
            {
                if (transfers.IndexOf(transfer.Name) == -1)
                {
                    transfers.Add(transfer.Name);
                    if (!transfer.Status)
                        return false;
                    else
                    {
                        foreach (Condition cond in transfer.Start)
                        {
                            if (!CheckAllTransfer(cond, transfers))
                                return false;
                        }
                    }
                }
            }
            foreach (Condition cond in condition.Conditions)
            {
                if (!CheckAllTransfer(cond, transfers))
                    return false;
            }
            return true;
        }
        public List<Condition> Start { get; set; }
        public List<Condition> End { get; set; }
        public string Expression { get; set; }
        private void WriteCodeLog(string source)
        {
            using (FileStream fs = File.Open(DateTime.Now.ToString("yyyyMMdd") + ".code.log", FileMode.OpenOrCreate, FileAccess.Write))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.BaseStream.Position = sw.BaseStream.Length;
                    sw.WriteLine(DateTime.Now.ToString() + "|" + Name + "|" + source);
                }
            }
        }
        public void Run(Dictionary<string, object> dict)
        {
            using (FileStream fs = File.Open(DateTime.Now.ToString("yyyyMMdd") + ".log", FileMode.OpenOrCreate, FileAccess.Write))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.BaseStream.Position = sw.BaseStream.Length;
                    string text = "{";
                    foreach (Condition condition in Start) text += condition.Name + ",";
                    text = text.Remove(text.Length - 1) + "}->" + Name + "->{";
                    foreach (Condition condition in End) text += condition.Name + ",";
                    text = text.Remove(text.Length - 1) + "}";
                    sw.WriteLine("INFO|" + DateTime.Now.ToString() + "|" + text);
                }
            }

            string source = sourceBegin + Expression + sourceEnd;
            CompilerResults results = provider.CompileAssemblyFromSource(compilerParams, source);
            //WriteCodeLog(source);


            if (results.Errors.Count >0)
            {
                using (FileStream fs = File.Open(DateTime.Now.ToString("yyyyMMdd") + ".log", FileMode.OpenOrCreate, FileAccess.Write))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        sw.BaseStream.Position = sw.BaseStream.Length;
                        sw.WriteLine("ERROR|" + DateTime.Now.ToString() + "|" + Name + "|" + results.Errors[0].ErrorText);
                    }
                }
            }
            else
            {
                object o = results.CompiledAssembly.CreateInstance(snamespace + "." + sclass);
                MethodInfo mi = o.GetType().GetMethod(smehod);
                mi.Invoke(o, new object[] { dict });
                this.Probability *= 0.9;
                this.Status = true;
            }
        }

        public virtual void ToXml(XmlDocument xml, XmlNode transfersXML)
        {
            
            XmlNode transferXML = xml.CreateNode(XmlNodeType.Element, "Transfer", "");

            XmlAttribute attr = xml.CreateAttribute("name");
            attr.Value = Name;
            transferXML.Attributes.Append(attr);

            attr = xml.CreateAttribute("probability");
            attr.Value = Probability.ToString();
            transferXML.Attributes.Append(attr);

            attr = xml.CreateAttribute("allTransfers");
            attr.Value = AllTransfers.ToString();
            transferXML.Attributes.Append(attr);

            attr = xml.CreateAttribute("status");
            attr.Value = Status.ToString();
            transferXML.Attributes.Append(attr);

            foreach (Condition condition in Start)
            {
                XmlNode startXML = xml.CreateNode(XmlNodeType.Element, "Start", "");
                XmlAttribute startNameAttr = xml.CreateAttribute("name");
                startNameAttr.Value = condition.Name;
                startXML.Attributes.Append(startNameAttr);
                transferXML.AppendChild(startXML);
            }

            foreach (Condition condition in End)
            {
                XmlNode startXML = xml.CreateNode(XmlNodeType.Element, "End", "");
                XmlAttribute startNameAttr = xml.CreateAttribute("name");
                startNameAttr.Value = condition.Name;
                startXML.Attributes.Append(startNameAttr);
                transferXML.AppendChild(startXML);
            }

            XmlNode expressionXml = xml.CreateNode(XmlNodeType.Element, "Expression", "");
            expressionXml.InnerText = Expression;

            transferXML.AppendChild(expressionXml);
            transfersXML.AppendChild(transferXML);
        }

        public Image GetImage()
        {
            throw new NotImplementedException();
            //Line line = new Line(Start.Location, End.Location, new LineCapBase(), new AsynchronousMessageCap());
            //return line.GetImage();
        }
    }
}
