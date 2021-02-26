using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixMultiplikation
{
    class Program
    {
        static void Main(string[] args)
        {
            double[,] matrix1 = EnterMatrix("ersten");

            Console.WriteLine("");

            double[,] matrix2 = EnterMatrix("zweiten");

            if (matrix1.GetLength(0) != matrix2.GetLength(1))
            {
                Console.WriteLine("Matrizen können nicht multipliziert werden.");
                Console.ReadKey();
                return;
            }
            double[,] resultMatrix = Multiplikation(matrix1, matrix2);

            Console.WriteLine("");
            for (int i = 0; i < resultMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < resultMatrix.GetLength(1); j++)
                {
                    Console.Write("Das Ergebnis an der Stelle (" + i + "," + j + ") lautet: " + resultMatrix[i, j] + "\n");
                }
            }
 
            Console.ReadKey();
        }

        private static double[,] EnterMatrix(string a)
        {
            Console.WriteLine("Geben sie die Anzahl der Zeilen der " + a+ " Matrix an:");
            int matrix1Rows = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Geben sie die Anzahl der Spalten der " + a+ " Matrix an:");
            int matrix1Columns = Convert.ToInt32(Console.ReadLine());

            double[,] matrix1 = new double[matrix1Rows, matrix1Columns];

            for (int i = 0; i < matrix1Rows; i++)
            {
                for (int j = 0; j < matrix1Columns; j++)
                {
                    double input;
                    Console.Write("Geben sie den Eintrag für ({0},{1}) an: ", i, j);
                    while (!double.TryParse(Console.ReadLine(), out input))
                    {
                        Console.Write("Geben sie den Eintrag für({ 0},{ 1}) an: ", i, j);
                    }
                    matrix1[i, j] = input;
                }
            }
            return matrix1;
        }

        public static double[,] Multiplikation(double[,] matrix1, double[,] matrix2)
        {
            double[,] matrixResult = new double [ matrix1.GetLength(0), matrix2.GetLength(1) ];
            for (int row = 0; row < matrix1.GetLength(0); row++)
            {
                for (int column = 0; column < matrix2.GetLength(1); column++)
                {
                    for (int rowB = 0; rowB < matrix2.GetLength(0); rowB++)
                    {
                        matrixResult[row, column] += matrix1[row, rowB] * matrix2[rowB, column];
                    }
                }
            }
            return matrixResult;
        }
    }
}
