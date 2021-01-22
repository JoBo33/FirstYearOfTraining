using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aufgabe3
{
    class Program
    {
        static void Main(string[] args)
        {
            Raetsel("Wer ist deutscher Meister", "Bayern München", "Ja, das stimmt", "Nein das stimmt nicht");
            Console.ReadLine();
        }

        public static bool Raetsel(string frage, string antwort, string richtig, string falsch)
        {
            string nutzerAntwort = Console.ReadLine();
            if (nutzerAntwort == antwort)
            {
                Console.WriteLine(richtig);
                return true;
            }
            else
            {
                Console.WriteLine(falsch);
                return false;
            }
        }
    }
}
