using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace KSU.Visio.Lib
{
    public class Text : Rectangle_object
    {
        public Text(Point location, Size size)
            : base(location, size) { }
        string txt = "";

        Font FontText = new Font("Courier New", 12);//считываем шрифт с текстбокса
		SolidBrush Brush = new SolidBrush(Color.Black);//черная тушь для текста

        public string Textstring
        {
            get { return txt; }
            set { txt = value; }
        }
        override public void Draw(Graphics gr)
        {
            Pen Pe = new Pen(Color.Transparent);
            Pe.Width = 1;
            
            gr.DrawRectangle(pen, Location.X+Size.Width, Location.Y, Location.X - Location.X+Size.Width, Location.Y - Location.Y);
            gr.DrawString(txt, FontText, Brush, Location.X+Size.Width, Location.Y);
        }
    }
}
