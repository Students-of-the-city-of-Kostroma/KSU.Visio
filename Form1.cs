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
<<<<<<< HEAD
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
        People People = new People();
        Figures cs;
        delegate Figures NewFigure();
        NewFigure ns;
=======
        
>>>>>>> again
        public Form1()
        {
            
        }

        private void button_color_Click(object sender, EventArgs e)
        {
           
        }

        private void clear_Click(object sender, EventArgs e)
        {
           
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
           
        }




        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
           
        }

        private void panel1_MouseEnter(object sender, EventArgs e)
        {
           
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void set(object sender, DoWorkEventArgs e)
        {
            
        }

        

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
            
        }

        private void quadro_Click(object sender, EventArgs e)
        {

        }

        private void ellipse_Click(object sender, EventArgs e)
        {

        }

        private void savebtn_Click(object sender, EventArgs e)
        {
            
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
<<<<<<< HEAD
            //e.Graphics.DrawImage(btm,0,0);
          //  gr.Clear(Color.White);
            //e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            //Figurecontrol.DrawSh(e.Graphics);
            Figures s = new sObject();

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
=======
           
>>>>>>> again
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            ;
        }

       

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
           
        }

            private void Form1_MouseMove(object sender, MouseEventArgs e)
            {
           
                
           
            
            }

        private void Form1_Load(object sender, EventArgs e)
        {
<<<<<<< HEAD
            //ns = new NewFigure(T);
            //btm = new Bitmap(this.ClientRectangle.Width, ClientRectangle.Height);
            //btm1 = new Bitmap(pictureBox1.Width-10, pictureBox1.Height-10);
            //gr = Graphics.FromImage(btm1);

            //Figures s = new sObject();
            //s.DrawSh(gr);
=======
>>>>>>> again
            
         
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
           
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            
          
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            
            
        }

        private Figures S2()
        {
<<<<<<< HEAD
            //trackBar2.Enabled = true;
            Figures t = new sObject();
            //t.N = trackBar2.Value;
            t.P = (Pen)p.Clone();
            return t;
            //trackBar2.Value = cs.N;
=======
            
>>>>>>> again

        }

        private Figures T()
        {
            

        }


        private Figures P()
        {
<<<<<<< HEAD
            //trackBar2.Enabled = true;
            Figures t = new People();            
            t.P = (Pen)p.Clone();
            return t;
            //trackBar2.Value = cs.N;
=======
            
>>>>>>> again

        }
      

        private Figures S()
        {
            

        }

        private void trackBar2_ValueChanged(object sender, EventArgs e)
        {
           
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

           
        }

        private void Form1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {

        }

        private void Form1_DragOver(object sender, DragEventArgs e)
        {

        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            
<<<<<<< HEAD
         //   Figurecontrol.AddNew(new sObject());
=======
       
>>>>>>> again
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void panel1_MouseMove_1(object sender, MouseEventArgs e)
        {
           
            
        }

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {
            
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
          
        }

        private void panel1_DragOver(object sender, DragEventArgs e)
        {

            
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
           
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
<<<<<<< HEAD
            Figures s = new sObject();
            
            Graphics gr = pictureBox1.CreateGraphics();
            gr.Clear(Color.White);
            
            s.SetSPEP(new Point(0, 0), new Point(pictureBox1.Width-5, pictureBox1.Height-1));
            s.DrawSh(gr);
=======
           
>>>>>>> again

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
<<<<<<< HEAD
            //if (
            textBox1.Enabled = false;
            Figurecontrol.SelectFigure(e.Location);
              //  )
            {
                panel1.Invalidate();
            }
            
            textBox1.Text = "";
=======
            
>>>>>>> again
        }

        private void panel1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
<<<<<<< HEAD
            if (Figurecontrol.GetSelected() != null)
            {
                textBox1.Enabled = true;
                textBox1.Text = Figurecontrol.GetSelected().GetText();
                textBox1.Focus();
            }
=======
            
>>>>>>> again
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
<<<<<<< HEAD
            if (textBox1.Enabled )
            if (Figurecontrol.GetSelected() != null)
            {
                Figurecontrol.GetSelected().SetText(textBox1.Text);
                panel1.Invalidate();
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
           
=======
           
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
           
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {

>>>>>>> again
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
<<<<<<< HEAD
            textBox1.Enabled = false;
            textBox1.Text = "";
=======
            
>>>>>>> again
        }
    }

        
    }

