using OxyPlot;
using OxyPlot.Axes;
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
    public class PlotWithOxyPlot
    {
        public PlotModel pm = new PlotModel();
        public LinearAxis Xaxis { get; set; } 
        public LinearAxis Yaxis { get; set; }

        List<DataPoint> _polyline = new List<DataPoint>(); 
        public List<List<DataPoint>> _lines2 = new List<List<DataPoint>>();
        DataPoint _start2 { get; set; }
        DataPoint _pointOnPolyLine { get; set; }
        DataPoint _end2 { get; set; }

        LineCount lineC = new LineCount();

        public PlotWithOxyPlot()
        {
            Xaxis = new LinearAxis();
            Yaxis = new LinearAxis();
            _start2 = new DataPoint();
            _end2 = new DataPoint();
            _pointOnPolyLine = new DataPoint();
        }

        public void PlotGraph(ref PlotView plotView)
        {
            Xaxis = new LinearAxis { Position = AxisPosition.Bottom, Minimum = 0, Maximum = 100 };
            Yaxis = new LinearAxis { Position = AxisPosition.Left, Minimum = 0, Maximum = 100 };
            pm.PlotMargins = new OxyThickness(60, 10, 10, 30);
            Xaxis.AbsoluteMinimum = 0;
            Yaxis.AbsoluteMinimum = 0;

            Xaxis.IsPanEnabled = false;
            Yaxis.IsPanEnabled = false;

            pm.Axes.Add(Xaxis);
            pm.Axes.Add(Yaxis);
            plotView.Model = pm;
        }


        public void HandleMouseClick(MouseEventArgs e, RichTextBox richTextBox, DataGridView dataGridView, PlotView plotView)
        {
            pm.InvalidatePlot(true);
            lineC._lineCount += e.Clicks;
            Pen pen = new Pen(Color.Blue, 0);
            double x = (e.X - 69) * ((Xaxis.ActualMaximum - Xaxis.ActualMinimum) / (695.0 - 69)); 
            double y = (452 - e.Y) * ((Yaxis.ActualMaximum - Xaxis.ActualMinimum) / (452.0 - 18));

            _pointOnPolyLine = new DataPoint(x, y);
            _polyline.Add(_pointOnPolyLine);

            if (lineC._lineCount == 1)
            {
                richTextBox.Text += String.Format("Start: X={0}, Y={1},\n", x, y);
            }
            else if (lineC._lineCount > 2)
            {
                richTextBox.Text += String.Format("Breakpoint: X={0}, Y={1},\n", _polyline[_polyline.Count - 2].X, _polyline[_polyline.Count - 2].Y);
            }
        }
        public void EndOfPolyline(RichTextBox richTextBox, DataGridView dataGridView, PlotView plotView)
        {
            if(lineC._lineCount < 2)
            {
                return;
            }
            FunctionSeries fs = new FunctionSeries();
            List<DataPoint> polylines2 = new List<DataPoint>();
            for (int i = 0; i < _polyline.Count; i++)
            {
                fs.Points.Add(_polyline[i]);
                polylines2.Add(_polyline[i]);
            }
            _lines2.Add(polylines2);
            _polyline.Clear();
            richTextBox.Text += (String.Format("End: X={0}, Y={1} \n", polylines2[polylines2.Count-1].X, polylines2[polylines2.Count - 1].Y));
            lineC._lineCount = 0;
            plotView.Model.Series.Add(fs);
            dataGridView.Rows.Add();
            pm.InvalidatePlot(true);
        }
    }
}
