using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplePolugonGUI.Structures
{
    public class Point
    { 
        public int X { get; set; }
        public int Y { get; set; }
        public List<Segment> segments { get; set; } = new List<Segment>();
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
        public System.Drawing.Point ToClassicPoint() 
        {
            return new System.Drawing.Point(X, Y);
        }

        public Segment getSegmentLeft()
        {
            foreach (Segment segment in segments)
            {
                if (this.Equals(segment.Left))
                    return segment;
            }
            return null;
        }
        public Segment getSegmentRight()
        {
            foreach (Segment segment in segments)
            {
                if (this.Equals(segment.Right))
                    return segment;
            }
            return null;
        }
    }
}
