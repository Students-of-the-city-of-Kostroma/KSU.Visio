using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Windows;


namespace OOP_drow
{
    public partial class Form1 : Form
    {
        FigureControl Figurecontrol = new FigureControl();
        RadioButton crb;
        List<Figures> StackOfFigure = new List<Figures>();
        Figures SelectedFigure;
        int shirina = 3;
        Point current = new Point();
        Point old = new Point();
        Point Mousedwn = new Point();
        Pen p = new Pen(Color.LightSeaGreen, 3);
        Pen l = new Pen(Color.White, 3);
        Pen n;
        System.Drawing.Drawing2D.GraphicsState gs;
        Graphics gr ;
        Graphics graph;
        Bitmap btm=new Bitmap(500,500);
        Bitmap btm1 = new Bitmap(500, 500);
        MyPicture mp = new MyPicture();
        BinaryFormatter bf = new BinaryFormatter();
        CopyFiles CF;
        string path1 = "", path2 = "";
        bool pause = false;
        Form F;
        //Star s = new Star();
        //Star2 s2 = new Star2();
        //Triangle t = new Triangle();
        Rectangle Figurerect;
        MyEllipse mel=new MyEllipse();
        Person person = new Person();
        Figures cs;
        delegate Figures NewFigure();
        NewFigure ns;
        public Form1()
        {
            InitializeComponent();
            //trackBar1.Focus();
            //p.SetLineCap(System.Drawing.Drawing2D.LineCap.Round, System.Drawing.Drawing2D.LineCap.Round, System.Drawing.Drawing2D.DashCap.Round);
            //graph = panel1.CreateGraphics();
        }

        private void button_color_Click(object sender, EventArgs e)
        {
            
            this.Refresh();
            ColorDialog cd = new ColorDialog();
            if (cd.ShowDialog() == DialogResult.OK)
            {
                p.Color = cd.Color;
                button_color.BackColor = p.Color;
                button_color.ForeColor = Color.FromArgb(p.Color.ToArgb() ^ Color.White.ToArgb());
            }
        }

        private void clear_Click(object sender, EventArgs e)
        {
            //panel1.Invalidate();
            gr.Clear(Color.White);
            btm.Dispose();
            btm = new Bitmap(btm1);
            Refresh();
            mp.Shirinas.Clear();
            mp.Points.Clear();
            mp.Colors.Clear();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            //shirina = trackBar1.Value;
            //p = new Pen(p.Color, shirina);
            //l = new Pen(Color.White, shirina);
            //l.SetLineCap(System.Drawing.Drawing2D.LineCap.Round, System.Drawing.Drawing2D.LineCap.Round, System.Drawing.Drawing2D.DashCap.Round);
            //p.SetLineCap(System.Drawing.Drawing2D.LineCap.Round, System.Drawing.Drawing2D.LineCap.Round, System.Drawing.Drawing2D.DashCap.Round);
            //label2.Text = trackBar1.Value.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //graph.Clear(Color.White);
            //label1.Invalidate();
            //using (var streem = new FileStream("risunok.dat", FileMode.Open))
            //{
            //    mp = (MyPicture)bf.Deserialize(streem);
            //}
            //if (mp.Points.Count > 0)
            //{
            //    for (int i = 0; i < mp.Points.Count - 1; i++)
            //    {
            //        p = new Pen(mp.Colors[i], mp.Shirinas[i]);
            //        p.SetLineCap(System.Drawing.Drawing2D.LineCap.Round, System.Drawing.Drawing2D.LineCap.Round, System.Drawing.Drawing2D.DashCap.Round);
            //        System.Threading.Thread.Sleep(1);
            //        graph.DrawLine(p, mp.Points[i], mp.Points[i + 1]);
            //        i++;
            //    }
            //}
        }




        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            for (int i = 0; i < mp.Points.Count - 1; i++)
            {
                n = new Pen(mp.Colors[i], mp.Shirinas[i]);
                n.SetLineCap(System.Drawing.Drawing2D.LineCap.Round, System.Drawing.Drawing2D.LineCap.Round, System.Drawing.Drawing2D.DashCap.Round);
                graph.DrawLine(n, mp.Points[i], mp.Points[i + 1]);
                i++;
            }
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                current = e.Location;
                graph.DrawLine(p, old, current);
                mp.AddLine(old, current, shirina, p.Color);
                old = current;
            }
            if (e.Button == MouseButtons.Right)
            {
                current = e.Location;
                graph.DrawLine(l, old, current);
                mp.AddLine(old, current, shirina, l.Color);
                old = current;
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            StackOfFigure.Add(cs);
        }

        private void panel1_MouseEnter(object sender, EventArgs e)
        {
            //trackBar1.Focus();
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (CF != null)
                CF.stop();
        }

        private void set(object sender, DoWorkEventArgs e)
        {
            //trackBar1.Value = CF.Step();
        }

