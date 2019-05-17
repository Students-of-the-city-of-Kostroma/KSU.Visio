using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Paint
{
    [Serializable]
    class FlatMes:Figure
    {
        Point e;
        Point e2;
        public MyArrow(Color color, float depth, Point e, Point e2) : base(color, depth)
        {
            this.e = e;
            this.e2 = e2;
        }
        public Point E
        {
            get
            {
                return e;
            }
            set
            {
                e = value;
            }
        }

        public Point E2
        {
            get
            {
                return e2;
            }
            set
            {
                e2 = value;
            }
        }

        public override void Draw(Graphics gr)
        {
            Pen p = new Pen(Color, Depth);
            gr.DrawLine(p, e, e2);
        }
    }
}
    }
}
