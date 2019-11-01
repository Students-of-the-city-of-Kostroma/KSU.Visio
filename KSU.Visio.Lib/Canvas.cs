using KSU.Visio.Lib.StateDiagram;
using KSU.Visio.Lib.Xml;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace KSU.Visio.Lib
{
    [Serializable]
    public class Canvas
	{
        internal List<Figure> figures = new List<Figure>();
		public event EventHandler Changed;
		protected Graphics canvas = null;
        protected Size size;


		protected Bitmap image = null;
		public Bitmap Image { get { return image; } }

        public virtual void ToXml(XmlDocument xml)
        {
            XmlNode sizeXML = Xml.XmlConvert.ToXmlNode(xml, size);

            XmlNode figuresXML = xml.CreateNode(XmlNodeType.Element, "Figures", "");
            foreach (Figure figure in figures)
                figuresXML.AppendChild(figure.ToXml(xml));

            XmlNode root = xml.DocumentElement;
            root.AppendChild(sizeXML);
            root.AppendChild(figuresXML);            
        }

        public Canvas(XmlDocument xml)
        {
            XmlNode emulatorXML = xml.DocumentElement;
            XmlNode sizeXML = emulatorXML.SelectSingleNode("Size");
            this.size = Xml.XmlConvert.XmlNodeToSize(sizeXML);
            Init();
        }

		public Canvas(Size size)
		{
			this.size = size;
            Init();
        }

        void Init()
        {
            this.image = new Bitmap(size.Width, size.Height);
            this.canvas = Graphics.FromImage(this.image);
        }
		~Canvas()
		{
			canvas.Dispose();
		}

        public virtual void UpdateCanvas()
		{
			canvas.Clear(Color.Transparent);
			foreach (Figure figure in this.figures)
				if(figure.Selected)
                    figure.Draw(canvas);
            foreach (Figure figure in this.figures)
                if (!figure.Selected)
                    figure.Draw(canvas);
            Changed?.Invoke(this, new EventArgs());
        }

		public virtual void AddFigure(Figure figure)
		{
			this.figures.Add(figure);
			figure.Changed += Figure_Changed;
			UpdateCanvas();
		}

		private void Figure_Changed(object sender, EventArgs e)
		{
			UpdateCanvas();
		}

		public Figure GetFigureClickedOn(Point xy)
		{
			foreach (Figure figure in this.figures)
			{
				if (figure.Inside(xy))
					return figure;
			}
			return null;
		}
        public void SaveToXMLFile(string path = "test.xml")
        {
            XmlDocument xml = new XmlDocument();
            xml.LoadXml("<?xml version=\"1.0\"?><"+GetType().Name+" />");
            ToXml(xml);
            xml.Save(path);
        }
    }
}
