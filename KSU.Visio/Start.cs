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

namespace Form_draw
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
            //Начинаем добавлять элементы
            AddFigureInObjectPanel(new Actor(location, size));
            //AddFigureInObjectPanel(new Line(location, size));
        }

        protected void AddFigureInObjectPanel(Figure figure)
        {
            PictureBox pb = new PictureBox();
            pb.Size = figure.Size;
            pb.Image = figure.GetImage();
            pb.Margin = new Padding(0);
            pb.BorderStyle = BorderStyle.FixedSingle;
            pb.Tag = figure;
            pb.Click += Pb_Click;
            objectsPanel.Controls.Add(pb);
        }

        private void Pb_Click(object sender, EventArgs e)
        {
            canvasPB.Cursor = Cursors.Cross;
            PictureBox pb = (PictureBox)sender;
            Figure figure = (Figure)pb.Tag;
            canvasPB.Tag = figure.Clone();
        }
        Point? mouseDownLocation = null;
        private void canvasPB_MouseDown(object sender, MouseEventArgs e)
        {
            if(mouseDownLocation == null && canvasPB.Cursor == Cursors.Cross)
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
            if (mouseDownLocation != null)
            {
                Figure figure = (Figure)canvasPB.Tag;
                Point location;
                Size size;
                Figure.PointsToLocationAndSize((Point)mouseDownLocation, e.Location, out location, out size);
                figure.Location = location;
                figure.Size = size;
            }
        }

        private void canvasPB_MouseUp(object sender, MouseEventArgs e)
        {
            if (mouseDownLocation != null)
            {
                canvasPB.Cursor = Cursors.Default;
                mouseDownLocation = null;
            }
        }
    }
}
