using KSU.Visio.Lib;
using KSU.Visio.Lib.StateDiagram;
using System;
using System.Collections.Generic;
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
            emulator = Emulator.LoadFromXMLFile();
            emulator.Changed += Canvas_Changed;
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
            Point location = new Point(0, 0);
            Size size = new Size(objectsPanel.Size.Width / 2, objectsPanel.Size.Height / 11);
            //Начинаем добавлять элементы на панель элементов
            AddFigureInObjectPanel(new Actor(location, size));
            AddFigureInObjectPanel(new Continuation(location, size));
            AddFigureInObjectPanel(new Life_line(location, size));
            AddFigureInObjectPanel(new White_rectangle(location, size));
            AddFigureInObjectPanel(new Instance_specification(location, size));
            AddFigureInObjectPanel(new Frame(location, size));
            AddFigureInObjectPanel(new Line(location, location + size));
            AddFigureInObjectPanel(new Line(location, location + size, new LineCapBase(), new LostMessageCap()));
            AddFigureInObjectPanel(new Line(location, location + size, new LineCapBase(), new AsynchronousMessageCap()));
            AddFigureInObjectPanel(new Line(location, location + size, new LostMessageCap(), new LineCapBase()));
			AddFigureInObjectPanel(new Lib.Component(location, size));
			AddFigureInObjectPanel(new Note(location, size));
			AddFigureInObjectPanel(new Package(location, size));
			AddFigureInObjectPanel(new Limitation(location, size));
			AddFigureInObjectPanel(new Node(location,size));
			AddFigureInObjectPanel(new Line(location, location + size, new LineCapBase(), new Dependence()));
			AddFigureInObjectPanel(new Line(location, location + size, new Interface(), new LineCapBase()));
            emulator.Size = canvasPB.Size;
        }

        private void Start_FormClosed(object sender, FormClosedEventArgs e)
        {
            File.Delete(DateTime.Now.ToString("yyyyMMdd") + ".code.log");
            File.Delete(DateTime.Now.ToString("yyyyMMdd") + ".log");
        }

        private void canvasPB_Resize(object sender, EventArgs e)
        {
            emulator.Size = canvasPB.Size;
        }

        private void StartB_Click(object sender, EventArgs e)
        {
            emulator.Run();
            emulator.SaveToXMLFile(DateTime.Now.ToString("yyyyMMdd") + ".xml");
        }

        private void CanvasPB_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (canvasPB.Tag != null)
                {
                    object[] param = canvasPB.Tag as object[];
                    Condition condition = param[0] as Condition;
                    Point startLocationCondition = (Point)param[1];
                    Size startLocationCanvasPB = (Size)param[2];
                    condition.Location = startLocationCondition + new Size(e.Location) - startLocationCanvasPB;
                }
            }
        }

        private void CanvasPB_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Condition condition = emulator.GetFigureClickedOn(e.Location) as Condition;
                if (condition == null) emulator.ToOwner();
                else emulator.ToParent(condition);
            }
        }

        private void CanvasPB_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Figure condition = emulator.GetFigureClickedOn(e.Location);
                if (condition != null)
                    canvasPB.Tag = new object[] { condition, condition.Location, new Size(e.Location) };
            }
        }

        private void CanvasPB_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                canvasPB.Tag = null;
        }
    }
}
