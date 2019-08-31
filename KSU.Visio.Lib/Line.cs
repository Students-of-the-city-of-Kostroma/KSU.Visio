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
                if(start!= value)
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
                    penDefault.CustomStartCap = GetAdjustableArrowCap(startLineCap);
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
                    penDefault.CustomEndCap = GetAdjustableArrowCap(endLineCap);
                    ChangedMetod();
                }
            }
        }

        public static AdjustableArrowCap GetAdjustableArrowCap(CustomCap customCap)
        {
            switch (customCap)
            {
                case CustomCap.Line: return new AdjustableArrowCap(0f, 0f);
                case CustomCap.LostMessage: return new AdjustableArrowCap(3f, 3f);
                default: throw new Exception("Не реализовано для перечисления "+ customCap);
            }
            throw new Exception("Этого не может быть, потому что не может быть");
        }
        public Line(Point start,  Point end,
            CustomCap startLineCap = CustomCap.Line,
            CustomCap endLineCap = CustomCap.Line) : 
            base(PointsToLocation(start, end), PointsToSize(start, end))

        {
            Start = start;
            End = end;
            StartLineCap = startLineCap;
            EndLineCap = endLineCap;
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

            gr.DrawLine(penDefault, start, end);
        }


    }
}
