using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace OOP_drow
{
   public class Actor : Rectangle_object
    {
        Point[] Act = new Point[8];//Отрисовка самого актера внутри рентагле

        override public void Draw(Graphics gr)
        {
            Pen Pe = new Pen(Line_color);
            Pe.Width = Line_width;
            Point FP = new Point();//левый верхний угол
            Point SP = new Point();//нижний правый угол

            //Какая координата Х будет левой, а какая - правой
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

            //Какая координата У будет верхней, а какая - нижней
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

            int s = (int)((SP.X + FP.X) / 2);//Середина рисунка (по вертикали)
            int k = SP.Y - FP.Y; //ширина рисунка
            int headX = (int)((SP.X - FP.X) * 0.3);//расстояние края головы от границы прямоугольника
            Act[0] = new Point(FP.X + headX, FP.Y);//левая вернхняя точка головы
            Act[1] = new Point(SP.X - headX, FP.Y + (int)(0.3 * k));//правая нижняя точка головы
            Act[2] = new Point(s, FP.Y + (int)(0.3 * k));//шея (начало туловища)
            Act[3] = new Point(s, FP.Y + (int)(0.7 * k));//конец туловища
            Act[4] = new Point(FP.X, FP.Y + (int)(0.4 * k));//левая рука
            Act[5] = new Point(SP.X, FP.Y + (int)(0.4 * k));//правая рука
            Act[6] = new Point(FP.X, SP.Y);//конец левой ноги
            Act[7] = new Point(SP.X, SP.Y);//конец правой ноги
            gr.DrawEllipse(Pe, Act[0].X, Act[0].Y, Act[1].X - Act[0].X, Act[1].Y - Act[0].Y);//голова
            gr.DrawLine(Pe, Act[2], Act[3]);//туловище
            gr.DrawLine(Pe, Act[4], Act[5]);//руки
            gr.DrawLine(Pe, Act[3], Act[6]);//левая нога
            gr.DrawLine(Pe, Act[3], Act[7]);//правая нога
        }
    }
}