        //private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        //{
        //    while (CF.Step() < 100)
        //    {
        //        if (backgroundWorker1.CancellationPending)
        //        {
        //            e.Cancel = true;
        //        }
        //        else
        //        {
        //            sm();
        //            backgroundWorker1.ReportProgress(CF.Step());
        //        }
        //    }
        //}

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void brush_Click(object sender, EventArgs e)
        {

        }

        private void treugol_Click(object sender, EventArgs e)
        {
            System.Drawing.Pen myPen = new System.Drawing.Pen(System.Drawing.Color.Red);
            System.Drawing.Graphics formGraphics;
            formGraphics = this.CreateGraphics();
            formGraphics.DrawRectangle(myPen, new Rectangle(0, 0, 200, 300));
            myPen.Dispose();
            formGraphics.Dispose();
        }

        private void quadro_Click(object sender, EventArgs e)
        {

        }

        private void ellipse_Click(object sender, EventArgs e)
        {


            System.Drawing.Pen myPen = new System.Drawing.Pen(System.Drawing.Color.Red);
            System.Drawing.Graphics formGraphics;
            formGraphics = this.CreateGraphics();
            formGraphics.DrawEllipse(myPen, new Rectangle(0, 0, 200, 300));
            myPen.Dispose();
            formGraphics.Dispose();
        }

        private void savebtn_Click(object sender, EventArgs e)
        {
            //if (panel1.Image != null) 

            //    SaveFileDialog savedialog = new SaveFileDialog();
            //    savedialog.Title = "Сохранить картинку как...";

            //    savedialog.OverwritePrompt = true;

            //    savedialog.CheckPathExists = true;

            //    savedialog.Filter = "Image Files(*.BMP)|*.BMP|Image Files(*.JPG)|*.JPG|Image Files(*.GIF)|*.GIF|Image Files(*.PNG)|*.PNG|All files (*.*)|*.*";

            //    savedialog.ShowHelp = true;
            //    if (savedialog.ShowDialog() == DialogResult.OK) 
            //    {
            //        try
            //        {
            //            image.Save(savedialog.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
            //        }
            //        catch
            //        {
            //            MessageBox.Show("Невозможно сохранить изображение", "Ошибка",
            //            MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        }
            //    }
            //}
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //e.Graphics.DrawImage(btm,0,0);
          //  gr.Clear(Color.White);
            //e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            //Figurecontrol.DrawSh(e.Graphics);
            Figures s = new CInstance();

            Graphics gr = pictureBox1.CreateGraphics();
            gr.Clear(Color.White);

            s.SetSPEP(new Point(0, 0), new Point(pictureBox1.Width - 5, pictureBox1.Height - 1));
            s.DrawSh(gr);

            //if (Figurerect != Rectangle.Empty)
            //    e.Graphics.DrawRectangle(new Pen(Color.OrangeRed,1), Figurerect);

//            e.Graphics.DrawImage(btm1,0,0);

          //  e.Graphics.DrawLine(new Pen(Color.Red), 0, 0, 100, 100);
            //e.Graphics.re
            //e.Graphics = gr;
            //e.Graphics.
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            ;
        }

       

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (ns != null)
            {
                cs = ns();
                cs.SetRect(e.Location, e.Location);
                StackOfFigure.Add(cs);
                Mousedwn = e.Location;
            }

            
            //this.Text = Mousedwn.X.ToString();
            //gr = this.CreateGraphics();
            
