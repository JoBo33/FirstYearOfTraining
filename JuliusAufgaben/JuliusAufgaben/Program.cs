using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aufgabe1
{
    class Program
    {
        static void Main(string[] args)
        {
            //string benutzereingabe = Console.ReadLine();
            //Eingabe(benutzereingabe);
            //Console.ReadLine();
            
            string name = Eingabe2("Wie heißt du?");
            Console.ReadLine();
        }

       // public static string Eingabe (string benutzereingabe)
       // {
       //     Console.WriteLine("Eingabe");
       //     Console.WriteLine(benutzereingabe);
       //     return benutzereingabe;
       // }

        public static string Eingabe2(string Frage)
        {
            Console.WriteLine(Frage);

            return Console.ReadLine();
        }
    }
}
