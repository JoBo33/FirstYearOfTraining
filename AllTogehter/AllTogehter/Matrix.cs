using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AllTogether
{
    public class Matrix
    {
        double[,] matrix;
        public int rowCount;
        public int columnCount;

        public Matrix(int rowCount, int columnCount)
        {
            matrix = new double[rowCount, columnCount];
            this.rowCount = rowCount;
            this.columnCount = columnCount;
        }
        public double this[int rows, int columns]
        {
            get { return matrix[rows, columns]; }
            set { matrix[rows, columns] = value; }
        }


        public static Matrix operator +(Matrix a, Matrix b)
        {
            Matrix c = new Matrix(a.rowCount, a.columnCount);
            if (AddAndSubPossible(a, b))
            {
                for (int row = 0; row < a.rowCount; row++)
                {
                    for (int column = 0; column < a.columnCount; column++)
                    {
                        c[row, column] = a[row, column] + b[row, column];
                    }
                }
                return c;
            }
            return null;
        }
        public static Matrix operator -(Matrix a, Matrix b)
        {
            Matrix c = new Matrix(a.rowCount, a.columnCount);
            if (AddAndSubPossible(a, b))
            {
                for (int row = 0; row < a.rowCount; row++)
                {
                    for (int column = 0; column < a.columnCount; column++)
                    {
                        c[row, column] = a[row, column] - b[row, column];
                    }
                }
                return c;
            }
            return null;
        }
        public static Matrix operator *(Matrix a, Matrix b)
        {
            Matrix c = new Matrix(a.rowCount, b.columnCount);
            if (a.columnCount == b.rowCount)
            {
                for (int row = 0; row < a.rowCount; row++)
                {
                    for (int column = 0; column < b.columnCount; column++)
                    {
                        for (int rowB = 0; rowB < b.rowCount; rowB++)
                        {
                            c[row, column] += a[row, rowB] * b[rowB, column];
                        }
                    }
                }
                return c;
            }
            return null;
        }
        public static bool AddAndSubPossible(Matrix a, Matrix b)
        {
            bool possible = false;
            if (a.rowCount == b.rowCount && a.columnCount == b.columnCount)
            {
                possible = true;
            }
            return possible;
        }


        public void Gauß()  
        {
            int iterations = columnCount; 

            for (int k = 0; k < iterations-1; k++)
            {
                
                if (this[k, k] == 0)
                {
                    int countPossibleChanges = 0;
                    for (int t = k; t < rowCount; t++)
                    {
                        if (this[t, k] != 0)
                        {
                            ChangeRows(k, t);
                            countPossibleChanges += 1;
                        }
                    }
                    if (countPossibleChanges == 0)
                    {
                        return;
                    }
                }
                for (int i = rowCount - 1; i >= k; i--)
                {
                    double pivot = this[i, k];
                    if (pivot != 0)
                    {
                        for (int j = columnCount - 1; j >= 0; j--)
                        {
                            this[i, j] = this[i, j] / pivot;
                        }
                    }
                }
                
                for (int i = rowCount - 1; i > k; i--)
                {
                    if (this[i, k] != 0)
                    {
                        for (int j = columnCount - 1; j >= 0; j--)
                        {
                            this[i, j] = Math.Round(this[i, j], 8-k*4) - Math.Round(this[k, j], 8-k*4);
                        }
                    }
                }
                
            }

        }
        public void ReduzierteDiagonalenform()   
        {
            int iterations = columnCount;

            for (int k = iterations - 2; k >= 0; k--)
            {
                if (this[k, k] == 0)
                {
                    int countPossibleChanges = 0;
                    for (int t = k; t < rowCount; t++)
                    {
                        if (this[t, k] != 0)
                        {
                            ChangeRows(k, t);
                            countPossibleChanges++;
                        }
                    }
                    if (countPossibleChanges == 0)
                    {
                        return;
                    }
                }
                for (int i = 0; i <= k; i++)
                {
                    double pivot = matrix[i, k];
                    if (pivot != 0)
                    {
                        for (int j = 0; j < columnCount; j++)
                        {
                            matrix[i, j] = matrix[i, j] / pivot;
                        }
                    }
                }
                for (int i = 0; i < k; i++)
                {
                    if (this[i, k] != 0)
                    {
                        for (int j = 0; j < columnCount; j++)
                        {
                            matrix[i, j] = matrix[i, j] - matrix[k, j];
                        }
                    }
                }
            }
        }
        public bool LineareKombinationTest(int rows, int columns)
        {
            
            int count = 0;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (double.IsNaN(matrix[i, j]))
                    {
                        return false;
                    }
                    else if (matrix[i,j]==0)
                    {
                        count++;
                    }
                }
                if (count == columns)
                {

                    return false;
                }
                count = 0;
            }
            return true;
        }


        public bool IsInvertible()
        {

            if (!(NotOnlyZerosTest(columnCount, rowCount) && NotOnlyZerosTest(rowCount, columnCount)))
            {
                return false;
            }
           
            if(ColumnRowLinearDependentTest(columnCount, rowCount) || ColumnRowLinearDependentTest(rowCount, columnCount))
            {
                return false;
            }
            return true;
        }
        private bool ColumnRowLinearDependentTest(int column, int row)
        {
            for (int iteration = 0; iteration < rowCount; iteration++)
            {
                for (column = iteration + 1; column < columnCount; column++)
                {
                    int count = 1;
                    double factor = matrix[0, iteration] / matrix[0, column];
                    for (row = 1; row < columnCount; row++)
                    {
                        if (factor == matrix[row, iteration] / matrix[row, column])
                        {
                            count++;
                        }
                        else
                        {
                            break;
                        }
                        if (count == columnCount)
                        {

                            return true;
                        }
                    }
                }
            }
            return false;
        }
        public bool NotOnlyZerosTest(int rows, int columns)
        {
            int count = 0;
            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < columns; column++)
                {
                    if (matrix[row, column] == 0)
                    {
                        count++;
                    }
                    else
                    {
                        count = 0;
                        break;
                    }
                }
                if (count == columns)
                { return false; }
            }
            return true;
        }


        public static Matrix UnitMatrix(Matrix matrix1)
        {
            Matrix matrix = new Matrix(matrix1.rowCount, matrix1.columnCount);
            for (int row = 0; row < matrix.rowCount; row++)
            {
                matrix[row, row] = 1;
            }
            return matrix;
        }
        public void Invert()
        {
            Matrix einheitsmatrix = Matrix.UnitMatrix(this);

            InvertFirstHalf(this, einheitsmatrix);
            InvertSecondHalf(this, einheitsmatrix);

            for (int i = 0; i < einheitsmatrix.rowCount; i++)
            {
                for (int j = 0; j < einheitsmatrix.columnCount; j++)
                {
                    matrix[i, j] = einheitsmatrix[i, j];
                }
            }
        }
        private void InvertFirstHalf(Matrix matrix1, Matrix matrix2)
        {
            int iterations = matrix1.columnCount; 

            for (int k = 0; k < iterations; k++)
            {
                if (matrix1[k, k] == 0)
                {
                    for (int t = 0; t < matrix1.rowCount; t++)
                    {
                        if (matrix1[t, k] != 0)
                        {
                            matrix1.ChangeRows(k, t);
                            matrix2.ChangeRows(k, t);
                        }
                    }
                }
                for (int i = matrix1.rowCount - 1; i >= k; i--)
                {
                    double pivot = matrix1[i, k];
                    if (pivot != 0)
                    {
                        for (int j = matrix1.columnCount - 1; j >= 0; j--)
                        {
                            matrix2[i, j] = matrix2[i, j] / pivot;
                            matrix1[i, j] = matrix1[i, j] / pivot;
                        }
                    }
                }
                for (int i = matrix1.rowCount - 1; i > k; i--)
                {
                    if (matrix1[i, k] != 0)
                    {
                        for (int j = matrix1.columnCount - 1; j >= 0; j--)
                        {
                            matrix1[i, j] = matrix1[i, j] - matrix1[k, j];
                            matrix2[i, j] = matrix2[i, j] - matrix2[k, j];
                        }
                    }
                }
            }

        }
        private void InvertSecondHalf(Matrix matrix1, Matrix matrix2)
        {
            int iterations = matrix1.columnCount;

            for (int k = iterations - 1; k >= 0; k--)
            {
                if (matrix1[k, k] == 0)
                {
                    for (int t = 0; t < matrix1.rowCount; t++)
                    {
                        if (matrix1[t, k] != 0)
                        {
                            matrix1.ChangeRows(k, t);
                            matrix2.ChangeRows(k, t);
                        }
                    }
                }
                for (int i = 0; i <= k; i++)
                {
                    double pivot = matrix1[i, k];
                    if (pivot != 0)
                    {
                        for (int j = 0; j <= matrix1.columnCount - 1; j++)
                        {
                            matrix1[i, j] = matrix1[i, j] / pivot;
                            matrix2[i, j] = matrix2[i, j] / pivot;
                        }
                    }
                }
                for (int i = 0; i < k; i++)
                {
                    if (matrix1[i, k] != 0)
                    {
                        for (int j = 0; j < matrix1.columnCount; j++)
                        {
                            matrix1[i, j] = matrix1[i, j] - matrix1[k, j];
                            matrix2[i, j] = matrix2[i, j] - matrix2[k, j];
                        }
                    }
                }
            }
        }


        public void ChangeRows(int rowA, int rowB)
        {
            if (rowA == rowB) return;
            if (rowB < this.rowCount && rowA < this.rowCount)
            {
                for (int i = 0; i < this.columnCount; i++)
                {
                    double temp = matrix[rowA, i];
                    matrix[rowA, i] = matrix[rowB, i];
                    matrix[rowB, i] = temp;
                }
            }
        }
        public void RoundMatrix()
        {
            for (int row = 0; row < rowCount; row++)
            {
                for (int column = 0; column < columnCount; column++)
                {
                    matrix[row, column] = Math.Round(matrix[row, column], 3);
                }
            }

        }
        public static void InsertColumns(DataGridView dataGridView)
        {
            for (int i = 0; i < dataGridView.RowCount; i++)
            {
                if (dataGridView.Rows[i].Cells[dataGridView.ColumnCount - 1].Value != null)
                {
                    DataGridViewColumn column = new DataGridViewColumn();
                    DataGridViewCell cell = new DataGridViewTextBoxCell(); // Specify which type of cell in this column
                    column.CellTemplate = cell;
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                    dataGridView.Columns.Add(column);
                }
            }
        }
        public static void DeleteEmptyColumns(DataGridView dataGridView)
        {
            int count = 0;
            for (int i = dataGridView.ColumnCount - 1; i > 0; i--)
            {
                count = 0;
                for (int j = 0; j < dataGridView.RowCount; j++)
                {
                    if (dataGridView.Rows[j].Cells[i].Value == null)
                    {
                        count++;
                    }
                    else
                    {
                        break;
                    }
                }
                if (count == dataGridView.RowCount)
                {
                    dataGridView.Columns.RemoveAt(i);
                }
            }
        }
        public void MatrixToDataGridView(DataGridView dataGridView)
        {
            for (int row = 0; row < rowCount; row++)
            {
                for (int column = 0; column < columnCount; column++)
                {
                    dataGridView[column, row].Value = matrix[row, column];
                }
            }
        }
        public static Matrix DataGridViewToMatrix(DataGridView dataGridView)
        {
            Matrix matrix = new Matrix(dataGridView.RowCount - 1, dataGridView.ColumnCount);
            for (int rows = 0; rows < dataGridView.RowCount - 1; rows++)
            {
                for (int columns = 0; columns < dataGridView.ColumnCount; columns++)
                {
                    matrix[rows, columns] = Convert.ToDouble(dataGridView[columns, rows].Value);
                }
            }
            return matrix;
        }
        
    }
}