            //btm = new Bitmap(this.DisplayRectangle.Width, this.DisplayRectangle.Height);
            //int x, y;
            //x = (this.Size.Width - this.ClientSize.Width) / 2;
            //y = this.Size.Height- this.ClientSize.Height-x;
            //this.DrawToBitmap(btm, new Rectangle(0, 0, ClientRectangle.Width, ClientRectangle.Height));
            //btm.
            //this.CreateGraphics().
        }

            private void Form1_MouseMove(object sender, MouseEventArgs e)
            {
           
                    //btm1 = (Bitmap)btm.Clone();
                //btm.
              //  p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                    //
                //if (e.Button == MouseButtons.Left)
                //{
                //    if (Mousedwn != e.Location)
                //    {
                //        //gr.Clear(Color.White);
                //        //gr.DrawImage(btm, 0, 0);
                //        ////gr.DrawRectangle(new Pen(Color.Red), Mousedwn.X, Mousedwn.Y, e.Location.X - Mousedwn.X, e.Location.Y - Mousedwn.Y);
                        
                //        //cs.DrawSh(Mousedwn, e.Location, gr, p);

                //        if (cs!=null)
                //        {cs.SetRect(Mousedwn, e.Location);
                //        //Refresh();
                //        try
                //        {
                //            cs.SetSPEP(Mousedwn, e.Location);
                //        }
                //        catch
                //        {
                //        }
                //        Invalidate();}
                //    }
                //    return;
                //}

                //foreach (Figures s in StackOfFigure)
                //{
                    
                //    if (s.HasPoint(e.Location))
                //    {
                //        //Refresh();
                //        Cursor =Cursors.Arrow;
                //        return;
                        
                //    }
                //}
                //Cursor = Cursors.Default;
                    //System.Threading.Thread.Sleep(50);
           
            
            }

        private void Form1_Load(object sender, EventArgs e)
        {
            //ns = new NewFigure(T);
            //btm = new Bitmap(this.ClientRectangle.Width, ClientRectangle.Height);
            //btm1 = new Bitmap(pictureBox1.Width-10, pictureBox1.Height-10);
            //gr = Graphics.FromImage(btm1);

            //Figures s = new CInstance();
            //s.DrawSh(gr);
            
            //pictureBox1.Image = Image.FromHbitmap(btm1.GetHbitmap(Color.Blue));

            //button_color.BackColor = p.Color;
            
            //button_color.ForeColor = Color.FromArgb(p.Color.ToArgb() ^ Color.White.ToArgb());
         
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (ns != null)
                {
                    btm.Dispose();
                    btm = new Bitmap(btm1);
                    cs = null;
                    Invalidate();
                }
            }
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            
            //label3.Text = ((TrackBar)sender).Value.ToString();
            //((Triangle)cs).N = ((TrackBar)sender).Value;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            ns = new NewFigure(T);
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            ns = new NewFigure(S);
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            //trackBar2.Enabled = false;
            //cs = mel;
            ns = new NewFigure(S2);
            
        }

        private Figures S2()
        {
            //trackBar2.Enabled = true;
            Figures t = new CInstance();
            //t.N = trackBar2.Value;
            t.P = (Pen)p.Clone();
            return t;
            //trackBar2.Value = cs.N;

        }

        private Figures T()
        {
            ////trackBar2.Enabled = true;
            Triangle t = new Triangle();
            //t.N=trackBar2.Value;
            t.P = (Pen)p.Clone();
            return t;
            ////trackBar2.Value = cs.N;

        }


        private Figures P()
        {
            //trackBar2.Enabled = true;
            Figures t = new Person();            
            t.P = (Pen)p.Clone();
            return t;
            //trackBar2.Value = cs.N;

        }
      

        private Figures S()
        {
            //trackBar2.Enabled = true;
            Triangle t = new Star();
            //t.N = trackBar2.Value;
            t.P = (Pen)p.Clone();
            return t;
            //trackBar2.Value = cs.N;

        }

        private void trackBar2_ValueChanged(object sender, EventArgs e)
        {
            //label3.Text = ((TrackBar)sender).Value.ToString();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

            //trackBar2.Enabled = false;
            ns = new NewFigure(P);
        }

        private void Form1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ;
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            ns = null;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                DataFormats.Format df = DataFormats.GetFormat("Figures");
                Figures shp;
                shp = Figurecontrol.CreateFigure((String)((PictureBox)sender).Tag);
                    if (shp!=null)
                    {DataObject dtobj = new DataObject(df.Name,shp);                
                
                ((PictureBox)sender).DoDragDrop(dtobj,DragDropEffects.All);
                    }

            }
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {

        }

        private void Form1_DragOver(object sender, DragEventArgs e)
        {

        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            
         //   Figurecontrol.AddNew(new CInstance());
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void panel1_MouseMove_1(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (Mousedwn != e.Location)
                {
                    //gr.Clear(Color.White);
                    //gr.DrawImage(btm, 0, 0);
                    ////gr.DrawRectangle(new Pen(Color.Red), Mousedwn.X, Mousedwn.Y, e.Location.X - Mousedwn.X, e.Location.Y - Mousedwn.Y);

                    //cs.DrawSh(Mousedwn, e.Location, gr, p);
                    if (Figurecontrol.cs != null)
                    {
                        Figurecontrol.OnMouseMove(e.Location);
                        panel1.Invalidate();
                    }
                }
                return;
            }

            
        }

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {
            Figurecontrol.DrawSh(e.Graphics);
        }

        private void panel1_Paint_2(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {

        }

        private void panel1_MouseEnter_1(object sender, EventArgs e)
        {
            ;
        }

        private void panel1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("Figures"))
            {
                Figurecontrol.AddNew((Figures)e.Data.GetData("Figures"));
            }
        }

        private void panel1_DragOver(object sender, DragEventArgs e)
        {

            if (e.Data.GetDataPresent("Figures"))
            {
                Figurecontrol.OnMouseMove(panel1.PointToClient(new Point(e.X, e.Y)));
                panel1.Invalidate();
            }

        }

        private void Form1_Shown(object sender, EventArgs e)
        {
           
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Figures s = new CInstance();
            
            Graphics gr = pictureBox1.CreateGraphics();
            gr.Clear(Color.White);
            
            s.SetSPEP(new Point(0, 0), new Point(pictureBox1.Width-5, pictureBox1.Height-1));
            s.DrawSh(gr);

        }

        private void panel1_DragLeave(object sender, EventArgs e)
        {
            
            
            
        }

        private void panel1_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {
            
        }

        private void panel1_DragDrop(object sender, DragEventArgs e)
        {
           
        }

        private void panel1_MouseDown_1(object sender, MouseEventArgs e)
        {
            //if (
            Figurecontrol.SelectFigure(e.Location);
              //  )
            {
                panel1.Invalidate();
            }

        }
    }

        
    }

