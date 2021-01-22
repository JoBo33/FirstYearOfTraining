using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AllTogether
{
    public class Vector
    {
        // globale attributes
        float[] vector;
        int length;
        static Bitmap bm;
        public Vector(int length)
        {
            // fill constuctor 
            vector = new float[length];
            this.length = length;
        }
        public Vector(float x1, float x2)
        {
            length = 2;
            vector = new float[2];
            vector[0] = x1;
            vector[1] = x2;
        }
        public float this[int length]
        {
            // getter setter 
            get { return vector[length]; }
            set { vector[length] = value; }
        }
        //override addition operator
        public static Vector operator +(Vector vector1, Vector vector2)
        {
            if (vector1.length == vector2.length)
            {
                Vector result = new Vector(vector2.length);

                for (int i = 0; i < vector2.length;  i++)
                {
                    result[i] = vector1[i] + vector2[i];
                }

                return result;
            }

            return null;
        }
        // override subtraction operator
        public static Vector operator -(Vector vector1, Vector vector2)
        {
            if (vector1.length == vector2.length)
            {
                Vector result = new Vector(vector2.length);
                for (int i = 0; i < vector2.length; i++)
                {
                    result[i] = vector1[i] - vector2[i];
                }
                return result;
            }
            return null;
        }

        // create the first vector 
        public static Vector VectorToArray(DataGridView dataGridView, int row)       
        {
            Vector vector = new Vector(dataGridView.ColumnCount-1);
            for (int i = 0; i < vector.length; i++)
            {
                if (dataGridView[i, row].Value != null)
                {
                    vector[i] = Convert.ToSingle(dataGridView[i, row].Value);
                }
                else
                {
                    vector = null;
                    return vector;
                }
            }
            return vector;
        }
        public static Vector[] CreateVectorArray(DataGridView dataGridView, List<int> chosenVectors)
        {
            Vector[] vectors = new Vector[chosenVectors.Count];
            for (int row = 0; row < dataGridView.RowCount-1; row++)
            {
                vectors[row] = VectorToArray(dataGridView, row);
            }
            return vectors;
        }

        private static bool TwoVectorsGiven(List<int> list)
        {
            if (list.Count != 2)
            {
                MessageBox.Show("not exactly 2 vectors given!");
                return false;
            }
            return true;
        }

        public double ScalarProduct(Vector vectorTwo)
        {
            double scalarProduct = 0;
            for (int i = 0; i < this.length; i++)
            {
                scalarProduct += vector[i] * vectorTwo[i];
            }
            return scalarProduct;
        }
        public static string PrintScalarProduct(DataGridView dataGridView, List<int> list)
        {
            if (TwoVectorsGiven(list))
            {
                Vector[] vectors = CreateVectorArray(dataGridView, list);
                string output = string.Empty;
                for (int i = 0; i < dataGridView.RowCount; i++)
                {
                    for (int j = 0; j < dataGridView.RowCount; j++)
                    {
                        if (i == list[0] && j == list[1])
                        {
                            try
                            {
                                output = vectors[i - 1].ScalarProduct(vectors[j - 1]).ToString();
                            }
                            catch (NullReferenceException)
                            {
                                MessageBox.Show("Wrong input!");
                                return null;
                            }
                        }
                    }
                }
                return output;
            }
            return string.Empty;
        }
        // Calculate angle between 2 vectors
        public double Angle(Vector vectorTwo)
        {
            double angle = 0;
            double sumOfVectorOneValues = 0;
            double sumOfVectorTwoValues = 0;
            for (int i = 0; i < length; i++)
            {
                sumOfVectorOneValues += Math.Pow(vector[i], 2);
                sumOfVectorTwoValues += Math.Pow(vectorTwo[i], 2);
            }
            angle = Math.Round((Math.Acos(ScalarProduct(vectorTwo) / Math.Sqrt(sumOfVectorOneValues * sumOfVectorTwoValues)) / Math.PI) * 180, 4);
            return angle;
        }
        public static string PrintAngle(DataGridView dataGridView, List<int> list)
        {
            if (TwoVectorsGiven(list))
            {
                Vector[] vectors = CreateVectorArray(dataGridView, list);
                string output = string.Empty;
                for (int i = 0; i < dataGridView.RowCount; i++)
                {
                    for (int j = 0; j < dataGridView.RowCount; j++)
                    {
                        if (i == list[0] && j == list[1])
                        {
                            try
                            {
                                output = vectors[i - 1].Angle(vectors[j - 1]).ToString() + "°";
                            }
                            catch (NullReferenceException)
                            {
                                MessageBox.Show("Wrong input!");
                                return null;
                            }
                        }
                    }
                }
                return output;
            }
            return string.Empty;
        }
       
        public static void MakeGraph(PointF[] pointFs, PictureBox pictureBox, float min, float max)
        {
            AdjustMinMax(ref min, ref max);
            // Make the Bitmap.
            int wid = pictureBox.ClientSize.Width;
            int hgt = pictureBox.ClientSize.Height;
            if (hgt == 0 || wid == 0) return;
            bm = new Bitmap(wid, hgt);
            Graphics gr = Graphics.FromImage(bm);
            gr.SmoothingMode = SmoothingMode.HighQuality;
            TransformToMapTheGraph(gr, wid, hgt, min, max);
            DrawGraphAndAxes(gr, pointFs, min, max);
            // Display the result.
            pictureBox.Image = bm;
        }
        private static void AdjustMinMax (ref float min, ref float max)
        {
            if (min == 0)
            {
                min = -max/10;
            }
            if (Math.Abs(min) / 10 > max)
            {
                max = Math.Abs(min) / 5;
            }
            else
            {
                max++;
            }
        }
        private static void TransformToMapTheGraph (Graphics gr, int wid, int hgt, float min, float max)
        {
            // Transform to map the graph bounds to the Bitmap.
            RectangleF rect = new RectangleF(
                min + min / 10, min + min / 10, max - min + max / 10, max - min + max / 10);
            PointF[] pts =
            {
                new PointF(0, hgt),
                new PointF(wid, hgt),
                new PointF(0, 0),
                };
            gr.Transform = new System.Drawing.Drawing2D.Matrix(rect, pts);
        }
        private static void DrawGraphAndAxes(Graphics gr, PointF[] pointFs, float min, float max)
        {
            Pen pen = new Pen(Color.Blue, 0);
            Font font = new Font("Calibri", 0.015f * (max - min));
            // Draw the axes.
            gr.DrawLine(pen, min + min / 10, 0, max + max / 10, 0);
            gr.DrawLine(pen, 0, min + min / 10, 0, max + max / 10);
            gr.ScaleTransform(1, -1);
            //labeling the axes
            for (int x = (int)min, y = (int)min; x <= max && y <= max; x++, y++)
            {
                gr.DrawString(x.ToString(), font, Brushes.Black, x, -(max-min)*0.018f);
                gr.DrawString(y.ToString(), font, Brushes.Black, -0.1f, -y);
                x = (int)(x + (max - min) / 10);
                y = (int)(y + (max - min) / 10);
            }
            gr.ScaleTransform(1, -1);
            for (int x = (int)min, y = (int)min; x <= max && y <= max; x++, y++)
            {
                gr.DrawLine(pen, x, -0.1f, x, 0.1f);
                gr.DrawLine(pen, -0.1f, y, 0.1f, y);
            }
            pen.Color = Color.Red;
            gr.DrawLines(pen, pointFs);
        }

        public static PointF[] VectorToOrigin(Vector[] vectors, PointF[] pointFs, ref float min, ref float max)
        {
            pointFs = new PointF[vectors.Length * 2];
            for (int i = 0; i < vectors.Length; i++)
            {
                pointFs[i * 2] = new PointF(vectors[i][0], vectors[i][1]);
            }
            MinMaxCoordinates(pointFs, ref min, ref max);
            return pointFs;
        }
        public static PointF[] VectorAddition(Vector[] vectors, PointF[] pointFs, ref float min, ref float max)
        {
            Vector startingVector = new Vector(vectors[0].length);
            for (int i = 0; i < vectors.Length; i++)
            {
                pointFs[i + 1] = new PointF(startingVector[0] + vectors[i][0], startingVector[1] + vectors[i][1]);
                startingVector[0] = startingVector[0] + vectors[i][0];
                startingVector[1] = startingVector[1] + vectors[i][1];
            }
            MinMaxCoordinates(pointFs, ref min, ref max);
            return pointFs;
        }
        public static PointF[] VectorSubtraction(Vector[] vectors, PointF[] pointFs, ref float min, ref float max)
        {
            Vector startingVector = new Vector(vectors[0].length);
            pointFs[1] = new PointF(startingVector[0] + vectors[0][0], startingVector[1] + vectors[0][1]);
            startingVector[0] = startingVector[0] + vectors[0][0];
            startingVector[1] = startingVector[1] + vectors[0][1];
            for (int i = 1; i < vectors.Length; i++)
            {
                pointFs[i + 1] = new PointF(startingVector[0] - vectors[i][0], startingVector[1] - vectors[i][1]);
                startingVector[0] = startingVector[0] - vectors[i][0];
                startingVector[1] = startingVector[1] - vectors[i][1];
            }
            MinMaxCoordinates(pointFs, ref min, ref max);
            return pointFs;
        }
        public static PointF[] VectorConnect(Vector[] vectors, PointF[] pointFs, ref float min, ref float max)
        {
            pointFs = new PointF[vectors.Length];
            if (min > 0)
            {
                for (int i = 0; i < vectors.Length; i++)
                {
                    pointFs[i] = new PointF(vectors[i][0] - Math.Abs(min), vectors[i][1] - Math.Abs(min));
                }
            }
            else if (max < 0)
            {
                for (int i = 0; i < vectors.Length; i++)
                {
                    pointFs[i] = new PointF(vectors[i][0] - max, vectors[i][1] - max);
                }
            }
            else
            {
                for (int i = 0; i < vectors.Length; i++)
                {
                    pointFs[i] = new PointF(vectors[i][0], vectors[i][1]);
                }
            }
            MinMaxCoordinates(pointFs, ref min, ref max);
            return pointFs;
        }

        public static void MinMaxCoordinates(PointF[] pointFs, ref float min, ref float max)
        {
            for (int i = 0; i < pointFs.Length; i++)
            {
                if (pointFs[i].X > max)
                {
                    max = pointFs[i].X;
                }
                if (pointFs[i].X < min)
                {
                    min = pointFs[i].X;
                }
                if (pointFs[i].Y < min)
                {
                    min = pointFs[i].Y;
                }
                if (pointFs[i].Y > max)
                {
                    max = pointFs[i].Y;
                }
            }
        }
    }
} 
