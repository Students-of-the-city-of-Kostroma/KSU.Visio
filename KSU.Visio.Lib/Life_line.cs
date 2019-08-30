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

        public Life_line(Point location, Size size)
            : base(location, size) { }

        override public void Draw(Graphics gr)
        {
            Pen Pe = (Pen)penDefault.Clone();
            gr.DrawRectangle(penDefault, Location.X+Size.Width, Location.Y, Location.X - Location.X+Size.Width, Location.Y - Location.Y);
            Pen.DashStyle = DashStyle.Dash;
            Pen.Color = Pe.Color;
            SortLine(); 
            gr.DrawLine(Pen, Line_ends[0], Line_ends[1]);


        }
        private void SortLine()
        {
            
            int mid = (Location.X + Location.X+Location.X+Size.Width) / 2;//середина нижней линии прямоугольника (из этой точки выходит линия жизни) 
            Line_ends[0] = new Point(mid, Location.Y);
            Line_ends[1] = new Point(mid, Location.Y + Line_length);
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
