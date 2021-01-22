using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MatrizenRechnung
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        #region Events
        // Events

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            InsertColumns(dataGridView1);
        }
        private void dataGridView2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            InsertColumns(dataGridView2);
        }
        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {            
            NumberAsInput(dataGridView1, e);
        }
        private void dataGridView2_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            NumberAsInput(dataGridView2, e);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            DeleteEmptyColumns(dataGridView1);
            DeleteEmptyColumns(dataGridView2);
            SizeOfMatrixAnswer(dataGridView1, dataGridView2);
            Matrix matrix1 = DataGridViewToMatrix(dataGridView1);
            Matrix matrix2 = DataGridViewToMatrix(dataGridView2);
            Matrix matrix3 = SelectedOperatorAndCalculation(matrix1, matrix2);
            if (matrix3 != null)
            {
                dataGridView3.ClearSelection();
                MatrixToDataGridView(matrix3);
            }
            else
            {
                MessageBox.Show("Calculation not possible. Please check the input.");
            }
            

        }
        #endregion
        // Eigene Methoden bei Ausführung der Events  !!!!Static wegen Matrixinertation projekt!!!!
        #region Methods
        public static void InsertColumns (DataGridView dataGridView)
        {
            for (int i = 0; i < dataGridView.RowCount; i++)
            {
                if (dataGridView.Rows[i].Cells[dataGridView.ColumnCount - 1].Value != null)
                {
                    dataGridView.ColumnCount++;
                    //dataGridView.Columns[i].Name = "Punkt " + (i + 1);
                    break;
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

        public void SizeOfMatrixAnswer (DataGridView dataGridView1, DataGridView dataGridView2)
        {
            if (dataGridView1.RowCount > dataGridView2.RowCount)
            {
                dataGridView3.RowCount = dataGridView1.RowCount;
            }
            else
            {
                dataGridView3.RowCount = dataGridView2.RowCount;
            }
            if (dataGridView1.ColumnCount > dataGridView2.ColumnCount)
            {
                dataGridView3.ColumnCount = dataGridView1.ColumnCount;
            }
            else
            {
                dataGridView3.ColumnCount = dataGridView2.ColumnCount;
            }
        }

        public static void NumberAsInput(DataGridView dataGridView, DataGridViewCellValidatingEventArgs e)
        {
            if (!(e.FormattedValue.ToString() == "-" || e.FormattedValue.ToString() == "" || Double.TryParse(e.FormattedValue.ToString(), out double value)))
            {             
                e.Cancel = true;
            }
        }
        
        public static Matrix DataGridViewToMatrix(DataGridView dataGridView)
        {
            Matrix matrix = new Matrix(dataGridView.RowCount-1, dataGridView.ColumnCount);
            for (int rows = 0; rows < dataGridView.RowCount-1; rows++)
            {
                for (int columns = 0; columns < dataGridView.ColumnCount; columns++)
                {
                    matrix[rows, columns] = Convert.ToDouble(dataGridView[columns, rows].Value);
                }
            }
            return matrix;
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

        public void MatrixToDataGridView(Matrix matrix)
        {
            for (int row = 0; row < matrix.rowCount; row++)
            {
                for (int column = 0; column < matrix.columnCount; column++)
                {
                        dataGridView3[column, row].Value = matrix[row, column];
                }
            }

        }
        #endregion
    }
}
