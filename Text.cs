using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace OOP_drow
{
    class Text : Rectangle_object
    {
        string txt = "";
        static TextBox Text1 = new TextBox();//текстбокс для считывания шрифта
        Font FontText = new Font(Text1.Font, FontStyle.Bold);//считываем шрифт с текстбокса
        SolidBrush Brush = new SolidBrush(Color.Black);//черная тушь для текста

        public string Textstring
        {
            get { return txt; }
            set { txt = value; }
        }
        override public void Draw(Graphics gr)
        {
            Pen Pe = new Pen(Line_color);
            Pe.Width = 1;
            Point FP = new Point();//левый верхний угол
            Point SP = new Point();//правый нижний угол
                                   //Определяет, какая координата Х принадлежит левой, а какая парвой точке
            if (Basic_points[0].X < Basic_points[1].X)
            {
                FP.X = Basic_points[0].X;
                SP.X = Basic_points[1].X;
            }
            else
            {
                SP.X = Basic_points[0].X;
                FP.X = Basic_points[1].X;
            }
            //Определяет, какая координата У принадлежит верхней, а какая нижней точке
            if (Basic_points[0].Y < Basic_points[1].Y)
            {
                FP.Y = Basic_points[0].Y;
                SP.Y = Basic_points[1].Y;
            }
            else
            {
                SP.Y = Basic_points[0].Y;
                FP.Y = Basic_points[1].Y;
            }
            gr.DrawRectangle(Pe, FP.X, FP.Y, SP.X - FP.X, SP.Y - FP.Y);
            gr.DrawString(txt, FontText, Brush, FP.X, FP.Y);
        }
    }
}
