using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using OxyPlot.Axes;
using OxyPlot;
using OxyPlot.Series;



// https://www.matse-ausbildung.de/kollision.html

namespace CollisionAtSea
{
    public partial class CollisionAtSea : Form
    {
        // generate instances of needed classes
        CommonPoint commonPoint = new CommonPoint();
        PlotWithOxyPlot oxyPlot = new PlotWithOxyPlot();
        StepWidth stepWidth = new StepWidth();

        public CollisionAtSea()
        {
            InitializeComponent();
            oxyPlot.PlotGraph(ref plotViewCollision);
        }

        private void dataGridViewCollision_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (!(e.FormattedValue.ToString() == "-" || e.FormattedValue.ToString() == "" || Double.TryParse(e.FormattedValue.ToString(), out double value)))
            {
                e.Cancel = true;
            }
        }

        private void dataGridViewCollision_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            dataGridViewCollision.Rows[e.RowIndex].HeaderCell.Value = "Boat " + (e.RowIndex);
        }

        private void plotViewCollision_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                oxyPlot.HandleMouseClick(e, richTextBoxPoints, dataGridViewCollision, plotViewCollision);
            }
            else if (e.Button == MouseButtons.Right)
            {
                oxyPlot.EndOfPolyline(richTextBoxPoints,  dataGridViewCollision, plotViewCollision);

            }
        }

        // event to start collisions-calculation
        private void buttonCollision_Click(object sender, EventArgs e)
        {
            int lineCount = oxyPlot._lines2.Count;
            if (lineCount < 2 || lineCount > dataGridViewCollision.RowCount) return;
            Calculate(oxyPlot._lines2);
        }

        // in this project is the Calculate(List<List<DataPoint>> lines) method the "control-method"
        // most other methods are called here 
        private void Calculate(List<List<DataPoint>> lines)
        {
            Pen pen = new Pen(Color.Red, 0);

            for (int point = 0; point < lines.Count; point++)
            {
                for (int nextPoint = point+1; nextPoint < lines.Count; nextPoint++)
                {
                    DataPoint[] line1 = lines[point].ToArray();
                    DataPoint[] line2 = lines[nextPoint].ToArray();

                    for (int i = 1; i < line1.Length; i++)
                    {
                        DataPoint[] comparativeLine1 = new DataPoint[2] { line1[i - 1], line1[i] };

                        for (int j = 1; j < line2.Length; j++)
                        {
                            double delayedStart1 = DelayCalculation(i, line1, point);  // the delay one boat has when they start at a breakpoint
                            
                            DataPoint[] comparativeLine2 = new DataPoint[2] { line2[j - 1], line2[j] };
                            double delayedStart2 = DelayCalculation(j, line2, nextPoint);    // the delay one boat has when they start at a breakpoint

                            AdjustDelay(ref delayedStart1, ref delayedStart2);

                            double xValueCommonPoint = 0;
                            double yValueCommonPoint = 0;

                            if (commonPoint.CalculateCommonPoint(ref xValueCommonPoint, ref yValueCommonPoint, comparativeLine1, comparativeLine2, point, nextPoint, richTextBoxShowCollisionResults))
                            {
                                continue;
                            }

                            commonPoint.DrawCommonPoint(xValueCommonPoint, yValueCommonPoint, pen, plotViewCollision);

                            stepWidth.CalculateStepWidth(comparativeLine1, comparativeLine2, point, nextPoint, dataGridViewCollision);

                            CompareNewAndOldDeltaValues(comparativeLine1, comparativeLine2, point, nextPoint, pen, delayedStart1, delayedStart2);
                        }
                    }

                }
            }
            oxyPlot.pm.InvalidatePlot(true);
            plotViewCollision.Invalidate();
        }

        private double DelayCalculation(int i, DataPoint[] line1, int point)
        {
            double delayedStart = 0;
            if (i > 1)
            {
                for (int segment = 1; segment < i; segment++)
                {
                    delayedStart += Math.Sqrt(Math.Pow(line1[segment - 1].X - line1[segment].X, 2) + Math.Pow(line1[segment - 1].Y - line1[segment].Y, 2)) / Convert.ToDouble(dataGridViewCollision[0, point].Value);
                }
            }
            return delayedStart;
        }

        private void AdjustDelay(ref double delayedStart1, ref double delayedStart2)
        {
            if (delayedStart1 > delayedStart2)
            {
                delayedStart1 -= delayedStart2;
                delayedStart2 = 0;
            }
            else
            {
                delayedStart2 -= delayedStart1;
                delayedStart1 = 0;
            }
        }

        private void CompareNewAndOldDeltaValues(DataPoint[] line1, DataPoint[] line2, int point, int nextPoint, Pen pen, double delayedStart1, double delayedStart2)
        {
            float smallestDistance = float.MaxValue;

            DataPoint newPositionA = line1[0];
            DataPoint newPositionB = line2[0];

            double newXA = line1[0].X;
            double newYA = line1[0].Y;
            double newXB = line2[0].X;
            double newYB = line2[0].Y;

            double oldDeltaX = 0;
            double oldDeltaY = 0;
            double newDeltaX = 0;
            double newDeltaY = 0;

            DataPoint smallestDistance1 = new DataPoint();
            DataPoint smallestDistance2 = new DataPoint();

            smallestDistance = (float)Math.Sqrt(Math.Pow(((line2[0].X + stepWidth._stepWidthXB * delayedStart1) - (line1[0].X + stepWidth._stepWidthXA * delayedStart2)), 2) + 
                Math.Pow(((line2[0].Y + stepWidth._stepWidthYB * delayedStart1) - (line1[0].Y + stepWidth._stepWidthYA * delayedStart2)), 2));
            if (smallestDistance < (float)numericUpDownMinDistance.Value)
            {
                FunctionSeries fs = new FunctionSeries()
                {
                    Color = OxyColor.FromArgb(100, 255, 0, 0),
                };
                fs.Points.Add(newPositionA);
                fs.Points.Add(newPositionB);
                plotViewCollision.Model.Series.Add(fs);
                richTextBoxShowCollisionResults.Text += ("Boat: " + point + ", Boat: " + nextPoint + " Collision!" + "They start too close to each other. Start distance is: " + smallestDistance + "\n");
                return;
            }

            bool distancePossible = false; // just do distancecalculation if both boats are on comparatives lines

            double i = 0.01;
            while (true)
            {
                double oldXA = newXA;
                double oldYA = newYA;
                double oldXB = newXB;
                double oldYB = newYB;
                CalculateNewPosition(ref newXA, ref newXB, ref newYA, ref newYB, ref newPositionA, ref newPositionB, line1, line2, i, delayedStart1, delayedStart2);

                CalculateDelta(ref newDeltaX, ref newDeltaY, ref oldDeltaX, ref oldDeltaY, newXA, newXB, newYA, newYB, oldXA, oldYA, oldXB, oldYB);

                if (i > delayedStart1 && i > delayedStart2)
                {
                    distancePossible = true;
                }

                if (CheckPrintCondition(pen, ref smallestDistance, point, nextPoint, ref smallestDistance1, ref smallestDistance2, newPositionA, newPositionB, newDeltaX, newDeltaY, distancePossible, line1, line2) && distancePossible)
                {
                    break;
                }

                i = Math.Round(0.01 + i, 2);
            }
        }

        private void CalculateNewPosition(ref double newXA, ref double newXB, ref double newYA, ref double newYB, ref DataPoint newPositionA, ref DataPoint newPositionB, DataPoint[] line1, DataPoint[] line2, double i, double delayedStart1, double delayedStart2)
        {
            if (i > delayedStart1)
            {
                newXA = Math.Round(line1[0].X + stepWidth._stepWidthXA * (i-delayedStart1), 4);
                newYA = Math.Round(line1[0].Y + stepWidth._stepWidthYA * (i-delayedStart1), 4);
                newPositionA = new DataPoint(Math.Round(newXA,4), Math.Round(newYA,4));
            }
            if (i > delayedStart2)
            {
                newXB = Math.Round(line2[0].X + stepWidth._stepWidthXB * (i-delayedStart2), 4);
                newYB = Math.Round(line2[0].Y + stepWidth._stepWidthYB * (i-delayedStart2), 4);
                newPositionB = new DataPoint(Math.Round(newXB,4), Math.Round(newYB,4));
            }
        }

        private void CalculateDelta(ref double newDeltaX, ref double newDeltaY, ref double oldDeltaX, ref double oldDeltaY, double newXA, double newXB, double newYA, double newYB, double oldXA, double oldYA, double oldXB, double oldYB)
        {
            oldDeltaX = Math.Round(Math.Abs(oldXA - oldXB), 4);
            oldDeltaY = Math.Round(Math.Abs(oldYA - oldYB), 4);

            newDeltaX = Math.Round(Math.Abs(newXA - newXB), 4);
            newDeltaY = Math.Round(Math.Abs(newYA - newYB), 4);
        }

        private bool CheckPrintCondition(Pen pen, ref float smallestDistance, int point, int nextPoint, ref DataPoint smallestDistance1, ref DataPoint smallestDistance2, DataPoint newPositionA, DataPoint newPositionB, double newDeltaX, double newDeltaY, bool distancePossible, DataPoint[] line1, DataPoint[] line2)
        {
            double currentDistance = Math.Round((float)Math.Sqrt(Math.Pow(newDeltaX, 2) + Math.Pow(newDeltaY, 2)),4);
            bool newSmallestDistanceFound = false;
            if (smallestDistance >= currentDistance && distancePossible)
            {
                smallestDistance1 = newPositionA;
                smallestDistance2 = newPositionB;
                smallestDistance = Convert.ToSingle(currentDistance);
                newSmallestDistanceFound = true;
            }
            
            float minDistance = (float)numericUpDownMinDistance.Value;
            FunctionSeries fs = new FunctionSeries()
            {
                Color = OxyColor.FromArgb(100, 255, 0, 0),
            };
            if (smallestDistance1.X != 0 && smallestDistance1.Y != 0 && smallestDistance2.X != 0 && smallestDistance2.Y != 0)
            {
                fs.Points.Add(smallestDistance1);
                fs.Points.Add(smallestDistance2);
            }
            else
            {
                fs.Points.Add(newPositionA);
                fs.Points.Add(newPositionB);
            }

            if (newSmallestDistanceFound)
            {
                if (smallestDistance < minDistance) 
                {
                    plotViewCollision.Model.Series.Add(fs);
                    richTextBoxShowCollisionResults.Text += ("Boat: " + point + ", Boat: " + nextPoint + " Collision!" + " Distance is shorter then: "+ minDistance +" Distance is: " + smallestDistance + "\n");
                    return true;
                }
                return false;
            }
            else
            {
                if ((line1[0].X < newPositionA.X && line1[1].X < newPositionA.X) || (line1[0].X > newPositionA.X && line1[1].X > newPositionA.X) || (line1[0].Y < newPositionA.Y && line1[1].Y < newPositionA.Y) || (line1[0].Y > newPositionA.Y && line1[1].Y > newPositionA.Y))
                {
                    //richTextBox.Text += ("Boat: " + point + " Boat: " + nextPoint + " No collision! They do not cut each other between start and finish.\n");
                    return true;
                }
                else if ((line2[0].X < newPositionB.X && line2[1].X < newPositionB.X) || (line2[0].X > newPositionB.X && line2[1].X > newPositionB.X) || (line2[0].Y < newPositionB.Y && line2[1].Y < newPositionB.Y) || (line2[0].Y > newPositionB.Y && line2[1].Y > newPositionB.Y))
                {
                    //richTextBox.Text += ("Boat: " + point + " Boat: " + nextPoint + " No collision! They do not cut each other between start and finish.\n");
                    return true;
                }
                else if (distancePossible)
                {
                    plotViewCollision.Model.Series.Add(fs);
                    richTextBoxShowCollisionResults.Text += ("Boat: " + point + ", Boat: " + nextPoint + " No collision! " + "smallest distance is: " + smallestDistance + "\n");
                }

                return true;
            }
        }

        // Event to set everything back to default - clear the application
        private void buttonDefaultWindow_Click(object sender, EventArgs e)
        {
            commonPoint._commonPoint = new DataPoint();

            oxyPlot._lines2.Clear();
            plotViewCollision.Model.Series.Clear();
            oxyPlot.Xaxis.Reset();
            oxyPlot.Yaxis.Reset();
            plotViewCollision.InvalidatePlot(true);

            richTextBoxPoints.Clear();
            richTextBoxShowCollisionResults.Clear();
            dataGridViewCollision.Rows.Clear();
            numericUpDownMinDistance.Value = 1.0m;
        }

    }
}
