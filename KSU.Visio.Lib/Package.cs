using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace KSU.Visio.Lib
{
	/// <summary>
	/// Класс Пакет
	/// </summary>
	public class Package: Figure
	{
		/// <summary>
		/// Конструктор класса "Пакет"
		/// </summary>
		/// <param name="location">Расположение</param>
		/// <param name="size">Размер</param>
		public Package(Point location, Size size) : base(location, size)
		{

		}

		public override Figure Clone()
		{
			Figure figure = new Package(Location, Size);
			figure.Selected = Selected;
			return figure;
		}

		public override void Draw(Graphics gr)
		{
			base.Draw(gr);
			int x1 = location.X;
			int y1 = location.Y;
			Point[] points = { new Point(x1, y1),new Point(x1+(Convert.ToInt32(size.Width*0.3)),y1), new Point(x1 + (Convert.ToInt32(size.Width * 0.3)), y1+(Convert.ToInt32(size.Height*0.25))), new Point(x1, y1+ (Convert.ToInt32(size.Height * 0.25))), new Point(x1, y1+(Convert.ToInt32(size.Height))-1), new Point(x1 + (Convert.ToInt32(size.Width)-1), y1 + (Convert.ToInt32(size.Height))-1), new Point(x1 + (Convert.ToInt32(size.Width)-1),y1+(Convert.ToInt32(size.Height*0.25))), new Point(x1, y1 + (Convert.ToInt32(size.Height * 0.25))) };
			gr.DrawPolygon(pen, points);
		}
	}
}
