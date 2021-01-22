using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MatrizenRechnung;

namespace MatrixInvertation
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }        
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            MatrizenRechnung.Form1.InsertColumns(dataGridView1);
        }
        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            MatrizenRechnung.Form1.NumberAsInput(dataGridView1, e);
        }       
        private void button1_Click(object sender, EventArgs e)
        {
            MatrizenRechnung.Form1.DeleteEmptyColumns(dataGridView1);
            if (dataGridView1.ColumnCount != dataGridView1.RowCount-1)
            {
                MessageBox.Show("not a square matrix.");
                return;
            }
            Matrix matrix1 = MatrizenRechnung.Form1.DataGridViewToMatrix(dataGridView1);
            if (matrix1.IsInvertible() == false)
            {
                MessageBox.Show("Die Zeilenvektoren/Spaltenvektoren sind linear abhängig. \nDiese Matrix ist daher nicht invertierbar.");
                return;
            }
            dataGridView2.ColumnCount = dataGridView1.ColumnCount;
            dataGridView2.RowCount = dataGridView1.RowCount;
            matrix1.Invert();
            matrix1.RoundMatrix();
            matrix1.MatrixToDataGridView(dataGridView2);
        }
    }
}
