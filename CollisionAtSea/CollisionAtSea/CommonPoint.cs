using OxyPlot;
using OxyPlot.Series;
using OxyPlot.WindowsForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CollisionAtSea
{
    class CommonPoint
    {
        public DataPoint _commonPoint { get; set; }
        PlotWithOxyPlot oxyPlot = new PlotWithOxyPlot();

        public CommonPoint()
        {
            _commonPoint = new DataPoint();
        }


        public void DrawCommonPoint(double xValueCommonPoint, double yValueCommonPoint, Pen pen, PlotView plotView)
        {
            _commonPoint = new DataPoint(xValueCommonPoint-0.1, yValueCommonPoint);
            DataPoint _commonPoint2 = new DataPoint(xValueCommonPoint+0.1, yValueCommonPoint);
            FunctionSeries fs = new FunctionSeries()
            {
                Color = OxyColor.FromArgb(255, 255, 0, 0),
            };
            fs.Points.Add(_commonPoint);
            fs.Points.Add(_commonPoint2);
            plotView.Model.Series.Add(fs);
        }

        public bool CalculateCommonPoint(ref double xValueCommonPoint, ref double yValueCommonPoint, DataPoint[] line1, DataPoint[] line2, int point, int nextPoint, RichTextBox richTextBox)
        {
            double counter = (line2[1].Y - line2[0].Y) * (line1[0].X - line2[1].X) - (line1[0].Y - line2[1].Y) * (line2[1].X - line2[0].X);
            double denominator = (line1[1].Y - line1[0].Y) * (line2[1].X - line2[0].X) - (line2[1].Y - line2[0].Y) * (line1[1].X - line1[0].X);
            double helpValue = counter / denominator;

            if (helpValue == 0)
            {
                //richTextBox.Text += ("Boat: " + point + " Boat: " + nextPoint + " No collision! The boats are parallel.\n");
                return true;
            }

            xValueCommonPoint = line1[0].X + helpValue * (line1[1].X - line1[0].X);
            yValueCommonPoint = line1[0].Y + helpValue * (line1[1].Y - line1[0].Y);

            if ((line1[0].X < xValueCommonPoint && line1[1].X < xValueCommonPoint) || (line1[0].X > xValueCommonPoint && line1[1].X > xValueCommonPoint) || (line1[0].Y < yValueCommonPoint && line1[1].Y < yValueCommonPoint) || (line1[0].Y > yValueCommonPoint && line1[1].Y > yValueCommonPoint))
            {
                //richTextBox.Text += ("Boat: " + point + " Boat: " + nextPoint + " No collision! They do not cut each other between start and finish.\n");
                return true;
            }
            else if ((line2[0].X < xValueCommonPoint && line2[1].X < xValueCommonPoint) || (line2[0].X > xValueCommonPoint && line2[1].X > xValueCommonPoint) || (line2[0].Y < yValueCommonPoint && line2[1].Y < yValueCommonPoint) || (line2[0].Y > yValueCommonPoint && line2[1].Y > yValueCommonPoint))
            {
                //richTextBox.Text += ("Boat: " + point + " Boat: " + nextPoint + " No collision! They do not cut each other between start and finish.\n");
                return true;
            }

            return false;
        }
    }
}
