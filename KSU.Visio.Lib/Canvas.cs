using KSU.Visio.Lib.Xml;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml;

namespace KSU.Visio.Lib
{
    public class Canvas
	{
        protected List<Figure> figures = new List<Figure>();
		public event EventHandler Changed;
		protected Graphics canvas = null;
        protected Size size;
        public Size Size
        {
            get { return size; }
            set
            {
                size = value;
                Init();
                UpdatePositionFigures();
            }
        }


		protected Bitmap image = null;
		public Bitmap Image { get { return image; } }

        public virtual void ToXml(XmlDocument xml)
        {
            //XmlAttribute sizeAttr = xml.CreateAttribute("size");
            //sizeAttr.Value = size.ToString(); 

            XmlNode figuresXML = xml.CreateNode(XmlNodeType.Element, "Models", "");
            foreach (Figure figure in figures)
                figure.ToXml(xml, figuresXML);

            XmlNode root = xml.DocumentElement;
            //root.Attributes.Append(sizeAttr);
            root.AppendChild(figuresXML);            
        }

        public Canvas(XmlDocument xml)
        {
            XmlNode emulatorXML = xml.DocumentElement;
            XmlAttribute sizeXML = emulatorXML.Attributes["size"];
            if (sizeXML == null)
                Size = new Size(100, 100);
            else
                Size = SDXmlConvert.StringWHToSize("{X=150, Y=150}");
            Init();
        }

		public Canvas(Size size)
		{
			this.Size = size;
            Init();
        }

        void Init()
        {
            this.image = new Bitmap(Size.Width, Size.Height);
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
                figure.Draw(canvas);
            Changed?.Invoke(this, new EventArgs());
        }

		public virtual void AddFigure(Figure figure)
		{
            this.figures.Add(figure);
            figure.Changed += Figure_Changed;
			UpdateCanvas();
		}
        public void UpdatePositionFigures()
        {
            Size fSize = new Size(200, 20);

            for (int i = 0; i < figures.Count; i++)
            {
                if (figures[i].Location.X == 0 && figures[i].Location.Y == 0)
                    figures[i].Location = new Point(0, i * (fSize.Height + 5));
                if (figures[i].Size.Width == 0 && figures[i].Size.Height == 0)
                    figures[i].Size = fSize;
            }
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
