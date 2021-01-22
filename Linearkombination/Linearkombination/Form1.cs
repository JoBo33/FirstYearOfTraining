using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Linearkombination
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int points = 0;
            try
            {
                points = Convert.ToInt32(textBoxPunkte.Text);
                int dimension = Convert.ToInt32(textBoxDimension.Text);
                if (points-1 != dimension)
                {
                    throw new InvalidOperationException("Da ein Punkt getestet wird, muss ein Punkt \nmehr als Dimensionen vorhanden sein.");                   
                }
                dataGridView1.ColumnCount = points;
                dataGridView2.ColumnCount = points;
                for (int i = 0; i < points; i++)
                {
                    dataGridView1.Columns[i].Name = "Punkt " + (i + 1).ToString();
                    dataGridView2.Columns[i].Name = "Endtableau Spalte " + (i + 1).ToString();
                }
       
                dataGridView1.RowCount = dimension;
                dataGridView2.RowCount = dimension;
                for (int i = 0; i < dimension; i++)
                {
                    dataGridView1.Rows[i].HeaderCell.Value = "x" + (i + 1).ToString();
                    dataGridView2.Rows[i].HeaderCell.Value = "x" + (i + 1).ToString();
                }
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Da ein Punkt getestet wird, muss ein Punkt \nmehr als Dimensionen vorhanden sein.");
            }
            catch (Exception)
            {
                MessageBox.Show("Not Possible");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int dimension = 0;
            int points = 0;
            double[,] matrix = new double[dimension, points];
            try
            {
                dimension = Convert.ToInt32(textBoxDimension.Text);
                points = Convert.ToInt32(textBoxPunkte.Text);
                matrix = new double[dimension, points];
                for (int row = 0; row < dimension; row++)
                {
                    for (int columns = 0; columns < points; columns++)
                    {
                        matrix[row, columns] = Convert.ToDouble(dataGridView1[columns, row].Value);
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Not a numeric value");
            }

            
            Gauß(matrix, dimension, points);
            GaußErgebnisseEinsetzen(matrix, dimension, points);
            for(int i = 0; i < dimension; i++)
            {
                for (int j = 0; i < dimension; i++)
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
            for (int row = 0; row < dimension; row++)
            {
                for (int columns = 0; columns < points; columns++)
                {
                    dataGridView2[columns, row].Value = matrix[row, columns];
                }
            }

        }

        public void Gauß(double[,] matrix, int dimension, int points)
        {
            int iterations = points;
       
            for (int k = 0; k < iterations-1; k++)
            {
                for (int i = dimension - 1; i >= k; i--)
                {
                    for (int j = points - 1; j >= k; j--)
                    {
                        matrix[i, j] = matrix[i, j] / matrix[i, k];
                    }
                }
                for (int i = dimension - 1; i > k; i--)
                {
                    for (int j = points - 1; j >= k; j--)
                    {
                        matrix[i, j] = matrix[i, j] - matrix[k, j];
                    }
                }
            }
        }
        public void GaußErgebnisseEinsetzen(double[,] matrix, int dimension, int points)
        {
            int iterations = points;

            for (int k = iterations-2; k >=0 ; k--)
            {
                for (int i = 0; i <= k; i++)
                {
                    double pivot = matrix[i, k];
                    for (int j = 0; j < points; j++)
                    {
                        matrix[i, j] = matrix[i, j] / pivot;
                    }
                }
                for (int i = 0; i < k; i++)
                {
                    for (int j = 0; j < points; j++)
                    {
                        matrix[i, j] = matrix[i, j] - matrix[k, j];
                    }
                }
            }
            // lösungen in die jeweils höhere gleichung einsetzten und ausrechnen
            // wenn überall möglich eine linearkombination 
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label1.Text = "\u21D2";
        }
    }
}
