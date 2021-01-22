using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aufgabe4
{
    class Program   // Klasse zum ausführen
    {
        
        static void Main(string[] args)
        {
            Random rnd = new Random();
            Meincoolesgame gamex = new Meincoolesgame();                                            // Objekt der Methodenklasse erstellen
            string name = gamex.AntwortAufFrage("Name:");                                           // Eintragung allgemeiner Daten über die Konsole
            gamex.AntwortAufFrage("Wohnort:");
            gamex.ConvertableToInt("Alter:");
            gamex.AntwortAufFrage("Haarfarbe:");
            gamex.ConvertableToInt("Gewicht:");
            string modus = gamex.AntwortAufFrage("Welcher Spielmodus? (Spielmodus1, Spielmodus2, Spielmodus3 oder Spielmodus4)");
            if ("Spielmodus1" == modus)                                                                                           // Abfrage welcher Modus gespielt werden soll
            {
                gamex.Spiemodus1();

                if (gamex.getLevel() > 1)
                {
                    Console.WriteLine("Gut gemacht " + name + ", du hast es bis  Level " + gamex.getLevel() + " geschafft");
                }
                else
                {
                    Console.WriteLine("Schade " + name + ", du hast keine Antwort richtig.");
                }
            }
            else if ("Spielmodus2" == modus)
            {
                gamex.Spielmodus2();
                if (gamex.getLevel() > 1)
                {
                    Console.WriteLine("Gut gemacht " + name + ", du hast " + gamex.getLevel() + " Punkte erreicht");
                }
                else
                {
                    Console.WriteLine("Schade " + name + ", du hast keine Antwort richtig.");
                }
            }
            else if ("Spielmodus3" == modus)
            {
                gamex.Schwierigkeitsgrad();
                gamex.Rückmeldung(gamex.RandomRechnen());
            }
            else
            {
                gamex.VorbereitungEinstQuersumme(rnd.Next());                
            }
            Console.ReadLine();
        }
}

    class Meincoolesgame // Klasse für die Methoden
    {      
        int versuche, level, sum;       // Membervariablen
        string schwierigkeit;
        Random rnd = new Random();
        public Meincoolesgame()   // simpler Konstruktor
        {
            versuche = 0;
            level = 0;
            sum = 0;
        }
        public int ConvertableToInt(string benutzereingabe)    // Methode zur Überprüfung ob Eingabe ein Integer ist
        {
            Console.WriteLine(benutzereingabe);
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
                return ConvertableToInt(benutzereingabe);
            }
        }
        public string AntwortAufFrage(string Frage)   // Methode zum auslesen der Nutzerantworten
        {
            Console.WriteLine(Frage);
            return Console.ReadLine();
        }
        public bool Raetsel(string frage, string antwort, string richtig, string falsch)  // Methode zum überprüfen ob das Rätsel richtig beantwortet wird
        {
            bool correct = false;
            for (int i = 0; i <= versuche; i++)
            {
                Console.WriteLine(frage);
                string nutzerAntwort = Console.ReadLine();
                if (nutzerAntwort == antwort)
                {
                    Console.WriteLine(richtig);
                    level += 1;                                                         // level zählt die Anzhal an richtigen antworten
                    correct = true;
                    break;
                }
                else
                {
                    Console.WriteLine(falsch);
                    Console.WriteLine("noch " + (versuche - i) + " Versuche");
                }
            }
            return correct;
        }
        public int getLevel()                                                          // Getter-Methode zum ausgeben des Levels/ der erreichten Punkte
        {
            return level;
        }
        public string Schwierigkeitsgrad()                                             // Methode zum wählen des Schwierigkeitsgrades
        {
            Console.WriteLine("Schwierigkeitsgrad wählen");
            schwierigkeit = Console.ReadLine();
            return schwierigkeit;
        }
        public double RandomRechnen()                                                  // Methode zum Spielmodus3
        {                                                                          
            double num = 0;
            double num2 = 0;
            double ergebnis = 0;
            string[] rechenzeichen = new string[5] { "+", "-", "*", "/", "%" };
            int randomRechenzeichen = rnd.Next(rechenzeichen.Length);                   // Random ausgewählter Operator
            if (schwierigkeit == "Einfach")                                             // Abfrage die radom 2 Zahlen generiert (in Abhängigkeit vom Schwierigkeitsgrad)
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
            switch (randomRechenzeichen)                                                // Berechnung der generierten Rechnung
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
        public void Rückmeldung(double ergebnis)                                                // Methode zur Konsolenausgabe bei richtiger und falscher Rechnung des Nutzers
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

        public void Spiemodus1()                                                               // Methode für den ersten Spielmodus 
        {                                                                                      // 3 Versuche (0,1,2), wenn nicht geschafft abbruch
            versuche = 2;
            Raetsel("Welche Ausbildung mache ich", "MATSE", "Ja, das stimmt", "Nein das stimmt nicht");           
            if (Raetsel("Wie hoch ist die Mehrwertsteuer (vor Senkung)?", "19%", "Ja, das stimmt", "Nein das stimmt nicht"))
            {
                if (Raetsel("Ergebnis von 7*4+3+8*2", "47", "Ja, das stimmt", "Nein das stimmt nicht"))
                {
                    if (Raetsel("Hauptstadt von Thüringen", "Erfurt", "Ja, das stimmt", "Nein das stimmt nicht"))
                    {
                        if (Raetsel("Wie viele Planeten hat unser Sonnensystem", "8", "Ja, das stimmt", "Nein das stimmt nicht"))
                        {
                            if (Raetsel("Rekordtorschütze der Bundesliga", "Gerd Müller", "Ja, das stimmt", "Nein das stimmt nicht"))
                            {
                                if (Raetsel("Was ist ein Sonett", "Gedichtsform", "Ja, das stimmt", "Nein das stimmt nicht"))
                                {
                                    if (Raetsel("In Welcher Einheit wird elrktrischer Widerstand gemessen?", "Ohm", "Ja, das stimmt", "Nein das stimmt nicht"))
                                    {
                                        if (Raetsel("Chemisches Symbol für Blei", "Pb", "Ja, das stimmt", "Nein das stimmt nicht"))
                                        {
                                            if (Raetsel("Höhe Eiffelturm?", "300m", "Ja, das stimmt", "Nein das stimmt nicht"))
                                            {
                                                Console.WriteLine("Glückwunsch , du hast das Spiel gewonnen");
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
           
           
        }
        public void Spielmodus2()                                                               // Methode zum Spielmodus2 
        {                                                                                       // 1 Versuch pro frage wenn falsche Antwort nächste frage
            Raetsel("Welche Ausbildung mache ich", "MATSE", "Ja, das stimmt", "Nein das stimmt nicht");
            Raetsel("Wie hoch ist die Mehrwertsteuer (vor Senkung)?", "19%", "Ja, das stimmt", "Nein das stimmt nicht");
            Raetsel("Ergebnis von 7*4+3+8*2", "47", "Ja, das stimmt", "Nein das stimmt nicht");
            Raetsel("Hauptstadt von Thüringen", "Erfurt", "Ja, das stimmt", "Nein das stimmt nicht");
            Raetsel("Wie viele Planeten hat unser Sonnensystem", "8", "Ja, das stimmt", "Nein das stimmt nicht");
            Raetsel("Rekordtorschütze der Bundesliga", "Gerd Müller", "Ja, das stimmt", "Nein das stimmt nicht");
            Raetsel("Was ist ein Sonett", "Gedichtsform", "Ja, das stimmt", "Nein das stimmt nicht");
            Raetsel("In Welcher Einheit wird elrktrischer Widerstand gemessen?", "Ohm", "Ja, das stimmt", "Nein das stimmt nicht");
            Raetsel("Chemisches Symbol für Blei", "Pb", "Ja, das stimmt", "Nein das stimmt nicht");
            Raetsel("Höhe Eiffelturm?", "300m", "Ja, das stimmt", "Nein das stimmt nicht");

        }
        public void VorbereitungEinstQuersumme(int zahl)                                        // Consolenausgabe und Nutzereingabe bevor Rechnung startet
        {
            Console.WriteLine("Berechne die einstellige Quersumme von: " + zahl);
            int nutzereingabe = Convert.ToInt32(Console.ReadLine());
            int quersumme = EinstQuersumme(zahl);
            if (quersumme == nutzereingabe)
            {
                Console.WriteLine("Richtig");
            }
            else
            {
                Console.WriteLine("Falsch");
            }
            Console.WriteLine("die einstellige Quersumme liegt bei: " + quersumme);
        }
        public int EinstQuersumme(int zahl)                                                     // Methode zum Spielmodus4
        {                                                                                       // Berechnung der einstelligen Quersumme    
            sum = 0;
            string zahlstring = zahl.ToString();         
            char[] zahlchar = zahlstring.ToCharArray();
            int[] zahlArray = new int[zahlchar.Length];
            for (int i = 0; i < zahlchar.Length; i++)
            {
                zahlArray[i] = (int)char.GetNumericValue(zahlchar[i]);
            }
            for (int i = 0; i < zahlArray.Length; i++)
            {
                sum += zahlArray[i];
            }
            if (sum >= 10)
            {
                EinstQuersumme(sum);
            }
            
            return sum;
        }
    }

}
