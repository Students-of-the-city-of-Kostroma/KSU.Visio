using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace OOP_drow
{

    public class MyPanel : Panel
    {
        public MyPanel()
        {
            this.DoubleBuffered = true;
            this.ResizeRedraw = true;
        }
    }

    class FigureControl
    {
        int dx=0, dy=0;
        Pen p = new Pen(Color.LightGreen,2);
        List<Figures> lstOfFigure = new List<Figures>();
        public Figures cs;
        public FigureControl()
        {
            p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
        }

        public void AddNew(Figures s)
        {
            if (cs != s)
            {
                lstOfFigure.Add(s);
                cs = s;
            }
        }

        public void Remove(Figures s)
        {
            lstOfFigure.Remove(s);
            cs = null;
        }

        public void DrawSh(Graphics gr)
        {
            foreach (Figures s in lstOfFigure)
                s.DrawSh(gr);
            if (cs != null)
            {

                gr.DrawRectangle(p, cs.sp.X-5, cs.sp.Y-5, cs.width+10, cs.height+10);
            }
        }
        public void OnMouseMove(Point location)
        {
            cs.SetSPEP(new Point(location.X+dx,location.Y+dy), new Point(location.X + cs.width+dx, location.Y  + cs.height+dy));
            
        }

        public Figures CreateFigure(String s)
        {
            dx =dy= 0;

            if (s == "Object")
                return new CInstance();
            if (s == "Rect")
                return new DiaObj();
            return null;
        }

        public Boolean SelectFigure(Point p)
        {
            cs = null;
            for (int i = lstOfFigure.Count - 1; i >= 0; i--)
            {
                if (lstOfFigure[i].HasPoint(p))
                {
                    cs = lstOfFigure[i];
                    dx = cs.sp.X - p.X;
                    dy = cs.sp.Y - p.Y;
                    return true;
                }

            }
            return false;
        }
        public void RemoveFigure(Figures shp)
        {
            lstOfFigure.Remove(shp);
        }

    }

    class Figures
    {

        protected String Text;
        protected List<Figures> CInstanceListOfFigures = new List<Figures>();
        protected Point[] poligonpoints=null;
        protected Pen selectedpen = (new Pen(Color.Blue, 1));
        protected int ControlPointRectangleHalfSize = 5;
        protected int checkrange = 5;

        protected int deltapoint = 5;
        //Line ln=new Line();
        
        public Pen P
            
        {
            get { return p; }
            set
            {
                p = value;
            }
        }


        public void SetText(String s)
        {
            Text = s;
            if (s=="")
                Text=null;
        }

        public virtual void SetSPEP(Point sp, Point ep)
        {
            
            
            this.sp = sp;
            this.ep = ep;
            if (sp.X > ep.X)
            {
                
                this.sp.X = ep.X;
                this.ep.X = sp.X;
            }

            if (sp.Y > ep.Y)
            {
                
                this.sp.Y = ep.Y;
                this.ep.Y = sp.Y;
            }

            height = ep.Y - sp.Y;
            width = ep.X - sp.X;




        }
        protected Pen p=new Pen(Color.Black,3);
        protected bool selected = true;
        protected int DefaultSize = 25;
        public int height=150, width=150;
        public Point sp, ep;
        public bool Selected { get { return selected; } set { selected = value; } }

        public virtual Boolean HasPoint(Point p)
        {
            foreach (Figures s in CInstanceListOfFigures)
            {
                if (s.HasPoint(p))
                    return true;
            }
            return false;

        }
        protected Rectangle rect;


        //public bool Selected { get { return selected; } set { selected = value; } }

        public void SetSP(Point p)
        {
            sp = p;
            foreach (Figures s in CInstanceListOfFigures)
                s.SetSP(p);
        }
        public virtual void DrawSh( Graphics gr)
        {
      
            foreach (Figures s in CInstanceListOfFigures)
            {
                s.DrawSh(gr);
            }
      

        }

        public Figures()
        {
            sp = new Point(0, 0);
            ep.X = sp.X + width;
            ep.Y = sp.Y + height;
        }

        public virtual void SetRect(Point p1, Point p2)
        {
            sp = p1;
            ep = p2;
            if (sp.X == ep.X && sp.Y == ep.Y)
            {
                ep.X = sp.X + DefaultSize;
                ep.Y = sp.Y + DefaultSize;
            }

            int x, y, height, width;

            x = p1.X > p2.X ? p2.X : p1.X;
            y = p1.Y > p2.Y ? p2.Y : p1.Y;
            height = Math.Abs(p1.Y - p2.Y);
            width = Math.Abs(p1.X - p2.X);
            rect = new Rectangle(x, y, width, height);
        }

        public virtual Rectangle GetRect()
        {
            return new Rectangle(sp.X,sp.Y,ep.X-sp.X,ep.Y-sp.Y);
        }
    
    }


    class Line : Figures
    {
        public override void DrawSh(Graphics gr)
        {
            gr.DrawLine(p,sp.X,sp.Y,sp.X+width,sp.Y+height);
            if (Text != null)
            {
                //gr.MeasureString(Text, new Font("Arial", 10), width);
                //gr.DrawString(
            }

        }

        public override bool HasPoint(Point p)
        {
            if (width == 0)
                if (p.X <= sp.X + deltapoint && p.X >= sp.X - deltapoint)
                    if (p.Y <= sp.Y - deltapoint + height && p.Y >= sp.Y - deltapoint)
                        return true;
            if (width != 0)
            if (p.X <= sp.X+width  && p.X >= sp.X )
            {
                int y;
                y = (p.X - sp.X) * (height) / (width) + sp.Y;
                if (p.Y <= y + deltapoint  && p.Y >= y - deltapoint)
                return true;
            }
            return false;
        }
    }


    class DiaObj : Figures
    {
        public DiaObj()
        {
            
            CInstanceListOfFigures.Add(new Line());
            CInstanceListOfFigures.Add(new Line());
            CInstanceListOfFigures.Add(new Line());
            CInstanceListOfFigures.Add(new Line());
            SetSPEP(sp,ep);
            
            
            SetText("Это 1 ");
        }


        public override void SetSPEP(Point sp1, Point ep1)
        {
            base.SetSPEP(sp1,ep1);
            CInstanceListOfFigures[0].SetSPEP(sp, new Point(sp.X, ep.Y));
            CInstanceListOfFigures[1].SetSPEP(sp,new Point(ep.X, sp.Y));
            CInstanceListOfFigures[2].SetSPEP(new Point(ep.X, sp.Y),new Point(ep.X, ep.Y));
            CInstanceListOfFigures[3].SetSPEP(new Point(sp.X, ep.Y), new Point(ep.X, ep.Y));
        }
        public override void DrawSh(Graphics gr)
        {
            base.DrawSh(gr);
            if (Text != null)
            {
                //gr.MeasureString(Text, new Font("Arial", 10), width);
                
                //gr.TranslateTransform(sp.X, sp.Y);
                //gr.RotateTransform(30);
                gr.DrawString(Text, new Font("Arial", 10), new SolidBrush(Color.Black), 
                    //sp.X, sp.Y);
                    new RectangleF(sp.X,sp.Y,width,height));
                //gr.ResetTransform();
            }
        }
    }


    class CInstance : Figures
    {
        

        public CInstance()
        {
            CInstanceListOfFigures.Add(new DiaObj());
            CInstanceListOfFigures.Add(new Line());
            SetSPEP(sp, ep);
         }
        public override void SetSPEP(Point sp1, Point ep1)
        {
            base.SetSPEP(sp1, ep1);
            CInstanceListOfFigures[0].SetSPEP(sp, new Point(ep.X, sp.Y + (ep.Y - sp.Y) / 2));
            CInstanceListOfFigures[1].SetSPEP(new Point(sp.X+(ep.X - sp.X) / 2, sp.Y + (ep.Y - sp.Y) / 2), new Point(sp.X+(ep.X - sp.X) / 2, ep.Y));
        }
        
       
    }

    class Star : Triangle
    {


        public override void DrawSh( Graphics gr)
        {


            //Int32[,] pa = new Int32[n*2, 2];
            //Point p1, p2 = new Point(), p3 = new Point();
            //Point[] par = new Point[n*2];
            //double angstep = Math.PI * 2 / n;
            //double r, sinf, cosf, sinp, cosp, sin120, cos120;
            //pa[0, 0] = ep.X;
            //pa[0, 1] = ep.Y;

            //r = Math.Sqrt(Math.Pow((ep.X - sp.X), 2) + Math.Pow((ep.Y - sp.Y), 2));
            //double r2 = r / 3;
            //sinf = (ep.Y - sp.Y) / r;
            //cosf = (ep.X - sp.X) / r;

            //cos120 = Math.Cos(angstep / 2);
            //sin120 = Math.Sin(angstep / 2);
            //sinp = sinf * cos120 + cosf * sin120;
            //cosp = cosf * cos120 - sinf * sin120;
            //pa[1, 0] = (Int32)(sp.X + r2 * cosp);
            //pa[1, 1] = (Int32)(sp.Y + r2 * sinp);
            //par[0] = new Point();
            //gr.DrawLine(p, pa[0, 0], pa[0, 1], pa[1, 0], pa[1, 1]);
            //par[0].X = pa[0, 0];
            //par[0].Y = pa[0, 1];

            //for (int i = 1; i < n; i++)
            //{
            //    cos120 = Math.Cos(angstep * i);
            //    sin120 = Math.Sin(angstep * i);
            //    sinp = sinf * cos120 + cosf * sin120;
            //    cosp = cosf * cos120 - sinf * sin120;
            //    pa[2*i, 0] = (Int32)(sp.X + r * cosp);
            //    pa[2*i, 1] = (Int32)(sp.Y + r * sinp);
            //    gr.DrawLine(p, pa[2*i - 1, 0], pa[2*i - 1, 1], pa[2*i, 0], pa[2*i, 1]);
            //    cos120 = Math.Cos(angstep * i+angstep / 2);
            //    sin120 = Math.Sin(angstep * i+angstep / 2);
            //    sinp = sinf * cos120 + cosf * sin120;
            //    cosp = cosf * cos120 - sinf * sin120;
            //    pa[2*i+1, 0] = (Int32)(sp.X + r2 * cosp);
            //    pa[2 * i + 1, 1] = (Int32)(sp.Y + r2 * sinp);
            //    gr.DrawLine(p, pa[2 * i + 1, 0], pa[2 * i + 1, 1], pa[2 * i, 0], pa[2 * i, 1]);
            //    par[i] = new Point(pa[i, 0], pa[i, 1]);
            //}
            //gr.DrawLine(p, pa[0, 0], pa[0, 1], pa[2*n - 1, 0], pa[2*n - 1, 1]);
            Region reg;
            
            if (!selected)
            {
                gr.DrawPolygon(p, poligonpoints);
                gr.FillPolygon(new SolidBrush(Color.GreenYellow),poligonpoints);
            }
            {
                selectedpen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                gr.DrawPolygon(selectedpen, poligonpoints);
                gr.FillPolygon(new SolidBrush(Color.GreenYellow), poligonpoints);

                Rectangle rect;
                foreach (Point p in poligonpoints)
                {

                    rect=new Rectangle(p.X - ControlPointRectangleHalfSize, p.Y - ControlPointRectangleHalfSize, ControlPointRectangleHalfSize * 2, ControlPointRectangleHalfSize*2);
                    gr.DrawRectangle(selectedpen, rect);
                    gr.FillRectangle(new SolidBrush(Color.LightGreen), rect);
                }
            }

            //            Region r = new Region(new System.Drawing.Drawing2D.RegionData());

            //gr.FillPolygon(new SolidBrush(p.Color), par);
            //gr.FillClosedCurve(new SolidBrush(p.Color),par);
            //cos120 = Math.Cos(Math.PI / 180 * 120);
            //sin120=Math.Sin(Math.PI / 180 * 120);
            //sinp = sinf * cos120 + cosf *sin120 ;
            //cosp = cosf * cos120 - sinf * sin120;
            //p2.X = (Int32)(sp.X + r * cosp);
            //p2.Y = (int)(sp.Y + r * sinp);
            //sinp = sinf * cos120 - cosf * sin120;
            //cosp = cosf * cos120 + sinf * sin120;
            //p3.X = (Int32)(sp.X + r * cosp);
            //p3.Y = (int)(sp.Y + r * sinp);
            //gr.DrawLine(p,ep,p2);
            //gr.DrawLine(p, p2, p3);
            //gr.DrawLine(p, ep, p3);

        }


        public override bool HasPoint(Point p1)
        {

            //for (int i = 0; i < poligonpoints.Length; i++)
            //{
            //    //if poligonpoints[i].X 
            //}
                return false;
        }

        public override void SetRect(Point p11, Point p21)
        {
            
            base.SetRect(p11, p21);
            poligonpoints = new Point[n * 2];

            Int32[,] pa = new Int32[n * 2, 2];
            Point p1, p2 = new Point(), p3 = new Point();
            Point[] par = new Point[n * 2];
            double angstep = Math.PI * 2 / n;
            double r, sinf, cosf, sinp, cosp, sin120, cos120;
            pa[0, 0] = ep.X;
            pa[0, 1] = ep.Y;

            r = Math.Sqrt(Math.Pow((ep.X - sp.X), 2) + Math.Pow((ep.Y - sp.Y), 2));
            double r2 = r / 3;
            sinf = (ep.Y - sp.Y) / r;
            cosf = (ep.X - sp.X) / r;

            cos120 = Math.Cos(angstep / 2);
            sin120 = Math.Sin(angstep / 2);
            sinp = sinf * cos120 + cosf * sin120;
            cosp = cosf * cos120 - sinf * sin120;
            pa[1, 0] = (Int32)(sp.X + r2 * cosp);
            pa[1, 1] = (Int32)(sp.Y + r2 * sinp);
            par[0] = new Point();
            //gr.DrawLine(p, pa[0, 0], pa[0, 1], pa[1, 0], pa[1, 1]);
            par[0].X = pa[0, 0];
            par[0].Y = pa[0, 1];
            par[1] = new Point(pa[1, 0], pa[1, 1]);
            for (int i = 1; i < n; i++)
            {
                cos120 = Math.Cos(angstep * i);
                sin120 = Math.Sin(angstep * i);
                sinp = sinf * cos120 + cosf * sin120;
                cosp = cosf * cos120 - sinf * sin120;
                pa[2 * i, 0] = (Int32)(sp.X + r * cosp);
                pa[2 * i, 1] = (Int32)(sp.Y + r * sinp);
                //gr.DrawLine(p, pa[2 * i - 1, 0], pa[2 * i - 1, 1], pa[2 * i, 0], pa[2 * i, 1]);
                par[2 * i ] = new Point(pa[2*i, 0], pa[2*i, 1]);
                cos120 = Math.Cos(angstep * i + angstep / 2);
                sin120 = Math.Sin(angstep * i + angstep / 2);
                sinp = sinf * cos120 + cosf * sin120;
                cosp = cosf * cos120 - sinf * sin120;
                pa[2 * i + 1, 0] = (Int32)(sp.X + r2 * cosp);
                pa[2 * i + 1, 1] = (Int32)(sp.Y + r2 * sinp);
                //gr.DrawLine(p, pa[2 * i + 1, 0], pa[2 * i + 1, 1], pa[2 * i, 0], pa[2 * i, 1]);
                par[2*i+1] = new Point(pa[2*i+1, 0], pa[2*i+1, 1]);
            }

            
            //par[2 * n-1] = par[0];
            poligonpoints = par;
        }

    }


    class Star2 : Triangle
    {

       
        public override void DrawSh(Graphics gr)
        {


            Int32[,] pa = new Int32[n * 2, 2];
            Point p1, p2 = new Point(), p3 = new Point();
            Point[] par = new Point[n * 2];
            double angstep = Math.PI * 2 / n;
            double r, sinf, cosf, sinp, cosp, sin120, cos120;
            pa[0, 0] = ep.X;
            pa[0, 1] = ep.Y;

            r = Math.Sqrt(Math.Pow((ep.X - sp.X), 2) + Math.Pow((ep.Y - sp.Y), 2));
            double r2 = r / 3;
            sinf = (ep.Y - sp.Y) / r;
            cosf = (ep.X - sp.X) / r;
            float f = (float)(Math.Asin(sinf) / Math.PI * 180);
            f = cosf < 0 ? 180-f: f;
            gr.TranslateTransform(sp.X,sp.Y);
            gr.RotateTransform(f);
            gr.DrawEllipse(p, 0, 0, ep.X - sp.X, ep.Y - sp.Y);
            //cos120 = Math.Cos(angstep / 2);
            //sin120 = Math.Sin(angstep / 2);
            //sinp = sinf * cos120 + cosf * sin120;
            //cosp = cosf * cos120 - sinf * sin120;
            //pa[0, 0] = (Int32)(r * cosp);
            //pa[0, 1] = (Int32)(r * sinp);
            //pa[0, 0] = (Int32)r;
            //pa[0, 1] = (Int32)0;
            //pa[1, 0] = (Int32)(r2 * cos120);
            //pa[1, 1] = (Int32)(r2 * sin120);
            //par[0] = new Point();
            //gr.DrawLine(p, pa[0, 0], pa[0, 1], pa[1, 0], pa[1, 1]);
            //par[0].X = pa[0, 0];
            //par[0].Y = pa[0, 1];

            //for (int i = 1; i < n; i++)
            //{
            //    cos120 = Math.Cos(angstep * i);
            //    sin120 = Math.Sin(angstep * i);
            //    sinp = sinf * cos120 + cosf * sin120;
            //    cosp = cosf * cos120 - sinf * sin120;
            //    pa[2 * i, 0] = (Int32)( r * cos120);
            //    pa[2 * i, 1] = (Int32)( r * sin120);
            //    gr.DrawLine(p, pa[2 * i - 1, 0], pa[2 * i - 1, 1], pa[2 * i, 0], pa[2 * i, 1]);
            //    cos120 = Math.Cos(angstep * i + angstep / 2);
            //    sin120 = Math.Sin(angstep * i + angstep / 2);
            //    sinp = sinf * cos120 + cosf * sin120;
            //    cosp = cosf * cos120 - sinf * sin120;
            //    pa[2 * i + 1, 0] = (Int32)( r2 * cos120);
            //    pa[2 * i + 1, 1] = (Int32)( r2 * sin120);
            //    gr.DrawLine(p, pa[2 * i + 1, 0], pa[2 * i + 1, 1], pa[2 * i, 0], pa[2 * i, 1]);
            //    par[i] = new Point(pa[i, 0], pa[i, 1]);
            //}
            //gr.DrawLine(p, pa[0, 0], pa[0, 1], pa[2 * n - 1, 0], pa[2 * n - 1, 1]);
            gr.ResetTransform();
            //gr.RotateTransform(-f);
            //gr.TranslateTransform(-sp.X, -sp.Y);
            //gr.FillPolygon(new SolidBrush(p.Color), par);
            //gr.FillClosedCurve(new SolidBrush(p.Color),par);
            //cos120 = Math.Cos(Math.PI / 180 * 120);
            //sin120=Math.Sin(Math.PI / 180 * 120);
            //sinp = sinf * cos120 + cosf *sin120 ;
            //cosp = cosf * cos120 - sinf * sin120;
            //p2.X = (Int32)(sp.X + r * cosp);
            //p2.Y = (int)(sp.Y + r * sinp);
            //sinp = sinf * cos120 - cosf * sin120;
            //cosp = cosf * cos120 + sinf * sin120;
            //p3.X = (Int32)(sp.X + r * cosp);
            //p3.Y = (int)(sp.Y + r * sinp);
            //gr.DrawLine(p,ep,p2);
            //gr.DrawLine(p, p2, p3);
            //gr.DrawLine(p, ep, p3);

        }

    }

    class MyEllipse : Figures
    {
       
        public override void DrawSh( Graphics gr)
        {
            

         gr.DrawEllipse(p,sp.X>ep.X?ep.X:sp.X,sp.Y>ep.Y?ep.Y:sp.Y,Math.Abs(sp.X-ep.X),Math.Abs(sp.Y-ep.Y));
            //gr.FillPolygon(new SolidBrush(p.Color), par);
            //gr.FillClosedCurve(new SolidBrush(p.Color),par);
            //cos120 = Math.Cos(Math.PI / 180 * 120);
            //sin120=Math.Sin(Math.PI / 180 * 120);
            //sinp = sinf * cos120 + cosf *sin120 ;
            //cosp = cosf * cos120 - sinf * sin120;
            //p2.X = (Int32)(sp.X + r * cosp);
            //p2.Y = (int)(sp.Y + r * sinp);
            //sinp = sinf * cos120 - cosf * sin120;
            //cosp = cosf * cos120 + sinf * sin120;
            //p3.X = (Int32)(sp.X + r * cosp);
            //p3.Y = (int)(sp.Y + r * sinp);
            //gr.DrawLine(p,ep,p2);
            //gr.DrawLine(p, p2, p3);
            //gr.DrawLine(p, ep, p3);

        }
    }

    public static class MathAndTrag
    {
        public static double GetRadius(Point p1, Point p2)
        {
            return Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
        }
    }


    class Person: Figures
    {
        Point cp;
        int proportion=2;
        int width, height;
        public override void SetRect(Point p1, Point p2)
        {
            base.SetRect(p1,p2);
            //sp = p1;
            //ep = p2;
            int x, y;

            
            height = Math.Abs(p1.Y - p2.Y);
            width = Math.Abs(p1.X - p2.X);
            if (height < proportion * width)
            {
                width = height / proportion;
            }
            if (height > proportion * width)
            {
                height = width * proportion;
                
            }
            sp.X = p1.X > p2.X ? p1.X-width : p1.X;
            sp.Y = p1.Y > p2.Y ? p1.Y-height : p1.Y;

           // rect = new Rectangle(x, y, width, height);
            ep.X = sp.X + width;
            ep.Y = sp.Y + height;
            cp.X = sp.X + width / 2;
            cp.Y = sp.Y + height / 2;
        }
        public override void DrawSh( Graphics gr)
        {


            if (ep != sp)
            {
                Pen contour = new Pen(Color.FromArgb(0, 0, 255), 1);
                contour.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                contour.DashOffset = 50F;
                contour.DashPattern = new float[] { 15.0F, 14.0F };



                ////    gr.el
                //    gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                //    if (Selected)
                //    {
                //gr.DrawRectangle(contour, new Rectangle(sp, new Size(ep.X - sp.X, ep.Y - sp.Y)));
                //    }
                

                gr.DrawEllipse(p, sp.X, sp.Y, ep.X - sp.X, ep.X - sp.X);
                gr.DrawLine(p, cp.X, sp.Y +width, cp.X, ep.Y -width/2);
                gr.DrawArc(p, sp.X, ep.Y - width / 2, width, width, 180, 180);
                gr.DrawLine(p, sp.X, cp.Y + width / 4, ep.X, cp.Y + width / 4);
                //gr.FillPolygon(new SolidBrush(p.Color), par);
                //gr.FillClosedCurve(new SolidBrush(p.Color),par);
                //cos120 = Math.Cos(Math.PI / 180 * 120);
                //sin120=Math.Sin(Math.PI / 180 * 120);
                //sinp = sinf * cos120 + cosf *sin120 ;
                //cosp = cosf * cos120 - sinf * sin120;
                //p2.X = (Int32)(sp.X + r * cosp);
                //p2.Y = (int)(sp.Y + r * sinp);
                //sinp = sinf * cos120 - cosf * sin120;
                //cosp = cosf * cos120 + sinf * sin120;
                //p3.X = (Int32)(sp.X + r * cosp);
                //p3.Y = (int)(sp.Y + r * sinp);
                //gr.DrawLine(p,ep,p2);
                //gr.DrawLine(p, p2, p3);
                //gr.DrawLine(p, ep, p3);
            }
        }
    }




    class Triangle : Figures
    {
        protected int n=5;
        
        public int N
        {
            get { return n;}
            set {if (value < 3) n = 3;
            else
                if (value > 10) n = 10;
                else
            n = value;}
        }

       

        public Triangle()
        {
            n = 3;
            p = new Pen(Color.Black);
        }



        public override Rectangle GetRect()
        {
            int R = (int)Math.Sqrt(Math.Pow((sp.X - ep.X), 2) + Math.Pow((sp.Y - ep.Y), 2));
            Rectangle rect;
            
            return new Rectangle(sp.X-(int)R, sp.Y-(int)R, (int)2*R, (int)2*R);
        }
        
        public override void DrawSh( Graphics gr)
        {
            Int32[,] pa = new Int32[n, 2];
            Point p1, p2 = new Point(), p3 = new Point();
            Point[] par=new Point[n];
            double angstep = Math.PI*2/n;
            double r, sinf, cosf, sinp, cosp, sin120, cos120;
            pa[0, 0] = ep.X;
            pa[0, 1] = ep.Y;
            r = Math.Sqrt(Math.Pow((ep.X - sp.X), 2) + Math.Pow((ep.Y - sp.Y), 2));
            sinf = (ep.Y - sp.Y) / r;
            cosf = (ep.X - sp.X) / r;
            par[0]=new Point();
            par[0].X=pa[0,0];
            par[0].Y=pa[0,1];
            
            for (int i = 1; i < n; i++)
            {
                cos120 = Math.Cos(angstep * i);
                sin120 = Math.Sin(angstep * i);
                sinp = sinf * cos120 + cosf * sin120;
                cosp = cosf * cos120 - sinf * sin120;
                pa[i, 0] = (Int32)(sp.X + r * cosp);
                pa[i, 1] = (Int32)(sp.Y + r * sinp);
                gr.DrawLine(p, pa[i - 1, 0], pa[i - 1, 1], pa[i, 0], pa[i, 1]);
                par[i] = new Point(pa[i, 0], pa[i, 1]);
            }
            gr.DrawLine(p, pa[0, 0], pa[0, 1], pa[n-1, 0], pa[n-1, 1]);

            //gr.FillPolygon(new SolidBrush(p.Color), par);
            //gr.FillClosedCurve(new SolidBrush(p.Color),par);
            //cos120 = Math.Cos(Math.PI / 180 * 120);
            //sin120=Math.Sin(Math.PI / 180 * 120);
            //sinp = sinf * cos120 + cosf *sin120 ;
            //cosp = cosf * cos120 - sinf * sin120;
            //p2.X = (Int32)(sp.X + r * cosp);
            //p2.Y = (int)(sp.Y + r * sinp);
            //sinp = sinf * cos120 - cosf * sin120;
            //cosp = cosf * cos120 + sinf * sin120;
            //p3.X = (Int32)(sp.X + r * cosp);
            //p3.Y = (int)(sp.Y + r * sinp);
            //gr.DrawLine(p,ep,p2);
            //gr.DrawLine(p, p2, p3);
            //gr.DrawLine(p, ep, p3);

        }
    }
}
