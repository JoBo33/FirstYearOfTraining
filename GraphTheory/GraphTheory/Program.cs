using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;


namespace GraphTheory
{
    class Program
    {
        static void Main(string[] args)
        {
            // generate array with distances between points
            int[,] distances = new int[8, 8] 
            {
                {0,0,5,1,4,0,0,0},
                {0,0,0,0,0,0,0,0},
                {0,0,0,0,1,1,0,0},
                {0,11,0,0,0,0,0,2},
                {0,6,0,6,0,0,0,0},
                {0,0,0,0,0,0,1,0},
                {0,1,0,0,7,0,0,0},
                {0,6,0,0,0,0,0,0}
            };

            Dijkstra(distances);
        }

        public static void Dijkstra(int[,] distances)
        {
            int smallestDistance = 0;
            int[] predecessor = new int[8] { 0, 0, 0, 0, 0, 0, 0, 0 };

            for (int j = 0; j < distances.GetLength(1)-1; j++)
            {

                int newSmallestDistance = int.MaxValue;
                int newSmallestDistanceIndex = int.MaxValue;

                for (int i = 0; i < distances.GetLength(1); i++)
                {
                    if (distances[0, i] != 0 && smallestDistance < distances[0, i] && newSmallestDistance > distances[0, i])
                    {
                        newSmallestDistance = distances[0, i];
                        newSmallestDistanceIndex = i;
                    }
                }

                for (int i = 1; i < distances.GetLength(1); i++)
                {

                    int newDistance = int.MaxValue;

                    if (distances[newSmallestDistanceIndex, i] != 0)
                    {
                        newDistance = newSmallestDistance + distances[newSmallestDistanceIndex, i];
                    }

                    if (newDistance < distances[0, i] || distances[0, i] == 0 && newDistance != int.MaxValue)
                    {
                        distances[0, i] = newDistance;
                        predecessor[i] = newSmallestDistanceIndex;
                    }
                }
                smallestDistance = newSmallestDistance;
            }

        }
    }
}
