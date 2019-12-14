using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace KSU.Visio.Lib
{
	/// <summary>
	/// Класс "Узел"
	/// </summary>
	public class Node:Figure
	{
		public Node(Point location, Size size) : base(location, size)
		{

		}

		public override Figure Clone()
		{
			Figure figure = new Node(Location, Size);
			figure.Selected = Selected;
			return figure;
		}

		public override void Draw(Graphics gr)
		{
			base.Draw(gr);
			int x1 = location.X;
			int y1 = location.Y;
			Point[] points = { new Point(x1, y1+Convert.ToInt32(size.Height*0.25)), new Point(x1+Convert.ToInt32(size.Width*4/5), y1+Convert.ToInt32(size.Height*0.25)), new Point(x1+Convert.ToInt32(size.Width-1), y1), new Point(x1+Convert.ToInt32(size.Width*0.25),y1), new Point(x1, y1 + Convert.ToInt32(size.Height * 0.25)), new Point(x1,y1+Convert.ToInt32(size.Height-1)), new Point(x1+Convert.ToInt32(size.Width*4/5), y1+Convert.ToInt32(size.Height-1)), new Point(x1 + Convert.ToInt32(size.Width * 4 / 5), y1 + Convert.ToInt32(size.Height * 0.25)), new Point(x1 + Convert.ToInt32(size.Width - 1), y1), new Point(x1+Convert.ToInt32(size.Width-1), y1+Convert.ToInt32(size.Height*4/5)), new Point(x1+Convert.ToInt32(size.Width*4/5), y1+Convert.ToInt32(size.Height-1)),new Point(x1+Convert.ToInt32(size.Width*4/5), y1+Convert.ToInt32(size.Height*0.25)) };
			gr.DrawPolygon(pen, points);
		}

		public override bool Hit_testing(Point Point)
		{
			return Point.X > Location.X
				&& Point.Y > Location.Y
				&& Point.X < Location.X + size.Width
				&& Point.Y < Location.Y + size.Height;
		}
	}
}
