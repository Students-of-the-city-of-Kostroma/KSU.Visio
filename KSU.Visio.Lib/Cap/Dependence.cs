using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSU.Visio.Lib.Cap
{
	/// <summary>
	/// Класс зависимость
	/// </summary>
	public class Dependence: LineCapBase
	{
		public Dependence() : base()
		{
			strokePath.AddLine(0, 0, -3, -10);
			strokePath.AddLine(0, 0, 3, -10);
		}
	}
}
