using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace KSU.Visio.Lib
{
    public abstract class Figure : ICloneable
    {
        public abstract object Clone();
        public static void PointsToLocationAndSize(Point p1, Point p2, out Point location, out Size size)
        {
            location = new Point(
                ((p1.X < p2.X) ? p1.X : p2.X),
                ((p1.Y < p2.Y) ? p1.Y : p2.Y));
            size = new Size(
                Math.Abs(p1.X - p2.X),
                Math.Abs(p1.Y - p2.Y));
        }
        public event EventHandler Changed;
        protected Point location;
        protected Size size;

        public Point Location
        {
            get { return location; }
            set
            {
                if (location != value)
                {
                    location = value;
                    if (Changed != null) Changed(this, new EventArgs());
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
                    if (Changed != null) Changed(this, new EventArgs());
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
        /// перо по умолчанию
        /// </summary>
        protected Pen penDefault = new Pen(Color.Black, 2);


		/// <summary>
		/// Нарисовать фигуру
		/// </summary>
		/// <param name="gr">чем рисовать</param>
		public abstract void Draw(Graphics gr);

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
            Location = Location + (Size)delta;
        }
    }
}
