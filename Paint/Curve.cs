using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Paint
{
    [Serializable]
    class Curve : Figure
    {
        List<Point> points = new List<Point>();
        public Curve(Color color, float depth) : base (color, depth)
        {

        }
        public void AddPoint(Point p)
        {
            points.Add(p);
        }
        public override void Draw(Graphics gr)
        {
            Pen p = new Pen(Color, Depth);

            for (int i = 0; i < points.Count - 1; i++)
            {
                gr.DrawLine(p, points[i].X, points[i].Y, points[i + 1].X, points[i + 1].Y);
            } 

        }
    }
}
