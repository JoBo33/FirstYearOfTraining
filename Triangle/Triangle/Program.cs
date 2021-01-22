using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triangle
{
    class Program
    {
        static void Main(string[] args)
        {
            Triangle triangle = new Triangle();
            triangle.WhatIsGiven();
            Console.ReadLine();
        }
        
    }
    class Triangle
    {
        double a;
        double b;
        double c;
        double angle_a;
        double angle_b;
        double angle_c;
        double scope;
        double area;
        double s1;
        double s2;
        double a1;
        double a2;
        double missing;
        double sum;
        double sin;

        public void WhatIsGiven()
        {
            Console.WriteLine("pick 3 out of a, b, c, angle_a, angle_b, angle_c" + "(a, b, c = Site; angle_a, angle_b, angle_c = Angle");
            Console.WriteLine("What is given?");
            string r = Console.ReadLine();
            if (r == "abc" || r == "bca" || r == "cba" || r == "acb" || r == "cab")
            {
                Console.WriteLine("a: ");
                a = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("b: ");
                b = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("c: ");
                c = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Possible inputs: Area, Scope, AngleA, AngleB, AngleC");
                Console.WriteLine("What you want to know?");
                string t = Console.ReadLine();

                switch (t)
                {
                    case "Area":
                        Area();
                        Console.WriteLine(area);
                        break;
                    case "Scope":
                        Scope();
                        Console.WriteLine(scope);
                        break;
                    case "AngleA":
                        AngleAcos();
                        Console.WriteLine(angle_a);
                        break;
                    //case "AngleB":
                    //    AngleBcos();
                    //    Console.WriteLine(angle_b);
                    //    break;
                    case "AngleC":
                        AngleCcos();
                        Console.WriteLine(angle_c);
                        break;
                    default:
                        Console.WriteLine("False input");
                        break;
                }
               
            }
            else if (r == "abangle_c" || r == "acangle_b" || r == "bcangle_a")
            {
  
                 Console.WriteLine("Site1: ");
                 s1 = Convert.ToDouble(Console.ReadLine());
                 Console.WriteLine("Site2: ");
                 s2 = Convert.ToDouble(Console.ReadLine());
                 Console.WriteLine("angle: ");
                 a1 = Convert.ToDouble(Console.ReadLine());
                 MissingSite();
                 Console.WriteLine("Possible inputs: Area, Scope, Missing Site, Missing Angles");
                 Console.WriteLine("What you want to know?");
                 string help = Console.ReadLine();
                 switch (help)
                 {
                     case "Area":
                         s1 = a;
                         s2 = b;
                         missing = c;
                         Area();
                         Console.WriteLine(area);
                         break;
                     case "Scope":
                         s1 = a;
                         s2 = b;
                         missing = c;
                         Scope();
                         Console.WriteLine(scope);
                         break;
                     case "Missing Site":
                         Console.WriteLine(missing);
                         break;
                    case "Missing Angles":
                        MissingSite();
                        Console.WriteLine("First angle");
                        a = s1;
                        c = missing;     
                        AngleAsin();
                        
                        Console.WriteLine(DegToRad(angle_a));
                        Console.WriteLine("Second angle");
                        a2 = angle_a *(180/Math.PI);
                        Anglesum();
                        Console.WriteLine(sum);
                        break;
                     default:
                         Console.WriteLine("False input");
                         break;
                 }
                   
                
            }
            else if (r == "abangle_a" || r == "abangle_b" || r == "acangle_a" || r == "acangle_c" || r == "bcangle_b" || r == "bcangle_c") {
                
                Console.WriteLine("Site1: ");
                s1 = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Site2: ");
                s2 = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("angle: ");
                a1 = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Possible inputs: Area, Scope, Missing Site, Missing Angles");
                Console.WriteLine("What you want to know?");
                MissingSite();
                string help = Console.ReadLine();
                switch (help)
                {
                    case "Area":
                        s1 = a;
                        s2 = b;
                        missing = c;
                        Area();
                        Console.WriteLine(area);
                        break;
                    case "Scope":
                        s1 = a;
                        s2 = b;
                        missing = c;
                        Scope();
                        Console.WriteLine(scope);
                        break;
                    case "Missing Site":
                        a = s1;
                        c = s2;
                        AngleAsin();

                        Console.WriteLine(missing);
                        break;
                    case "Missing Angles":
                        Console.WriteLine("First angle");
                        a = s1;
                        c = s2;
                        AngleAsin();
                        Console.WriteLine(DegToRad(angle_a));
                        Console.WriteLine("Second angle");
                        a2 = angle_a * (180 / Math.PI);
                        Anglesum();
                        Console.WriteLine(sum);
                   //     a = s1;
                   //     b = s2;
                   //     angle_a = a1;
                   //     
                   //     Console.WriteLine("First angle");
                   //     AngleAsin();
                   //     Console.WriteLine(angle_b);
                   //     Console.WriteLine("Second angle");
                   //     Console.WriteLine(DegToRad(angle_b));
                   //     a2 = angle_b * (180 / Math.PI);
                   //     Anglesum();
                   //     Console.WriteLine(sum);
                        break;

                }
            }
            else if (r == "aangle_aangle_b" || r == "aangle_aangle_c" || r == "aangle_bangle_c" || r == "bangle_aangle_b" || r == "bangle_aangle_c" || r == "bangle_bangle_c" || r == "cangle_aangle_b" || r == "cangle_aangle_c" || r == "cangle_bangle_c")
            {
                Console.WriteLine("Site: ");
                s1 = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Angle1: ");
                a1 = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Angle2: ");
                a2 = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Possible inputs: Area, Scope, Missing Sites, Missing Angle");
                Console.WriteLine("What you want to know?");
                string help = Console.ReadLine();
                switch (help)
                {
                    case "Area":
                        s1 = a;
                        s2 = b;
                        missing = c;
                        Area();

                        Console.WriteLine(area);
                        break;
                    case "Scope":
                        s1 = a;
                        s2 = b;
                        missing = c;
                        Scope();
                        Console.WriteLine(scope);
                        break;
                    case "Missing Sites":
                        Anglesum();
                        Console.WriteLine("Missing sites");
                        Console.WriteLine("First site");
                        angle_a = a2;
                        Sinus();
                        Console.WriteLine(sin);
                        Console.WriteLine("Second Site");
                        angle_a = sum;
                        Sinus();
                        Console.WriteLine(sin);

                        break;
                    case "Missing Angle":
                        Console.WriteLine("Missing angle:");
                        Anglesum();
                        Console.WriteLine(sum);
                        break;
                    default:
                        Console.WriteLine("False input");
                        break;

                }
            }
            else if (r == "angle_aangle_bangle_c")
            {
                Console.WriteLine("Site calculation only based on angle not possible");
            }
            else
            {
                Console.WriteLine("False input");
            }

        }
        public double Area()   // Oberfläche
        {
            double s = (Scope() / 2);
            area = Math.Sqrt(s * (s - a) * (s - b) * (s - c));
            return area;
        }
        public double Scope()   // Umfang
        {
            scope = (a + b + c);
            return scope;
        }
        public double MissingSite()   // Fehlende Seite wenn 2 gegeben
        {
            missing = Math.Sqrt(s1 * s1 + s2 * s2 - 2 * s1 * s2 * Math.Cos(a1));
            return missing;
        }
        public double AngleAcos()   //Winkelberechnen wenn 3 seiten, Kosinussatz
        {
            double one = (a * a - b * b - c * c);
            double two = -2 * b * c;
            double three = one / two;
            angle_a = Math.Acos(three);
            return angle_a;
        }
        public double AngleBsin()   //Winkelberechnen wenn 3 seiten, Kosinussatz
        {
            angle_b = Math.Asin((b * b - a * a - c * c) / (-2 * b * c));
            return angle_b;
        }
        public double AngleCcos()    //Winkelberechnen wenn 3 seiten, Kosinussatz
        {
            angle_c = Math.Acos((c * c - b * b - a * a) / (-2 * a * b));
            return angle_c;
        }
        public double AngleAsin()  // Winkelberechnen wenn 2 seiten, Sinussatz
        {
            angle_a = Math.Asin(((a1 * (Math.PI / 180)) / c) * a);
            return angle_a;
        }
        
        public double Anglesum()  // Winkelsumme bei grad
        {
            sum = 180 - a1 - a2;
            return sum;
            
        }
        public static double DegToRad(double angle_a)  // umrenen von bogenmaß zu Grad
        {
            angle_a = angle_a * (180 / Math.PI);
            return angle_a;
        }
        public double Sinus()   // 2 Winkel
        {
            sin = (s1 / a1) * angle_a;
            return sin;
        }
    }
}
