using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimplePolugonGUI.Comparers;

namespace SimplePolugonGUI.Structures
{
    public class SweepLineStatus : List<Segment>
    {
        public void Insert(Segment seg)
        {
            this.Add(seg);
            this.Sort(new SegmentComparer());
        }
        public void Delete(Segment seg)
        {
            this.Remove(seg);
            this.Sort(new SegmentComparer());
        }
        public Segment Above(Segment seg)
        {
            var index = this.FindIndex(0, this.Count, x => x.Equals(seg));
            if (index == -1 || index == this.Count - 1)
                return null;
            return this[index + 1];
        }
        public Segment Below(Segment seg)
        {
            var index = this.FindIndex(0, this.Count, x => x.Equals(seg));
            if (index == -1 || index == 0)
                return null;
            return this[index - 1];
        }
    }
}
