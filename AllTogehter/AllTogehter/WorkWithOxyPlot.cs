using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.WindowsForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllTogether
{
    public class WorkWithOxyPlot
    {
        static public PlotModel pm = new PlotModel();
        static public LinearAxis Xaxis { get; set; }
        static public LinearAxis Yaxis { get; set; }

        public WorkWithOxyPlot()
        {
            Xaxis = new LinearAxis();
            Yaxis = new LinearAxis();
        }

        public static void PlotGraph(ref PlotView plotView)
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

        public static void PlotColumnSeries(ref PlotView plotView, double[] arr)
        {
            PlotModel myModel = new PlotModel();
            ColumnSeries columnSeries = new ColumnSeries();

            LinearAxis yAxis = new LinearAxis { };
            yAxis.IsZoomEnabled = false;
            myModel.Axes.Add(yAxis);

            CategoryAxis xAxis = new CategoryAxis { };
            xAxis.IsZoomEnabled = false;
            myModel.Axes.Add(xAxis);

            for (int i = 0; i < arr.Length; i++)
            {
                columnSeries.Items.Add(new ColumnItem(arr[i]));
            }

            myModel.Series.Add(columnSeries);
            plotView.Model = myModel;
            plotView.Refresh();

        }   

        public static void MakePlotmodel(PlotView plotView)
        {
            plotView.Model = pm;
        }
    }
}
