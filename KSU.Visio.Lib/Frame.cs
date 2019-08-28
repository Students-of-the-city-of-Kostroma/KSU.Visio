using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace KSU.Visio.Lib
{
    public class Frame : Rectangle_object
    {
        Point[] Corner = new Point[4];//хранит точки, отвечающие за угол фрейма

        public Frame(int FPx, int FPy, int SPx, int SPy)
            : base(FPx, FPy, SPx, SPy) { }
        public Frame()
            : base(10, 10, 20, 20) { }
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
            int k1x = (int)(FP.X + 0.2 * (SP.X - FP.X));//координата Х первой точки (которая лежит на верхней границе) и той, что под ней
            int k2y = (int)(FP.Y + 0.15 * (SP.Y - FP.Y));//координата У второй  точки, которая лежит под первой
            int k3x = (int)(FP.X + 0.15 * (SP.X - FP.X));//координата х третей точки, которая лежит левее и ниже второй точки
            int k3y = (int)(FP.Y + 0.2 * (SP.Y - FP.Y));//координата у третей точки, а так же четвертой точки (которая лежит на левой границе)
            Corner[0] = new Point(k1x, FP.Y);
            Corner[1] = new Point(k1x, k2y);
            Corner[2] = new Point(k3x, k3y);
            Corner[3] = new Point(FP.X, k3y);
            gr.DrawLine(Pe, Corner[0], Corner[1]);
            gr.DrawLine(Pe, Corner[1], Corner[2]);
            gr.DrawLine(Pe, Corner[2], Corner[3]);
        }

    }
}
