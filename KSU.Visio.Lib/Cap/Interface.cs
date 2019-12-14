using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSU.Visio.Lib.Cap
{
	public class Interface: LineCapBase
	{
		public Interface ():base()
		{
			strokePath.AddEllipse(5, 10, -10, -10);
		}
	}
}
