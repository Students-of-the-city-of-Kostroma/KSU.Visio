using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace KSU.Visio.Lib
{
    public class Line : Figure
    {
        //Выбранный конец. 0 - первый конец, 1- второй конец
        int Selected_end = -1;

        public override void Draw(Graphics gr)
        {
            Pen Pe = new Pen(Line_color);
            Pe.Width = Line_width;
            gr.DrawLine(Pe, LeftTop, RightBottom);
        }

        public override bool Hit_testing(Point Point)
        {
            //уравнение окружности с центром в точке клика (находим растояние до ближайшей точки данного объекта)
            double d1 = Math.Sqrt(Math.Pow(Point.X - LeftTop.X, 2) + Math.Pow(Point.Y - LeftTop.Y, 2));
            double d2 = Math.Sqrt(Math.Pow(Point.X - RightBottom.X, 2) + Math.Pow(Point.Y - RightBottom.Y, 2));
            int r = 8;//радиус

            // Если расстояние d1 меньше радиуса, то цепляем первый конец для перемещения, если d2 - второй
            if (d1 <= r)
            {
                Selected_end = 0;
                return true;
            }
            if (d2 <= r)
            {
                Selected_end = 1;
                return true;
            }
            return false;
        }

        public override void Shift(Point Point)
        {
            if (Selected_end == 0)
            {
                LeftTop = Point;
            }
            if (Selected_end == 1)
            {
                RightBottom = Point;
            }
        }
    }
}
