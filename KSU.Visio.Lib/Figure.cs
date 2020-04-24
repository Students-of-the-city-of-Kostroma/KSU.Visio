using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Xml;
using KSU.Visio.Lib.StateDiagram;

namespace KSU.Visio.Lib
{
	/// <summary>
	/// Абстрактный класс, реализует базовые функции для фигуры
	/// </summary>
    [Serializable]
    public abstract class Figure
    {
        public string Name { get; set; }
        //public Guid ID => id;
        //protected Guid id;
        public abstract Figure Clone();
		/// <summary>
		/// Метод вычисляющий размер фигуры исходя из двух точек. Принимает на вход две точки возвращает размер фигуры.
		/// </summary>
		/// <param name="p1"></param>
		/// <param name="p2"></param>
		/// <returns></returns>
        public static Size PointsToSize(Point p1, Point p2)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            return new Size(
                Math.Abs(p1.X - p2.X),
                Math.Abs(p1.Y - p2.Y));
        }
		/// <summary>
		/// Метод вычисляющий расположение фигуры. Принимает на вход две точки Point. Возвращает точку Point расположение фигуры.
		/// </summary>
		/// <param name="p1"></param>
		/// <param name="p2"></param>
		/// <returns></returns>
		public static Point PointsToLocation(Point p1, Point p2)
        {
            return new Point(
                ((p1.X < p2.X) ? p1.X : p2.X),
                ((p1.Y < p2.Y) ? p1.Y : p2.Y));
        }
		/// <summary>
		/// событие, которое представляет делегат EventHandler. Генерируется когда задаются поля selected, size и location.
		/// </summary>
		public event EventHandler Changed;
		/// <summary>
		/// поле типа Point, хранящее расположение фигуры. Это поле является верхним левым углом
		/// </summary>
		protected Point location;
		/// <summary>
		///  поле типа Size, хранящее размер фигуры
		/// </summary>
		protected Size size;
		/// <summary>
		/// поле типа bool. Необходимо для выделения выбранного компонента
		/// </summary>
		protected bool selected = false;
		/// <summary>
		/// Свойства поля selected. Возвращает значание, либо если полю не присвоено значение, то присваевает его и вызывает ChangedMetod
		/// </summary>
		public bool Selected
        {
            get { return selected; }
            set
            {
                if (selected != value)
                {
                    selected = value;
                    ChangedMetod();
                }
            }
        }
        /// <summary>
		/// Метод который создает новый объект.
		/// </summary>
        protected void ChangedMetod()
        {
            Changed?.Invoke(this, new EventArgs());
        }


        public virtual XmlNode ToXml(XmlDocument xml, XmlNode root)
        {
            XmlNode figureXML = xml.CreateNode(XmlNodeType.Element, GetType().Name, "");

            XmlAttribute nameAttr = xml.CreateAttribute("name");
            nameAttr.Value = Name;

            //XmlAttribute locAttr = xml.CreateAttribute("location");
            //locAttr.Value = location.ToString();
            
            //XmlAttribute sizeAttr = xml.CreateAttribute("size");
            //sizeAttr.Value = size.ToString();
            
            //XmlAttribute idXML = xml.CreateAttribute("id");
            //idXML.Value = id.ToString();

            figureXML.Attributes.Append(nameAttr);
            //figureXML.Attributes.Append(idXML);
            //figureXML.Attributes.Append(locAttr);
            //figureXML.Attributes.Append(sizeAttr);

            root.AppendChild(figureXML);

            return figureXML;
        }

		/// <summary>
		/// Свойства поля location. Возвращает значание, либо если полю не присвоено значение, то присваевает его и вызывает ChangedMetod
		/// </summary>
		public Point Location
        {
            get { return location; }
            set
            {
                if (location != value)
                {
                    location = value;
                    ChangedMetod();
                }
            }
        }
		/// <summary>
		/// Свойства поля size. Возвращает значание, либо если полю не присвоено значение, то присваевает его и вызывает ChangedMetod
		/// </summary>
		public Size Size
        {
            get { return size; }
            set
            {
                if (size != value)
                {
                    size = value;
                    ChangedMetod();
                }
            }
        }

        public Figure(XmlNode figureXML)
        {
            Name = figureXML.Attributes["name"].Value;

            this.Location = Xml.SDXmlConvert.XmlNodeToPoint(
                figureXML.SelectSingleNode("Location"));
            
            this.Size = Xml.SDXmlConvert.XmlNodeToSize(
                figureXML.SelectSingleNode("Size"));

            //this.id = new Guid(figureXML.Attributes["id"].Value);
        }
		/// <summary>
		/// Конструктор класса Figure. Присваивает значения location и size.
		/// </summary>
		/// <param name="location"></param>
		/// <param name="size"></param>
		public Figure(Point location, Size size)
        {
            this.Location = location;
            this.size = size;
            //id = Guid.NewGuid();
        }
        /// <summary>
        /// Вернет изображение размеров с фигуру с изображением фигуры
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public Bitmap GetImage()
        {
            Bitmap bm = new Bitmap(size.Width, size.Height);
            Graphics gr = Graphics.FromImage(bm);
            Draw(gr);
            gr.Dispose();
            return bm;
        }
        public Rectangle ToRectangle()
        {
            return new Rectangle(location, size);
        }
        /// <summary>
        /// перо для рисования выделенного объекта
        /// </summary>
        protected Pen penSelected = new Pen(Color.BlueViolet, 1);
        /// <summary>
        /// перо для рисования объекта
        /// </summary>
        protected Pen pen = new Pen(Color.Black, 1);
        protected Font font = new Font("Arial", 10f);
        protected Brush brush = new SolidBrush(Color.Black);



        /// <summary>
        /// Предварительная прорисовка объекта
        /// </summary>
        /// <param name="gr">чем рисовать</param>
        public virtual void Draw(Graphics gr)
        {
            if (Selected)
            {
                gr.FillRectangle(Brushes.BlueViolet, new Rectangle(Location , Size));
            }
        }

		/// <summary>
		/// Проверяет, попадает ли точка внутрь прямоугольной области фигуры
		/// </summary>
		/// <param name="point">точка</param>
		/// <returns></returns>
        public bool Inside(Point point)
        {
            return point.X > Location.X
                && point.Y > Location.Y
                && point.X < Location.X + size.Width
                && point.Y < Location.Y + size.Height;
        }

        /// <summary>
        /// Сместить фигуру
        /// </summary>
        /// <param name="delta">на сколько сместить фигуру</param>
        public void Move(Point delta)
        {
            Location += (Size)delta;
        }
		/// <summary>
		/// Проверяет, попадает ли точка внутрь прямоугольной области фигуры
		/// </summary>
		/// <param name="point">точка</param>
		/// <returns></returns>
		public virtual bool Hit_testing(Point Point)
		{
			return Point.X > Location.X
				&& Point.Y > Location.Y
				&& Point.X < Location.X + size.Width
				&& Point.Y < Location.Y + size.Height;
		}
	}
}
