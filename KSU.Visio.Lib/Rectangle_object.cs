using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace KSU.Visio.Lib
{
	/// <summary>
	/// Класс рисующий прямоугольную область.
	/// </summary>
	public class Rectangle_object : Figure
    {
		/// <summary>
		/// Вызывает конструктор класса Figure. Присваивает значения location и size.
		/// </summary>
		/// <param name="location">Расположение</param>
		/// <param name="size">Размер</param>
		public Rectangle_object(Point location, Size size)
            : base(location, size) { }
		/// <summary>
		/// Метод возвращает копию этого объекта
		/// </summary>
		/// <returns>Возвращает копию фигуры</returns>
        public override Figure Clone()
        {
            return new Rectangle_object(Location, Size);
        }
		/// <summary>
		/// Метод который отрисовывает данную фигуру в заданных координатах, с заданным размером.
		/// </summary>
		/// <param name="gr">Чем рисовать</param>
		public override void Draw(Graphics gr)
        {
            gr.DrawRectangle(pen, Location.X+Size.Width, Location.Y, Location.X - Location.X+Size.Width, Location.Y - Location.Y);
        }
	}
}
