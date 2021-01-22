using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aufgabe2
{
    class Program
    {
        static void Main(string[] args)
        {
            int alter = Eingabe("Wie alt bist du?");
            Console.WriteLine(alter);
            Console.ReadLine();

        }

        public static int Eingabe(string benutzereingabe)
        {
            string antwort = Console.ReadLine();
            int numAntwort = 0;
            bool canConvert = int.TryParse(antwort, out numAntwort);
            if (canConvert == true)
            {
                return numAntwort;
            }
            else
            {
                Console.WriteLine("Falsche Eingabe, versuchen sie es erneut");
                return Eingabe(benutzereingabe);
            }

        }
    }
}
