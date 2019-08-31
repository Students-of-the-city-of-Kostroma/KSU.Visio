using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace KSU.Visio.Lib
{
    public abstract class Figure
    {
        public abstract Figure Clone();

        public static Size PointsToSize(Point p1, Point p2)
        {
            return new Size(
                Math.Abs(p1.X - p2.X),
                Math.Abs(p1.Y - p2.Y));
        }
        public static Point PointsToLocation(Point p1, Point p2)
        {
            return new Point(
                ((p1.X < p2.X) ? p1.X : p2.X),
                ((p1.Y < p2.Y) ? p1.Y : p2.Y));
        }
        public event EventHandler Changed;
        protected Point location;
        protected Size size;
        protected bool selected = false;
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
        
        protected void ChangedMetod()
        {
            Changed?.Invoke(this, new EventArgs());
        }

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

        public Figure(Point location, Size size)
        {
            this.Location = location;
            this.size = size;
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

        /// <summary>
        /// перо для рисования выделенного объекта
        /// </summary>
        protected Pen penSelected = new Pen(Color.BlueViolet, 1);
        /// <summary>
        /// перо для рисования объекта
        /// </summary>
        protected Pen penDefault = new Pen(Color.Black, 1);

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
    }
}
