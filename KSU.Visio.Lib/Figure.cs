﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace KSU.Visio.Lib
{
    public abstract class Figure
    {
		public event EventHandler Changed;
		private Point LeftTop = new Point();//левый верхний угол
		private Point RightBottom = new Point();//правый нижний угол

		public Figure (Point FP, Point SP)
		{
			//Определяет, какая координата Х принадлежит левой, а какая парвой точке
			if (FP.X < SP.X)
			{
				FP.X = FP.X;
				SP.X = SP.X;
			}
			else
			{
				SP.X = FP.X;
				FP.X = SP.X;
			}
			//Определяет, какая координата У принадлежит верхней, а какая нижней точке
			if (FP.Y < SP.Y)
			{
				FP.Y = FP.Y;
				SP.Y = SP.Y;
			}
			else
			{
				SP.Y = FP.Y;
				FP.Y = SP.Y;
			}
		}

		/// <summary>
		/// ширина линии
		/// </summary>
		public int Line_width = 2;

		private Color line_color;
		/// <summary>
		/// цвет
		/// </summary>
		public Color Line_color {
			get { return line_color; }
			set
			{
				line_color = value;
				if (Changed != null) Changed(this,new EventArgs());
			}
		}



		public abstract void Draw(Graphics gr);

        public abstract bool Hit_testing(Figure Figureigure, Point Point);

        public abstract void Shift(Point Point);


    }
}