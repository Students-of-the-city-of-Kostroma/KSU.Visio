using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace KSU.Visio.Lib
{
    public class White_rectangle : Rectangle_object
    {
        SolidBrush Brush = new SolidBrush(Color.White);

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
            gr.FillRectangle(Brush, FP.X, FP.Y, SP.X - FP.X, SP.Y - FP.Y);
            gr.DrawRectangle(Pe, FP.X, FP.Y, SP.X - FP.X, SP.Y - FP.Y);

        }

    }
}
