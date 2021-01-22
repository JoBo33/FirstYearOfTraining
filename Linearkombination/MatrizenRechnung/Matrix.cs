using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MatrizenRechnung
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
                        c[row,column] = a[row, column] + b[row, column];
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

        public void Gauß(Matrix matrix)  // not testet // enthält nich alle fälle
        {
            int iterations = matrix.columnCount;

            for (int k = 0; k < iterations - 1; k++)
            {
                for (int i = matrix.rowCount - 1; i >= k; i--)
                {
                    if (matrix[i, k] != 0)
                    {
                        for (int j = matrix.columnCount - 1; j >= k; j--)
                        {
                            matrix[i, j] = matrix[i, j] / matrix[i, k];
                        }
                    }
                }
                for (int i = matrix.rowCount - 1; i > k; i--)
                {
                    for (int j = matrix.columnCount - 1; j >= k; j--)
                    {
                        matrix[i, j] = matrix[i, j] - matrix[k, j];
                    }
                }
            }
        }
        public void ReduzierteDiagonalenform(Matrix matrix)   // not testet // enthält nich alle fälle
        {
            int iterations = matrix.columnCount;

            for (int k = iterations - 2; k >= 0; k--)
            {
                for (int i = 0; i <= k; i++)
                {
                    double pivot = matrix[i, k];
                    if (pivot != 0)
                    {
                        for (int j = 0; j < matrix.columnCount; j++)
                        {
                            matrix[i, j] = matrix[i, j] / pivot;
                        }
                    }
                }
                for (int i = 0; i < k; i++)
                {
                    for (int j = 0; j < matrix.columnCount; j++)
                    {
                        matrix[i, j] = matrix[i, j] - matrix[k, j];
                    }
                }
            }
        }

        public bool IsInvertible()
        {
            
            if (!NotOnlyZerosTest())
            {
                return false;
            }
            int count = 0;
            for (int iteration = 0; iteration < rowCount; iteration++)
            {
                for (int row = iteration + 1; row < rowCount; row++)
                {
                    count = 1;
                    double factor = matrix[iteration, 0] / matrix[row, 0];
                    for (int column = 1; column < columnCount; column++)
                    {
                        if (factor == matrix[iteration, column] / matrix[row, column])
                        {
                            count++;
                        }
                        else
                        {
                            break;
                        }
                        if (count == columnCount)
                        {
                            
                            return false;
                        }
                    }
                }
            }
            for (int iteration = 0; iteration < rowCount; iteration++)
            {
                for (int column = iteration + 1; column < columnCount; column++)
                {
                    count = 1;
                    double factor = matrix[0, iteration] / matrix[0, column];
                    for (int row = 1; row < columnCount; row++)
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
                            
                            return false;
                        }
                    }
                }
            }
            return true;
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

        public static Matrix Einheitsmatrix(Matrix matrix1)
        {
            Matrix matrix = new Matrix(matrix1.rowCount, matrix1.columnCount);
            for (int row = 0; row < matrix.rowCount; row++)
            {
                matrix[row, row] = 1;
            }
            return matrix;
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


        public void Invert()
        {
            Matrix einheitsmatrix = Matrix.Einheitsmatrix(this);

            //InvertFirstHalf
            InvertFirstHalf(this, einheitsmatrix);
            //InvertSecondHalf
            InvertSecondHalf(this, einheitsmatrix);

            for (int i = 0; i < einheitsmatrix.rowCount; i++)
            {
                for (int j = 0; j < einheitsmatrix.columnCount; j++)
                {
                    matrix[i, j] = einheitsmatrix[i, j];
                }
            }
        }
        public bool NotOnlyZerosTest()
        {
            int count = 0;
            for (int row = 0; row < rowCount; row++)
            {
                for (int column = 0; column < columnCount; column++)
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
                if (count == columnCount)
                { return false; }
            }
            for (int column = 0; column < columnCount; column++)
            {
                for (int row = 0; row < columnCount; row++)
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
                if (count == rowCount)
                { return false; }
            }
            return true;
        }
        private static void InvertFirstHalf(Matrix matrix1, Matrix matrix2)
        {
            int iterations = matrix1.columnCount; // ausbessern und nach matrix , kopierkonstruktor in matrixklasse 

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
        private static void InvertSecondHalf(Matrix matrix1, Matrix matrix2)
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
    }
}
