using SimplePolugonGUI.Comparers;
using SimplePolugonGUI.Structures;
using System.Windows.Forms;
using Point = SimplePolugonGUI.Structures.Point;


namespace SimplePolugonGUI
{
    public partial class Form1 : Form
    {
        private List<Point> points = new List<Point>();
        private List<Segment> lines = new List<Segment>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            int x = e.X;
            int y = e.Y;

            points.Add(new Point(x, y));

            // З'єднати точку з попередньою точкою лінією
            if (points.Count > 1)
            {
                Point start = points[points.Count - 2];
                Point end = points[points.Count - 1];
                lines.Add(new Segment(start, end));
            }
            pictureBox1.Invalidate();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            // Малюємо всі лінії
            foreach (Segment line in lines)
            {
                g.DrawLine(Pens.Black, line.Left.ToClassicPoint(), line.Right.ToClassicPoint());
            }

            foreach (Point point in points)
            {
                g.FillEllipse(Brushes.DarkRed, point.X - 2, point.Y - 2, 4, 4);
            }
        }

        private void Check_Click(object sender, EventArgs e)
        {
            // Переконатися, що є хоча б дві точки для створення лінії
            if (points.Count >= 2)
            {
                // З'єднати останню точку з першою точкою
                Point start = points[points.Count - 1];
                Point end = points[0];
                lines.Add(new Segment(start, end));

                // Оновити графічне полотно, щоб відобразити нову лінію
                pictureBox1.Invalidate();
                var isSimplePolynom = CheckSimplePolynom();
                if (isSimplePolynom)
                    pictureBox1.BackColor = Color.Red;
                else
                    pictureBox1.BackColor = Color.Green;
            }
        }

        private bool CheckSimplePolynom()
        {
            //var p11 = new Point(1, 1);
            //var p12 = new Point(3, 4);
            //var p21 = new Point(3, 4);
            //var p22 = new Point(5, 3);
            //var p31 = new Point(5, 3);
            //var p32 = new Point(4, 2);
            //var p41 = new Point(4, 2);
            //var p42 = new Point(1, 1);

            //var seg1 = new Segment(p11, p12);
            //var seg2 = new Segment(p21, p22);
            //var seg3 = new Segment(p31, p32);
            //var seg4 = new Segment(p41, p42);

            //var segments = new SweepLineStatus();
            //segments.Add(seg1);
            //segments.Add(seg2);
            //segments.Add(seg3);
            //segments.Add(seg4);
            //////////////////////////
            var sweepLineStatus = new SweepLineStatus();
            var eventPoints = new List<Point>();

            foreach (var segment in lines)
            {
                eventPoints.Add(segment.Left);
                eventPoints.Add(segment.Right);
            }

            eventPoints.Sort(new PointComparer());

            foreach (var point in eventPoints)
            {
                Segment s;
                if (point.getSegmentLeft() != null)
                {
                    s = point.getSegmentLeft();
                    try
                    {
                        sweepLineStatus.Insert(s);
                    }
                    catch (InvalidOperationException e)
                    {
                        return true;
                    }
                    var aboveSegment = sweepLineStatus.Above(s);
                    var belowSegment = sweepLineStatus.Below(s);
                    if (aboveSegment != null && s.Intersects(aboveSegment)
                        || belowSegment != null && s.Intersects(belowSegment))
                    {
                        return true;
                    }
                }
                if (point.getSegmentRight() != null)
                {
                    s = point.getSegmentRight();
                    var aboveSegment = sweepLineStatus.Above(s);
                    var belowSegment = sweepLineStatus.Below(s);
                    if (aboveSegment != null && belowSegment != null && belowSegment.Intersects(aboveSegment))
                    {
                        return true;
                    }
                    sweepLineStatus.Delete(s);
                }

            }
            return false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Очищення списків точок і ліній
            points.Clear();
            lines.Clear();
            pictureBox1.BackColor = Color.White;

            // Оновлення графічного полотна
            pictureBox1.Invalidate();
        }
    }
}