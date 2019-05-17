using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paint
{
    public partial class Form1 : Form
    {
        Color Color = Color.Black;
        bool press = false;
        List<Figure> Fig = new List<Figure>();
        Pen pen;
        Pen p = new Pen(Color.Black, 3);
        Curve line;
        MyRectangle rec;
        MyEllipse ell;
        MyLine straigh;

        delegate void D(Point e, Point e2);
        D d;
        delegate void DFigure();
        DFigure dfigure;

        enum Figur { Rec, circle };
        Figur f;
        Point CurrentPoint;
        Point PrevPoint;

        public Form1()
        {
            InitializeComponent();
            p.SetLineCap(System.Drawing.Drawing2D.LineCap.Round, System.Drawing.Drawing2D.LineCap.Round, System.Drawing.Drawing2D.DashCap.Round);
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            press = true;
            if (press)
            {
                CurrentPoint = e.Location;
                if (d == Line)
                {
                    line = new Curve(Color, float.Parse(toolStripLabel2.Text));
                    CurrentPoint = e.Location;
                    line.AddPoint(CurrentPoint);
                }
            }
        }


        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (press)
            {
                d.Invoke(CurrentPoint, e.Location);
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (press && (line != null || rec != null || ell != null || straigh != null))
            {
                    dfigure();                
            }

            press = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult D = colorDialog1.ShowDialog();
            if (D == DialogResult.OK)
            {
                Color = colorDialog1.Color;
                
                button1.BackColor = colorDialog1.Color;
                button1.ForeColor = Color.FromArgb(colorDialog1.Color.ToArgb() ^ Color.White.ToArgb());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Fig.Clear();
            Refresh();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics gr = CreateGraphics();
            foreach (Figure f in Fig)// рисовка
            {
                f.Draw(gr);
            }
            gr.Dispose();
        }

        public void Figure(Point e, Point e2)
        {
            Refresh();
            //Repaint();
            float h, w;
            h = Math.Abs(e2.X - e.X);
            w = Math.Abs(e2.Y - e.Y);
            float x = e.X, y = e.Y;

            Graphics gr = CreateGraphics();
            Pen p = new Pen(Color, float.Parse(toolStripLabel2.Text));
            if (e.X > e2.X && e.Y > e2.Y)
            {
                x = e2.X;
                y = e2.Y;
            }
            if (e.X > e2.X && e2.Y > e.Y)
            {
                x = e2.X;
                y = e.Y;
            }
            if (e2.X > e.X && e.Y > e2.Y)
            {
                x = e.X;
                y = e2.Y;
            }
            switch (f)
            {
                case Figur.Rec:
                    {
                        gr.DrawRectangle(p, x, y, h, w);
                        rec = new MyRectangle(Color, float.Parse(toolStripLabel2.Text), Convert.ToInt32(x), Convert.ToInt32(y), Convert.ToInt32(h), Convert.ToInt32(w));
                        gr.Dispose();
                    }
                    break;
                case Figur.circle:
                    {
                        gr.DrawEllipse(p, x, y, h, w);
                        ell = new MyEllipse(Color, float.Parse(toolStripLabel2.Text), Convert.ToInt32(x), Convert.ToInt32(y), Convert.ToInt32(h), Convert.ToInt32(w));
                        gr.Dispose();
                    }
                    break;
            }
        }
        public void Straight(Point e, Point e2)
        {
            Refresh();
            //Repaint();
            Graphics gr = CreateGraphics();
            Pen p = new Pen(Color, float.Parse(toolStripLabel2.Text));
            gr.DrawLine(p, e, e2);
            straigh = new MyLine(Color, float.Parse(toolStripLabel2.Text), e, e2);
            gr.Dispose();
        }
        public void Line(Point e, Point e2)
        {
            PrevPoint = CurrentPoint;
            CurrentPoint = e2;
            line.AddPoint(CurrentPoint);

            Graphics gr = CreateGraphics();
            pen = new Pen(Color, float.Parse(toolStripLabel2.Text));
            gr.DrawLine(pen, e2, PrevPoint);

            gr.Dispose();
        }
        public void AddLine()
        {
            Fig.Add(line);
            line = null;
        }
        public void AddRec()
        {
            Fig.Add(rec);
            rec = null;
        }
        public void AddEll()
        {
            Fig.Add(ell);
            ell = null;
        }
        public void AddStr()
        {
            Fig.Add(straigh);
            straigh = null;
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            f = Figur.Rec;
            d = new D(Figure);
            dfigure = new DFigure(AddRec);
            pictureBox1.Image = Properties.Resources.Image11;
            pictureBox2.Image = Properties.Resources.line;
            pictureBox3.Image = Properties.Resources.circle;
            pictureBox4.Image = Properties.Resources.straingh;
        }       
        
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            d = new D(Line);
            dfigure = new DFigure(AddLine);
            pictureBox1.Image = Properties.Resources.Image1;
            pictureBox2.Image = Properties.Resources.line1;
            pictureBox3.Image = Properties.Resources.circle;
            pictureBox4.Image = Properties.Resources.straingh;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            f = Figur.circle;
            d = new D(Figure);
            dfigure = new DFigure(AddEll);
            pictureBox1.Image = Properties.Resources.Image1;
            pictureBox2.Image = Properties.Resources.line;
            pictureBox3.Image = Properties.Resources.circle1;
            pictureBox4.Image = Properties.Resources.straingh;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            d = new D(Straight);
            dfigure = new DFigure(AddStr);
            pictureBox1.Image = Properties.Resources.Image1;
            pictureBox2.Image = Properties.Resources.line;
            pictureBox3.Image = Properties.Resources.circle;
            pictureBox4.Image = Properties.Resources.straingh2;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            d = new D(Line);
            dfigure = new DFigure(AddLine);
            pen = new Pen(Color.Black, float.Parse(toolStripLabel2.Text));
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            toolStripLabel2.Text = String.Format("{0}",trackBar1.Value);
        }

        private void Save_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "dat файл (*.dat)|*.dat|Все файлы (*.*)|*.*";
            saveFileDialog.DefaultExt = "*.dat";
            saveFileDialog.InitialDirectory = @"G:\ООП\Paint\Paint";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
                BinaryFormatter binFormat = new BinaryFormatter();
                    using (FileStream fs = new FileStream(FileName, FileMode.OpenOrCreate))
                    {
                        binFormat.Serialize(fs, Fig);
                        fs.Close();
                    }
                MessageBox.Show("Файл сохранен");
            }

        }

        private void open_Click(object sender, EventArgs e)
        {
            //SaveFileDialog sv = new SaveFileDialog();
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "dat файл (*.dat)|*.dat|Все файлы (*.*)|*.*";
            op.DefaultExt = "*.dat";
            op.InitialDirectory = @"G:\ООП\Paint\Paint";
            if(openFileDialog1.ShowDialog()== DialogResult.OK)
            {
                Fig.Clear();                
                
                string FileName = openFileDialog1.FileName;
                BinaryFormatter binFormat = new BinaryFormatter();
                using (FileStream fs = new FileStream(FileName, FileMode.OpenOrCreate))
                {
                    List<Figure> Fig = (List<Figure>)binFormat.Deserialize(fs);
                    foreach(Figure f in Fig)
                    {
                        Fig.Add(f);
                    }
                    fs.Close();
                }
            }
            //Repaint();
            Refresh();

        }
        private void Repaint()          
        {
            foreach (var f in Fig)
            {
                Graphics gr = CreateGraphics();
                f.Draw(gr);
            }
        }
        string FileName = "", destinationPath = "", expansion = "";
        bool pause = false;
        bool stop = false;


        private void OpenFile_Click(object sender, EventArgs e)
        {
            if (!backgroundWorker1.IsBusy)
            {
                FileName = "";
                if (openFileDialog1.ShowDialog() != DialogResult.OK)
                    return;

                FileName = openFileDialog1.FileName;
                expansion = Path.GetExtension(FileName);
                label1.Text = FileName;
                Way.Enabled = true;
            }
        }

        

        private void Way_Click(object sender, EventArgs e)
        {
            if (FileName != "")
            {
                if (!backgroundWorker1.IsBusy)
                {
                    saveFileDialog1.FileName = FileName + expansion;
                    FileInfo fi = new FileInfo(saveFileDialog1.FileName);
                    saveFileDialog1.FileName = fi.Name;

                    if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                    {
                        return;
                    }
                    destinationPath = saveFileDialog1.FileName;
                    label2.Text = destinationPath;
                    Start.Enabled = true;
                }
            }

        }

        private void Start_Click(object sender, EventArgs e)
        {          
            if (FileName != "" && destinationPath != "")
            {
                if (pause && Start.Text == "Продолжить")
                {
                    pause = false;
                    Start.Text = "Пауза";
                    return;
                }
                if (!backgroundWorker1.IsBusy && Start.Text == "Старт")
                {
                    pause = false;
                    stop = false;
                    Start.Text = "Пауза";
                    this.ControlBox = false;
                    Stop.Enabled = true;
                    backgroundWorker1.RunWorkerAsync(Tuple.Create(FileName, destinationPath));
                }
                else
                {
                    pause = true;
                    Start.Text = "Продолжить";
                }
            }
        }

        

        private void Stop_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy)          
                stop = true;          
        }

        

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (FileName != "" && destinationPath != "")
                {
                    var data = (Tuple<string, string>)e.Argument;

                    using (var input = new FileStream(data.Item1, FileMode.Open, FileAccess.Read))
                    using (var output = new FileStream(data.Item2, FileMode.Create, FileAccess.Write))
                    {
                        byte[] buffer = new byte[1024];
                        int read;
                        while (!stop && (read = input.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            if (backgroundWorker1.CancellationPending)
                            {
                                Invoke((MethodInvoker)delegate
                                    {
                                        progressBar1.Value = 0;
                                    });
                                input.Close();
                                output.Close();
                                File.Delete(destinationPath);
                                e.Cancel = true;
                                return;
                            }
                            output.Write(buffer, 0, read);

                            float pct = (1.0f * input.Position) / input.Length * 100.0f;
                            backgroundWorker1.ReportProgress((int)pct);
                            while (pause)
                            {
                                Thread.Sleep(100);
                                if (stop)
                                {
                                    input.Close();
                                    output.Close();
                                    File.Delete(destinationPath);
                                    backgroundWorker1.ReportProgress(0);
                                    e.Cancel = true;
                                    return;
                                }
                            }
                        }
                        if (stop)
                        {
                            input.Close();
                            output.Close();
                            File.Delete(destinationPath);
                            backgroundWorker1.ReportProgress(0);
                            e.Cancel = true;
                        }
                    }
                }
            }
            catch(Exception h)
            {
                MessageBox.Show("Ошибка копирования: " + h.Message);
            }
        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (FileName != "" && destinationPath != "")
            {
                backgroundWorker1.CancelAsync();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            d = new D(Arrow);
            dfigure = new DFigure(AddArrow);
        }

        private void Instbtn_Click(object sender, EventArgs e)
        {

        }

        private void Peobtn_Click(object sender, EventArgs e)
        {

        }

        private void Diabtn_Click(object sender, EventArgs e)
        {

        }

        private void Retbtn_Click(object sender, EventArgs e)
        {

        }

        private void Inbtn_Click(object sender, EventArgs e)
        {

        }

        private void Aggbtn_Click(object sender, EventArgs e)
        {

        }

        private void Combtn_Click(object sender, EventArgs e)
        {

        }

        private void Assyncbtn_Click(object sender, EventArgs e)
        {

        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.ControlBox = true;
            if (e.Cancelled)
            {
                MessageBox.Show("Копирование отменено!");
                Start.Text = "Старт";
                return;
            }

            if (e.Error != null)
            {
                MessageBox.Show("Ошибка копирования: " + e.Error.Message);
                return;
            }

            MessageBox.Show("Копирование завершено!");
            Start.Text = "Старт";
        }
    }
}
