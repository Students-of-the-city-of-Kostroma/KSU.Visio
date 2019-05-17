using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Paint
{
    [Serializable]
    class MyRectangle: Figure
    {
        private Rectangle rec = new Rectangle();     // хранит  x, y, width, Height
        public MyRectangle(Color color, float depth, int x, int y, int width, int height) : base(color, depth)
        {
            X = x;
            Y = y;
            H = height;
            W = width;            
        }
        public int X
        {
            get
            {
                return rec.X;
            }
            set
            {
                rec.X = value;
            }
        }

        public int Y
        {
            get
            {
                return rec.Y;
            }
            set
            {
                rec.Y = value;
            }
        }
        public int H
        {
            get
            {
                return rec.Height;
            }
            set
            {
                rec.Height = value;
            }
        }
        public int W
        {
            get
            {
                return rec.Width;
            }
            set
            {
                rec.Width = value;
            }
        }           
        public override void Draw(Graphics gr)
        {
            Pen p = new Pen(Color,Depth);
            gr.DrawRectangle(p, rec);
        }
    }
}
