using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace KSU.Visio.Lib
{
    public class Text : Rectangle_object
    {

        string txt = "";
        //static TextBox Text1 = new TextBox();//текстбокс для считывания шрифта
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
            Point FP = new Point();//левый верхний угол
            Point SP = new Point();//правый нижний угол
                                   //Определяет, какая координата Х принадлежит левой, а какая парвой точке
            if (LeftTop.X < RightBottom.X)
            {
                FP.X = LeftTop.X;
                SP.X = RightBottom.X;
            }
            else
            {
                SP.X = LeftTop.X;
                FP.X = RightBottom.X;
            }
            //Определяет, какая координата У принадлежит верхней, а какая нижней точке
            if (LeftTop.Y < RightBottom.Y)
            {
                FP.Y = LeftTop.Y;
                SP.Y = RightBottom.Y;
            }
            else
            {
                SP.Y = LeftTop.Y;
                FP.Y = RightBottom.Y;
            }
            gr.DrawRectangle(Pe, FP.X, FP.Y, SP.X - FP.X, SP.Y - FP.Y);
            gr.DrawString(txt, FontText, Brush, FP.X, FP.Y);
        }
    }
}
