using KSU.Visio.Lib.Cap;
using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
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
        protected string sourceBegin = @"
            using System.Collections.Generic;
            namespace " + snamespace + "{" +
            "public class " + sclass + "{" +
            "public void " + smehod + "(object obj){ " +
            "Dictionary<string, object> dict = obj as Dictionary<string, object>;";
        protected string sourceEnd = @"}}}";
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

        public static void SetLink(Condition c1, Transfer t1, Condition c2)
        {
            t1.Start.Add(c1);
            t1.End.Add(c2);
            c1.Outputs.Add(t1);
            c1.Inputs.Add(t1);
        }

        public Transfer(XmlNode transferXML, Emulator emulator) 
        {
            //string startName = transferXML.Attributes["start"].Value;
            //string endName = transferXML.Attributes["end"].Value;
            //    foreach (SDBase figure in emulator.figures)
            //    {
            //        if (figure.Name == startName) Start = figure;
            //        if (figure.Name == endName) End = figure;
            //    }
            //    Expression = transferXML.InnerText;
            //    EndLineCap = new AsynchronousMessageCap();
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
        public List<Condition> Start { get; set; }
        public List<Condition> End { get; set; }
        public string Expression { get; set; }

        public void Run(Dictionary<string,object> dict)
        {
            CompilerResults results = provider.CompileAssemblyFromSource(compilerParams, sourceBegin + Expression + sourceEnd);

            if (results.Errors.Count != 0)
                throw new Exception("Mission failed!");

            object o = results.CompiledAssembly.CreateInstance(snamespace + "." + sclass);
            MethodInfo mi = o.GetType().GetMethod(smehod);
            mi.Invoke(o, new object[] { dict });
        }

        public virtual void ToXml(XmlDocument xml, XmlNode transfersXML)
        {

            XmlNode transferXML = xml.CreateNode(XmlNodeType.Element, "Transfer", "");

            XmlAttribute nameAttr = xml.CreateAttribute("name");
            nameAttr.Value = Name;
            transferXML.Attributes.Append(nameAttr);

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
