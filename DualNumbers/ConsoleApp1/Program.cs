using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace DualNumbers
{
    class Program
    {
        
        static void Main(string[] args)
        {
            int[] dual1 = new int[8];
            int[] dual2 = new int[8];
            int[] answer = new int[8];
            int[] dualOne = new int[8] { 0, 0, 0, 0, 0, 0, 0, 1 };
            Print(dual1, dual2, dualOne, answer);       
            Console.ReadLine();


        }

        public static void Print(int[] dual1, int[] dual2, int[] dualOne, int[] answer)
        {
            Console.WriteLine("First number:");
            char[] n1 = Console.ReadLine().ToCharArray();          
            for (int i = 0; i < n1.Length; i++)
            {
               dual1[i] = Convert.ToInt32(n1[i]-48);
            }

            Console.WriteLine("Second number:");
            char[] n2 = Console.ReadLine().ToCharArray();
            for (int i = 0; i < n2.Length; i++)
            {
                dual2[i] = Convert.ToInt32(n2[i] - 48);
            }
            Console.WriteLine("The answer is:");
            AdditionDualNumbers(dual1, dual2, answer);
            foreach (int i in answer)
                Console.WriteLine(i);
            Console.WriteLine("And: ");
            SubtractionDualNumbers(dual1, dual2, dualOne, answer);
            foreach (int i in answer)
                Console.WriteLine(i);

        }
        public static int[] AdditionDualNumbers(int[] dual1, int[] dual2, int[] answer)
        {
            int count = 0;
            for (int i = dual1.Length-1; i>=0; i--)
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
        public static void SubtractionDualNumbers(int[] dual1, int[] dual2, int[] one, int[] answer)
        {
            for (int j = 0; j<dual2.Length; j++)
            {
                if (dual2[j] == 0)
                {
                    dual2[j] = 1;
                }
                else
                {
                    dual2[j] = 0;
                }
               // dual[j] = (dual[j] + 1) % 2; ist das gleiche wie if else
            }
            dual2 = AdditionDualNumbers(dual2,one,answer);
            answer = AdditionDualNumbers(dual1, dual2, answer); 
        }
    }
}
