using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using KSU.Visio.Lib;

namespace KSU.Visio
{
    public partial class Start : Form
    {
        Canvas canvas;

        public Start()
        {
            InitializeComponent();
            canvas = new Canvas(canvasPB.Size);
            canvas.Changed += Canvas_Changed;
        }

        private void Canvas_Changed(object sender, EventArgs e)
        {
            canvasPB.Image = canvas.Image;
        }

        private void Start_Load(object sender, EventArgs e)
        {
            Point location = new Point(0, 0);
            Size size = new Size(objectsPanel.Size.Width / 2, objectsPanel.Size.Height / 7);
            //Начинаем добавлять элементы на панель элементов
            AddFigureInObjectPanel(new Actor(location, size));
            AddFigureInObjectPanel(new Frame(location, size));
            AddFigureInObjectPanel(new Line(location, location + size));
            AddFigureInObjectPanel(new Line(location, location + size, Line.CustomCap.Line, Line.CustomCap.LostMessage));
        }

        protected void AddFigureInObjectPanel(Figure figure)
        {
            figure.Changed += Figure_Changed;
            PictureBox pb = new PictureBox();
            pb.Size = figure.Size;
            pb.Image = figure.GetImage();
            pb.Margin = new Padding(0);
            pb.Tag = figure;
            pb.Click += Pb_Click;
            objectsPanel.Controls.Add(pb);
        }

        private void Figure_Changed(object sender, EventArgs e)
        {
            if (selectedPB != null)
            {
                Figure figure = (Figure)sender;
                selectedPB.Image = figure.GetImage();
            }
        }
        /// <summary>
        /// выделенный объект который будет рисоваться на холсте
        /// </summary>
        PictureBox selectedPB = null;
        private void Pb_Click(object sender, EventArgs e)
        {
            canvasPB.Cursor = Cursors.Cross;
            PictureBox pb = (PictureBox)sender;
            selectedPB = pb;
            Figure figure = (Figure)pb.Tag;
            figure.Selected = true;
            ///записываем рисуемый объект
            canvasPB.Tag = figure.Clone();
        }
        Point? mouseDownLocation = null;
        private void canvasPB_MouseDown(object sender, MouseEventArgs e)
        {
            if(selectedPB  != null && canvasPB.Tag != null)
            {
                mouseDownLocation = e.Location;
                Figure figure = (Figure)canvasPB.Tag;
                figure.Location = e.Location;
                figure.Size = new Size(0, 0);
                canvas.AddFigure(figure);
            }
        }

        private void canvasPB_MouseMove(object sender, MouseEventArgs e)
        {
            if (canvasPB.Tag != null && mouseDownLocation != null)
            {
                Figure figure = (Figure)canvasPB.Tag;
                if (figure.GetType() == typeof(Line))
                {
                    Line line = (Line)figure;
                    line.Start = (Point)mouseDownLocation;
                    line.End = e.Location;
                }
                else
                {
                    figure.Location = Figure.PointsToLocation((Point)mouseDownLocation, e.Location);
                    figure.Size = Figure.PointsToSize((Point)mouseDownLocation, e.Location);
                }
            }
        }

        private void canvasPB_MouseUp(object sender, MouseEventArgs e)
        {
            if (mouseDownLocation != null && selectedPB != null && canvasPB.Tag != null)
            {
                
                canvasPB.Cursor = Cursors.Default;
                mouseDownLocation = null;
                Figure figure = (Figure)selectedPB.Tag;
                figure.Selected = false;
                selectedPB = null;
                figure = (Figure)canvasPB.Tag;
                figure.Selected = false;
                canvasPB.Tag = null;
            }
        }
    }
}
