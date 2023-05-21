using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Point = SimplePolugonGUI.Structures.Point;

namespace SimplePolugonGUI.Comparers
{
    //Comparer to sort event point list
    class PointComparer : IComparer<Point>
    {
        public int Compare(Point? p1, Point? p2)
        {
            if (p1 is null || p2 is null)
                throw new ArgumentException("Invalid value");
            if (p1.X == p2.X)
            {
                if (p1.Y > p2.Y)
                    return 1;
                if (p1.Y < p2.Y)
                    return -1;
                if (p1.Y == p2.Y)
                    return 0;
            }
            if (p1.X > p2.X)
                return 1;
            else return -1;
        }
    }
}
