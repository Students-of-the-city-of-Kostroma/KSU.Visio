using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Drawing2D;
using System.Drawing;

namespace KSU.Visio.Lib
{
    public class Life_line : Rectangle_object
    {
        int Line_length = 50;
        static Pen Pen = new Pen(Brushes.Black, 3);
        Point[] Line_ends = new Point[2];
        bool Edit_line = false;//Определает, нужно ли изменять длинну линии

        public Life_line(int FPx = 10, int FPy = 10, int SPx = 20, int SPy = 20)
            : base(FPx, FPy, SPx, SPy) { }

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
            Pen.DashStyle = DashStyle.Dash;
            Pen.Color = Pe.Color;
            SortLine(); 
            gr.DrawLine(Pen, Line_ends[0], Line_ends[1]);


        }
        private void SortLine()
        {
            Point FP = new Point();//левая верхняя точка 
            Point SP = new Point();//правая нижняя точка 
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
            int mid = (LeftTop.X + RightBottom.X) / 2;//середина нижней линии прямоугольника (из этой точки выходит линия жизни) 
            Line_ends[0] = new Point(mid, SP.Y);
            Line_ends[1] = new Point(mid, SP.Y + Line_length);
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
                Edit_line = false;
                return true;
            }
            else return false;
        }

        public bool Hit_testing_line(Point Point)
        {
            SortLine();
            //уравнение окружности с центром в точке клика (находим растояние до ближайшей точки данного объекта)
            double d1 = Math.Sqrt(Math.Pow(Point.X - (this as Life_line).Line_ends[1].X, 2) + Math.Pow(Point.Y - (this as Life_line).Line_ends[1].Y, 2));
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
            int Xn = RightBottom.X - LeftTop.X;
            int Yn = RightBottom.Y - LeftTop.Y;
            //За новую первую точку берем положение мыши
            LeftTop = new Point(Point.X, Point.Y);
            //За новую вторую - положение мыши + высчитанная разница
            Xn = LeftTop.X + Xn;
            Yn = LeftTop.Y + Yn;
            RightBottom = new Point(Xn, Yn);
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
