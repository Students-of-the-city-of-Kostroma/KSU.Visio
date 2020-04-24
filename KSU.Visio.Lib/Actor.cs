using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace KSU.Visio.Lib
{
    /// <summary>
    /// Актер
    /// </summary>
    public class Actor : Figure
    {

        public Actor(Point location, Size size) : base(location, size)
        {
            
        }

        public override Figure Clone()
        {
            Figure figure = new Actor(Location, Size)
            {
                Selected = Selected
            };
            return figure;
        }

        public override void Draw(Graphics gr)
        {
            base.Draw(gr);
            //разметка
            int centerX = Location.X + Size.Width / 2;//Середина рисунка по вертикали
            //голова
            int headD = (int)(((Size.Height<Size.Width)? Size.Height: Size.Width) * 0.3);
            int headX = Location.X + Size.Width / 2 - headD / 2;
            int headY = Location.Y;
            //туловише
            int bodyY = Location.Y + (int)(Size.Height * 0.6);
            //руки
            int handY = Location.Y + (int)(Size.Height * 0.4);

            gr.DrawEllipse(pen, headX, headY, headD, headD);//голова
            gr.DrawLine(pen, centerX, Location.Y + headD, centerX, bodyY);//туловище
            gr.DrawLine(pen, Location.X, handY, Location.X + Size.Width, handY);//руки
            gr.DrawLine(pen, Location.X, Location.Y + Size.Height, centerX, bodyY);//левая нога
            gr.DrawLine(pen, centerX, bodyY, Location.X + Size.Width, Location.Y + Size.Height);//правая нога
        }
    }
}
