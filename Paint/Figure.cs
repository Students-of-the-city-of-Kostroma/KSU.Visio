using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Paint
{
    [Serializable]
    abstract public class Figure
    {
        //public Pen Pen { get; set; }
        public Color Color { get; set; }
        public float Depth { get; set; }
        //public Figure()
        //{
        //    Pen = new Pen(Color.Black, 1);
        //}
        public abstract void Draw(Graphics gr);
        public Figure(Color color, float depth)// прямоугольник и круг Pen pen
        {
            Color = color;
            Depth = depth;
            //Pen = pen;
        }             
    }
}
