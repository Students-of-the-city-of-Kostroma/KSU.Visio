using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSU.Visio.Lib.Cap
{
	/// <summary>
	/// Класс зависимость. Этот необходим для добавления стреки на конце линии
	/// </summary>
	public class Dependence: LineCapBase
	{
		/// <summary>
		/// Конструктор, вызыает конструктор класса LineCapBase. Рисует на конце линии стрелку.
		/// </summary>
		public Dependence() : base()
		{
			strokePath.AddLine(0, 0, -3, -10);
			strokePath.AddLine(0, 0, 3, -10);
		}
	}
}
