using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace KSU.Visio.Lib
{
    public class Instance_specification : Rectangle_object
    {
        Point[] Internal_line = new Point[2];

        override public void Draw(Graphics gr)
        {
            Pen Pe = new Pen(Line_color);
            Pe.Width = Line_width;
            Point FP = new Point();//левая верхняя точка
            Point SP = new Point();//правая нижняя точка
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
            int k1 = FP.X + 20; //левая граница линии
            int k2 = (int)(FP.Y + 0.3 * (SP.Y - FP.Y));//расстояние межу верхней границы прямоугольника и линией
            int k3 = SP.X - 20;//правая граница линии
            Internal_line[0] = new Point(k1, k2);
            Internal_line[1] = new Point(k3, k2);
            gr.DrawLine(Pe, Internal_line[0], Internal_line[1]);
        }
    }
}
