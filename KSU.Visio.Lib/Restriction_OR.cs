﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace KSU.Visio.Lib
{
	/// <summary>
	/// Класс необходим для рисования объекта "Ограничение ИЛИ".
	/// </summary>
	public class Restriction_OR:Figure
	{
		/// <summary>
		/// Конструктор класса "Ограничение ИЛИ". Вызывает конструктор класса Figure. Присваивает значения location и size.
		/// </summary>
		/// <param name="location">Расположение</param>
		/// <param name="size">Размер</param>
		public Restriction_OR(Point location, Size size) : base(location, size)
		{

		}
		/// <summary>
		///  Метод, создает копию этого объекта
		/// </summary>
		/// <returns>Возвращает копию фигуры</returns>
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