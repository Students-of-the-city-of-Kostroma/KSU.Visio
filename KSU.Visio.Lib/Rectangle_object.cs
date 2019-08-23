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
        }

        public override bool Hit_testing(Figure Figure, Point Point)
        {
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
            Basic_points[0] = FP;
            Basic_points[1] = SP;

            if (Point.X > Figure.Basic_points[0].X && Point.Y > Figure.Basic_points[0].Y && Point.X < Figure.Basic_points[1].X && Point.Y < Figure.Basic_points[1].Y)
            {
                return true;
            }
            else return false;
        }

        public override void Shift(Point K)
        {
            //Находим разницу между первой и второй точкой
            int Xn = Basic_points[1].X - Basic_points[0].X;
            int Yn = Basic_points[1].Y - Basic_points[0].Y;
            //За новую первую точку берем положение мыши
            Basic_points[0] = new Point(K.X, K.Y);
            //За новую вторую - положение мыши + высчитанная разница
            Xn = Basic_points[0].X + Xn;
            Yn = Basic_points[0].Y + Yn;
            Basic_points[1] = new Point(Xn, Yn);
        }

    }
}
