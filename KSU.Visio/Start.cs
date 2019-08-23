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
        public Start()
        {
            InitializeComponent();
            Tool = CreateRectangle;
            Txt.Parent = panel1;
            //Txt.Multiline=true;
            Txt.Hide();
            DoubleBuffered = true;
            typeof(Panel).InvokeMember("DoubleBuffered", BindingFlags.SetProperty
            | BindingFlags.Instance | BindingFlags.NonPublic, null,
            panel1, new object[] { true });
        }

        public delegate void tool();//делегат создаваемого объекта
        tool Tool;

        public delegate void action();//делегат на действие удаление или перемещение
        action Action;

        List<Figure> List_figures = new List<Figure>();//тут хранятся фигурки
        Figure Select;//выбранная фигура
        bool Mouse_press = false;//Зажата ли мышь
        Color Selected_color = Color.Blue;//цвет выбранной фигуры
        bool For_move = false;//нужнро ли будет двигать фигуру
        bool Move = false;//двигаем ли фигуру
        TextBox Txt = new TextBox();//текстбокс для текста

        bool Text_enter = false;
        bool Del = false;//удаление ли


        //Эти методы при начале отрисовки добавляют в лист объектов объект
        #region
        private void CreateLine()
        {
            List_figures.Add(new Line());
        }
        private void CreateRectangle()
        {
            List_figures.Add(new Rectangle_object());
        }
        private void CreateLifeLine()
        {
            List_figures.Add(new Life_line());
        }
        private void CreateInstance_specification()
        {
            List_figures.Add(new Instance_specification());
        }
        private void CreateFrame()
        {
            List_figures.Add(new Frame());
        }
        private void CreateFound_Message()
        {
            List_figures.Add(new Found_message());
        }
        private void CreateDispatch_mess()
        {
            List_figures.Add(new Dispatch_mess());
        }
        private void CreateReturn_mess()
        {
            List_figures.Add(new Return_mess());
        }
        private void CreateStrelka()
        {
            List_figures.Add(new Asyn_mess());
        }
        private void CreateActor()
        {
            List_figures.Add(new Actor());
        }
        private void CreateContinuation()
        {
            List_figures.Add(new Continuation());
        }
        private void CreateWhite_rectangle()
        {
            List_figures.Add(new White_rectangle());
        }
        private void CreatLost_Message()
        {
            List_figures.Add(new Lost_message());
        }
        private void CreateText()
        {
            List_figures.Add(new Text());
        }
        #endregion
        /// <summary>
        /// Рисование. Вызывается в методах движение/отпускания мыши
        /// </summary>
        private void DrawFigure()
        {
            panel1.Refresh();
            Graphics gr = panel1.CreateGraphics();
            List_figures.Last().Draw(gr);
            gr.Dispose();
        }

        /// <summary>
        /// отрисовка
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            foreach (Figure Figure in List_figures)//по листу всех худ объектов
            {
                for (int i = 0; i < Figure.Basic_points.Length - 1; i++)
                {
                    Figure.Draw(e.Graphics);
                }
            }
        }

        /// <summary>
        /// нажатие мыши
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            //проверка на текст
            Txt.Hide();
            if (Select != null && (Select is Text))
            {
                (Select as Text).Textstring = Txt.Text;
                Txt.Text = "";
                Text_enter = false;
            }

            //если режим удаления
            if (Del)
            {
                if (Hit_testing(e.Location))
                {
                    List_figures.Remove(Select);
                    panel1.Refresh();
                }
            }
            else
            {   //если режим движения
                if (For_move)
                {
                    if (Hit_testing(e.Location))
                        Mouse_press = true;
                    Select.Line_color = Selected_color;
                }
                else //если рисуем новую фигуру
                {
                    Tool();
                    List_figures.Last().Line_color = Color.Black;
                    Point SecondPoint = e.Location;
                    List_figures.Last().Basic_points[0] = SecondPoint;
                    Mouse_press = true;
                }
            }
        }


        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!Mouse_press)//если не нажато лкм, то и рисовать нечего, возврат
                return;
            if (!Move)
            {
                Graphics gr = panel1.CreateGraphics();
                Point FistPoint = List_figures.Last().Basic_points.Last();//берем первую точку из нового худ.объекта g(передадаать в первую и второую точки)
                Point SecondPoint = e.Location;//и текущее положение
                List_figures.Last().Basic_points[1] = SecondPoint;//закидываем его в лист точек худ.объекта
                DrawFigure();//Через делегат рисуем нужным инструментом
                gr.Dispose();
            }
            else
            {
                Graphics gr = panel1.CreateGraphics();
                Select.Shift(e.Location);
                panel1.Refresh();
                gr.Dispose();
                For_move = true;
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {

            if (Mouse_press & !Move)
            {
                Select = List_figures.Last();
                Point FistPoint = List_figures.Last().Basic_points.Last();
                Point SecondPoint = e.Location;
                List_figures.Last().Basic_points[1] = SecondPoint;
                Mouse_press = false;
                DrawFigure();//заканчиваем рисовать что было
                if (Select is Text)
                {
                    Txt.Location = Select.Basic_points[0];
                    Txt.Width = Select.Basic_points[1].X - Select.Basic_points[0].X;
                    Txt.Height = Select.Basic_points[1].Y - Select.Basic_points[0].Y;
                    Txt.Show();
                }
            }
            if (Mouse_press & Move)
            {
                Mouse_press = false;
                Select.Line_color = Color.Black;
                if (Select is Text)
                {
                    Txt.Location = Select.Basic_points[0];
                    Txt.Width = Select.Basic_points[1].X - Select.Basic_points[0].X;
                    Txt.Height = Select.Basic_points[1].Y - Select.Basic_points[0].Y;
                    Txt.Text = (Select as Text).Textstring;
                    Txt.Show();
                }
                panel1.Refresh();
            }
        }
        /// <summary>
        /// Режим перемещения фигур
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            For_move = true;
            Action = SelectFigure;
        }
        /// <summary>
        /// Проверка на попадание в фигуру
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        private bool Hit_testing(Point Point)
        {
            Graphics gr = panel1.CreateGraphics();
            for (int i = List_figures.Count - 1; i >= 0; i--)
            {
                if (List_figures[i] is Rectangle_object)
                {
                    if ((List_figures[i] as Rectangle_object).Hit_testing(List_figures[i], Point))
                    {
                        Select = List_figures[i];
                        Action();
                        return true;
                    }
                }
                if (List_figures[i] is Line)
                {
                    if ((List_figures[i] as Line).Hit_testing(List_figures[i], Point))
                    {
                        Select = List_figures[i];
                        Action();
                        return true;
                    }
                }
                if (List_figures[i] is Life_line)
                {
                    if ((List_figures[i] as Life_line).Hit_testing(List_figures[i], Point) || (List_figures[i] as Life_line).Hit_testing_line(List_figures[i], Point))
                    {
                        Select = List_figures[i];
                        Action();
                        return true;
                    }
                }
                if (List_figures[i] is Instance_specification)
                {
                    if ((List_figures[i] as Instance_specification).Hit_testing(List_figures[i], Point))
                    {
                        Select = List_figures[i];
                        Action();
                        return true;
                    }
                }
                if (List_figures[i] is Frame)
                {
                    if ((List_figures[i] as Frame).Hit_testing(List_figures[i], Point))
                    {
                        Select = List_figures[i];
                        Action();
                        return true;
                    }
                }
                if (List_figures[i] is Found_message)
                {
                    if ((List_figures[i] as Found_message).Hit_testing(List_figures[i], Point))
                    {
                        Select = List_figures[i];
                        Action();
                        return true;
                    }
                }
                if (List_figures[i] is Dispatch_mess)
                {
                    if ((List_figures[i] as Dispatch_mess).Hit_testing(List_figures[i], Point))
                    {
                        Select = List_figures[i];
                        Action();
                        return true;
                    }
                }
                if (List_figures[i] is Return_mess)
                {
                    if ((List_figures[i] as Return_mess).Hit_testing(List_figures[i], Point))
                    {
                        Select = List_figures[i];
                        Action();
                        return true;
                    }
                }
                if (List_figures[i] is Asyn_mess)
                {
                    if ((List_figures[i] as Asyn_mess).Hit_testing(List_figures[i], Point))
                    {
                        Select = List_figures[i];
                        Action();
                        return true;
                    }
                }
                if (List_figures[i] is Actor)
                {
                    if ((List_figures[i] as Actor).Hit_testing(List_figures[i], Point))
                    {
                        Select = List_figures[i];
                        Action();
                        return true;
                    }
                }
                if (List_figures[i] is Continuation)
                {
                    if ((List_figures[i] as Continuation).Hit_testing(List_figures[i], Point))
                    {
                        Select = List_figures[i];
                        Action();
                        return true;
                    }
                }
                if (List_figures[i] is White_rectangle)
                {
                    if ((List_figures[i] as White_rectangle).Hit_testing(List_figures[i], Point))
                    {
                        Select = List_figures[i];
                        Action();
                        return true;
                    }
                }
                if (List_figures[i] is Lost_message)
                {
                    if ((List_figures[i] as Lost_message).Hit_testing(List_figures[i], Point))
                    {
                        Select = List_figures[i];
                        Action();
                        return true;
                    }
                }
                if (List_figures[i] is Text)
                {
                    if ((List_figures[i] as Text).Hit_testing(List_figures[i], Point))
                    {
                        Select = List_figures[i];
                        Action();
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// если перемещаем фигуру
        /// </summary>
        private void SelectFigure()
        {
            Move = true;
            For_move = false;
            button1.Text = "ПЕРЕМЕЩЕНИЕ";
        }
        /// <summary>
        /// для пикчебоксов
        /// </summary>
        private void clickonpicturebox()
        {
            For_move = false;
            Move = false;
            Del = false;
            button1.Text = "Переместить";
        }
        /// <summary>
        /// для удаления
        /// </summary>
        private void DeleteFigure()
        {
            For_move = false;
            Move = false;
            button1.Text = "Переместить";
        }

        //Тут в делегат закидываются методы добавления фигур
        #region
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Tool = CreateRectangle;
            clickonpicturebox();

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Tool = CreateLine;
            clickonpicturebox();

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Tool = CreateLifeLine;
            clickonpicturebox();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Tool = CreateInstance_specification;
            clickonpicturebox();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Tool = CreateFrame;
            clickonpicturebox();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Tool = CreateFound_Message;
            clickonpicturebox();
        }
        private void pictureBox7_Click(object sender, EventArgs e)
        {
            Tool = CreateDispatch_mess;
            clickonpicturebox();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            Tool = CreateStrelka;
            clickonpicturebox();
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            Tool = CreateReturn_mess;
            clickonpicturebox();
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            Tool = CreateActor;
            clickonpicturebox();
        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {
            Tool = CreateContinuation;
            clickonpicturebox();
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            Tool = CreateWhite_rectangle;
            clickonpicturebox();
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            Tool = CreatLost_Message;
            clickonpicturebox();
        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {
            Tool = CreateText;
            clickonpicturebox();
        }
        #endregion

        /// <summary>
        /// очистить всё
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            List_figures.Clear();
            panel1.Refresh();
        }
        /// <summary>
        /// удаление конкретной фигуры
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            Del = true;
            Action = DeleteFigure;
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
