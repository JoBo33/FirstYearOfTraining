using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    class Program
    {
        static void Main(string[] args)
        {
            Adventure adventure = new Adventure();
            adventure.Story();
            Console.ReadLine();
        }

        
    }
    class Adventure
    {
        string richtig = "Korrekt du kannst weiter gehen";
        string falsch = "Du hast leider verloren";
        public void Story()
        {
            Console.WriteLine("Willkommen beim der heutigen Escape Room");
            Console.WriteLine("Um zu entkommen wird es immer 2 mögliche Wege (Rätsel eins oder zwei) geben.");
            Console.WriteLine("Du musst dich jeweils für ein Rätsel entscheiden. Wähle Weise, es gibt schwere und einfache Rätsel die zu lösen sind.");
            Console.WriteLine("Wenn du auch nur eins nicht lösen kannst hast du verloren!!!");
           // Console.WriteLine("Gehe nun in Richtung Norden.");
            Console.WriteLine("Info: Es öffnen sich 2 Tore. Welchen Raum möchtest du betreten?");
            
            if (Raum1(Console.ReadLine()))
            {
                Console.WriteLine("Sehr gut wie ich sehe hast du das erste Rätsel geschafft. Gut das es nur die Start frage war.\n" +
                    "mal gucken ob du das nächste auch noch schaffst.");
                Console.WriteLine("Info: Kurze Zeit später öffnen sich 2 Türen. Wo möchtest du lang?");
                if (Raum2(Console.ReadLine()))
                {
                    Console.WriteLine("Gut Gut ich bin gespannt ob wir uns nochmal sehen.");
                    Console.WriteLine("Info: Welcher Raum soll dein dritter Raum sein?");
                    if (Raum3(Console.ReadLine()))
                    {
                        Console.WriteLine("Sehr gut. Noch 2 dann hast es geschafft.");
                        Console.WriteLine("Info: Während dessen öffnet sich vor dir eine Tür (eins) und neben dir ein Schacht (zwei)");
                        if (Raum4(Console.ReadLine()))
                        {
                            Console.WriteLine("Ich habe nicht damit gerechnet, dass du soweit kommst.");
                            Console.WriteLine("Drücke nun entweder den rechten (eins) oder den linken (zwei) Knopf um das letzte Rätsel zu erhalten.");
                            if (Raum5(Console.ReadLine()))
                            {
                                Console.WriteLine("Glückwunsch du hast alle Rätsel richtig gelöst und bist entkommen.");
                            }
                        }
                    }
                }
            }
        }
        public bool Raum1(string entscheidung)
        {
            if ("eins" == entscheidung)
            {
                Console.WriteLine("Was ist x bei x = 5+2+7*3");
                string nutzereingabe = Console.ReadLine();
                if ("28" == nutzereingabe)
                {
                    Console.WriteLine(richtig);
                    return true;
                }
                else
                {
                    // Console.WriteLine("Versuche es erneut");
                    // Kreuzung1("links");
                    Console.WriteLine(falsch);
                }
            }
            else
            {
                Console.WriteLine("Was ist x bei x = 7+(5+2)^2*8-4");
                string nutzereingabe = Console.ReadLine();
                if ("395"== nutzereingabe)
                {
                    Console.WriteLine(richtig);
                    return true;
                }
                else
                {
                    Console.WriteLine(falsch);
                }
            }
            return false;
        }
        public bool Raum2(string entscheidung)
        {
            Console.Clear();
            if ("eins" == entscheidung)
            {
                Console.WriteLine("Zwei Brüder stehen vor einer offenen Regentonne und sehen hinein. Der Eine behauptet, \n" +
                    "dass die Regentonne noch mindestens halb voll sei.Der Andere behauptet, die Tonne sei weniger als halb voll. \n" +
                    "Beschreiben Sie einen Weg, wie die beiden herausfinden können, wer von ihnen Recht hat, ohne irgendwelche Hilfsmittel zu benutzen.");
                string nutzereingabe = Console.ReadLine();
                if ( nutzereingabe.Contains("kippen")|| nutzereingabe.Contains("Boden"))
                {
                    Console.WriteLine(richtig);
                    return true;
                }
                else
                {
                    Console.WriteLine(falsch);
                }
            }
            else
            {
                Console.WriteLine("Fünf Freunde haben gegeneinander ein Wettrennen gelaufen. Nach der anschließenden durchzechten Nacht \n" +
                    "können sie sich jedoch nur noch an wenige Details erinnern: " +
                    "Tim ist vor Lukas im Ziel eingelaufen. \n" +
                    "Janina war früher als Tim, \n" +
                    "Franz oder Lukas im Ziel. \n" +
                    "Anna ist vor Janina oder Franz im Ziel angekommen. \n" +
                    "Tim war vor Anna im Ziel oder Lukas war vor Anna im Ziel. \n" +
                    "Franz war früher als Tim im Ziel. \n" +
                    "Das Wettrennen Außerdem wissen sie noch, dass keine zwei von ihnen gleichzeitig im Ziel eingelaufen sind. \n" +
                    "Finden Sie heraus, in welcher Reihenfolge die fünf eingelaufen sind!");
                Console.WriteLine("Eingabevorlage: 1. name1, 2. name2, 3. ...");
                string nutzereingabe = Console.ReadLine();
                if ("1. Franz, 2. Tim, 3. Anna, 4. Janina, 5. Lukas" == nutzereingabe)
                {
                    Console.WriteLine(richtig);
                    return true;
                }
                else
                {
                    Console.WriteLine(falsch);
                }
            }
            return false;
        }
        public bool Raum3(string entscheidung)
        {
            Console.Clear();
            if ("eins" == entscheidung)
            {
                Console.WriteLine("In wie viele (nicht zwangsläufig gleich große) Teile kann man eine Kreisfläche mit vier geraden Linien maximal teilen.");
                string nutzereingabe = Console.ReadLine();
                if ("11" == nutzereingabe)
                {
                    Console.WriteLine(richtig);
                    return true;
                }
                else
                {
                    Console.WriteLine(falsch);
                }
            }
            else
            {
                Console.WriteLine("Als ein Mathematikprofessor eines Tages von einer Dienstreise zurück kam, musste er feststellen, \n" +
                    "dass er vergessen hatte, seine Kuckucksuhr aufzuziehen. Diese ist also stehengeblieben. \n" +
                    "Da es die einzige Uhr des Matheprofessors war, hatte er keine Möglichkeit, die Uhr direkt zu stellen. \n" +
                    "Also ging er zur Kirche des Dorfes und schaute auf die Kirchturmuhr. Als er zurück kam, konnte er seine Kuckucksuhr genau stellen. \n" +
                    "Finden Sie heraus, wie er das gemacht hat! Dazu ist zu sagen, dass der Professor in der Lage ist, mit einer konstanten Geschwindigkeit zu gehen.");
                Console.WriteLine("Gebe eine Gleichung mit folgenden Parametern an: \n" +
                    "beliebige Zeit (t1), Uhrzeit bei der Kirche (t2), Kuckucksuhrzeit als er wieder Zuhause war (t3)\n" +
                    "Beginne mit: Uhrzeit=... (nutze keine Leerzeichen)");
                string nutzereingabe = Console.ReadLine();
                if ("Uhrzeit=t2+(t3-t1)/2" == nutzereingabe)
                {
                    Console.WriteLine(richtig);
                    return true;
                }
                else
                {
                    Console.WriteLine(falsch);
                }
            }
            return false;
        }
        public bool Raum4(string entscheidung)
        {
            Console.Clear();
            if ("eins" == entscheidung)
            {
                Console.WriteLine("Ein Springer soll auf einem Schachbrett in der Ecke unten rechts starten.\n" +
                    "Beschreiben Sie, wie er seinen Weg gestalten muss, wenn er alle Felder genau einmal besucht haben und am Ende in der Ecke oben links stehen will!");
                string nutzereingabe = Console.ReadLine();
                if (nutzereingabe.Contains("nicht möglich") || nutzereingabe.Contains("geht nicht"))
                {
                    Console.WriteLine(richtig);
                    return true;
                }
                else
                {
                    Console.WriteLine(falsch);
                }
            }
            else
            {
                Console.WriteLine("Auf einem Tisch sind vier handelsübliche Würfel aufeinander gestapelt. Auf der oberen Fläche sind zwei Augen zu sehen. \n" +
                    "Bestimmen Sie die Summe aller nicht sichtbaren Augen!");
                string nutzereingabe = Console.ReadLine();
                if ("26" == nutzereingabe)
                {
                    Console.WriteLine(richtig);
                    return true;
                }
                else
                {
                    Console.WriteLine(falsch);
                }
            }
            return false;
        }
        public bool Raum5(string entscheidung)
        {
            Console.Clear();
            if ("eins" == entscheidung)
            {
                Console.WriteLine("Zwei Drogenfahnder halten eines Tages einen Drogentransporter an. \n" +
                    "Dieser hat 65 Säcke mit Mehl geladen. Sie wissen, dass sich in genau einem dieser Säcke Drogen befinden. \n" +
                    "Diese sehen aus wie Mehl, schmecken wie Mehl und lassen sich nur über ein aufwändiges \n" +
                    "Testverfahren identifizieren, das auch geringste Konzentrationen des Drogenstoffes noch nachweisen kann.");
                Console.WriteLine("Wie viele Tests brauchen die Fahnder im worst case, beim best möglichen Lösungsverfahren?");
                Console.WriteLine("Schreibe: [Anzahl] Tests");
                string nutzereingabe = Console.ReadLine();
                if ("7 Tests" == nutzereingabe)
                {
                    Console.WriteLine(richtig);
                    return true;
                }
                else
                {
                    Console.WriteLine(falsch);
                }
            }
            else
            {
                Console.WriteLine("Vor dir steht eine Kiste. In dieser Kiste liegen Säcke. In jedem dieser Säcke befindet sich die \n" +
                    "gleiche Anzahl an Goldmünzen. Insgesamt sind zwischen 150 und 200 Goldmünzen in der Kiste. \n" +
                    "Es ist mehr als ein Sack in der Kiste und in jedem Sack ist mehr als eine Münze. Wenn ich dir die Gesamtanzahl \n" +
                    "der Münzen nennen würde, dann könnten Sie mir genau sagen, wie viele Säcke in der Kiste sind und wie viele Münzen\n" +
                    "in einem Sack sind. Wie viele Goldmünzen sind insgesamt in der Kiste, wie viele Säcke sind in der Kiste \n" +
                    "und wie viele Münzen sind in jedem Sack?");
                Console.WriteLine("Schreibe: Säcke: [Anzahl], Münzen pro Sack: [Münzen pro Sack]");
                string nutzereingabe = Console.ReadLine();
                if ("Säcke: 13, Münzen pro Sack: 13" == nutzereingabe)
                {
                    Console.WriteLine(richtig);
                    return true;
                }
                else
                {
                    Console.WriteLine(falsch);
                }
            }
            return false;
        }
    }
}

