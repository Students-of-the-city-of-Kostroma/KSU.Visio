using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace KSU.Visio.Lib
{
    public class Line : Figure
    {
        /// <summary>
        /// Окончание линии
        /// </summary>
        public enum CustomCap { Line, LostMessage, AsynMessage, DispatchMessage, FoundMessage, ReturnMessage }

        protected Point start, end;


        /// <summary>
        /// Начало линии
        /// </summary>
        public Point Start
        {
            get { return start; }
            set
            {
                if (start != value)
                {
                    start = value;
                    Location = PointsToLocation(start, end);
                    Size = PointsToSize(start, end);
                }
            }
        }

        /// <summary>
        /// Конец линии
        /// </summary>
        public Point End
        {
            get { return end; }
            set
            {
                if (end != value)
                {
                    end = value;
                    Location = PointsToLocation(start, end);
                    Size = PointsToSize(start, end);
                }
            }
        }

        protected CustomCap startLineCap, endLineCap;

        public CustomCap StartLineCap
        {
            get { return startLineCap; }
            set
            {
                if (startLineCap != value)
                {
                    startLineCap = value;
                    ChangedMetod();
                }
            }
        }
        public CustomCap EndLineCap
        {
            get { return endLineCap; }
            set
            {
                if (endLineCap != value)
                {
                    endLineCap = value;
                    ChangedMetod();
                }
            }
        }
        #region Line

        GraphicsPath fLine = new GraphicsPath();
        GraphicsPath sLine = new GraphicsPath();

        #endregion
        #region LostMessage
        GraphicsPath fLostMessage = new GraphicsPath();
        GraphicsPath sLostMessage = new GraphicsPath();
        /// <summary>
        /// Задаем окончание лини для LostMessage
        /// </summary>
        private void InitLostMessageCap()
        {
            fLostMessage.AddEllipse(-2, -2, 4, 4);
            fLostMessage.AddLine(0, -2, -2, -5);
            fLostMessage.AddLine(-2, -5, 2, -5);
            fLostMessage.AddLine(2, -5, 0, -2);
        }
        #endregion


        public Line(Point start, Point end,
            CustomCap startLineCap = CustomCap.Line,
            CustomCap endLineCap = CustomCap.Line) :
            base(PointsToLocation(start, end), PointsToSize(start, end))

        {
            Start = start;
            End = end;

            StartLineCap = startLineCap;
            EndLineCap = endLineCap;

            InitLostMessageCap();
        }

        public override Figure Clone()
        {
            Figure figure = new Line(Location, Location, startLineCap, endLineCap);
            figure.Selected = Selected;
            return figure;
        }



        public override void Draw(Graphics gr)
        {
            base.Draw(gr);

            GraphicsPath fillPathStart = null, strokePathStart = null, fillPathEnd = null, strokePathEnd = null;
            SwithPathStartAndEnd(ref fillPathStart, ref strokePathStart, ref fillPathEnd, ref strokePathEnd);

            CustomLineCap customLineCapStart = new CustomLineCap(fillPathStart, strokePathStart);
            CustomLineCap customLineCapEnd = new CustomLineCap(fillPathEnd, strokePathEnd);

            pen.CustomStartCap = customLineCapStart;
            pen.CustomEndCap = customLineCapEnd;

            gr.DrawLine(pen, start, end);

            customLineCapStart.Dispose();
            customLineCapEnd.Dispose();
        }

        ~Line()
        {
            fLine.Dispose();
            sLine.Dispose();
            fLostMessage.Dispose();
            sLostMessage.Dispose();
        }

        private void SwithPathStartAndEnd(
            ref GraphicsPath fillPathStart, ref GraphicsPath strokePathStart, 
            ref GraphicsPath fillPathEnd, ref GraphicsPath strokePathEnd)
        {
            switch (StartLineCap)
            {
                case CustomCap.Line:
                    fillPathStart = fLine;
                    strokePathStart = sLine;
                    break;
                case CustomCap.LostMessage:
                    fillPathStart = fLostMessage;
                    strokePathStart = sLostMessage;
                    break;
                case CustomCap.AsynMessage:
                    break;
                case CustomCap.DispatchMessage:
                    break;
                case CustomCap.FoundMessage:
                    break;
                case CustomCap.ReturnMessage:
                    break;
            }

            switch (EndLineCap)
            {
                case CustomCap.Line:
                    fillPathEnd = fLine;
                    strokePathEnd = sLine;
                    break;
                case CustomCap.LostMessage:
                    fillPathEnd = fLostMessage;
                    strokePathEnd = sLostMessage;
                    break;
                case CustomCap.AsynMessage:
                    break;
                case CustomCap.DispatchMessage:
                    break;
                case CustomCap.FoundMessage:
                    break;
                case CustomCap.ReturnMessage:
                    break;
            }
        }

    }
}
