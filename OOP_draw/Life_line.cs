using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Drawing2D;
using System.Drawing;

namespace OOP_drow
{
    public class Life_line : Rectangle_object
    {
        int Line_length = 50;
        static Pen Pen = new Pen(Brushes.Black, 3);
        Point[] Line_ends = new Point[2];
        bool Edit_line = false;//Определает, нужно ли изменять длинну линии

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
            Pen.DashStyle = DashStyle.Dash;
            Pen.Color = Pe.Color;
            SortLine(); 
            gr.DrawLine(Pen, Line_ends[0], Line_ends[1]);


        }
        private void SortLine()
        {
            Point FP = new Point();//левая верхняя точка 
            Point SP = new Point();//правая нижняя точка 
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
            int mid = (Basic_points[0].X + Basic_points[1].X) / 2;//середина нижней линии прямоугольника (из этой точки выходит линия жизни) 
            Line_ends[0] = new Point(mid, SP.Y);
            Line_ends[1] = new Point(mid, SP.Y + Line_length);
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
                Edit_line = false;
                return true;
            }
            else return false;
        }

        public bool Hit_testing_line(Figure Figure, Point Point)
        {
            SortLine();
            //уравнение окружности с центром в точке клика (находим растояние до ближайшей точки данного объекта)
            double d1 = Math.Sqrt(Math.Pow(Point.X - (Figure as Life_line).Line_ends[1].X, 2) + Math.Pow(Point.Y - (Figure as Life_line).Line_ends[1].Y, 2));
            int r = 8;//радиус. Если расстояние d1 меньше радиуса, то цепляем конец для перемещения
            if (d1 <= r)
            {
                Edit_line = true;
                return true;
            }

            return false;
        }

        public override void Shift(Point Point)
        {
            if (Edit_line)
            {
                Shift_line(Point);
                return;
            }
            //Находим разницу между первой и второй точкой
            int Xn = Basic_points[1].X - Basic_points[0].X;
            int Yn = Basic_points[1].Y - Basic_points[0].Y;
            //За новую первую точку берем положение мыши
            Basic_points[0] = new Point(Point.X, Point.Y);
            //За новую вторую - положение мыши + высчитанная разница
            Xn = Basic_points[0].X + Xn;
            Yn = Basic_points[0].Y + Yn;
            Basic_points[1] = new Point(Xn, Yn);
        }

        public void Shift_line(Point K)
        {
            if (K.Y < Line_ends[0].Y + 10)//Если предлагаемое новое расстояние меньше десяти пикселей, то ничего не делаем
                return;
            int t = K.Y - Line_ends[1].Y;//Находим разницу между старым концом линии жизни и новым, а затем меняем расстояние
            Line_ends[1] = K;
            if (Line_length > t)
                Line_length += t;
            else Line_length -= t;

        }
    }
}
