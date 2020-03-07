using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace KSU.Visio.Lib
{
	/// <summary>
	/// Класс необходим для рисования объекта компонент 
	/// </summary>
	public class Component :Figure
	{
		/// <summary>
		/// Конструктор класса "Компонент". Вызывает конструктор класса Figure. Присваивает значения location и size.
		/// </summary>
		/// <param name="location">Расположение</param>
		/// <param name="size">Размер</param>
		public Component(Point location, Size size) : base(location, size)
		{

		}
		/// <summary>
		///  Метод, создает копию этого объекта
		/// </summary>
		/// <returns>Возвращает копию фигуры</returns>
		public override Figure Clone()
		{
			Figure figure = new Component(Location, Size);
			figure.Selected = Selected;
			return figure;
		}
		/// <summary>
		/// Метод который отрисовывает данную фигуру в заданных координатах, с заданным размером.
		/// </summary>
		/// <param name="gr">Чем рисовать</param>
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
