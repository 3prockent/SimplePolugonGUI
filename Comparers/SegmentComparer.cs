using SimplePolugonGUI.Structures;
using SimplePolugonGUI.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Point = SimplePolugonGUI.Structures.Point;


namespace SimplePolugonGUI.Comparers
{
    //Comparer to sort sweep line status list
    class SegmentComparer : IComparer<Segment>
    {
        public int Compare(Segment? s1, Segment? s2)
        {
            if (s1 is null || s2 is null)
                throw new NullReferenceException();
            if (s1.Intersects(s2))
                throw (new IntersectException());
            var A = new Point(s1.Right.X - s1.Left.X, s1.Right.Y - s2.Left.Y);
            var B = new Point(s2.Right.X - s2.Left.X, s2.Right.Y - s2.Left.Y);
            if (A.X * B.Y - B.X * A.Y > 0)
                return 1;
            return -1;
        }
    }
}
