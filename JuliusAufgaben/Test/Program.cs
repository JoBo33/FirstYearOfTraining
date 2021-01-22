using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Rechner rechner = new Rechner();
            rechner.Schwierigkeitsgrad();
            rechner.Rückmeldung(rechner.RandomRechnen());
            Console.ReadLine();
        }

        class Rechner
        {
            string schwierigkeit;
            Random rnd = new Random();
            public string Schwierigkeitsgrad()
            {
                Console.WriteLine("Schwierigkeitsgrad wählen");
                schwierigkeit = Console.ReadLine();
                return schwierigkeit;
            }
            public double RandomRechnen()
            {
                double num = 0;
                double num2 = 0;
                double ergebnis = 0;
                
                string[] rechenzeichen = new string[5] { "+", "-", "*", "/", "%" };
                int randomRechenzeichen = rnd.Next(rechenzeichen.Length);
                if (schwierigkeit == "Einfach")
                {
                    num = rnd.Next(50);
                    num2 = rnd.Next(50);
                }
                else if (schwierigkeit == "Mittel")
                {
                    num = rnd.Next(500);
                    num2 = rnd.Next(500);
                }
                else
                {
                    num = rnd.Next(5000);
                    num2 = rnd.Next(5000);
                }
                Console.WriteLine(num + " " + rechenzeichen[randomRechenzeichen] + " " + num2);               
                switch (randomRechenzeichen)
                {
                    case 0:
                        ergebnis = num + num2;
                        break;
                    case 1:
                        ergebnis = num - num2;
                        break;
                    case 2:
                        ergebnis = num * num2;
                        break;
                    case 3:
                        ergebnis = num / num2;
                        break;
                    case 4:
                        ergebnis = num % num2;
                        break;

                }
                return ergebnis;
            }
            public void Rückmeldung(double ergebnis)
            {
                string[] gratulation = new string[3] { "sehr gut", "gut gemacht", "richtig gerechnet" };
                string[] tadel = new string[3] { "falsch gerechnet", "konzentriere dich mehr", "schlecht gemacht" };
                double nutzerErgebnis = Convert.ToDouble(Console.ReadLine());
                if (ergebnis == nutzerErgebnis)
                {
                    int index = rnd.Next(gratulation.Length);
                    Console.WriteLine(gratulation[index]);
                }
                else
                {
                    int index1 = rnd.Next(tadel.Length);
                    Console.WriteLine(tadel[index1]);
                }
                Console.WriteLine("Das Ergebnis lautet: " + ergebnis);
            }
        }
    }
}
