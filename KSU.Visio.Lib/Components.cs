using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace KSU.Visio.Lib
{
	/// <summary>
	/// Класс компонент 
	/// </summary>
	public class Components :Figure
	{
		public Components(Point location, Size size) : base(location, size)
		{

		}

		public override Figure Clone()
		{
			Figure figure = new Components(Location, Size);
			figure.Selected = Selected;
			return figure;
		}

		public override void Draw(Graphics gr)
		{
			base.Draw(gr);
			int x1 = location.X;
			int y1 = location.Y;
			gr.DrawRectangle(pen, (x1 + (Convert.ToInt32(size.Width / 10))), y1, size.Width - size.Width / 8, size.Height-1);
			gr.DrawRectangle(pen, x1, (y1+ Convert.ToInt32(size.Height / 7)), Convert.ToInt32(size.Width/5), Convert.ToInt32( size.Height/7));
			gr.DrawRectangle(pen, x1, (y1 + Convert.ToInt32(size.Height / 7*5)), Convert.ToInt32(size.Width / 5), Convert.ToInt32(size.Height / 7));
		}

	}
}
