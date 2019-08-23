using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace OOP_drow
{
    [Serializable] 
    class MyPicture
    {
        List<Point> points;
        List<int> shirinas;
        List<Color> colors;
        

        public List<int> Shirinas
        {
            get { return shirinas; }
            set { shirinas = value; }
        }
        
        public List<Color> Colors
        {
            get { return colors; }
            set { colors = value; }
        }
        

        public List<Point> Points
        {
            get { return points; }
            set { points = value; }
        }

        

        public void AddLine(Point old, Point current, int shirina, Color c)
        {
            points.Add(old);
            points.Add(current);
            colors.Add(c);
            colors.Add(c);
            shirinas.Add(shirina);
            shirinas.Add(shirina);
        }



        public MyPicture() 
        {
            points= new List<Point>();
            shirinas = new List<int>();
            colors = new List<Color>();
        }
    }
}
