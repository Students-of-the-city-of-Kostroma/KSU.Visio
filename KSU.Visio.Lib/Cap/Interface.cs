using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSU.Visio.Lib.Cap
{
	/// <summary>
	/// Класс "интерфейс". Интерфейс имеет ввиду интерфейс как элемент диаграммы. Этот класс необходим для отображения круга на конце линии круга.
	/// </summary>
	public class Interface: LineCapBase
	{
		/// <summary>
		/// Конструктор, вызыает конструктор класса LineCapBase. Создает новую "кастомную" линию. Рисует на конце линии круг.
		/// </summary>
		public Interface ():base()
		{
			strokePath.AddEllipse(5, 10, -10, -10);
		}
	}
}
