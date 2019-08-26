using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace KSU.Visio.Lib
{
  public class Rectangle_object : Figure
    {
        override public void Draw(Graphics gr)
        {
            Pen Pe = new Pen(Line_color);
            Pe.Width = Line_width;
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
        }

        public override bool Hit_testing(Point Point)
        {
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
            LeftTop = FP;
            RightBottom = SP;

            if (Point.X > LeftTop.X && Point.Y > LeftTop.Y && Point.X < RightBottom.X && Point.Y < RightBottom.Y)
            {
                return true;
            }
            else return false;
        }

        public override void Shift(Point K)
        {
            //Находим разницу между первой и второй точкой
            int Xn = RightBottom.X - LeftTop.X;
            int Yn = RightBottom.Y - LeftTop.Y;
            //За новую первую точку берем положение мыши
            LeftTop = new Point(K.X, K.Y);
            //За новую вторую - положение мыши + высчитанная разница
            Xn = LeftTop.X + Xn;
            Yn = LeftTop.Y + Yn;
            RightBottom = new Point(Xn, Yn);
        }

    }
}
