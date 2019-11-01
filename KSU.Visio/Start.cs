using KSU.Visio.Lib;
using KSU.Visio.Lib.StateDiagram;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace KSU.Visio
{
    public partial class Start : Form
    {
        Emulator emulator;

        public Start()
        {
            InitializeComponent();
            try
            {
                emulator = Emulator.LoadFromXMLFile();
            }
            catch
            {
                emulator = new Emulator(canvasPB.Size);
                Point location = new Point(0, 0);
                Size size = new Size(objectsPanel.Size.Width / 2, objectsPanel.Size.Height / 7);
                //Начинаем добавлять элементы на панель элементов
                State state = new State(location, size) { Name = "Name" };
                Synchronizer synchronizer = new Synchronizer(location, size);
                Transfer transfer = new Transfer(state, synchronizer)
                {
                    Expression = "\r\n//Здесь должен быть какой-то код на C#\r\n"
                };
                emulator.AddFigure(state);
                emulator.AddFigure(synchronizer);
                emulator.Transfers.Add(transfer);
            }
            emulator.Changed += Canvas_Changed;
            UpdateImage();
        }

        void UpdateImage()
        {
            canvasPB.Image = emulator.Image;
        }

        private void Canvas_Changed(object sender, EventArgs e)
        {
            UpdateImage();
        }

        private void Start_Load(object sender, EventArgs e)
        {
 

            Close();






            //AddFigureInObjectPanel(new Actor(location, size));
            //AddFigureInObjectPanel(new Continuation(location, size));
            //AddFigureInObjectPanel(new Life_line(location, size));
            //AddFigureInObjectPanel(new White_rectangle(location, size));
            //AddFigureInObjectPanel(new Instance_specification(location, size));
            //AddFigureInObjectPanel(new Frame(location, size));
            //AddFigureInObjectPanel(new Line(location, location + size));
            //AddFigureInObjectPanel(new Line(location, location + size, new LineCapBase(), new LostMessageCap()));
            //AddFigureInObjectPanel(new Line(location, location + size, new LineCapBase(), new AsynchronousMessageCap()));
            //AddFigureInObjectPanel(new Line(location, location + size, new LostMessageCap(), new LineCapBase()));


        }

        protected void AddFigureInObjectPanel(Object ob)
        {
            if (ob is Figure figure)
            {
                figure.Changed += Figure_Changed;
                PictureBox pb = new PictureBox
                {
                    Size = figure.Size,
                    Image = figure.GetImage(),
                    Margin = new Padding(0),
                    Tag = figure
                };
                pb.Click += Pb_Click;
                objectsPanel.Controls.Add(pb);
            }
            if(ob is Transfer transfer)
            {
                PictureBox pb = new PictureBox
                {
                    Size = new Size(transfer.End.Location),
                    Image = transfer.GetImage(),
                    Margin = new Padding(0),
                    Tag = transfer
                };
                pb.Click += Pb_Click;
                objectsPanel.Controls.Add(pb);
            }
        }

        private void Figure_Changed(object sender, EventArgs e)
        {
            if (selectedPB != null)
            {
                Figure figure = (Figure)sender;
                selectedPB.Image = figure.GetImage();
            }
        }

#pragma warning disable IDE0069 // Следует высвобождать высвобождаемые поля
                               /// <summary>
                               /// выделенный объект который будет рисоваться на холсте
                               /// </summary>
        private PictureBox selectedPB = null;
#pragma warning restore IDE0069 // Следует высвобождать высвобождаемые поля
       
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
        private void CanvasPB_MouseDown(object sender, MouseEventArgs e)
        {
            if(selectedPB  != null && canvasPB.Tag != null)
            {
                mouseDownLocation = e.Location;
                Figure figure = (Figure)canvasPB.Tag;
                figure.Location = e.Location;
                figure.Size = new Size(0, 0);
                emulator.AddFigure(figure);
            }
        }

        private void CanvasPB_MouseMove(object sender, MouseEventArgs e)
        {
            if (canvasPB.Tag != null && mouseDownLocation != null)
            {
                Figure figure = (Figure)canvasPB.Tag;
                if (figure is Line line)
                {
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

        private void CanvasPB_MouseUp(object sender, MouseEventArgs e)
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

        private void Start_FormClosed(object sender, FormClosedEventArgs e)
        {
            emulator.SaveToXMLFile();
        }
    }
}
