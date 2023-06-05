using System;
using System.Drawing;
using System.Windows.Forms;

namespace CGLab
{
    public partial class Form1 : Form
    {
        private Point _startPoint;
        private Pen _pen;
        private bool _mouseButton;
        private Point[] _curve;

        public Form1()
        {
            InitializeComponent();
            MouseDown += Mouse_Down;
            MouseUp += Mouse_Up;
            MouseMove+= DrawCurve;
            _curve = new Point[180];
        }

        private void Color_Click(object sender, EventArgs e)
        {

            ColorDialog MyDialog = new ColorDialog();

            MyDialog.AllowFullOpen = false;

            MyDialog.ShowHelp = true;

            MyDialog.Color = Width.ForeColor;


            if (MyDialog.ShowDialog() == DialogResult.OK)
            {
                Color.BackColor = MyDialog.Color;
                Color.ForeColor = MyDialog.Color;
            }
        }


        private void Mouse_Down(object sender, MouseEventArgs e)
        {
            _pen = new Pen(Color.BackColor, (int)Width.Value);
            _startPoint = e.Location;
            _mouseButton = true;
        }

        private void Mouse_Up(object sender, MouseEventArgs e)
        {
            _startPoint = MousePosition;
            _mouseButton = false;
        }

        private void DrawCurve(object sender, MouseEventArgs e)
        {
            if (!_mouseButton) return;
            GetPoints(_startPoint, e.Location);
            var graph = this.CreateGraphics();
            graph.Clear(System.Drawing.Color.FromArgb(255, 227, 227, 227));
                graph.DrawCurve(_pen, _curve);
        }

        private void GetPoints(Point start, Point end)
        {
            Point center = new Point((start.X + end.X) / 2, (start.Y + end.Y) / 2);
            Point startVect = new Point(start.X - center.X, start.Y - center.Y);
            _curve[0] = start;
            for (int i = 1; i < 180; i++)
            {
                double angle = -i * Math.PI / 180;
                int x = (int)(startVect.X * Math.Cos(angle) + startVect.Y * Math.Sin(angle));
                int y = (int)(startVect.Y * Math.Cos(angle) - startVect.X * Math.Sin(angle));
                _curve[i] = new Point(x+ center.X, y+center.Y);
            }
            _curve[179] = end;
        }

    }
}
