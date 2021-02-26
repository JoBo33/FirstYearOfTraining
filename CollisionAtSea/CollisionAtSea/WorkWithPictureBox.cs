using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CollisionAtSea
{
    class WorkWithPictureBox
    {
        Bitmap bm;
        public Graphics gr;
        public bool _startOrEnd { get; set; }
        public Point _start { get; set; }
        public Point _end { get; set; }
        public List<Point[]> _lines { get; set; }

        public WorkWithPictureBox()
        {
            _lines = new List<Point[]>();
            _startOrEnd = true;
            _start = new Point();
            _end = new Point();
        }

        public void picture(PictureBox pictureBox)
        {
            // Make the Bitmap.
            int wid = pictureBox.ClientSize.Width;
            int hgt = pictureBox.ClientSize.Height;
            bm = new Bitmap(wid, hgt);
            //bmResized = new Bitmap(bm, pictureBoxCollision.ClientSize.Width, pictureBoxCollision.ClientSize.Height);
            gr = Graphics.FromImage(bm);
            gr.SmoothingMode = SmoothingMode.HighQuality;
            //TransformToMapTheGraph(gr, pictureBoxCollision.ClientSize.Width, pictureBoxCollision.ClientSize.Height, min, max);
            //DrawGraphAndAxes(gr, null, min, max);
            // Display the result.
            pictureBox.Image = bm;
        }

        public void HandleMouseClick(MouseEventArgs e, RichTextBox richTextBox, PictureBox pictureBox, DataGridView dataGridView)
        {
            Pen pen = new Pen(Color.Blue, 0);
            int x = e.X; //bm.Width * e.X / pictureBoxCollision.Width;
            int y = e.Y; //bm.Height * e.Y / pictureBoxCollision.Height;

            if (_startOrEnd)
            {
                richTextBox.Text += (String.Format("Start: X={0}, Y={1},\n", x, y));
                _start = new Point(x, y); //(x* pictureBoxCollision.Width/ bm.Width, y* pictureBoxCollision.Height/ bm.Height);
                _startOrEnd = false;
            }
            else
            {
                richTextBox.Text += (String.Format("End: X={0}, Y={1} \n", x, y));
                _end = new Point(x * pictureBox.Width / bm.Width, y * pictureBox.Height / bm.Height);
                Point[] line = new Point[2] { _start, _end };
                _lines.Add(line);
                _startOrEnd = true;
                gr.DrawLine(pen, _start, _end);
                dataGridView.Rows.Add();
            }

            pictureBox.Invalidate();
        }

        //private void TransformToMapTheGraph(Graphics gr, int wid, int hgt, float min, float max)
        //{
        //    // Transform to map the graph bounds to the Bitmap.
        //    RectangleF rect = new RectangleF(
        //        min + min / 10, min + min / 10, max - min + max / 10, max - min + max / 10);
        //    PointF[] pts =
        //    {
        //        new PointF(0, wid),
        //        new PointF(wid, hgt),
        //        new PointF(0, 0),
        //        };
        //    gr.Transform = new System.Drawing.Drawing2D.Matrix(rect, pts);
        //}
        //private void DrawGraphAndAxes(Graphics gr, PointF[] pointFs, float min, float max)
        //{
        //    Pen pen = new Pen(Color.Blue, 0);
        //    Font font = new Font("Calibri", 2.5f);//0.015f * (max - min));
        //    // Draw the axes.
        //    gr.DrawLine(pen, min + min / 10, 1, max + max / 10, 1);
        //    gr.DrawLine(pen, 0, min + min / 10+1, 0, max + max / 10);
        //    gr.ScaleTransform(1, -1);
        //    //labeling the axes
        //    for (int x = (int)min, y = (int)min; x <= max && y <= max; x++, y++)
        //    {
        //        gr.DrawString(x.ToString(), font, Brushes.Black, x, -4.5f); //-(max - min) * 0.018f);
        //        gr.DrawString(y.ToString(), font, Brushes.Black, -0.1f, -y);
        //        x = (int)(x + (max - min) / 10);
        //        y = (int)(y + (max - min) / 10);
        //    }
        //    gr.ScaleTransform(1, -1);
        //    for (int x = (int)min, y = (int)min; x <= max && y <= max; x++, y++)
        //    {
        //        gr.DrawLine(pen, x, -0.1f, x, 0.1f);
        //        gr.DrawLine(pen, -0.1f, y, 0.1f, y);
        //    }
        //    pen.Color = Color.Red;
        //   // gr.DrawLines(pen, pointFs);
        //}
    }
}
