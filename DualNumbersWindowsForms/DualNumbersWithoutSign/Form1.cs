using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DualNumbersWithoutSign
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            char[] n1 = textBoxSummand1.Text.ToCharArray();
            char[] n2 = textBoxSummand2.Text.ToCharArray();
            int[] dual1 = new int[8];
            int[] dual2 = new int[8];
            int[] dualOne = new int[] { 0, 0, 0, 0, 0, 0, 0, 1 };
            int[] answer = new int[8];
           
            if (isItADualNumber(dual1, dual2, answer, n1, n2))
            {
                if (n2.Length <= 8 && n1.Length <= 8)
                {
                    if (comboBox1.Text == "+")
                    {
                        AdditionDualNumbers(dual1, dual2, answer);
                    }
                    else
                    {
                        SubtractionDualNumbers(dual1, dual2, dualOne, answer);
                    }
                    string antwort = "";
                    foreach (int i in answer)
                        antwort += i.ToString();
                    textBoxSumme.Text = antwort;
                }
                else
                {
                    MessageBox.Show("Too many/few Bits");
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        public int[] AdditionDualNumbers(int[] dual1, int[] dual2, int[] answer)
        {
            int count = 0;
            for (int i = dual1.Length - 1; i >= 0; i--)
            {
                int sum = count + dual1[i] + dual2[i];
                answer[i] = sum % 2;
                if (sum > 1)
                {
                    count = 1;
                }
                else
                {
                    count = 0;
                }
            }
            if (count == 1)
            {
                textBoxÜbertrag.Text = "(1)";
            }
            else
            {
                textBoxÜbertrag.Text = "(0)";

            }
            return answer;
        }
        public void SubtractionDualNumbers(int[] dual1, int[] dual2, int[] dualOne, int[] answer)
        {
            for (int j = 0; j < dual2.Length; j++)
            {
                dual2[j] = (dual2[j] + 1) % 2;
            }
            dual2 = AdditionDualNumbers(dual2, dualOne, answer);
            answer = AdditionDualNumbers(dual1, dual2, answer);
        }
        public bool isItADualNumber(int[] dual1, int[] dual2, int[] answer, char[] n1, char[] n2)
        {
            bool isDualNumber = true;
            int j = 1;
            for (int i = n1.Length-1; i >= 0; i--)
            {
                if (n1[i] != '0' && n1[i] != '1')
                {
                    MessageBox.Show("Not a dual number");
                    isDualNumber = false;
                }
                else
                {
                    dual1[dual1.Length-j] = (int)char.GetNumericValue(n1[i]);
                    j++;
                }
            }
            int k = 1;
            for (int i = n2.Length - 1; i >= 0; i--)
            {
                if (n2[i] != '0' && n2[i] != '1')
                {
                    MessageBox.Show("Not a dual number");
                    isDualNumber = false;
                }
                else
                {
                    dual2[dual2.Length-k] = (int)char.GetNumericValue(n2[i]);
                    k++;
                }
            }
            return isDualNumber;
        }
    }
}
