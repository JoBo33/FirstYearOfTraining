using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace AllTogether
{
    class NumberSystems
    {
        //       Getter, Setter, Konstruktor erstellen
        //       Static umgehen
        public static int[] AdditionDualNumbers(int[] dual1, int[] dual2, int[] answer)
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
        public static void SubtractionDualNumbers(int[] dual1, int[] dual2, int[] dualOne, int[] answer)
        {
            for (int j = 0; j < dual2.Length; j++)
            {
                dual2[j] = (dual2[j] + 1) % 2;
            }
            dual2 = AdditionDualNumbers(dual2, dualOne, answer);
            answer = AdditionDualNumbers(dual1, dual2, answer);
        }
        public static bool isItADualNumber(int[] dual1, int[] dual2, int[] answer, char[] n1, char[] n2)
        {
            bool isDualNumber = true;
            int j = 1;
            for (int i = n1.Length - 1; i >= 0; i--)
            {
                if (n1[i] != '0' && n1[i] != '1')
                {
                    MessageBox.Show("Not a dual number");
                    isDualNumber = false;
                }
                else
                {
                    dual1[dual1.Length - j] = (int)char.GetNumericValue(n1[i]);
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
                    dual2[dual2.Length - k] = (int)char.GetNumericValue(n2[i]);
                    k++;
                }
            }
            return isDualNumber;
        }


        public static string ConvertDecToBin(string givenInput)
        {
            try
            {
                int givenInputDec = int.Parse(givenInput);
                return Convert.ToString(givenInputDec, 2);
            }
            catch (Exception)
            {
                MessageBox.Show("Not a Decimal Number");
            }
            return string.Empty;
        }
        public static string ConvertBinToDec(string givenInput)
        {
            try
            {
                return Convert.ToString(Convert.ToInt32(givenInput, 2));
            }
            catch (Exception)
            {
                MessageBox.Show("Not a Binary Number");
            }
            return string.Empty;
        }
        public static string ConvertDecToHex(string givenInput)
        {
            try
            {
                int givenInputDec = int.Parse(givenInput);
                return givenInputDec.ToString("X");
            }
            catch (Exception)
            {
                MessageBox.Show("Not a Decimal Number");
            }
            return string.Empty;
        }
        public static string ConvertHexToDec(string givenInput)
        {
            try
            {
                int dec = int.Parse(givenInput, System.Globalization.NumberStyles.HexNumber);
                return dec.ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("Not a Hexadecimal Number");
            }
            return string.Empty;
        }
        public static string ConvertBinToHex(string givenInput)
        {
            try
            {
                return Convert.ToInt32(givenInput, 2).ToString("X").ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("Not a Binary Number");
            }
            return string.Empty;
        }
        public static string ConvertHexToBin(string givenInput)
        {
            try
            {
                return Convert.ToString(Convert.ToInt32(givenInput, 16), 2).PadLeft(4, '0');
            }
            catch (Exception)
            {
                MessageBox.Show("Not a Hexadecimal Number");
            }
            return string.Empty;
        }


        public static string DecimalAdditionSubtraction(string summand1, string summand2, string chosenOperator)
        {
            try
            {
                int dec1 = Convert.ToInt32(summand1);
                int dec2 = Convert.ToInt32(summand2);
                if (chosenOperator == "+")
                {
                    return Convert.ToString(dec1 + dec2);
                }
                else
                {
                    return Convert.ToString(dec1 - dec2);
                }

            }
            catch (Exception)
            {
                MessageBox.Show("No decimal input");
            }
            return string.Empty;
        }
        public static string BinaryAdditionSubtraction(string summand1, string summand2,string chosenOperator)
        {
            try
            {
                char[] n1 = summand1.ToCharArray();
                char[] n2 = summand2.ToCharArray();
                int[] dual1 = new int[8];
                int[] dual2 = new int[8];
                int[] answer = new int[8];
                int[] dualOne = new int[] { 0, 0, 0, 0, 0, 0, 0, 1 };
                int j = 1;
                for (int i = n1.Length - 1; i >= 0; i--)
                {
                    if (n1[i] != '0' && n1[i] != '1')
                    {
                        throw new ArgumentException("Not a Binary Number");
                    }
                    else
                    {
                        dual1[dual1.Length - j] = (int)char.GetNumericValue(n1[i]);
                        j++;
                    }
                }
                j = 1;
                for (int i = n2.Length - 1; i >= 0; i--)
                {
                    if (n2[i] != '0' && n2[i] != '1')
                    {
                        throw new ArgumentException("Not a Binary Number");
                    }
                    else
                    {
                        dual2[dual2.Length - j] = (int)char.GetNumericValue(n2[i]);
                        j++;
                    }
                }
                if (chosenOperator == "+")
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
                return antwort;
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            catch (IndexOutOfRangeException e)
            {
                MessageBox.Show(e.ToString());
            }
            return string.Empty;
        }
        public static string HexadecimalAdditionSubtraction(string summand1, string summand2, string chosenOperator)
        {
            try
            {
                int number1 = int.Parse(summand1, System.Globalization.NumberStyles.HexNumber);
                int number2 = int.Parse(summand2, System.Globalization.NumberStyles.HexNumber);
                int sum = 0;
                if (chosenOperator == "+")
                {
                    sum = number1 + number2;
                }
                else
                {
                    sum = number1 - number2;
                }
                return sum.ToString("X");
            }
            catch (Exception)
            {
                MessageBox.Show("Not a hexadecimal input");
            }
            return string.Empty;
        }
    }
}
