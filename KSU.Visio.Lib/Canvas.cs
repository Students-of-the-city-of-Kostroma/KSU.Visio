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
		protected List<Figure> figures = new List<Figure>();
		public event EventHandler Changed;
		protected Graphics canvas = null;
		protected Size size;

		protected Bitmap image = null;
		public Bitmap Image { get { return image; } }

		public Canvas(Size size)
		{
			this.size = size;
			this.image = new Bitmap(size.Width, size.Height);
			this.canvas = Graphics.FromImage(this.image);
		}
		~Canvas()
		{
			canvas.Dispose();
		}

		private void UpdateCanvas()
		{
			canvas.Clear(Color.Transparent);
			foreach (Figure figure in this.figures)
				if(figure.Selected)
                    figure.Draw(canvas);
            foreach (Figure figure in this.figures)
                if (!figure.Selected)
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
				if (figure.Inside(xy))
					return figure;
			}
			return null;
		}

	}
}
