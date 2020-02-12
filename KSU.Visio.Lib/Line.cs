using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using KSU.Visio.Lib.Cap;

namespace KSU.Visio.Lib
{
    public class Line : Figure
    {
        /// <summary>
        /// Точка начала линии
        /// </summary>
        protected Point start;

        /// <summary>
        /// Точка начала линии
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
        /// Точка конца линии
        /// </summary>
        protected Point end;

        /// <summary>
        /// Точка конца линии
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
        /// <summary>
        /// Описание начала линии
        /// </summary>
        protected LineCapBase startLineCap;

        /// <summary>
        /// Описание начала линии
        /// </summary>
        public LineCapBase StartLineCap
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
        /// <summary>
        /// Описание конца линии
        /// </summary>
        protected LineCapBase endLineCap;

        /// <summary>
        /// Описание конца линии
        /// </summary>
        public LineCapBase EndLineCap
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



        public Line(Point start, Point end,
            LineCapBase startLineCap = null,
            LineCapBase endLineCap = null) :
            base(PointsToLocation(start, end), PointsToSize(start, end))

        {
            Start = start;
            End = end;

            StartLineCap = (startLineCap == null) ? new LineCapBase() : startLineCap;
            EndLineCap = (endLineCap == null) ? new LineCapBase() : endLineCap;
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

            CustomLineCap s = StartLineCap.GetCustomLineCap();
            CustomLineCap e = EndLineCap.GetCustomLineCap();

            pen.CustomStartCap = s;
            pen.CustomEndCap = e;

            gr.DrawLine(pen, start, end);

            s.Dispose();
            e.Dispose();
        }
	}
}
