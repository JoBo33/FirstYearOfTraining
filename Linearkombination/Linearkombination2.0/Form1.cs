using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Linearkombination2._0
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            label1.Text = "\u21D2";
        }
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                if (dataGridView1.Rows[i].Cells[dataGridView1.ColumnCount - 1].Value != null)
                {
                    dataGridView1.ColumnCount++;
                    dataGridView1.Columns[i].Name = "Punkt " + (i + 1);
                    break;
                }
            }
        }
        private void DeleteColumns()
        {
            int count = 0;
            for (int i = dataGridView1.ColumnCount - 1; i > 0; i--)
            {
                count = 0;
                for (int j = 0; j < dataGridView1.RowCount; j++)
                {
                    if (dataGridView1.Rows[j].Cells[i].Value == null)
                    {
                        count++;
                    }
                    else
                    {
                        break;
                    }
                }
                if (count == dataGridView1.RowCount)
                {
                    dataGridView1.Columns.RemoveAt(i);
                }
            }            
        }
        public bool RightInputTest()
        {
            bool inputCorrect = true;
            double[,] matrix = new double[dataGridView1.RowCount - 1, dataGridView1.ColumnCount];
            dataGridView2.ColumnCount = dataGridView1.ColumnCount;
            dataGridView2.RowCount = dataGridView1.RowCount - 1;
            try
            {
                if (dataGridView2.ColumnCount - 1 != dataGridView2.RowCount)
                {
                    throw new InvalidOperationException("Da ein Punkt getestet wird, muss ein Punkt \nmehr als Dimensionen vorhanden sein.");
                }
                for (int i = 0; i < dataGridView1.ColumnCount; i++)
                {
                    dataGridView1.Columns[i].Name = "Punkt " + (i + 1).ToString();
                    dataGridView2.Columns[i].Name = "Endtableau Spalte " + (i + 1).ToString();
                }

                for (int i = 0; i < dataGridView1.RowCount - 1; i++)
                {
                    dataGridView1.Rows[i].HeaderCell.Value = "x" + (i + 1).ToString();
                    dataGridView2.Rows[i].HeaderCell.Value = "x" + (i + 1).ToString();
                }
                for (int row = 0; row < dataGridView1.RowCount - 1; row++)
                {
                    for (int columns = 0; columns < dataGridView1.ColumnCount; columns++)
                    {
                        if (dataGridView1[columns, row].Value.ToString() != "")
                        {
                            matrix[row, columns] = Convert.ToDouble(dataGridView1[columns, row].Value);
                        }
                    }
                }
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Da ein Punkt getestet wird, muss ein Punkt \nmehr als Dimensionen vorhanden sein.");
                inputCorrect = false;
            }
            catch (Exception)
            {
                MessageBox.Show("Wrong Input");
                inputCorrect = false;
            }
            return inputCorrect;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DeleteColumns();
            RightInputTest();
            if (!RightInputTest())
            {
                return;
            }
            double[,] matrix = new double[dataGridView1.RowCount-1, dataGridView1.ColumnCount];
            for (int row = 0; row < dataGridView1.RowCount-1; row++)
            {
                for (int columns = 0; columns < dataGridView1.ColumnCount; columns++)
                {
                    if (dataGridView1[columns, row].Value.ToString() != "")
                    {
                        matrix[row, columns] = Convert.ToDouble(dataGridView1[columns, row].Value);
                    }
                }
            }
            Gauß(matrix);
            GaußErgebnisseEinsetzen(matrix);
            for (int i = 0; i < dataGridView1.RowCount-1; i++)
            {
               for (int j = 0; j < dataGridView1.ColumnCount; j++)
               {
                   if (double.IsNaN(matrix[i, j]))
                   {
                       textBoxTestErgebnis.Text = "Der Punkt lässt sich nicht als Linearkombination darstellen.";
                   }
                   else
                   {
                       textBoxTestErgebnis.Text = "Der Punkt lässt sich als Linearkombination darstellen.";
                   }
               }
            }          
            for (int columns = 0; columns < dataGridView2.ColumnCount; columns++)
            {
                for (int row = 0; row < dataGridView2.RowCount; row++)
                {
                    dataGridView2[columns, row].Value = matrix[row, columns]; 
                }
            }     
        }
        public void Gauß(double[,] matrix)
        {
            int iterations = dataGridView1.ColumnCount;
        
            for (int k = 0; k < iterations - 1; k++)
            {
                for (int i = dataGridView1.RowCount - 2; i >= k; i--)
                {
                    for (int j = dataGridView1.ColumnCount - 1; j >= k; j--)
                    {
                        matrix[i, j] = matrix[i, j] / matrix[i, k];
                    }
                }
                for (int i = dataGridView1.RowCount - 2; i > k; i--)
                {
                    for (int j = dataGridView1.ColumnCount - 1; j >= k; j--)
                    {
                        matrix[i, j] = matrix[i, j] - matrix[k, j];
                    }
                }
            }
        }
        public void GaußErgebnisseEinsetzen(double[,] matrix)
        {
            int iterations = dataGridView1.ColumnCount;
        
            for (int k = iterations - 2; k >= 0; k--)
            {
                for (int i = 0; i <= k; i++)
                {
                    double pivot = matrix[i, k];
                    for (int j = 0; j < dataGridView1.ColumnCount; j++)
                    {
                        matrix[i, j] = matrix[i, j] / pivot;
                    }
                }
                for (int i = 0; i < k; i++)
                {
                    for (int j = 0; j < dataGridView1.ColumnCount; j++)
                    {
                        matrix[i, j] = matrix[i, j] - matrix[k, j];
                    }
                }
            }
        }
    }
}

