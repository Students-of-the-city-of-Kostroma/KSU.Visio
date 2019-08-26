using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSU.Visio.Lib
{
	public class Canvas
	{
		private List<Figure> figures = new List<Figure>();
		public event EventHandler Changed;
		private Graphics canvas = null;
		public Canvas(Point size)
		{

		}

		public Canvas(Graphics canvas)
		{
			this.canvas = canvas;
		}

		private void UpdateCanvas()
		{
			canvas.Clear(Color.White);
			foreach (Figure figure in this.figures)
				figure.Draw(canvas);
			if (Changed != null) Changed(this, new EventArgs());
		}

		public void AddFigure(Figure figure)
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
				if (figure.Hit_testing(xy))
					return figure;
			}
			return null;
		}

		protected Graphics GetImage { get => canvas; }
	}
}
