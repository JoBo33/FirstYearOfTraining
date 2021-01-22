using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] ar = { { 23, 51, 17, 60 }, { 263, 4, 75, 3 }, { 5, 9, 115, 65 } };
            for (int i = 0; i < 3; i++)
            {
                for(int j = 0; j < 4; j++)
                {
                    Console.Write(ar[i,j].ToString() + '\t');
                }
                Console.WriteLine("");
            }
            Console.ReadKey();
        }

        public static void Wuerfeln()
        {
            int count = 0;
            int augenzahl = 0;
            Random rnd = new Random();
            do
            {
                augenzahl = rnd.Next(1, 7);
                Console.WriteLine("Es wurde eine: " + augenzahl + "gewüfelt");
                count++;
            }
            while (augenzahl < 6);
            Console.WriteLine("Es wurde " + count + " mal gewürfelt");
        }
    }
}
