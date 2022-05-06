using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace AllTogether
{
    public partial class Form1 : Form
    {

        WorkWithOxyPlot oxyPlot = new WorkWithOxyPlot();

        public Form1()
        {
            InitializeComponent();

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            label1.Text = "\u21D2";                                                                           //Part of: Gauß
            comboBox1.Text = "+";                                                                             //Part of: Number Systems
            splitContainerVector.FixedPanel = FixedPanel.Panel1;                                              //Part of: Draw Function (selfmade)
            labelFunctionGraphRange.Text = "Enter the range in which \nthe graph should be drawn";            //Part of: Draw Function (selfmade)
            numericUpDownMinCoordinate.Increment = 0.1M;                                                      //Part of: Draw Function (selfmade)
            numericUpDownMaxCoordinate.Increment = 0.1M;                                                      //Part of: Draw Function (selfmade)
            comboBoxFunctionWithOxy.Text = "Polynomial of degree 0";                                          //Part of: Draw Function with OxyPlot
            numericUpDownFuncX0.Increment = 0.1M;                                                             //Part of: Draw Function with OxyPlot
            numericUpDownFuncX1.Increment = 0.1M;                                                             //Part of: Draw Function with OxyPlot
            numericUpDownFuncX2.Increment = 0.1M;                                                             //Part of: Draw Function with OxyPlot
            numericUpDownFuncX3.Increment = 0.1M;                                                             //Part of: Draw Function with OxyPlot
            numericUpDownFuncX4.Increment = 0.1M;                                                             //Part of: Draw Function with OxyPlot
            numericUpDownFuncX5.Increment = 0.1M;                                                             //Part of: Draw Function with OxyPlot
            labelRangeFuncOxyPlot.Text = "Enter the range in which \nthe graph should be drawn \n(min/max on xAxis)";    //Part of: Draw Function with OxyPlot
            numericUpDownRangeMin.Increment = 0.1M;                                                           //Part of: Draw Function with OxyPlot
            numericUpDownRangeMax.Increment = 0.1M;                                                           //Part of: Draw Function with OxyPlot
            WorkWithOxyPlot.MakePlotmodel(plotViewGraphTheory);
        }

        #region Matrix
        public void NumberAsInput(DataGridView dataGridView, DataGridViewCellValidatingEventArgs e)
        {
            if (!(e.FormattedValue.ToString() == "-" || e.FormattedValue.ToString() == "" || Double.TryParse(e.FormattedValue.ToString(), out double value)))
            {
                e.Cancel = true;
            }
        }
        private void ColumnRowNameForInvert(DataGridView dataGridView)
        {
            for (int i = 0; i < dataGridView.ColumnCount; i++)
            {
                dataGridView.Columns[i].Name = string.Empty;
            }
            for (int i = 0; i < dataGridView.RowCount - 1; i++)
            {
                dataGridView.Rows[i].HeaderCell.Value = string.Empty;
            }
        }
        private void ColumnRowNameLGSTableaus()
        {
            for (int i = 0; i < dataGridViewLGS.ColumnCount; i++)
            {
                dataGridViewLGS.Columns[i].Name = "Point " + (i + 1).ToString();
                dataGridViewEndLGS.Columns[i].Name = "final tableau column " + (i + 1).ToString();
                dataGridViewLGS.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            for (int i = 0; i < dataGridViewLGS.RowCount - 1; i++)
            {
                dataGridViewLGS.Rows[i].HeaderCell.Value = "x" + (i + 1).ToString();
                dataGridViewEndLGS.Rows[i].HeaderCell.Value = "x" + (i + 1).ToString();
            }
        }
        private void dataGridViewLGS_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            NumberAsInput(dataGridViewLGS, e);
        }
        private void dataGridViewLGS_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            Matrix.InsertColumns(dataGridViewLGS);
        }
        private void dataGridViewLGS_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < dataGridViewLGS.RowCount - 1)
            {
                dataGridViewLGS.Rows.RemoveAt(e.RowIndex);
            }
        }
        private void dataGridViewLGS_ColumnHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridViewMatrixC1.ColumnCount > 1)
            {
                dataGridViewLGS.Columns.RemoveAt(e.ColumnIndex);
            }
        }
        public bool RightInputTest()
        {
            bool inputCorrect = true;
            Matrix matrix = new Matrix(dataGridViewLGS.RowCount - 1, dataGridViewLGS.ColumnCount);
            dataGridViewEndLGS.ColumnCount = dataGridViewLGS.ColumnCount;
            if (dataGridViewLGS.RowCount < 2) return false;
            dataGridViewEndLGS.RowCount = dataGridViewLGS.RowCount - 1;
            if (dataGridViewEndLGS.ColumnCount - 1 != dataGridViewEndLGS.RowCount)
            {
                inputCorrect = false;
                MessageBox.Show("Since a point is tested, there must be one \npoint more than dimensions.");
            }
            ColumnRowNameLGSTableaus();
            return inputCorrect;
        }


        private void buttonInvert_Click(object sender, EventArgs e)
        {

            dataGridViewEndLGS.Refresh();
            Matrix.DeleteEmptyColumns(dataGridViewLGS);
            if (dataGridViewLGS.ColumnCount != dataGridViewLGS.RowCount - 1)
            {
                MessageBox.Show("not a square matrix.");
                return;
            }
            else
            {
                ColumnRowNameForInvert(dataGridViewLGS);
            }
            Matrix matrix1 = Matrix.DataGridViewToMatrix(dataGridViewLGS);
            if (matrix1.IsInvertible() == false)
            {
                MessageBox.Show("The row vectors/column vectors are linear dependent. \nThis matrix is therefore not invertible.");
                return;
            }
            matrix1.Invert();
            matrix1.RoundMatrix();

            dataGridViewEndLGS.ColumnCount = dataGridViewLGS.ColumnCount;
            dataGridViewEndLGS.RowCount = dataGridViewLGS.RowCount - 1;
            matrix1.MatrixToDataGridView(dataGridViewEndLGS);
            ColumnRowNameForInvert(dataGridViewEndLGS);
        }
        private void buttonRowEchelonForm_Click(object sender, EventArgs e)
        {
            Matrix.DeleteEmptyColumns(dataGridViewLGS);
            if (!RightInputTest())
            {
                return;
            }
            Matrix matrix = Matrix.DataGridViewToMatrix(dataGridViewLGS);
            matrix.Gauß();
            matrix.RoundMatrix();
            matrix.MatrixToDataGridView(dataGridViewEndLGS);
        }
        private void buttonNormalizedRowEchelonForm_Click(object sender, EventArgs e)
        {
            Matrix.DeleteEmptyColumns(dataGridViewLGS);
            if (!RightInputTest())
            {
                return;
            }
            Matrix matrix = Matrix.DataGridViewToMatrix(dataGridViewLGS);
            matrix.Gauß();
            matrix.ReduzierteDiagonalenform();
            matrix.RoundMatrix();
            matrix.MatrixToDataGridView(dataGridViewEndLGS);
        }
        private void buttonLineareCombination_Click(object sender, EventArgs e)
        {
            Matrix.DeleteEmptyColumns(dataGridViewLGS);
            if (!RightInputTest())
            {
                return;
            }
            Matrix matrix = Matrix.DataGridViewToMatrix(dataGridViewLGS);
            matrix.Gauß();
            matrix.ReduzierteDiagonalenform();
            if (matrix.LineareKombinationTest(matrix.rowCount, matrix.columnCount - 1) && matrix.LineareKombinationTest(matrix.columnCount - 1, matrix.rowCount))
            {
                textBoxTestErgebnis.Text = "The point can be displayed as a linear combination.";
            }
            else
            {
                textBoxTestErgebnis.Text = "The point can not be displayed as a linear combination.";
            }
            matrix.RoundMatrix();
            matrix.MatrixToDataGridView(dataGridViewEndLGS);
        }


        public void SizeOfMatrixAnswerWhileMultiplicate(DataGridView dataGridViewMatrixC1, DataGridView dataGridViewMatrixC2)
        {
            if (dataGridViewMatrixC1.RowCount > dataGridViewMatrixC2.RowCount)
            {
                dataGridViewMatrixCAnswer.RowCount = dataGridViewMatrixC1.RowCount;
            }
            else
            {
                dataGridViewMatrixCAnswer.RowCount = dataGridViewMatrixC2.RowCount;
            }
            if (dataGridViewMatrixC1.ColumnCount > dataGridViewMatrixC2.ColumnCount)
            {
                dataGridViewMatrixCAnswer.ColumnCount = dataGridViewMatrixC1.ColumnCount;
            }
            else
            {
                dataGridViewMatrixCAnswer.ColumnCount = dataGridViewMatrixC2.ColumnCount;
            }
        }
        private void dataGridViewMatrixC1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            Matrix.InsertColumns(dataGridViewMatrixC1);
        }
        private void dataGridViewMatrixC2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            Matrix.InsertColumns(dataGridViewMatrixC2);
        }
        private void dataGridViewMatrixC1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            NumberAsInput(dataGridViewMatrixC1, e);
        }
        private void dataGridViewMatrixC2_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            NumberAsInput(dataGridViewMatrixC2, e);
        }
        private void dataGridViewMatrixC1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < dataGridViewMatrixC1.RowCount - 1)
            {
                dataGridViewMatrixC1.Rows.RemoveAt(e.RowIndex);
            }
        }
        private void dataGridViewMatrixC1_ColumnHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridViewMatrixC1.ColumnCount > 1)
            {
                dataGridViewMatrixC1.Columns.RemoveAt(e.ColumnIndex);
            }
        }
        private void dataGridViewMatrixC2_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < dataGridViewMatrixC2.RowCount - 1)
            {
                dataGridViewMatrixC2.Rows.RemoveAt(e.RowIndex);
            }
        }
        private void dataGridViewMatrixC2_ColumnHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridViewMatrixC2.ColumnCount > 1)
            {
                dataGridViewMatrixC2.Columns.RemoveAt(e.ColumnIndex);
            }
        }
        private void buttonMatrixCalculate_Click(object sender, EventArgs e)
        {
            Matrix.DeleteEmptyColumns(dataGridViewMatrixC1);
            Matrix.DeleteEmptyColumns(dataGridViewMatrixC2);
            SizeOfMatrixAnswerWhileMultiplicate(dataGridViewMatrixC1, dataGridViewMatrixC2);
            Matrix matrix1 = Matrix.DataGridViewToMatrix(dataGridViewMatrixC1);
            Matrix matrix2 = Matrix.DataGridViewToMatrix(dataGridViewMatrixC2);
            Matrix matrix3 = SelectedOperatorAndCalculation(matrix1, matrix2);
            if (matrix3 != null)
            {
                dataGridViewEndLGS.ClearSelection();
                matrix3.MatrixToDataGridView(dataGridViewMatrixCAnswer);
            }
            else
            {
                MessageBox.Show("Calculation not possible. Please check the input.");
            }
        }
        public Matrix SelectedOperatorAndCalculation(Matrix matrix1, Matrix matrix2)
        {
            Matrix matrix3 = new Matrix(matrix1.rowCount, matrix2.columnCount);
            if (listBox1.SelectedItem.ToString() == " +")
            {
                matrix3 = matrix1 + matrix2;

            }
            else if (listBox1.SelectedItem.ToString() == " -")
            {
                matrix3 = matrix1 - matrix2;
            }
            else
            {
                matrix3 = matrix1 * matrix2;
            }
            return matrix3;
        }

        #endregion Matrix


        #region NumberSystems
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        private void buttonConvert_Click(object sender, EventArgs e)
        {
            string givenInput = textBoxEntry.Text;
            if (radioButtonDecToBin.Checked)
            {
                textBoxConvertSolution.Text = NumberSystems.ConvertDecToBin(givenInput);
            }
            else if (radioButtonBinToDec.Checked)
            {
                textBoxConvertSolution.Text = NumberSystems.ConvertBinToDec(givenInput);
            }
            else if (radioButtonDecToHex.Checked)
            {
                textBoxConvertSolution.Text = NumberSystems.ConvertDecToHex(givenInput);
            }
            else if (radioButtonHexToDec.Checked)
            {
                textBoxConvertSolution.Text = NumberSystems.ConvertHexToDec(givenInput);
            }
            else if (radioButtonBinToHex.Checked)
            {
                textBoxConvertSolution.Text = NumberSystems.ConvertBinToHex(givenInput);
            }
            else if (radioButtonHexToBin.Checked)
            {
                textBoxConvertSolution.Text = NumberSystems.ConvertHexToBin(givenInput);
            }
        }


        private void buttonCalculateNumbers_Click(object sender, EventArgs e)
        {
            string summand1 = textBoxSummand1.Text;
            string summand2 = textBoxSummand2.Text;
            string chosenOperator = comboBox1.Text;
            if (radioButtonDecimal.Checked)
            {
                textBoxSum.Text = NumberSystems.DecimalAdditionSubtraction(summand1, summand2, chosenOperator);

            }
            else if (radioButtonBinary.Checked)
            {
                textBoxSum.Text = NumberSystems.BinaryAdditionSubtraction(summand1, summand2, chosenOperator);
            }
            else if (radioButtonHexadecimal.Checked)
            {
                textBoxSum.Text = NumberSystems.HexadecimalAdditionSubtraction(summand1, summand2, chosenOperator);
            }
            else
            {
                MessageBox.Show("You need to decide what an input it is.");
            }
        }

        #endregion  NumberSystems NumberSystems;


        #region Vectors
        List<int> chosenVectors = new List<int>();

        public bool InRange(DataGridView dataGridView)
        {
            for (int i = 0; i < dataGridView.ColumnCount; i++)
            {
                for (int j = 0; j < dataGridView.RowCount; j++)
                {
                    try
                    {
                        Convert.ToSingle(dataGridView[i, j].Value);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Input out of float range.");
                        return false;
                    }
                }
            }
            return true;
        }

        private void dataGridViewScalarProduct_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            NumberAsInput(dataGridViewScalarProduct, e);
        }

        public void ColumnRowName(DataGridView dataGridView)
        {
            for (int i = 0; i < dataGridView.ColumnCount; i++)
            {
                dataGridView.Columns[i].Name = "x" + (i + 1).ToString();

            }

            for (int i = 0; i < dataGridView.RowCount - 1; i++)
            {
                dataGridView.Rows[i].HeaderCell.Value = "Vector " + (i + 1).ToString();
            }
            dataGridView.Refresh();
        }

        private void dataGridViewScalarProduct_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            Matrix.InsertColumns(dataGridViewScalarProduct);
            ColumnRowName(dataGridViewScalarProduct);
        }

        private void buttonScalarProduct_Click(object sender, EventArgs e)
        {
            if (InRange(dataGridViewScalarProduct))
            {
                textBoxScalarProduct.Text = Vector.PrintScalarProduct(dataGridViewScalarProduct, chosenVectors);
            }
        }

        private void buttonAngle_Click(object sender, EventArgs e)
        {
            if (InRange(dataGridViewScalarProduct))
            {
                textBoxAngle.Text = Vector.PrintAngle(dataGridViewScalarProduct, chosenVectors);
            }
        }

        private void buttonDrawVectors_Click(object sender, EventArgs e)
        {
            if (InRange(dataGridViewScalarProduct))
            {
                float min = float.MaxValue;
                float max = float.MinValue;
                Vector[] vectors = Vector.CreateVectorArray(dataGridViewScalarProduct, chosenVectors);
                PointF[] pointFs = new PointF[vectors.Length + 1];
                pointFs[0] = new PointF(0, 0);
                if (vectors.Length > 0)
                {
                    pointFs = WichRadioButtonIsChecked(pointFs, vectors, ref min, ref max);
                }
                if (min != float.MaxValue && max != float.MinValue)
                {
                    Vector.MakeGraph(pointFs, pictureBoxCartesianPlane, min, max);
                }
            }
        }

        private void pictureBoxCartesianPlane_Resize(object sender, EventArgs e)
        {
            buttonDrawVectors_Click(sender, e);
        }

        public PointF[] WichRadioButtonIsChecked(PointF[] pointFs, Vector[] vectors, ref float min, ref float max)
        {
            min = 0;
            max = 0;
            try
            {
                if (radioButtonVectorToOrigin.Checked)
                {
                    pointFs = Vector.VectorToOrigin(vectors, pointFs, ref min, ref max);
                }
                else if (radioButtonVectorAddition.Checked)
                {
                    pointFs = Vector.VectorAddition(vectors, pointFs, ref min, ref max);
                }
                else if (radioButtonVectorSubtraction.Checked)
                {
                    pointFs = Vector.VectorSubtraction(vectors, pointFs, ref min, ref max);
                }
                else
                {
                    if (vectors.Length > 1)
                    {
                        pointFs = Vector.VectorConnect(vectors, pointFs, ref min, ref max);
                    }
                    else
                    {
                        MessageBox.Show("At least two vectors needed.");
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Empty cells found.");
            }
            return pointFs;
        }

        private void dataGridViewScalarProduct_SelectionChanged(object sender, EventArgs e)
        {
            int index = 0;
            int[] nowSelected = new int[dataGridViewScalarProduct.RowCount];
            foreach (DataGridViewRow row in dataGridViewScalarProduct.SelectedRows)
            {
                nowSelected[row.Index] = row.Index + 1;
                index++;
            }
            for (int i = 0; i < dataGridViewScalarProduct.RowCount - 1; i++)
            {
                if (chosenVectors.Contains(i + 1) && !nowSelected.Contains(i + 1))
                {
                    chosenVectors.Remove(i + 1);
                }
            }
            for (int i = 0; i < nowSelected.Length; i++)
            {
                if ((!chosenVectors.Contains(nowSelected[i])) && nowSelected[i] != 0 && nowSelected[i] < dataGridViewScalarProduct.RowCount)
                {
                    chosenVectors.Add(nowSelected[i]);
                }
            }
            textBoxAngleAndScalarProduct.Text = "Vector " + String.Join(", Vector ", chosenVectors);
            FillEmptyCellsWithZero();
        }

        private void FillEmptyCellsWithZero()
        {
            if (dataGridViewScalarProduct.CurrentCell.Value == null && dataGridViewScalarProduct.CurrentCell.RowIndex != dataGridViewScalarProduct.RowCount - 1 && dataGridViewScalarProduct.CurrentCell.ColumnIndex != dataGridViewScalarProduct.ColumnCount - 1)
            {
                dataGridViewScalarProduct.CurrentCell.Value = "0";
            }
        }

        private void dataGridViewScalarProduct_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            dataGridViewScalarProduct.SelectionMode = DataGridViewSelectionMode.ColumnHeaderSelect;
            dataGridViewScalarProduct.Columns[e.ColumnIndex].Selected = true;
        }

        private void dataGridViewScalarProduct_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            dataGridViewScalarProduct.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;
            dataGridViewScalarProduct.Rows[e.RowIndex].Selected = true;
        }

        private void dataGridViewScalarProduct_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                for (int column = 0; column < dataGridViewScalarProduct.ColumnCount; column++)
                {
                    if (dataGridViewScalarProduct.Columns[column].Selected && dataGridViewScalarProduct.ColumnCount > 1)
                    {
                        dataGridViewScalarProduct.Columns.Remove(dataGridViewScalarProduct.Columns[column]);
                    }
                }
                ColumnRowName(dataGridViewScalarProduct);
            }
        }
        #endregion Vector


        #region Function
        private void buttonDrawFunction_Click(object sender, EventArgs e)
        {
            if (textBoxFunction.Text == string.Empty)
            {
                return;
            }
            if (InRange(dataGridViewScalarProduct))
            {
                try
                {
                    float min = Convert.ToSingle(numericUpDownMinCoordinate.Value.ToString());
                    float max = Convert.ToSingle(numericUpDownMaxCoordinate.Value.ToString());
                    Vector.MakeGraph(RemoveNaNEntries(GeneratePoints(max, min)), pictureBoxFunction, min, max);
                }
                catch (Exception ex)
                {
                    //MessageBox.Show("just numbers, operators, variable 'x' and brackets'()' are allowed");
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private PointF[] GeneratePoints(float max, float min)
        {
            // Loop over x values to generate points.
            float stepsize = 0.1f;
            PointF[] pointFs = new PointF[(int)Math.Ceiling((max - min) * 10) + 1];
            int count = 0;
            for (float x = min; x < max + stepsize; x += stepsize)
            {
                pointFs[count] = new PointF(x, GivenFunction((float)Math.Round(x, 1)));
                count++;
            }
            return pointFs;
        }

        private PointF[] RemoveNaNEntries(PointF[] pointFs)
        {
            List<PointF> pointFS = pointFs.ToList();
            for (int i = 0; i < pointFS.Count; i++)
            {
                if (pointFS[i].Y.ToString() == "NaN")
                {
                    pointFS.RemoveAt(i);
                    i--;
                }
            }
            return pointFs = pointFS.ToArray();
        }

        private float GivenFunction(float x)
        {
            string firstElementMinus = string.Empty;
            string expression = textBoxFunction.Text.ToString();
            if (expression.ElementAt(0) == '-' && expression.ElementAt(1) == 'x')
            {
                firstElementMinus = "-";
                expression = expression.Remove(0, 1);
            }
            string function = expression.Replace("x", x.ToString());
            float y = (float)StringToFunction.Eval(function, firstElementMinus);
            if (y >= float.MaxValue)
            {
                y = float.MaxValue;
            }
            return y;
        }

        private void pictureBoxFunction_Resize(object sender, EventArgs e)
        {
            buttonDrawFunction_Click(sender, e);
        }


        #endregion Function


        #region Function with OxyPlot
        private void comboBoxFunctionWithOxy_SelectedIndexChanged(object sender, EventArgs e)
        {
            numericUpDownFuncX0.Value = 0;
            numericUpDownFuncX1.Value = 0;
            numericUpDownFuncX2.Value = 0;
            numericUpDownFuncX3.Value = 0;
            numericUpDownFuncX4.Value = 0;
            numericUpDownFuncX5.Value = 0;

            if (comboBoxFunctionWithOxy.SelectedIndex.Equals(0))
            {
                labelFuncX1.Visible = false;
                numericUpDownFuncX1.Visible = false;
                labelFuncX2.Visible = false;
                numericUpDownFuncX2.Visible = false;
                labelFuncX3.Visible = false;
                numericUpDownFuncX3.Visible = false;
                labelFuncX4.Visible = false;
                numericUpDownFuncX4.Visible = false;
                labelFuncX5.Visible = false;
                numericUpDownFuncX5.Visible = false;
            }
            else if (comboBoxFunctionWithOxy.SelectedIndex.Equals(1))
            {
                labelFuncX1.Visible = true;
                numericUpDownFuncX1.Visible = true;
                labelFuncX2.Visible = false;
                numericUpDownFuncX2.Visible = false;
                labelFuncX3.Visible = false;
                numericUpDownFuncX3.Visible = false;
                labelFuncX4.Visible = false;
                numericUpDownFuncX4.Visible = false;
                labelFuncX5.Visible = false;
                numericUpDownFuncX5.Visible = false;
            }
            else if (comboBoxFunctionWithOxy.SelectedIndex.Equals(2))
            {
                labelFuncX1.Visible = true;
                numericUpDownFuncX1.Visible = true;
                labelFuncX2.Visible = true;
                numericUpDownFuncX2.Visible = true;
                labelFuncX3.Visible = false;
                numericUpDownFuncX3.Visible = false;
                labelFuncX4.Visible = false;
                numericUpDownFuncX4.Visible = false;
                labelFuncX5.Visible = false;
                numericUpDownFuncX5.Visible = false;
            }
            else if (comboBoxFunctionWithOxy.SelectedIndex.Equals(3))
            {
                labelFuncX1.Visible = true;
                numericUpDownFuncX1.Visible = true;
                labelFuncX2.Visible = true;
                numericUpDownFuncX2.Visible = true;
                labelFuncX3.Visible = true;
                numericUpDownFuncX3.Visible = true;
                labelFuncX4.Visible = false;
                numericUpDownFuncX4.Visible = false;
                labelFuncX5.Visible = false;
                numericUpDownFuncX5.Visible = false;
            }
            else if (comboBoxFunctionWithOxy.SelectedIndex.Equals(4))
            {
                labelFuncX1.Visible = true;
                numericUpDownFuncX1.Visible = true;
                labelFuncX2.Visible = true;
                numericUpDownFuncX2.Visible = true;
                labelFuncX3.Visible = true;
                numericUpDownFuncX3.Visible = true;
                labelFuncX4.Visible = true;
                numericUpDownFuncX4.Visible = true;
                labelFuncX5.Visible = false;
                numericUpDownFuncX5.Visible = false;
            }
            else if (comboBoxFunctionWithOxy.SelectedIndex.Equals(5))
            {
                labelFuncX1.Visible = true;
                numericUpDownFuncX1.Visible = true;
                labelFuncX2.Visible = true;
                numericUpDownFuncX2.Visible = true;
                labelFuncX3.Visible = true;
                numericUpDownFuncX3.Visible = true;
                labelFuncX4.Visible = true;
                numericUpDownFuncX4.Visible = true;
                labelFuncX5.Visible = true;
                numericUpDownFuncX5.Visible = true;
            }
        }

        private void buttonFuncWithOxy_Click(object sender, EventArgs e)
        {
            if (numericUpDownRangeMin.Value >= numericUpDownRangeMax.Value)
            {
                MessageBox.Show("Not a possible range");
                return;
            }
            FunctionSeries fs = GetFunction();
            float xMin = Convert.ToSingle(fs.Points.ElementAt(0).X);
            float yMin = Convert.ToSingle(fs.Points.ElementAt(0).Y);
            float xMax = Convert.ToSingle(fs.Points.ElementAt(0).X);
            float yMax = Convert.ToSingle(fs.Points.ElementAt(0).Y);
            FindMaxMinCoordinates(fs, ref xMin, ref xMax, ref yMin, ref yMax);
            PlotGraph(fs, plotViewFunction, xMin, xMax, yMin, yMax);
        }

        private void FindMaxMinCoordinates(FunctionSeries fs, ref float xMin, ref float xMax, ref float yMin, ref float yMax)
        {
            for (int i = 1; i < fs.Points.Count; i++)
            {
                if (Convert.ToSingle(fs.Points.ElementAt(i).X) > xMax)
                {
                    xMax = Convert.ToSingle(fs.Points.ElementAt(i).X);
                }
                if (Convert.ToSingle(fs.Points.ElementAt(i).Y) > yMax)
                {
                    yMax = Convert.ToSingle(fs.Points.ElementAt(i).Y);
                }
                if (Convert.ToSingle(fs.Points.ElementAt(i).X) < xMin)
                {
                    xMin = Convert.ToSingle(fs.Points.ElementAt(i).X);
                }
                if (Convert.ToSingle(fs.Points.ElementAt(i).Y) < yMin)
                {
                    yMin = Convert.ToSingle(fs.Points.ElementAt(i).Y);
                }
            }
            if (yMin == yMax)
            {
                yMax++;
                yMin--;
            }
            else if (Math.Abs(yMin) < yMax / 10)
            {
                yMin = -yMax / 10;
            }
        }

        private void PlotGraph(FunctionSeries fs, OxyPlot.WindowsForms.PlotView plotView, float xMin, float xMax, float yMin, float yMax, FunctionSeries fs2 = null)
        {
            LinearAxis Xaxis = new LinearAxis { Position = AxisPosition.Bottom, Minimum = xMin, Maximum = xMax };
            LinearAxis Yaxis = new LinearAxis { Position = AxisPosition.Left, Minimum = yMin, Maximum = yMax };
            Xaxis.IsZoomEnabled = false;
            Yaxis.IsZoomEnabled = false;
            PlotModel pm = new PlotModel();
            pm.Axes.Add(Xaxis);
            pm.Axes.Add(Yaxis);
            plotView.Model = pm;
            plotView.Model.Series.Add(fs);
            if (fs2 != null)
            {
                plotView.Model.Series.Add(fs2);
            }
        }

        private FunctionSeries GetFunction()
        {
            float n = (float)numericUpDownRangeMax.Value;
            FunctionSeries fs = new FunctionSeries();
            for (float x = (float)numericUpDownRangeMin.Value; x <= n; x++)
            {
                DataPoint data = new DataPoint(x, GetValue(x));
                fs.Points.Add(data);
                x -= 0.9f;
            }
            return fs;
        }

        private float GetValue(float x)
        {
            float result;
            result = (float)numericUpDownFuncX5.Value * (float)Math.Pow(x, 5) + (float)numericUpDownFuncX4.Value * (float)Math.Pow(x, 4) +
                (float)numericUpDownFuncX3.Value * (float)Math.Pow(x, 3) + (float)numericUpDownFuncX2.Value * (float)Math.Pow(x, 2) +
                (float)numericUpDownFuncX1.Value * x + (float)numericUpDownFuncX0.Value;
            return result;
        }


        #endregion Function with OxyPlot


        #region Bezier curves

        private void dataGridViewBezier_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            NumberAsInput(dataGridViewBezier, e);
        }
        /*!
         * method checked wich radiobutton is active. Depent on this the method change the visibility or not
         */
        private void radioButtonCubaticBezier_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonCubaticBezier.Checked)
            {
                numericUpDownBezierX4.Visible = true;
                numericUpDownBezierY4.Visible = true;
                labelBazierPoint4.Visible = true;
            }
            else
            {
                numericUpDownBezierX4.Visible = false;
                numericUpDownBezierY4.Visible = false;
                labelBazierPoint4.Visible = false;
            }
        }
        /**
         * user triggered this event by clicking on buttonDrawBezierCurve.
         * First two functionseries will be initialised. Depent on the checked radiobutton
         * will the first functionseries be filled with points wich form the curve.
         */
        private void buttonDrawBezierCurve_Click(object sender, EventArgs e)
        {
            FunctionSeries fs = new FunctionSeries();
            FunctionSeries fs2 = new FunctionSeries();
            if (radioButtonQuadraticBezier.Checked)
            {
                fs = QuadraticBezierFunction();
            }
            else
            {
                fs = CubaticBezierFunction();
            }
            fs2 = ControlPolygon();

            ///
            /// the following part will find out the minimum and maximum
            /// values of graphs for building a appropriate coordinate system.
            /// At last graphs and coodinate system will be drawen.
            /// 
            float xMin = Convert.ToSingle(fs.Points.ElementAt(0).X);
            float yMin = Convert.ToSingle(fs.Points.ElementAt(0).Y);
            float xMax = Convert.ToSingle(fs.Points.ElementAt(0).X);
            float yMax = Convert.ToSingle(fs.Points.ElementAt(0).Y);
            FindMaxMinCoordinates(fs, ref xMin, ref xMax, ref yMin, ref yMax);
            FindMaxMinCoordinates(fs2, ref xMin, ref xMax, ref yMin, ref yMax);
            PlotGraph(fs, plotViewBezier1, xMin, xMax, yMin, yMax, fs2);
        }

        private void buttonBezierDraw_Click(object sender, EventArgs e)
        {
            float[,] givenPoints = new float[dataGridViewBezier.RowCount - 1, dataGridViewBezier.ColumnCount];
            for (int i = 0; i < dataGridViewBezier.RowCount - 1; i++)
            {
                givenPoints[i, 0] = Convert.ToSingle(dataGridViewBezier[0, i].Value);
                givenPoints[i, 1] = Convert.ToSingle(dataGridViewBezier[1, i].Value);
            }
            FunctionSeries fs = new FunctionSeries();
            FunctionSeries fs2 = new FunctionSeries();
            fs = GeneralBezier(givenPoints);
            for (int i = 0; i < givenPoints.GetLength(0); i++)
            {
                DataPoint data = new DataPoint(givenPoints[i, 0], givenPoints[i, 1]);
                fs2.Points.Add(data);
            }
            float xMin = Convert.ToSingle(fs.Points.ElementAt(0).X);
            float yMin = Convert.ToSingle(fs.Points.ElementAt(0).Y);
            float xMax = Convert.ToSingle(fs.Points.ElementAt(0).X);
            float yMax = Convert.ToSingle(fs.Points.ElementAt(0).Y);
            FindMaxMinCoordinates(fs, ref xMin, ref xMax, ref yMin, ref yMax);
            FindMaxMinCoordinates(fs2, ref xMin, ref xMax, ref yMin, ref yMax);
            PlotGraph(fs, plotViewBezier2, xMin, xMax, yMin, yMax, fs2);
        }
        //////////////////////////////////////////////////////////////////// 
        /// Points entered by the users form a graph called ControlPolygon.
        ////////////////////////////////////////////////////////////////////
        private FunctionSeries ControlPolygon()
        {
            FunctionSeries fs2 = new FunctionSeries();
            DataPoint data1 = new DataPoint((float)numericUpDownBezierX1.Value, (float)numericUpDownBezierY1.Value);
            DataPoint data2 = new DataPoint((float)numericUpDownBezierX2.Value, (float)numericUpDownBezierY2.Value);
            DataPoint data3 = new DataPoint((float)numericUpDownBezierX3.Value, (float)numericUpDownBezierY3.Value);
            fs2.Points.Add(data1);
            fs2.Points.Add(data2);
            fs2.Points.Add(data3);
            if (radioButtonCubaticBezier.Checked)
            {
                DataPoint data4 = new DataPoint((float)numericUpDownBezierX4.Value, (float)numericUpDownBezierY4.Value);
                fs2.Points.Add(data4);
            }
            return fs2;
        }

        /*!
        * The QuadraticBezierFunction-method calculate points on graph wich is going to draw. For that, the formula for quadratic Bezier curves
        * was simplified and then used.
        * At last all points are added to the functionseries.
        */
        private FunctionSeries QuadraticBezierFunction()
        {
            FunctionSeries fs = new FunctionSeries();
            float x;
            float y;
            for (float t = 0; t <= 1; t += 0.01f)
            {
                x = ((float)numericUpDownBezierX1.Value - 2 * (float)numericUpDownBezierX2.Value + (float)numericUpDownBezierX3.Value) * (float)Math.Pow(t, 2) +
                    (-2 * (float)numericUpDownBezierX1.Value + 2 * (float)numericUpDownBezierX2.Value) * t + (float)numericUpDownBezierX1.Value;
                y = ((float)numericUpDownBezierY1.Value - 2 * (float)numericUpDownBezierY2.Value + (float)numericUpDownBezierY3.Value) * (float)Math.Pow(t, 2) +
                    (-2 * (float)numericUpDownBezierY1.Value + 2 * (float)numericUpDownBezierY2.Value) * t + (float)numericUpDownBezierY1.Value;
                DataPoint data = new DataPoint(x, y);
                fs.Points.Add(data);
            }
            return fs;
        }

        /**
        * The CubaticBezierFunction-method calculate points on graph wich is going to draw. For that, the formula for cubatic Bezier curves
        * was simplified and then used.
        * At last all points are added to the functionseries.
        */
        private FunctionSeries CubaticBezierFunction()
        {
            FunctionSeries fs = new FunctionSeries();
            float x;
            float y;
            for (float t = 0; t <= 1; t += 0.01f)
            {
                x = (-(float)numericUpDownBezierX1.Value + 3 * (float)numericUpDownBezierX2.Value - 3 * (float)numericUpDownBezierX3.Value + (float)numericUpDownBezierX4.Value) * (float)Math.Pow(t, 3) +
                    (3 * (float)numericUpDownBezierX1.Value - 6 * (float)numericUpDownBezierX2.Value + 3 * (float)numericUpDownBezierX3.Value) * (float)Math.Pow(t, 2) +
                    (-3 * (float)numericUpDownBezierX1.Value + 3 * (float)numericUpDownBezierX2.Value) * t + (float)numericUpDownBezierX1.Value;
                y = (-(float)numericUpDownBezierY1.Value + 3 * (float)numericUpDownBezierY2.Value - 3 * (float)numericUpDownBezierY3.Value + (float)numericUpDownBezierY4.Value) * (float)Math.Pow(t, 3) +
                    (3 * (float)numericUpDownBezierY1.Value - 6 * (float)numericUpDownBezierY2.Value + 3 * (float)numericUpDownBezierY3.Value) * (float)Math.Pow(t, 2) +
                    (-3 * (float)numericUpDownBezierY1.Value + 3 * (float)numericUpDownBezierY2.Value) * t + (float)numericUpDownBezierY1.Value;
                DataPoint data = new DataPoint(x, y);
                fs.Points.Add(data);
            }
            return fs;
        }

        private FunctionSeries GeneralBezier(float[,] givenPoints)
        {
            FunctionSeries fs = new FunctionSeries();
            float x = 0;
            float y = 0;
            int n = dataGridViewBezier.RowCount - 2;
            for (float t = 0; t <= 1; t += 0.01f)
            {
                t = (float)Math.Round(t, 2);
                for (int i = 0; i < n + 1; i++)
                {
                    x += (float)(BinomialCoefficient(n, i) * Math.Pow(t, i) * Math.Pow((1 - t), (n - i))) * givenPoints[i, 0];
                    y += (float)(BinomialCoefficient(n, i) * Math.Pow(t, i) * Math.Pow((1 - t), (n - i))) * givenPoints[i, 1];
                }
                DataPoint data = new DataPoint(x, y);
                fs.Points.Add(data);
                x = 0;
                y = 0;
            }
            return fs;
        }
        private int BinomialCoefficient(int n, int i)
        {
            int coefficient = 0;
            if (i == 0)
            {
                coefficient = 1;
            }
            else if (i == 1)
            {
                coefficient = n;
            }
            else
            {
                coefficient += Faculty(n) / (Faculty(i) * Faculty(n - i));
            }
            return coefficient;
        }

        public int Faculty(int number)
        {
            if (number == 1 || number == 0)
                return 1;
            else
                return number * Faculty(number - 1);
        }


        #endregion Bezier curves

        #region Sorting algorithm

        private void buttonBubbleSort_Click(object sender, EventArgs e)
        {
            DisableAndEnableSortButtons();
            double[] sortingArray = GenerateRandomArray();
            double[] sorted = SortingAlgorithm.BubbleSort(sortingArray, plotViewSortingAlgorithm);
            DisableAndEnableSortButtons();
        }

        private void buttonInsertionSort_Click(object sender, EventArgs e)
        {
            DisableAndEnableSortButtons();
            double[] sortingArray = GenerateRandomArray();
            double[] sorted = SortingAlgorithm.InsertionSort(sortingArray, plotViewSortingAlgorithm);
            DisableAndEnableSortButtons();
        }

        private void buttonSelectionSort_Click(object sender, EventArgs e)
        {
            DisableAndEnableSortButtons();
            double[] sortingArray = GenerateRandomArray();
            double[] sorted = SortingAlgorithm.SelectionSort(sortingArray, plotViewSortingAlgorithm);
            DisableAndEnableSortButtons();
        }

        private void buttonQuicksort_Click(object sender, EventArgs e)
        {
            DisableAndEnableSortButtons();
            double[] sortingArray = GenerateRandomArray();
            double[] sorted = SortingAlgorithm.QuickSortInPlace(sortingArray, 0, sortingArray.Length - 1, plotViewSortingAlgorithm);
            DisableAndEnableSortButtons();
        }

        private void buttonMergeSort_Click(object sender, EventArgs e)
        {
            DisableAndEnableSortButtons();
            double[] sortingArray = GenerateRandomArray();
            double[] sorted = SortingAlgorithm.MergeSort(sortingArray, plotViewSortingAlgorithm);
            DisableAndEnableSortButtons();
        }

        private void buttonMergeSortInPlace_Click(object sender, EventArgs e)
        {
            DisableAndEnableSortButtons();
            double[] sortingArray = GenerateRandomArray();
            SortingAlgorithm.MergeSortInPlace(sortingArray, 0, sortingArray.Length - 1, plotViewSortingAlgorithm);
            DisableAndEnableSortButtons();
        }

        private double[] GenerateRandomArray()
        {
            Random rnd = new Random();
            double[] sortingArray = new double[(int)numericUpDownNumberCount.Value];
            for (int i = 0; i < sortingArray.Length; i++)
            {
                sortingArray[i] = rnd.NextDouble() * ((double)numericUpDownMaxValue.Value - (double)numericUpDownMinValue.Value) + (double)numericUpDownMinValue.Value;

            }
            WorkWithOxyPlot.PlotColumnSeries(ref plotViewSortingAlgorithm, sortingArray);
            return sortingArray;
        }

        private void DisableAndEnableSortButtons()
        {
            if (buttonBubbleSort.Enabled == false || buttonInsertionSort.Enabled == false || buttonSelectionSort.Enabled == false
                || buttonQuicksort.Enabled == false || buttonMergeSort.Enabled == false || buttonMergeSortInPlace.Enabled == false)
            {
                buttonBubbleSort.Enabled = true;
                buttonInsertionSort.Enabled = true;
                buttonSelectionSort.Enabled = true;
                buttonQuicksort.Enabled = true;
                buttonMergeSort.Enabled = true;
                buttonMergeSortInPlace.Enabled = true;
            }
            else
            {
                buttonBubbleSort.Enabled = false;
                buttonInsertionSort.Enabled = false;
                buttonSelectionSort.Enabled = false;
                buttonQuicksort.Enabled = false;
                buttonMergeSort.Enabled = false;
                buttonMergeSortInPlace.Enabled = false;
            }
        }
        #endregion Sorting algotithm

        private void buttonGraphTheory_Click(object sender, EventArgs e)
        {
            // ToDo: first index have to be rowcount
            float[,] distances = new float[dataGridViewGraphTheoryDistances.ColumnCount,dataGridViewGraphTheoryDistances.ColumnCount];
                                                                 //actually rowCount
            for (int i = 0; i < dataGridViewGraphTheoryDistances.ColumnCount; i++)
            {
                for (int j = 0; j < dataGridViewGraphTheoryDistances.ColumnCount; j++)
                {
                    if (i == j)
                    {
                        distances[i, j] = 0;
                    }
                    else if(dataGridViewGraphTheoryDistances[j,i].Value == null) //.ToString() == string.Empty)
                    {
                        distances[i, j] = float.MaxValue;
                    }
                    else
                    {
                        distances[i, j] = Convert.ToSingle(dataGridViewGraphTheoryDistances[j, i].Value);
                    }
                }
            }

            float[,] result = new float[distances.GetLength(0), distances.GetLength(1)];
            List<Tuple<float, int, int>> sortedDistances = new List<Tuple<float, int, int>> { };
            switch (comboBoxGraphTheory.SelectedItem)
            {
                case "Boruvka":
                    sortedDistances = DistancesInSortedList(distances);
                    result = GraphTheory.Boruvka(distances, sortedDistances);
                    break;
                case "Kruskal":
                    sortedDistances = DistancesInSortedList(distances);
                    result = GraphTheory.Kruskal(sortedDistances ,distances.GetLength(0));
                    break;
                case "Dijkstra":
                    GraphTheory.Dijkstra(distances);
                    break;
                case "Floyd-Warshall":
                    result = GraphTheory.FloydWarshall(distances);
                    break;
                default:
                    MessageBox.Show("You have to choose an algorithm!");
                    break;
            }
            FillEndGrid(result);
        }

        private List<Tuple<float, int, int>> DistancesInSortedList(float[,] distances)
        {
            List<Tuple<float, int, int>> sortedDistances = new List<Tuple<float, int, int>> { };
            for (int i = 0; i < distances.GetLength(0); i++)
            {
                for (int j = i; j < distances.GetLength(1); j++)
                {
                    if (distances[i,j] != 0 && distances[i,j] != float.MaxValue)
                    {
                        sortedDistances.Add(Tuple.Create(distances[i, j], i, j));
                    }
                }
            }
            sortedDistances.Sort();
            return sortedDistances;
        }

        private void FillEndGrid(float[,] result)
        {
            dataGridViewGraphTheoryEndmatrix.RowCount = result.GetLength(0);
            dataGridViewGraphTheoryEndmatrix.ColumnCount = result.GetLength(1);
            for (int i = 0; i < result.GetLength(0); i++)
            {
                for (int j = 0; j < result.GetLength(1); j++)
                {
                    dataGridViewGraphTheoryEndmatrix[j, i].Value = result[i, j];
                }
            }
        }

        private void plotViewGraphTheory_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                FunctionSeries series = new FunctionSeries();
                DataPoint a = new DataPoint(e.X, e.Y);
                plotViewGraphTheory.Model.Series.Add(series);
            }

            //plotViewGraphTheory.Model.PlotArea plotArea = 
        }

        private void dataGridViewGraphTheoryPoints_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            NumberAsInput(dataGridViewGraphTheoryPoints,e);
        }


        private void dataGridViewGraphTheoryPoints_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            dataGridViewGraphTheoryDistances.Rows.Add();
            AdjustRowAndColumnHeaderValue();
            dataGridViewGraphTheoryDistances.Columns.Add(dataGridViewGraphTheoryDistances.Rows[dataGridViewGraphTheoryDistances.Rows.Count - 1].HeaderCell.Value.ToString(), dataGridViewGraphTheoryDistances.Rows[dataGridViewGraphTheoryDistances.Rows.Count - 1].HeaderCell.Value.ToString());
        }

        private void AdjustRowAndColumnHeaderValue()
        {
            List<string> alphabet = new List<string> { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N" };
            for (int i = 0; i < dataGridViewGraphTheoryPoints.RowCount; i++)
            {
                dataGridViewGraphTheoryDistances.Rows[i].HeaderCell.Value = alphabet[i];
                dataGridViewGraphTheoryPoints.Rows[i].HeaderCell.Value = alphabet[i];
            }
            for (int i = 0; i < dataGridViewGraphTheoryDistances.ColumnCount; i++)
            {
                dataGridViewGraphTheoryDistances.Columns[i].HeaderText = alphabet[i];
            }
        }

        private void dataGridViewGraphTheoryDistances_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            NumberAsInput(dataGridViewGraphTheoryDistances, e);
        }

        private void dataGridViewGraphTheoryPoints_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            dataGridViewGraphTheoryDistances.Rows.RemoveAt(0);
            dataGridViewGraphTheoryDistances.Columns.RemoveAt(0);
            AdjustRowAndColumnHeaderValue();
        }
    }
}
