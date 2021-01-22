using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Enter a number: ");
            //int n = Convert.ToInt32(Console.ReadLine());
            //int sum = 0;
            //int count = 1;
            //while(count <= n)
            //{
            //    sum += count;
            //    count++;
            //}
            //Console.WriteLine(sum);
            //sum = 0;
            //for(int i = 1; i <= n; i++)
            //{
            //    sum += i;
            //}
            //int formula = (n * (n + 1) / 2);
            //Console.WriteLine(sum);
            //Console.WriteLine(formula);
            //Console.ReadLine();



            Console.WriteLine("Enter a number between 1 and 15: ");
            int a = Convert.ToInt32(Console.ReadLine());
            if (a < 16)
            {
                int result = 1;
                int j = 1;
                while (j <= a)
                {
                    result *= j;
                    j++;
                }
                Console.WriteLine(result);

                result = 1;
                for (int i = 1; i <= a; i++)
                {
                    result *= i;
                }
                Console.WriteLine(result);
                Console.ReadLine();
            }
        }
    }
}
