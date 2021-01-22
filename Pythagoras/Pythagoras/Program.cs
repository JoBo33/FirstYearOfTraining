using System;
/*using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;*/


namespace Pythagoras
{
    class Program
    {

        public static void Main(string[] args)
        {
            
            Pythagoras pythagoras = new Pythagoras();
            pythagoras.len_a();
            pythagoras.len_b();
            Console.WriteLine("Length of C is: " + pythagoras.calculate());
            Console.ReadLine();
            

            
            Prime prime = new Prime();
            prime.Range();
            prime.Prime_numbers();
            Console.ReadLine();



            Fac fac = new Fac(Convert.ToInt32(Console.ReadLine()));
            //fac.N();
            Console.WriteLine("n!:");
            fac.Facul(Convert.ToInt32(Console.ReadLine()));
            Console.ReadLine();
        }

        //static void Main(string[] args)
        //  
        //    
        //    Console.WriteLine("A: ");
        //    double a = Convert.ToDouble(Console.ReadLine());
        //    Console.WriteLine("B: ");
        //    double b = Convert.ToDouble(Console.ReadLine());
        //    double c = Math.Sqrt(a * a + b * b);
        //    Console.WriteLine(c);
        //    Console.ReadLine();
        //     
        //}
    }

    class Pythagoras
    {

        public double a;
        public double b;

        
       // public Pythagoras(double a, double b)
       // {
       //     this.a = a;
       //     this.b = b;
       // }
       //
       // 
       // public Pythagoras()
       // {
       // }
        

        public void len_a()
        {
            Console.WriteLine("A: ");
            a = Convert.ToDouble(Console.ReadLine());
        }
        public void len_b()
        {
            Console.WriteLine("B: ");
            b = Convert.ToDouble(Console.ReadLine());
        }
        public double calculate()
        {
            double c = Math.Sqrt(a * a + b * b);
            return c;
        }

    }
    class Prime
    {
        int x;

        public void Range()
        {
            Console.WriteLine("Prime numbers from 0 to ");
            x = Convert.ToInt32(Console.ReadLine());
        }
        public int Prime_numbers()
        {
            Console.WriteLine("Prime Numbers:");

            for ( int i = 1; i <= x; i++)
            {
                int count = 0;
                for (int j = i; j > 0; j--)
                {
                    if (i % j == 0)
                    {
                        count += 1;
                    }
                }
                if (count == 2)
                {
                    Console.WriteLine(i);
                        
                }
            }
            return 0;
        }
    }
    class Fac
    {
        int n;

        public Fac(int n)
        {
            this.n = n;
        }
        //public void N()
        //{
        //    Console.WriteLine("n!");
        //    n = Convert.ToInt32(Console.ReadLine());
        //}
        public int Facul(int n)
        {   
            if (n == 0)
            {
                n=0;
                Console.WriteLine("0");
                
            }
            if (n == 1)
            {   
                n = 1;
                
                
            }
            else
            {
                n = n * Facul(n - 1); 
            }
            Console.WriteLine(n);
            //Console.ReadLine();
            return n;
        }

    }
}
