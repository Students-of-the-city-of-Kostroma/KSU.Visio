using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace OOP_drow
{
    class Instance_specification : Rectangle_object
    {
        Point[] Internal_line = new Point[2];

        override public void Draw(Graphics gr)
        {
            Pen Pe = new Pen(Line_color);
            Pe.Width = Line_width;
            Point FP = new Point();//левая верхняя точка
            Point SP = new Point();//правая нижняя точка
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
            int k1 = FP.X + 20; //левая граница линии
            int k2 = (int)(FP.Y + 0.3 * (SP.Y - FP.Y));//расстояние межу верхней границы прямоугольника и линией
            int k3 = SP.X - 20;//правая граница линии
            Internal_line[0] = new Point(k1, k2);
            Internal_line[1] = new Point(k3, k2);
            gr.DrawLine(Pe, Internal_line[0], Internal_line[1]);
        }
    }
}
