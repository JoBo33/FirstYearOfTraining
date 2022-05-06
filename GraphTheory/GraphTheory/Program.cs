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
            // generate adjacency matrix for Dijkstra example
            int[,] distanceDijkstra = new int[8, 8] 
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

            Dijkstra(distanceDijkstra);

            //generate adjacency matrix for Floyd example
            int[,] distanceFloyd = new int[5, 5]
            {
                {0,4,1,int.MaxValue,int.MaxValue},
                {int.MaxValue,0,2,3,int.MaxValue},
                {1,2,0,int.MaxValue,1},
                {int.MaxValue,3,int.MaxValue,0,int.MaxValue},
                {int.MaxValue,int.MaxValue,1,8,0}
            };

            FloydWarshall(distanceFloyd);

            // generate tuple with all edges in form of Tuple<node, node, weight of edge>
            Tuple<string, string, int>[] distanceKruskal = new Tuple<string, string, int>[15]
            {
                Tuple.Create("A","B",1 ),
                Tuple.Create("A","D",6 ),
                Tuple.Create("A","E",2 ),
                Tuple.Create("B","C",5 ),
                Tuple.Create("B","E",4 ),
                Tuple.Create("B","F",3 ),
                Tuple.Create("C","G",3 ),
                Tuple.Create("D","H",5 ),
                Tuple.Create("E","F",2 ),
                Tuple.Create("E","H",2 ),
                Tuple.Create("E","I",7 ),
                Tuple.Create("F","G",4 ),
                Tuple.Create("G","J",1 ),
                Tuple.Create("H","I",3 ),
                Tuple.Create("I","J",5 )
            };

            Kruskal(distanceKruskal);


            Console.ReadLine();
        }

        public static void Dijkstra(int[,] distances)
        {
            //the smallest distance in the iteration before newSmallestDistance (line 75)
            int smallestDistance = 0;

            //save the predecessor on the same way like the "Dijkstra-table"
            int[] predecessor = new int[8] { 0, 0, 0, 0, 0, 0, 0, 0 };

            //search the smallest edge wich wasn't used to far
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

                //calculate all new distances
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


        public static void FloydWarshall(int[,] distance)
        {
            // iterate threw rows
            for (int i = 0; i < distance.GetLength(1); i++)
            {

                // iterate threw columns
                for (int j = 0; j < distance.GetLength(1); j++)
                {

                    // choose iterative every node
                    for (int k = 0; k < distance.GetLength(1); k++)
                    {

                        // Is there a possible connection
                        if (distance[i, k] + distance[k, j] > 0)
                        {

                            // check if the way with the specific node is shorter than without the node
                            if (distance[i, j] > distance[i, k] + distance[k, j])
                            {
                                distance[i, j] = distance[i, k] + distance[k, j];
                            }
                        }
                    }
                }
            }
        }

        public static void Kruskal(Tuple<string,string,int>[] distance)
        {
            // maxIterations should be number of nodes - 1 but here it is the number of edges
            int maxIterations = distance.GetLength(0);

            //initialise a list for the minimal spanning tree
            List<Tuple<string,string,int>> spanningTree = new List<Tuple<string, string, int>> { };

            for (int j = 0; j < maxIterations; j++)
            {
                //search in every iteration the smallest edge
                int smallestDistance = int.MaxValue;
                int index = int.MaxValue;

                for (int i = 0; i < distance.GetLength(0); i++)
                {

                    if (smallestDistance > distance[i].Item3)
                    {
                        smallestDistance = distance[i].Item3;
                        index = i;
                    }
                }

                //check if the smallest edge builds a circle
                bool buildingCircle = false;

                //checks if both nodes already in the spanning tree
                // proplem: it can be a conntectoredge
                for (int i = 0; i < spanningTree.Count; i++)
                {

                    if (spanningTree[i].Item1 == distance[index].Item1 || spanningTree[i].Item2 == distance[index].Item1 && !buildingCircle)
                    {
                        for (int k = 0; k < spanningTree.Count; k++)
                        {

                            if (spanningTree[k].Item1 == distance[index].Item2 || spanningTree[k].Item2 == distance[index].Item2)
                            {
                                buildingCircle = true;
                                break;
                            }
                        }

                    }
                }

                // add edge to the spanning tree
                if (!buildingCircle)
                {
                    spanningTree.Add(distance[index]);
                }

                //remove smallest edge from graph array
                List<Tuple<string, string, int>> tmp = new List<Tuple<string, string, int>>(distance);
                tmp.RemoveAt(index);
                distance = tmp.ToArray();
            }
        }
    }
}
