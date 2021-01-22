using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConvertTo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            string givenInput = textBoxEntry.Text;
            if (radioButton1.Checked)
            {
                try
                {
                    int givenInputDec = int.Parse(givenInput);
                    textBoxAnswer.Text = Convert.ToString(givenInputDec, 2);
                    //   ConvertFromDecToBi(givenInputDec);
                }
                catch (Exception)
                {
                    MessageBox.Show("Not a Decimal Number");
                }
            }
            else if (radioButton2.Checked)
            {
                try
                {
                    textBoxAnswer.Text = Convert.ToString(Convert.ToInt32(givenInput, 2));
                }
                catch (Exception)
                {
                    MessageBox.Show("Not a Decimal Number");
                }
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
            }
            else if (radioButton3.Checked)
            {
                try
                {
                    int givenInputDec = int.Parse(givenInput);
                    textBoxAnswer.Text = givenInputDec.ToString("X");
                }
                catch (Exception)
                {
                    MessageBox.Show("Not a Decimal Number");
                }

            }
            else if (radioButton4.Checked)
            {
                try
                {
                    int dec = int.Parse(givenInput, System.Globalization.NumberStyles.HexNumber);
                    textBoxAnswer.Text = dec.ToString();
                }
                catch (Exception)
                {
                    MessageBox.Show("Not a Hexadecimal Number");
                }
            }
            else if (radioButton5.Checked)
            {
                try
                {
                    textBoxAnswer.Text = Convert.ToInt32(givenInput, 2).ToString("X").ToString();
                }
                catch (Exception)
                {
                    MessageBox.Show("Not a Hexadecimal Number");
                }
            }
            else if (radioButton6.Checked)
            {
                try
                {
                    textBoxAnswer.Text = Convert.ToString(Convert.ToInt32(givenInput, 16), 2).PadLeft(4, '0');
                }
                catch (Exception)
                {
                    MessageBox.Show("Not a Hexadecimal Number");
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string summand1 = textBoxSummand1.Text;
            string summand2 = textBoxSummand2.Text;
            if (radioButton7.Checked)
            {
                try
                {
                    int dec1 = Convert.ToInt32(summand1);
                    int dec2 = Convert.ToInt32(summand2);
                    if (comboBox1.Text == "+")
                    {
                        textBoxSum.Text = Convert.ToString(dec1 + dec2);
                    }
                    else
                    {
                        textBoxSum.Text = Convert.ToString(dec1 - dec2);
                    }

                }
                catch (Exception)
                {
                    MessageBox.Show("No decimal input");
                }
            }
            else if (radioButton8.Checked)
            {
                try
                {
                    char[] n1 = textBoxSummand1.Text.ToCharArray();
                    char[] n2 = textBoxSummand2.Text.ToCharArray();
                    int[] dual1 = new int[8];
                    int[] dual2 = new int[8];
                    int[] answer = new int[9];
                    int[] dualOne = new int[] { 0, 0, 0, 0, 0, 0, 0, 1 };
                    int j = 1;
                    for (int i = n1.Length - 1; i >= 0; i--)
                    {
                        if (n1[i] != '0' && n1[i] != '1')
                        {
                            MessageBox.Show("Not a Binary Number");
                            break;
                        }
                        else
                        {
                            dual1[dual1.Length-j] = (int)char.GetNumericValue(n1[i]);
                            j++;
                        }
                    }
                    j = 1;
                    for (int i = n2.Length - 1; i >= 0; i--)
                    {
                        if (n2[i] != '0' && n2[i] != '1')
                        {
                            MessageBox.Show("Not a Binary Number");
                            break;
                        }
                        else
                        {
                            dual2[dual2.Length-j] = (int)char.GetNumericValue(n2[i]);
                            j++;
                        }
                    }
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
                    textBoxSum.Text = antwort;
                }
                catch (Exception)
                {
                    MessageBox.Show("Not a 8-Bit binary input");
                }
            }
            else if (radioButton9.Checked)
            {
                try
                {
                    int number1 = int.Parse(textBoxSummand1.Text, System.Globalization.NumberStyles.HexNumber);
                    int number2 = int.Parse(textBoxSummand2.Text, System.Globalization.NumberStyles.HexNumber);
                    int sum = 0;
                    if (comboBox1.Text == "+")
                    {
                        sum = number1 + number2;
                    }
                    else
                    {
                        sum = number1 - number2;
                    }
                    textBoxSum.Text = sum.ToString("X");
                }
                catch (Exception)
                {
                    MessageBox.Show("Not a hexadecimal input");
                }
            }
            else
            {
                MessageBox.Show("You need to decide what an input it is.");
            }
        }
        public int[] AdditionDualNumbers(int[] dual1, int[] dual2, int[] answer)
        {
            int count = 0;
            for (int i = dual2.Length - 1; i >= 0; i--)
            {
                int sum = count + dual1[i] + dual2[i];
                answer[i + 1] = sum % 2;
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
                answer[0] = 1;
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
            answer = AdditionDualNumbers(dual1, dual2.Skip(1).ToArray(), answer);
        }
        //   public void ConvertFromDecToBi(int givenInputDec)
        //   {
        //       double temp = givenInputDec;
        //       int[] binary = new int[16];
        //       for (int i = 0; i < binary.Length; i++)
        //       {
        //           binary[binary.Length - i - 1] = Convert.ToInt32(temp % 2);
        //           temp = Math.Floor(Convert.ToDouble(temp / 2));
        //       }
        //       string dual = "";
        //       foreach (int i in binary)
        //           dual += i.ToString();
        //       textBoxAnswer.Text = dual;
        //       
        //   }
        //   public void ConvertFromBiToDec(int[] binary)
        //   {
        //       int power = 0;
        //       double dec = 0; 
        //       for(int i = binary.Length-1; i>=0; i--)
        //       {
        //           if (binary[i] == 1)
        //           {
        //               dec += Math.Pow(2, power);                    
        //           }
        //           power++;
        //       }
        //       textBoxAnswer.Text = dec.ToString();
        //   }
    }
}
