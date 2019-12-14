using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace KSU.Visio.Lib
{
	public class Restriction_OR:Figure
	{
		public Restriction_OR(Point location, Size size) : base(location, size)
		{

		}

		public override Figure Clone()
		{
			Figure figure = new Restriction_OR(Location, Size);
			figure.Selected = Selected;
			return figure;
		}

		public override void Draw(Graphics gr)
		{

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
