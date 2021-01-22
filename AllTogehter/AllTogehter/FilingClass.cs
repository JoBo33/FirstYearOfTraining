using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AllTogehter
{
    class FilingClass
    {
        #region Number systems
        //        char[] dualChar = givenInput.ToCharArray();
        //        int j = 1;
        //        int[] binary = new int[16];
        //        for (int i = dualChar.Length - 1; i >= 0; i--)
        //        {
        //            if (dualChar[i] != '0' && dualChar[i] != '1')
        //            {
        //                MessageBox.Show("Not a dual number");
        //            }
        //            else
        //            {
        //                binary[binary.Length - j] = (int)char.GetNumericValue(dualChar[i]);
        //                j++;
        //            }
        //        }
        //        ConvertFromBiToDec(binary);
        #endregion Number systems
        #region Drawing vectors (form1)
        /*
        private void textBoxAngleAndScalarProduct_Enter(object sender, EventArgs e)
        {
            if (textBoxAngleAndScalarProduct.Text == "e.g. v1 and v2")
            {
                textBoxAngleAndScalarProduct.Text = "";
                textBoxAngleAndScalarProduct.ForeColor = System.Drawing.Color.Black;
            }
        }
        private void textBoxAngleAndScalarProduct_Leave(object sender, EventArgs e)
        {
            //TODO: Anstatt textuelle Definition der zu verwendenden Vektoren -> Vektorspalten aktivieren, usw. Ferner Operation über me
            if (textBoxAngleAndScalarProduct.Text == "")
            {
                textBoxAngleAndScalarProduct.Text = "e.g. v1 and v2";
                textBoxAngleAndScalarProduct.ForeColor = System.Drawing.Color.Silver;
            }
        }
        private void buttonDrawVectors_MouseHover(object sender, EventArgs e)
        {
            labeltest.Text = "It is just a 2D coordinate system so it just uses \nthe first and second entry of the vectors!";
            labeltest.Visible = true;
        }
        private void buttonDrawVectors_MouseLeave(object sender, EventArgs e)
        {
            labeltest.Visible = false;
        }
        private void textBoxConnectVectors1_Enter(object sender, EventArgs e)
        {
            if (textBoxConnectVectors1.Text == "e.g. v1 -> v2")
            {
                textBoxConnectVectors1.Text = "";
                textBoxConnectVectors1.ForeColor = System.Drawing.Color.Black;
            }
        }
        private void textBoxConnectVectors1_Leave(object sender, EventArgs e)
        {
            if (textBoxConnectVectors1.Text == "")
            {
                textBoxConnectVectors1.Text = "e.g. v1 -> v2";
                textBoxConnectVectors1.ForeColor = System.Drawing.Color.Silver;
            }
        }
        private void textBoxConnectVectors2_Enter(object sender, EventArgs e)
        {
            if (textBoxConnectVectors2.Text == "e.g. v1 - v2")
            {
                textBoxConnectVectors2.Text = "";
                textBoxConnectVectors2.ForeColor = System.Drawing.Color.Black;
            }
        }
        private void textBoxConnectVectors2_Leave(object sender, EventArgs e)
        {
            if (textBoxConnectVectors2.Text == "")
            {
                textBoxConnectVectors2.Text = "e.g. v1 - v2";
                textBoxConnectVectors2.ForeColor = System.Drawing.Color.Silver;
            }
        }
        private void textBoxConnectVectors3_Enter(object sender, EventArgs e)
        {
            if (textBoxConnectVectors3.Text == "e.g. v1 + v2")
            {
                textBoxConnectVectors3.Text = "";
                textBoxConnectVectors3.ForeColor = System.Drawing.Color.Black;
            }
        }
        private void textBoxConnectVectors3_Leave(object sender, EventArgs e)
        {
            if (textBoxConnectVectors3.Text == "")
            {
                textBoxConnectVectors3.Text = "e.g. v1 + v2";
                textBoxConnectVectors3.ForeColor = System.Drawing.Color.Silver;
            }
        }
        */
        #endregion
        #region vectorclass
        /*
        public static void DrawVectorsFromTextBox(DataGridView dataGridView, Graphics gr, string text, Vector[] vectors, Color colorVector1, Color colorVector2, Color colorVector3, ComboBox combo)
        {
            int five = 5;
            //gr.ScaleTransform(1.0f, -1.0f);
            // shorter way if the textbox is empty
            if (string.Empty == text)
            {
                return;
            }
            Pen pen = new Pen(colorVector1, 2);
            // tested what be should drawed
            for (int i = 0; i < dataGridView.RowCount; i++)
            {
                // e.g. if entry == v1 
                if ("v" + i.ToString() == text)
                {
                    pen.Color = colorVector1;
                    gr.DrawLine(pen, 0, 0, (float)vectors[i - 1][0] * five, (float)vectors[i - 1][1] * five);
                }

                for (int j = 0; j < dataGridView.RowCount; j++)
                {
                    // e.g. if entry == v1 - v2  then subtract
                    if ("v" + i.ToString() + " - " + "v" + j.ToString() == text)
                    {
                        drawSubtractedVectors(gr, vectors, colorVector1, colorVector2, colorVector3, i, j, pen, combo);
                    }
                    // e.g. if entry == v1 + v2  then add 
                    else if ("v" + i.ToString() + " + " + "v" + j.ToString() == text)
                    {
                        drawAddedVectors(gr, vectors, colorVector1, colorVector2, colorVector3, i, j, pen, combo);
                    }
                    // e.g. if entry ==  v1 -> v2  then connect given vectors 
                    else if ("v" + i.ToString() + " -> " + "v" + j.ToString() == text)
                    {
                        pen.Color = colorVector3;
                        gr.DrawLine(pen, (float)vectors[i - 1][0] * five, (float)vectors[i - 1][1] * five, (float)vectors[j - 1][0] * five, (float)vectors[j - 1][1] * five);
                    }
                    else if ("Vector " + i.ToString() + "Vector " + j.ToString() == text)
                    {
                        drawAddedVectors(gr, vectors, colorVector1, colorVector2, colorVector3, i, j, pen, combo);
                    }
                }
            }
        }
              //  public static void CreateGraph(Vector[] vectors, PictureBox pictureBox, RadioButton radioButtonVectorToOrigin, RadioButton radioButtonAddition, RadioButton radioButtonSubtraktion)
      //  {
      //      float x1Max = 1;
      //      float x2Max = 1;
      //      float x1Min = -1;
      //      float x2Min = -1;
      //      
      //      PointF[] pointFs = new PointF[vectors.Length + 1];
      //      if (vectors.Length != 0)
      //      {
      //          Vector startingVector = new Vector(vectors[0].length);
      //          pointFs[0] = new Point(0, 0);
      //          if (radioButtonAddition.Checked)
      //          {
      //              for (int i = 0; i < vectors.Length; i++)
      //              {
      //                  pointFs[i+1] = new PointF(startingVector[0] + vectors[i][0], startingVector[1] + vectors[i][1]);
      //                  startingVector[0] = startingVector[0] + vectors[i][0];
      //                  startingVector[1] = startingVector[1] + vectors[i][1];
      //                  MinMaxCoordinates(ref x1Min, ref x1Max, ref x2Min, ref x2Max, startingVector);
      //              }
      //          }
      //          else if (radioButtonSubtraktion.Checked)
      //          {
      //              pointFs[1] = new PointF(startingVector[0] + vectors[0][0], startingVector[1] + vectors[0][1]);
      //              startingVector[0] = startingVector[0] + vectors[0][0];
      //              startingVector[1] = startingVector[1] + vectors[0][1];
      //              MinMaxCoordinates(ref x1Min, ref x1Max, ref x2Min, ref x2Max, startingVector);
      //              for (int i = 1; i < vectors.Length; i++)
      //              {
      //                  pointFs[i+1] = new PointF(startingVector[0] - vectors[i][0], startingVector[1] - vectors[i][1]);
      //                  startingVector[0] = startingVector[0] - vectors[i][0];
      //                  startingVector[1] = startingVector[1] - vectors[i][1];
      //                  MinMaxCoordinates(ref x1Min, ref x1Max, ref x2Min, ref x2Max, startingVector);
      //              }
      //          }
      //          if (!(radioButtonAddition.Checked || radioButtonSubtraktion.Checked))
      //              for (int i = 0; i < vectors.Length; i++)
      //              {
      //                  if (vectors[i][0] > x1Max)
      //                  {
      //                      x1Max = vectors[i][0];
      //                  }
      //                  else if (vectors[i][0] < x1Min)
      //                  {
      //                      x1Min = vectors[i][0];
      //                  }
      //                  if (vectors[i][1] > x2Max)
      //                  {
      //                      x2Max = vectors[i][1];
      //                  }
      //                  else if (vectors[i][1] < x2Min)
      //                  {
      //                      x2Min = vectors[i][1];
      //                  }
      //              }
      //      }
      //
      //
      //      int width = pictureBox.ClientSize.Width;
      //      int heigth = pictureBox.ClientSize.Height;
      //      if (heigth == 0 || width == 0) return;
      //      bm = new Bitmap(width, heigth);
      //      Graphics graphic = Graphics.FromImage(bm);
      //
      //      //Font myFont = new Font("Calibri", 10);
      //      //graphic.RotateTransform(180);
      //      //graphic.ScaleTransform(-1, 1);
      //      //graphic.DrawString("x2", myFont, Brushes.Black, new Point(-14, (int)x2Min)); 
      //      //graphic.DrawString("x1", myFont, Brushes.Black, new Point((int)x1Max - (int)x1Max / 10, (int)x1Max / 12));
      //      //graphic.DrawString(((int)(x1Max / 5)).ToString(), myFont, Brushes.Black, new Point(((int)x1Max * 1 / 5 - (int)x1Max * 1 / 50), (int)x1Max / 15));
      //      //graphic.DrawString(((int)(x1Max * 2 / 5)).ToString(), myFont, Brushes.Black, new Point(((int)x1Max * 2 / 5 - (int)x1Max * 1 / 50), (int)x1Max / 15));
      //      //graphic.DrawString(((int)(x1Max * 3 / 5)).ToString(), myFont, Brushes.Black, new Point(((int)x1Max * 3 / 5 - (int)x1Max * 1 / 50), (int)x1Max / 15));
      //      //graphic.DrawString(((int)(x1Max * 4 / 5)).ToString(), myFont, Brushes.Black, new Point(((int)x1Max * 4 / 5 - (int)x1Max * 1 / 50), (int)x1Max / 15));
      //
      //
      //      // initialise a bitmap
      //     // int width = pictureBox.ClientSize.Width;
      //     // int heigth = pictureBox.ClientSize.Height;
      //     // if (heigth == 0 || width == 0) return;
      //     // bm = new Bitmap(width, heigth);
      //      //Graphics graphic = Graphics.FromImage(bm);
      //
      //      // transform to "normal" cartesian plane
      //      float dx = width * (1 - (x1Max / (x1Max - x1Min)));
      //      float dy = heigth / ((x2Max - x2Min) / x2Max);
      //      graphic.TranslateTransform(dx, dy);
      //
      //      float sx = width / (x1Max - x1Min);
      //      float sy = -heigth / (x2Max - x2Min);
      //      graphic.ScaleTransform(sx, sy);
      //
      //      drawCoordinateSystem(graphic, x1Min, x1Max, x2Min, x2Max);
      //      Pen pen = new Pen(Color.Black, 0);
      //      DrawVectors(graphic, vectors, pointFs, radioButtonVectorToOrigin, radioButtonAddition, radioButtonSubtraktion);
      //
      //      
      //      pictureBox.Image = bm;
      //     
      //  }
       public static void drawCoordinateSystem(Graphics gr, float xmin, float xmax, float ymin, float ymax)
        {
            Pen pen = new Pen(Color.Gray, 0);

            gr.DrawLine(pen, xmin, 0, xmax, 0);
            gr.DrawLine(pen, 0, ymin, 0, ymax);

            // Create Axes
            pen.Color = Color.Black;
            gr.DrawLine(pen, xmin, 0, xmax, 0);                      // x-axe  
            gr.DrawLine(pen, (xmax * 1 / 5), 0, (xmax / 5), -5);    
            gr.DrawLine(pen, (xmax * 2 / 5), 0, (xmax * 2 / 5), -5);
            gr.DrawLine(pen, (xmax * 3 / 5), 0, (xmax * 3 / 5), -5);
            gr.DrawLine(pen, (xmax * 4 / 5), 0, (xmax * 4 / 5), -5);
            gr.DrawLine(pen, xmax, 0,xmax, -5);                     
            gr.DrawLine(pen, 0, ymin, 0, ymax);            // y-axe  
            gr.DrawLine(pen, 0, (xmax * 1 / 5), -5, (xmax * 1 / 5));  
            gr.DrawLine(pen, 0, (xmax * 2 / 5), -5, (xmax * 2 / 5)); 
            gr.DrawLine(pen, 0, (xmax * 3 / 5), -5, (xmax * 3 / 5)); 
            gr.DrawLine(pen, 0, (xmax * 4 / 5), -5, (xmax * 4 / 5)); 
            gr.DrawLine(pen, 0, (xmax *  5), -5, (xmax * 5)); 

            // axes head
            gr.DrawLine(pen, xmax, 0, xmax - xmax/50, ymax/50);
            gr.DrawLine(pen, xmax, 0, xmax - xmax/50, -ymax/50);
            gr.DrawLine(pen, 0,ymax, xmax/50, ymax - ymax/50);
            gr.DrawLine(pen, 0, ymax, -xmax/50, ymax -ymax/50);
            // scale the axes
            //Font myFont = new Font("Calibri", 1);
            //gr.RotateTransform(180);
            //gr.ScaleTransform(-1, 1);
            //gr.DrawString("x2", myFont, Brushes.Black, new Point(-14, (int)ymin)); 
            //gr.DrawString("x1", myFont, Brushes.Black, new Point((int)xmax- (int)xmax/10, (int)xmax / 12));
            //gr.DrawString(((int)(xmax/5)).ToString(), myFont, Brushes.Black, new Point(((int)xmax * 1 / 5 - (int)xmax * 1 / 50), (int)xmax/15));
            //gr.DrawString(((int)(xmax * 2 / 5)).ToString(), myFont, Brushes.Black, new Point(((int)xmax * 2 / 5 - (int)xmax * 1 / 50), (int)xmax / 15));
            //gr.DrawString(((int)(xmax * 3 / 5)).ToString(), myFont, Brushes.Black, new Point(((int)xmax * 3 / 5 - (int)xmax * 1 / 50), (int)xmax / 15));
            //gr.DrawString(((int)(xmax * 4 / 5)).ToString(), myFont, Brushes.Black, new Point(((int)xmax * 4 / 5 - (int)xmax * 1 / 50), (int)xmax / 15));
           //// gr.DrawString((xmax).ToString(), myFont, Brushes.Black, new Point(((int)xmax * 6 / 6 - (int)xmax * 1 / 50), 7));
            //gr.DrawString("30", myFont, Brushes.Black, new Point(-22, -157));
            //gr.DrawString("25", myFont, Brushes.Black, new Point(-22, -132));
            //gr.DrawString("20", myFont, Brushes.Black, new Point(-22, -107));
            //gr.DrawString("15", myFont, Brushes.Black, new Point(-22, -82));
            //gr.DrawString("10", myFont, Brushes.Black, new Point(-22, -57));
            //gr.DrawString(" 5", myFont, Brushes.Black, new Point(-22, -32));
            
        }
          // public static PointF[] Calculation(Vector[] vectors, PointF[] pointFs, RadioButton radioButtonVectorToOrigin, RadioButton radioButtonVectorAddition, RadioButton radioButtonVectorSubtraction,ref float min,ref float max)
       // {
       //     if (vectors.Length != 0)
       //     {
       //         
       //         Vector startingVector = new Vector(vectors[0].length);
       //         if (vectors[0][0] < vectors[0][1])
       //         {
       //             min = vectors[0][0];
       //             max = vectors[0][1];
       //         }
       //         else
       //         {
       //             min = vectors[0][1];
       //             max = vectors[0][0];
       //         }
       //         pointFs[0] = new Point(0, 0);
       //         if (radioButtonVectorAddition.Checked)
       //         {
       //             for (int i = 0; i < vectors.Length; i++)
       //             {
       //                 pointFs[i + 1] = new PointF(startingVector[0] + vectors[i][0], startingVector[1] + vectors[i][1]);
       //                 startingVector[0] = startingVector[0] + vectors[i][0];
       //                 startingVector[1] = startingVector[1] + vectors[i][1];
       //                 MinMaxCoordinates(ref min, ref max, startingVector);
       //             }
       //         }
       //         else if (radioButtonVectorSubtraction.Checked)
       //         {
       //             pointFs[1] = new PointF(startingVector[0] + vectors[0][0], startingVector[1] + vectors[0][1]);
       //             startingVector[0] = startingVector[0] + vectors[0][0];
       //             startingVector[1] = startingVector[1] + vectors[0][1];
       //             MinMaxCoordinates(ref min, ref max, startingVector);
       //             for (int i = 1; i < vectors.Length; i++)
       //             {
       //                 pointFs[i + 1] = new PointF(startingVector[0] - vectors[i][0], startingVector[1] - vectors[i][1]);
       //                 startingVector[0] = startingVector[0] - vectors[i][0];
       //                 startingVector[1] = startingVector[1] - vectors[i][1];
       //                 MinMaxCoordinates(ref min, ref max, startingVector);
       //             }
       //         }
       //         if (!(radioButtonVectorAddition.Checked || radioButtonVectorSubtraction.Checked))
       //         {
       //             for (int i = 0; i < vectors.Length; i++)
       //             {
       //                 if (vectors[i][0] > max)
       //                 {
       //                     max = vectors[i][0];
       //                 }
       //                 else if (vectors[i][0] < min)
       //                 {
       //                     min = vectors[i][0];
       //                 }
       //                 if (vectors[i][1] > max)
       //                 {
       //                     max = vectors[i][1];
       //                 }
       //                 else if (vectors[i][1] < min)
       //                 {
       //                     min = vectors[i][1];
       //                 }
       //             }
       //         }
       //     }
       //     return pointFs;
       // }d
       if (width >= height)
            {
                //// horizontal labeling 
                //gr.DrawString((Math.Round((max - min) * 1 / 10 + min, 2)).ToString(), myFont, Brushes.Black, height - height * 9 / 10, height - height * 47 / 1000);
                //gr.DrawString((Math.Round((max - min) * 2 / 10 + min, 2)).ToString(), myFont, Brushes.Black, height - height * 8 / 10, height - height * 47 / 1000);
                //gr.DrawString((Math.Round((max - min) * 3 / 10 + min, 2)).ToString(), myFont, Brushes.Black, height - height * 7 / 10, height - height * 47 / 1000);
                //gr.DrawString((Math.Round((max - min) * 4 / 10 + min, 2)).ToString(), myFont, Brushes.Black, height - height * 6 / 10, height - height * 47 / 1000);
                //gr.DrawString((Math.Round((max - min) * 5 / 10 + min, 2)).ToString(), myFont, Brushes.Black, height - height * 5 / 10, height - height * 47 / 1000);
                //gr.DrawString((Math.Round((max - min) * 6 / 10 + min, 2)).ToString(), myFont, Brushes.Black, height - height * 4 / 10, height - height * 47 / 1000);
                //gr.DrawString((Math.Round((max - min) * 7 / 10 + min, 2)).ToString(), myFont, Brushes.Black, height - height * 3 / 10, height - height * 47 / 1000);
                //gr.DrawString((Math.Round((max - min) * 8 / 10 + min, 2)).ToString(), myFont, Brushes.Black, height - height * 2 / 10, height - height * 47 / 1000);
                //gr.DrawString((Math.Round((max - min) * 9 / 10 + min, 2)).ToString(), myFont, Brushes.Black, height - height * 1 / 10, height - height * 47 / 1000);
                //// vertical labeling             
                //gr.DrawString((Math.Round((max - min) * 1 / 10 + min, 2)).ToString(), myFont, Brushes.Black, height - height * 99 / 100, height * 9 / 10);
                //gr.DrawString((Math.Round((max - min) * 2 / 10 + min, 2)).ToString(), myFont, Brushes.Black, height - height * 99 / 100, height * 8 / 10);
                //gr.DrawString((Math.Round((max - min) * 3 / 10 + min, 2)).ToString(), myFont, Brushes.Black, height - height * 99 / 100, height * 7 / 10);
                //gr.DrawString((Math.Round((max - min) * 4 / 10 + min, 2)).ToString(), myFont, Brushes.Black, height - height * 99 / 100, height * 6 / 10);
                //gr.DrawString((Math.Round((max - min) * 5 / 10 + min, 2)).ToString(), myFont, Brushes.Black, height - height * 99 / 100, height * 5 / 10);
                //gr.DrawString((Math.Round((max - min) * 6 / 10 + min, 2)).ToString(), myFont, Brushes.Black, height - height * 99 / 100, height * 4 / 10);
                //gr.DrawString((Math.Round((max - min) * 7 / 10 + min, 2)).ToString(), myFont, Brushes.Black, height - height * 99 / 100, height * 3 / 10);
                //gr.DrawString((Math.Round((max - min) * 8 / 10 + min, 2)).ToString(), myFont, Brushes.Black, height - height * 99 / 100, height * 2 / 10);
                //gr.DrawString((Math.Round((max - min) * 9 / 10 + min, 2)).ToString(), myFont, Brushes.Black, height - height * 99 / 100, height * 1 / 10);
         }
         else
            {
                //// horizontal labeling 
                //gr.DrawString((Math.Round((max - min) * 1 / 10 + min, 2)).ToString(), myFont, Brushes.Black, width - width * 9 / 10, height - height * 47 / 1000);
                //gr.DrawString((Math.Round((max - min) * 2 / 10 + min, 2)).ToString(), myFont, Brushes.Black, width - width * 8 / 10, height - height * 47 / 1000);
                //gr.DrawString((Math.Round((max - min) * 3 / 10 + min, 2)).ToString(), myFont, Brushes.Black, width - width * 7 / 10, height - height * 47 / 1000);
                //gr.DrawString((Math.Round((max - min) * 4 / 10 + min, 2)).ToString(), myFont, Brushes.Black, width - width * 6 / 10, height - height * 47 / 1000);
                //gr.DrawString((Math.Round((max - min) * 5 / 10 + min, 2)).ToString(), myFont, Brushes.Black, width - width * 5 / 10, height - height * 47 / 1000);
                //gr.DrawString((Math.Round((max - min) * 6 / 10 + min, 2)).ToString(), myFont, Brushes.Black, width - width * 4 / 10, height - height * 47 / 1000);
                //gr.DrawString((Math.Round((max - min) * 7 / 10 + min, 2)).ToString(), myFont, Brushes.Black, width - width * 3 / 10, height - height * 47 / 1000);
                //gr.DrawString((Math.Round((max - min) * 8 / 10 + min, 2)).ToString(), myFont, Brushes.Black, width - width * 2 / 10, height - height * 47 / 1000);
                //gr.DrawString((Math.Round((max - min) * 9 / 10 + min, 2)).ToString(), myFont, Brushes.Black, width - width * 1 / 10, height - height * 47 / 1000);
                //// vertical labeling             
                //gr.DrawString((Math.Round((max - min) * 1 / 10 + min, 2)).ToString(), myFont, Brushes.Black, width - width * 99 / 100, height - width * 1 / 10);
                //gr.DrawString((Math.Round((max - min) * 2 / 10 + min, 2)).ToString(), myFont, Brushes.Black, width - width * 99 / 100, height - width * 2 / 10);
                //gr.DrawString((Math.Round((max - min) * 3 / 10 + min, 2)).ToString(), myFont, Brushes.Black, width - width * 99 / 100, height - width * 3 / 10);
                //gr.DrawString((Math.Round((max - min) * 4 / 10 + min, 2)).ToString(), myFont, Brushes.Black, width - width * 99 / 100, height - width * 4 / 10);
                //gr.DrawString((Math.Round((max - min) * 5 / 10 + min, 2)).ToString(), myFont, Brushes.Black, width - width * 99 / 100, height - width * 5 / 10);
                //gr.DrawString((Math.Round((max - min) * 6 / 10 + min, 2)).ToString(), myFont, Brushes.Black, width - width * 99 / 100, height - width * 6 / 10);
                //gr.DrawString((Math.Round((max - min) * 7 / 10 + min, 2)).ToString(), myFont, Brushes.Black, width - width * 99 / 100, height - width * 7 / 10);
                //gr.DrawString((Math.Round((max - min) * 8 / 10 + min, 2)).ToString(), myFont, Brushes.Black, width - width * 99 / 100, height - width * 8 / 10);
                //gr.DrawString((Math.Round((max - min) * 9 / 10 + min, 2)).ToString(), myFont, Brushes.Black, width - width * 99 / 100, height - width * 9 / 10);
          }
           public static void CreateGraph2(Vector[] vectors, PointF[] pointFs, PictureBox pictureBox, float min, float max)
         {
           // Pen pen = new Pen(Color.Black, 10);
           // int width = pictureBox.ClientSize.Width;
           // int height = pictureBox.ClientSize.Height;
           // if (height == 0 || width == 0) return;
           // bm = new Bitmap(width, height);
           // Graphics gr = Graphics.FromImage(bm);
           // gr.SmoothingMode = SmoothingMode.AntiAlias;
           // gr.DrawLine(pen, 0, height, width, height);  // x axe                    
           // gr.DrawLine(pen, 0, 0, 0, height);  // y axe
           // LabelingAndTransform(gr, width, height, min, max);
           // pen.Width = 0;
           // pen.Color = Color.Red;
           // gr.DrawLines(pen, pointFs);
           // pictureBox.Image = bm;
        }
         // public static void LabelingAndTransform(Graphics gr, int width, int height, float min , float max)
        // {
        //Font myFont = new Font("Calibri", 10);
        //if (min >= 0 && max >= 0)
        //{
        //    if (width >= height)
        //    {
        //        int i = 1;
        //        while (i < 10)
        //        {
        //            // horizontal labeling 
        //            gr.DrawString((Math.Round((max - min) * i / 10 + min, 2)).ToString(), myFont, Brushes.Black, height - height * (10 - i) / 10 + height/10, height - height * 47 / 1000);
        //            // vertical labeling
        //            gr.DrawString((Math.Round((max - min) * i / 10 + min, 2)).ToString(), myFont, Brushes.Black, height - height * 99 / 100, height * (10 - i) / 10 - height/10);
        //            i++;
        //        }
        //        gr.TranslateTransform(0 + height / 10, height - height / 10);
        //    }
        //    else
        //    {
        //        int i = 1;
        //        while (i < 10)
        //        {
        //            // horizontal labeling 
        //           gr.DrawString((Math.Round((max - min) * i / 10 + min, 2)).ToString(), myFont, Brushes.Black, width - width * (10 - i) / 10, height - height * 47 / 1000);
        //            // vertical labeling  
        //            gr.DrawString((Math.Round((max - min) * i / 10 + min, 2)).ToString(), myFont, Brushes.Black, width - width * 99 / 100, height - width * i / 10 -width/10);
        //            i++;
        //        }
        //        gr.TranslateTransform(0+width/10, height-width/10);
        //    }
        //}
        //else if (min <= 0 && max <= 0)
        //{
        //    if (width >= height)
        //    {
        //        int i = 1;
        //        while (i < 10)
        //        {
        //            // horizontal labeling 
        //            gr.DrawString((Math.Round((max - min) * i / 10 + min, 2)).ToString(), myFont, Brushes.Black, width - height * (10 - i) / 10, height - height * 47 / 1000);
        //            // vertical labeling
        //            gr.DrawString((Math.Round((max - min) * i / 10 + min, 2)).ToString(), myFont, Brushes.Black, height - height * 99 / 100, height * (10 - i) / 10);
        //            i++;
        //
        //        }
        //    }
        //    else
        //    {
        //        int i = 1;
        //        while (i < 10)
        //        {
        //            // horizontal labeling 
        //            gr.DrawString((Math.Round((max - min) * i / 10 + min, 2)).ToString(), myFont, Brushes.Black, width - width * (10 - i) / 10, height - height * 47 / 1000);
        //            // vertical labeling  
        //            gr.DrawString((Math.Round((max - min) * i / 10 + min, 2)).ToString(), myFont, Brushes.Black, width - width * 99 / 100, width - width * i / 10);
        //            i++;
        //        }
        //    }
        //    gr.TranslateTransform(width, 0);
        //}
        //else
        //{   // not ready yet 
        //    if (width >= height)
        //    {
        //        int i = 1;
        //        while ( i < 10)
        //        {
        //            // horizontal labeling                                                                           
        //            gr.DrawString((Math.Round((max - min) * (10-i) / 10 + min, 2)).ToString(), myFont, Brushes.Black, (width / ((max - min) / max) - width * i / 10) + width - width / ((max - min) / max), height - height * 47 / 1000); //  width / ((max - min) / max) - height * (i) / 10 + height / ((max - min) / max)
        //            // vertical labeling
        //            gr.DrawString((Math.Round((max - min) * i / 10 + min, 2)).ToString(), myFont, Brushes.Black, height - height * 99 / 100, height * (10 - i) / 10);
        //            i++;
        //        }
        //    }
        //    else
        //    {
        //        int i = 1;
        //        while (i < 10)
        //        {
        //            // horizontal labeling 
        //            gr.DrawString((Math.Round((max - min) * i / 10 + min, 2)).ToString(), myFont, Brushes.Black, (width / ((max - min) / max) - width * (10-i) / 10) + width - width / ((max - min) / max), height - height * 47 / 1000); 
        //            // vertical labeling
        //            gr.DrawString((Math.Round((max - min) * i / 10 + min, 2)).ToString(), myFont, Brushes.Black, height - height * 99 / 100, (height / ((max - min) / max) - width * i / 10) + width - width / ((max - min) / max));
        //            i++;
        //        }
        //    }
        //    float dx = width - width / ((max - min) / max);
        //    float dy = height / ((max - min) / max);
        //    gr.TranslateTransform(dx, dy);
        //}
        //float sx;
        //float sy;
        //if (width < height)
        //{
        //    sx = width / (max - min);
        //    sy = -width / (max - min);
        //}
        //else
        //{
        //    sx = height / (max - min);
        //    sy = -height / (max - min);
        //}
        //gr.ScaleTransform(sx, sy);
        //}
        */
        #endregion
    }
}
