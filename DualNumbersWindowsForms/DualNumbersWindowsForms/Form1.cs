using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace DualNumbersWindowsForms
{
    public partial class DualzahlenRechner : Form
    {
        public DualzahlenRechner()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            char[] n1 = textBoxSummand1.Text.ToCharArray();
            char[] n2 = textBoxSummand2.Text.ToCharArray();
            int[] dual1 = new int[n1.Length];
            int[] dual2 = new int[n2.Length];
            int[] dualOne = new int[] { 0, 0, 0, 0, 0, 0, 0, 1 };
            int[] answer = new int[8];
            if (isItADualNumber(dual1, dual2, answer, n1, n2))
            {
                if (n2.Length == 8 && n1.Length == 8)
                {
                    SignBitAnswer(dual1, dual2);
                    if (comboBox1.Text == "+")
                    {
                        if ( dual1[0] == 0 && dual2[0] == 1)
                        {
                            SubtractionDualNumbers(dual1, dual2, dualOne, answer); 
                        }
                        else
                        {
                            AdditionDualNumbers(dual1, dual2, answer);
                        }
                    }
                    else
                    {
                        if ( dual1[0] == 0 && dual2[0] == 1)
                        {
                            AdditionDualNumbers(dual1, dual2, answer);
                        }
                        else
                        {                            
                            SubtractionDualNumbers(dual1, dual2, dualOne, answer); 
                        }
                    }
                    string antwort = "";
                    for (int i = 0; i < answer.Length; i++)
                    {
                        antwort += answer[i].ToString();
                    }
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
            return answer;
        }
        public void SubtractionDualNumbers(int[] dual1, int[] dual2, int[] dualOne, int[] answer)
        {
            for (int j = 0; j < dual2.Length; j++)
            {
                dual2[j] = (dual2[j] + 1) % 2;
            }
            dual2 = AdditionDualNumbers(dual2, dualOne, answer);
            answer = AdditionDualNumbers(dual1, answer, answer);
        }
        public void SignBitAnswer(int[] dual1, int[] dual2)
        {
            if (dual1[0] == 0 && dual2[0] == 0 && comboBox1.Text == "+")
            {
               textBoxSignBit.Text = "0";
            }
            else if (dual1[0] == 1 && dual2[0] == 0 && comboBox1.Text == "-")
            {
                textBoxSignBit.Text = "1";
            }
            else if (dual1[0] == 0 && dual2[0] == 1 || dual1[0] == 1 && dual2[0] == 0 || dual1[0] == 1 && dual2[0] == 1)
            {
                if (IsOneBigger(dual1, dual2) == false)
                {
                    if (dual1[0] == dual2[0])
                    {
                        if (comboBox1.Text == "+")
                        {
                            textBoxSignBit.Text = dual1[0].ToString();
                        }
                        else
                        {
                            textBoxSignBit.Text = "0";
                        }
                    }
                }
            }
        }
        public bool isItADualNumber(int[] dual1, int[] dual2, int[] answer, char[] n1, char[] n2)
        {
            bool isDualNumber = true;
            for (int i = 0; i < n1.Length; i++)
            {
                if (n1[i] != '0' && n1[i] != '1')
                {
                    MessageBox.Show("Not a dual number");
                    isDualNumber = false;
                }
                else
                {
                    dual1[i] = (int)char.GetNumericValue(n1[i]);
                }
            }
            for (int i = 0; i < n2.Length; i++)
            {
                if (n2[i] != '0' && n2[i] != '1')
                {
                    MessageBox.Show("Not a dual number");
                    isDualNumber = false;
                }
                else
                {
                    dual2[i] = (int)char.GetNumericValue(n2[i]);
                }
            }
            return isDualNumber;
        }
        public bool IsOneBigger(int[] dual1, int[] dual2)
        {
            bool yes = false;
            for (int i = 1; i < dual1.Length; i++)
            {
                if (dual1[i] > dual2[i])
                {
                    textBoxSignBit.Text = dual1[0].ToString();
                    yes = true;
                    break;
                }
                else if (dual1[i] < dual2[i])
                {
                    textBoxSignBit.Text = dual2[0].ToString();
                    yes = true;
                    break;
                }
            }
            return yes;
        }
    }
}
 
