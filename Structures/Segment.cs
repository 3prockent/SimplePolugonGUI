using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplePolugonGUI.Structures
{
    public class Segment
    {
        public Point Left { get; set; }
        public Point Right { get; set; }

        public Segment(Point start, Point end)
        {
            if (start.X > end.X)
            {
                var temp = start;
                start = end;
                end = temp;

            }
            Left = start;
            Left.segments.Add(this);
            Right = end;
            Right.segments.Add(this);

        }

        public bool Intersects(Segment another)
        {
            double thisStartX = this.Left.X;
            double thisStartY = this.Left.Y;
            double thisEndX = this.Right.X;
            double thisEndY = this.Right.Y;
            double anotherStartX = another.Left.X;
            double anotherStartY = another.Left.Y;
            double anotherEndX = another.Right.X;
            double anotherEndY = another.Right.Y;

            if (thisStartX == anotherEndX && thisStartY == anotherEndY || thisEndX == anotherStartX && thisEndY == anotherStartY
                || thisStartX == anotherStartX && thisStartY == anotherStartY || thisEndX == anotherEndX && thisEndY == anotherEndY)
            {
                return false;
            }

            double q = (thisStartY - anotherStartY) * (anotherEndX - anotherStartX) - (thisStartX - anotherStartX) * (anotherEndY - anotherStartY);
            double d = (thisEndX - thisStartX) * (anotherEndY - anotherStartY) - (thisEndY - thisStartY) * (anotherEndX - anotherStartX);

            if (d == 0)
            {
                return false;
            }

            double r = q / d;

            q = (thisStartY - anotherStartY) * (thisEndX - thisStartX) - (thisStartX - anotherStartX) * (thisEndY - thisStartY);
            double s = q / d;

            if (r < 0 || r > 1 || s < 0 || s > 1)
            {
                return false;
            }

            return true;
        }
    }
}
