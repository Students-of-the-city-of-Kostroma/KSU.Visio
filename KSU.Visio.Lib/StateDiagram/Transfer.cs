using KSU.Visio.Lib.Cap;
using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

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
        protected static string snamespace = "KSU.Visio.Lib.StateDiagram.Transfer";
        protected static string sclass = "Expression";
        protected static string smehod = "Run";
        protected static Dictionary<string, string> providerOptions = new Dictionary<string, string> { { "CompilerVersion", "v3.5" } };
        protected static CSharpCodeProvider provider = new CSharpCodeProvider(providerOptions);
        protected static CompilerParameters compilerParams = new CompilerParameters
        {
            GenerateInMemory = true,
            GenerateExecutable = false
        };
        protected string sourceBegin = "" +
            "using System.Collections.Generic;" +
            "using System;" +
            "using System.IO;" +
            "" +
            "namespace " + snamespace + "{" +
            "public class " + sclass + "{" +
            "public void " + smehod + "(object obj){ " +
            "try{" +
            "Dictionary<string, object> dict = obj as Dictionary<string, object>;";
        protected string sourceEnd = "}catch (Exception ex){" +
            "using (FileStream fs = File.Open(DateTime.Now.ToString(\"yyyyMMdd\") + \".log\", FileMode.OpenOrCreate, FileAccess.Write))" +
            "{" +
                "using (StreamWriter sw = new StreamWriter(fs))" +
                "{" +
                    "sw.BaseStream.Position = sw.BaseStream.Length;" +
                    "sw.WriteLine(\"DEBUG|\" + DateTime.Now.ToString() + \"|\" + ex.Message);"+
                "}" +
            "}}}}}";
        /// <summary>
        /// Описание конца линии
        /// </summary>
        protected LineCapBase EndLineCap { get; set; }

        public Transfer()
        {
            Probability = 1;
            EndLineCap = new AsynchronousMessageCap();
            Start = new List<Condition>();
            End = new List<Condition>();
        }
        public Transfer(XmlNode root, Condition owner)
        {
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

            using (FileStream fs = File.Open(DateTime.Now.ToString("yyyyMMdd") + ".code.log", FileMode.OpenOrCreate, FileAccess.Write))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.BaseStream.Position = sw.BaseStream.Length;
                    sw.WriteLine(DateTime.Now.ToString() + "|" + Name + "|" + source);
                }
            }

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
