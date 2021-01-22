using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triangle2
{
    class Program
    {
        static void Main(string[] args)
        {
            Triangle triangle = new Triangle();
            triangle.Input();
            Console.ReadLine();
        }
    }
    class Triangle
    {
        double angle;
        double angle1;
        double angle2;
        double angle3;
        double site1;
        double site2;
        double site3;
        public void Input()
        {
            Console.WriteLine("given Inputs:");
            Console.WriteLine("a: ");
            double a = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("b: ");
            double b = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("c: ");
            double c = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("alpha: ");
            double alpha = Convert.ToDouble(Console.ReadLine()) * Math.PI / 180;
            Console.WriteLine("beta: ");
            double beta = Convert.ToDouble(Console.ReadLine()) * Math.PI / 180;
            Console.WriteLine("gamma: ");
            double gamma = Convert.ToDouble(Console.ReadLine()) * Math.PI / 180;

            if (b == 0 && c == 0 && a == 0 && alpha != 0 && beta != 0 && gamma != 0)
            {
                Console.WriteLine("Site calculation only based on angle is not possible");
            }
            else
            {
                if (a == 0)
                {
                    Console.WriteLine("Calculate a");
                    Console.WriteLine("a:");
                    if (b != 0 && c != 0 && (alpha != 0 || beta != 0 || gamma != 0))
                    {
                        site1 = b;
                        site2 = c;
                        if (alpha != 0)
                        {
                            angle1 = alpha;
                            MissingSite();

                        }
                        else if (beta != 0)
                        {
                            angle1 = beta;
                        }
                        else if (gamma != 0)
                        {
                            angle1 = gamma;                   
                        }
                        Sinus();
                        angle2 = RadToDeg(angle2);
                        Anglesum();
                    }
                    else if (alpha != 0 && beta != 0 && (b != 0 || c != 0))
                    {
                        if (b != 0)
                        {
                            angle1 = beta;
                            angle3 = alpha;
                            site1 = b;
                        }
                        else if (c != 0)
                        {
                            angle1 = beta;
                            angle2 = alpha;
                            Anglesum();
                            site1 = c;
                            angle1 = angle3;
                        }
                    }
                    else if (alpha != 0 && gamma != 0 && (b != 0 || c != 0))
                    {
                        angle1 = gamma;
                        angle2 = alpha;
                        Anglesum();
                        if (b != 0)
                        {
                            angle1 = angle3;
                            site1 = b;
                        }
                        else if (c != 0)
                        {
                            angle3 = alpha;
                            site1 = c;                        
                        }
                    }
                    else if (beta != 0 && gamma != 0 && (b != 0 || c != 0))
                    {
                        angle1 = beta;
                        angle2 = gamma;
                        Anglesum();
                        if (b != 0)
                        {
                            site1 = b;                           
                        }
                        else if (c != 0)
                        {
                            angle1 = gamma;                           
                            site1 = c;
                        }
                    }
                    Sinus2();
                    Console.WriteLine(site3);
                }
                if (b == 0)
                {
                    Console.WriteLine("Calculate b");
                    Console.WriteLine("b:");
                    if (a != 0 && c != 0 && (alpha != 0 || beta != 0 || gamma != 0))
                    {
                        site1 = a;
                        site2 = c;
                        if (alpha != 0)
                        {
                            angle1 = alpha;
                            Sinus();
                            angle2 = RadToDeg(angle2); ;
                            Anglesum();
                            angle2 = angle3;
                        }
                        else if (beta != 0)
                        {
                            angle1 = beta;
                            MissingSite();
                        }
                        else if (gamma != 0)
                        {

                        }
                    }
                    else if (alpha != 0 && beta != 0 && (a != 0 || c != 0))
                    {
                        if (a != 0)
                        {

                        }
                        else if (c != 0)
                        {
                            angle1 = beta;
                            angle2 = alpha;
                            Anglesum();
                            site1 = c;
                            angle1 = angle3;
                            angle2 = beta;
                        }
                    }
                    else if (alpha != 0 && gamma != 0 && (a != 0 || c != 0))
                    {
                        if (a != 0)
                        {
                            angle1 = gamma;
                            angle2 = alpha;
                            Anglesum();
                            angle1 = alpha;
                            site1 = a;
                        }
                        else if (c != 0)
                        {
                            angle1 = gamma;
                            angle2 = alpha;
                            Anglesum();
                            angle2 = angle3;
                            site1 = c;
                        }
                    }
                    else if (beta != 0 && gamma != 0 && (a != 0 || c != 0))
                    {
                        if (a != 0)
                        {
                            angle1 = beta;
                            angle2 = gamma;
                            Anglesum();
                            angle1 = angle3;
                            angle3 = beta;
                            site1 = a;
                        }
                        else if (c != 0)
                        {                          
                            angle1 = gamma;
                            angle3 = beta;
                            site1 = c;
                        }
                    }
                    Sinus2();
                    Console.WriteLine(site3);
                }
                if (c == 0)
                {
                    Console.WriteLine("Calculate c");
                    Console.WriteLine("c:");
                    if (a != 0 && b != 0 && (alpha != 0 || beta != 0 || gamma != 0))
                    {
                        if (alpha != 0)
                        {
                            angle1 = alpha;
                            site1 = a;
                            site2 = b;
                        }
                        else if (beta != 0)
                        {
                            angle1 = beta;
                            site2 = a;
                            site1 = b;
                        }
                        else if (gamma != 0)
                        {
                            site1 = a;
                            site2 = b;
                            angle1 = gamma;
                            MissingSite();
                        }
                        Sinus();
                        angle2 = RadToDeg(angle2);
                        Anglesum();
                    }
                    else if (alpha != 0 && beta != 0 && (a != 0 || b != 0))
                    {
                        if (a != 0)
                        {

                        }
                        else if (b != 0)
                        {
                            angle1 = beta;
                            angle2 = alpha;
                            site1 = b;
                            Anglesum();
                        }
                    }
                    else if (alpha != 0 && gamma != 0 && (a != 0 || b != 0))
                    {
                        if (a != 0)
                        {
                            angle1 = alpha;
                            angle2 = gamma;
                            site1 = a;
                        }
                        else if (b != 0)
                        {
                            angle1 = gamma;
                            angle2 = alpha;
                            Anglesum();
                            angle1 = angle3;
                            angle2 = gamma;
                            site1 = b;                          
                        }
                    }
                    else if (beta != 0 && gamma != 0 && (a != 0 || b != 0))
                    {
                        angle1 = beta;
                        angle2 = gamma;
                        Anglesum();
                        if (a != 0)
                        {
                            angle1 = angle3;
                            angle3 = gamma;
                            site1 = a;
                        }
                       
                        else if (b != 0)
                        {
                            site1 = b;
                            angle3 = gamma;
                        }
                    }
                    Sinus2();
                    Console.WriteLine(site3);
                }
                if (alpha == 0)
                {
                    Console.WriteLine("Calculate alpha");
                    Console.WriteLine("alpha:");
                    if (a != 0 && b != 0 && c != 0)
                    {
                        site1 = a;
                        site2 = b;
                        site3 = c;
                        Kosinus();
                        Console.WriteLine(RadToDeg(angle2));
                    }
                    else if (beta != 0 && gamma != 0 && (a != 0 || b != 0 || c != 0))
                    {
                        angle1 = beta;
                        angle2 = gamma;
                        Anglesum();
                        Console.WriteLine(angle3);
                    }
                    else if (a != 0 && b != 0 && (beta != 0 || gamma != 0))
                    {
                        if (beta != 0)
                        {
                            angle1 = beta;
                            site2 = a;
                            site1 = b;
                            Sinus();
                            Console.WriteLine(RadToDeg(angle2));
                        }
                        else if (gamma != 0)
                        {
                            site1 = a;
                            site2 = b;
                            angle1 = gamma;
                            MissingSite();
                            site1 = site3;
                            site2 = a;
                            Sinus();
                            RadToDeg(angle2);
                            angle1 = gamma;
                            angle2 = angle;
                            Anglesum();
                            Console.WriteLine(angle3);
                        }
                    }
                    else if (a != 0 && c != 0 && (beta != 0 || gamma != 0))
                    {
                        site1 = a;
                        site2 = c;
                        if (beta != 0)
                        {
                            angle1 = beta;
                            MissingSite();
                            Kosinus();
                            RadToDeg(angle2);
                            Console.WriteLine(angle);
                        }
                        else if (gamma != 0)
                        {
                            angle1 = gamma;
                            Sinus();
                            RadToDeg(angle2);
                            angle2 = angle;
                            Anglesum();
                            Console.WriteLine(angle3);
                        }
                    }
                    else if (b != 0 && c != 0 && (beta != 0 || gamma != 0))
                    {
                        site1 = b;
                        site2 = c;
                        if (beta != 0)
                        {
                            angle1 = beta;
                        }
                        else if (gamma != 0)
                        {
                            angle1 = gamma;
                        }
                        Sinus();
                        RadToDeg(angle2);
                        angle2 = angle;
                        Anglesum();
                        Console.WriteLine(angle3);
                    }
                }
                if (beta == 0)
                {
                    Console.WriteLine("Calculate beta");
                    Console.WriteLine("beta:");
                    if (a != 0 && b != 0 && c != 0)
                    {
                        site1 = b;
                        site2 = a;
                        site3 = c;
                        Kosinus();
                        RadToDeg(angle2);
                        Console.WriteLine(angle);
                    }
                    else if (alpha != 0 && gamma != 0 && (a != 0 || b != 0 || c != 0))
                    {
                        angle1 = gamma;
                        angle2 = alpha;
                        Anglesum();
                        Console.WriteLine(angle3);
                    }
                    else if (a != 0 && b != 0 && (alpha != 0 || gamma != 0))
                    {
                        site1 = a;
                        site2 = b;
                        if (alpha != 0)
                        {
                            angle1 = alpha;
                        }
                        else if (gamma != 0)
                        {
                            angle1 = gamma;
                            MissingSite();
                            site1 = site3;
                            site2 = a;
                        }
                        Sinus();
                        RadToDeg(angle2);
                        Console.WriteLine(angle);
                    }
                    else if (a != 0 && c != 0 && (alpha != 0 || gamma != 0))
                    {
                        site1 = a;
                        site2 = c;
                        if (alpha != 0)
                        {
                            angle1 = alpha;
                            Sinus();
                            RadToDeg(angle2);
                            angle2 = angle;
                            Anglesum();
                            Console.WriteLine(angle3);
                        }
                        else if (gamma != 0)
                        {
                            angle1 = gamma;
                            Sinus();
                            RadToDeg(angle2);
                            Console.WriteLine(angle);
                        }
                    }
                    else if (b != 0 && c != 0 && (alpha != 0 || gamma != 0))
                    {
                        site1 = b;
                        site2 = c;
                        if (alpha != 0)
                        {
                            angle1 = alpha;
                            MissingSite();
                            site1 = site3;
                            site2 = b;
                            Sinus();
                        }
                        else if (gamma != 0)
                        {
                            angle1 = gamma;
                        }
                        Sinus();
                        RadToDeg(angle2);
                        Console.WriteLine(angle);
                    }
                }
                if (gamma == 0)
                {
                    Console.WriteLine("Calculate gamma");
                    Console.WriteLine("gamma:");
                    if (a != 0 && b != 0 && c != 0)
                    {
                        site1 = c;
                        site2 = a;
                        site3 = b;
                        Kosinus();
                        RadToDeg(angle2);
                        Console.WriteLine(angle);
                    }
                    else if (beta != 0 && alpha != 0 && (a != 0 || b != 0 || c != 0))
                    {
                        angle1 = beta;
                        angle2 = alpha;
                        Anglesum();
                        Console.WriteLine(angle3);
                    }
                    else if (a != 0 && b != 0 && (alpha != 0 || beta != 0))
                    {
                        if (alpha != 0)
                        {
                            angle1 = alpha;
                            site1 = a;
                            site2 = b;
                           
                        }
                        else if (beta != 0)
                        {
                            angle1 = beta;
                            site2 = a;
                            site1 = b;
                        }
                        Sinus();
                        RadToDeg(angle2);
                        angle2 = angle;
                        Anglesum();
                        Console.WriteLine(angle3);
                    }
                    else if (a != 0 && c != 0 && (alpha != 0 || beta != 0))
                    {
                        site1 = a;
                        site2 = c;
                        if (alpha != 0)
                        {
                            angle1 = alpha;
                            Sinus();
                            RadToDeg(angle2);
                            Console.WriteLine(angle);
                        }
                        else if (beta != 0)
                        {
                            angle1 = beta;
                            MissingSite();
                            Kosinus();
                            RadToDeg(angle2);
                            angle2 = angle;
                            Anglesum();
                            Console.WriteLine(angle3);
                        }
                    }
                    else if (b != 0 && c != 0 && (alpha != 0 || beta != 0))
                    {
                        site1 = b;
                        site2 = c;
                        if (alpha != 0)
                        {                           
                            angle1 = alpha;
                            MissingSite();
                            site1 = site3;
                            site2 = b;
                            Sinus();
                            RadToDeg(angle2);
                            angle2 = angle;
                            Anglesum();
                            Console.WriteLine(angle3);
                        }
                        else if (beta != 0)
                        {
                            angle1 = beta;
                            Sinus();
                            RadToDeg(angle2);
                            Console.WriteLine(angle);
                        }
                    }
                }

            }
        }
        public double RadToDeg(double angle1)  // umrenen von bogenmaß zu Grad
        {
            angle = angle1 * (180 / Math.PI);
            return angle;
        }
        public double Anglesum()  // Winkelsumme bei grad
        {
            angle3 = 180 - angle1 - angle2;
            return angle3;
        }
        public double Sinus()  // Winkelberechnen wenn 2 seiten, Sinussatz
        {
            angle2 = Math.Asin(((Math.Sin(angle1)) / site1) * site2);
            return angle2;
        }
        public double Sinus2()  // Winkelberechnen wenn 2 seiten, Sinussatz
        {
            site3 = ((site1 / (Math.Sin(angle1)))) * (Math.Sin(angle3));
            return site3;
        }
        public double MissingSite()   // Fehlende Seite wenn 2 gegeben, Kosinussatz
        {
            site3 = Math.Sqrt(site1 * site1 + site2 * site2 - 2 * site1 * site2 * Math.Cos(angle1));
            return site3;
        }
        public double Kosinus()
        {
            angle2 = Math.Acos((site1 * site1 - site2 * site2 - site3 * site3) / (-2 * site2 * site3));
            return angle2;
        }
    }
}
