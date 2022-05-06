using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllTogether
{
     public class GraphTheory
    {

        public static void Dijkstra(float[,] distances)
        {
            //the smallest distance in the iteration before newSmallestDistance (line 75)
            float smallestDistance = 0;

            //save the predecessor on the same way like the "Dijkstra-table"
            int[] predecessor = new int[8] { 0, 0, 0, 0, 0, 0, 0, 0 };

            //search the smallest edge wich wasn't used to far
            for (int j = 0; j < distances.GetLength(1) - 1; j++)
            {

                float newSmallestDistance = float.MaxValue;
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

                    float newDistance = float.MaxValue;

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

        public static float[,] FloydWarshall(float[,] distance)
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
            return distance;
        }



        public static float[,] Kruskal(List<Tuple<float, int, int>> sortedEdges, int numberOfVertices)
        {
            float[,] adj_MatrixOfSearchedMST = new float[numberOfVertices, numberOfVertices]; // it will be the adjacency matrix of the sought  MST

            foreach (Tuple<float, int, int> item in sortedEdges) // the edges are allready sorted, it is necessary to find MST with Kruskal
            {
                adj_MatrixOfSearchedMST[item.Item2, item.Item3] = item.Item1; // the vertices of this edge are connected to see if a circuit is created
                adj_MatrixOfSearchedMST[item.Item3, item.Item2] = item.Item1; // for this, this connection must be established at each vertex

                if (CircuitExistenceCheck(adj_MatrixOfSearchedMST)) // if this results in a circuit, then this step is undone
                {
                    adj_MatrixOfSearchedMST[item.Item2, item.Item3] = 0;
                    adj_MatrixOfSearchedMST[item.Item3, item.Item2] = 0;
                }
            }
            return adj_MatrixOfSearchedMST; // in the end, MST is created
        }
        public static bool CircuitExistenceCheck(float[,] adj_MatrixOfSearchedMST)
        {
            /* 
             
             The number of edges is used to determine whether or not there is a circuit.
             It is true that the number of edges in a circuit-free graph can be at most the number of vertices -1.
             So finding it and comparing it to the number of vertices is the goal. 
             It should be noted that some vertices are not yet connected. 
             So, the number of not yet connected vertices must be taken into account, so subtracted from the number of total vertices at the calculation.
             
             The number of edges can be calculated with the number of the entries of adjacency matrix. The number of edges is equal to the number of entries divided by 2 

            */
            int numberOfEntries = 0;
            float sumOfEntriesOfTheCurrentRow = 0; // with the sum of the entries in its row in the adjacency matrix, you can calculate whether a vertex is not yet connected
            int numberOfVertices = adj_MatrixOfSearchedMST.GetLength(0);
            int numberOfNotConnectedVertices = 0;

            for (int i = 0; i < numberOfVertices; i++)
            {
                for (int j = 0; j < numberOfVertices; j++)
                {
                    sumOfEntriesOfTheCurrentRow = sumOfEntriesOfTheCurrentRow + adj_MatrixOfSearchedMST[i, j];
                    if (adj_MatrixOfSearchedMST[i, j] > 0)
                    {
                        numberOfEntries += 1;
                    }
                }

                if (!(sumOfEntriesOfTheCurrentRow > 0)) // if the sum of the entries in a row is not greater than zero, it means that this vertex is not yet connected.
                {
                    numberOfNotConnectedVertices += 1;
                }
                sumOfEntriesOfTheCurrentRow = 0;
            }

            int numberOfEdges = numberOfEntries / 2;
            if (numberOfEdges <= numberOfVertices - numberOfNotConnectedVertices - 1) // consideration of the number of vertices not yet connected
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public static float[,] Boruvka(float[,] adjacenyMatrix, List<Tuple<float, int, int>> sortedEdges)
        {
            int numberOfVertices = adjacenyMatrix.GetLength(0);
            float minOfTheCurrentVertex = 0;
            int columnIndexOfTheConnectedVertexAtThisCurrentVertex = 0;
            float[,] adj_MatrixOfSearchedMST = new float[numberOfVertices, numberOfVertices]; // it will be the adjacency matrix of the sought  MST
            int numberOfEdges = 0;
            int numberOfTotalEdges = sortedEdges.Count;

            for (int rowIndexOfTheVertex = 0; rowIndexOfTheVertex < numberOfVertices; rowIndexOfTheVertex++) // it searchs in the rows (vertices) of the adjazenz matrix
            {
                Tuple<float, int> minOfTheCurrentVertexWithColumnIndexOfTheConnectedVertex = MinimumOfAVertex(adjacenyMatrix, rowIndexOfTheVertex);
                minOfTheCurrentVertex = minOfTheCurrentVertexWithColumnIndexOfTheConnectedVertex.Item1; // lightest edge of this vertex
                columnIndexOfTheConnectedVertexAtThisCurrentVertex = minOfTheCurrentVertexWithColumnIndexOfTheConnectedVertex.Item2; // column index of the connected vertex to the current row vertex at the lightest edge
                adj_MatrixOfSearchedMST[columnIndexOfTheConnectedVertexAtThisCurrentVertex, rowIndexOfTheVertex] = minOfTheCurrentVertex; // the vertices of this edge are connected to see if a circuit is created
                adj_MatrixOfSearchedMST[rowIndexOfTheVertex, columnIndexOfTheConnectedVertexAtThisCurrentVertex] = minOfTheCurrentVertex; // for this, this connection must be established at each vertex

                if (CircuitExistenceCheck(adj_MatrixOfSearchedMST)) // if this results in a circuit, then this step is undone
                {
                    adj_MatrixOfSearchedMST[columnIndexOfTheConnectedVertexAtThisCurrentVertex, rowIndexOfTheVertex] = 0;
                    adj_MatrixOfSearchedMST[rowIndexOfTheVertex, columnIndexOfTheConnectedVertexAtThisCurrentVertex] = 0;
                }
                else
                {
                    // in order to be able to connect the component at the end, you still have to know which edges are still left
                    // to do this, you delete already inserted edges from sorted edges.
                    sortedEdges.Remove(Tuple.Create(minOfTheCurrentVertex, Math.Min(rowIndexOfTheVertex, columnIndexOfTheConnectedVertexAtThisCurrentVertex), Math.Max(rowIndexOfTheVertex, columnIndexOfTheConnectedVertexAtThisCurrentVertex)));
                    // the number of edges already inserted is used to abort when an MST has already been created
                    // thus one does not iterate unnecessarily in further edges in sorted edges
                }
            }
            numberOfEdges = numberOfTotalEdges - sortedEdges.Count;
            foreach (Tuple<float, int, int> item in sortedEdges)
            {
                adj_MatrixOfSearchedMST[item.Item2, item.Item3] = item.Item1;
                adj_MatrixOfSearchedMST[item.Item3, item.Item2] = item.Item1;

                numberOfEdges += 1;
                if (CircuitExistenceCheck(adj_MatrixOfSearchedMST)) // if this results in a circuit, then this step is undone
                {
                    adj_MatrixOfSearchedMST[item.Item2, item.Item3] = 0;
                    adj_MatrixOfSearchedMST[item.Item3, item.Item2] = 0;
                    numberOfEdges -= 1;
                }

                if (numberOfEdges == numberOfVertices - 1) // the number of edges in a circuit-free graph can be at most the number of vertices -1
                                                           // when the time has come, there will only be a circuit with each next edge
                                                           // so, ajacency matrix of searched MST is already done
                {
                    return adj_MatrixOfSearchedMST;
                }

            }

            return adj_MatrixOfSearchedMST; // it may be that there is no longer any element in the sorted edges. 
                                            // so here too, adjacency matrix of searched MST is already donewith it
        }

        public static Tuple<float, int> MinimumOfAVertex(float[,] adjacenyMatrix, int indexOfVertexRow) // it finds the non-zero minimum of a row 
        {
            int numberOfVertices = adjacenyMatrix.GetLength(0);
            float tempMin = 0;
            int indexOfVertexColumn = 0;
            for (int i = 0; i < numberOfVertices; i++)
            {
                if (adjacenyMatrix[indexOfVertexRow, i] > 0) // first the first element greater than zero is found
                {
                    tempMin = adjacenyMatrix[indexOfVertexRow, i];
                    indexOfVertexColumn = i;
                    break;
                }
            }
            for (int i = indexOfVertexColumn + 1; i < numberOfVertices; i++) // and then the minimum. To do this, 
                                                                             // you simply start with the next entry after the first element greater than zero
            {
                if (adjacenyMatrix[indexOfVertexRow, i] > 0 && adjacenyMatrix[indexOfVertexRow, i] < tempMin)
                {
                    tempMin = adjacenyMatrix[indexOfVertexRow, i];
                    indexOfVertexColumn = i;
                }
            }
            Tuple<float, int> minOfTheCurrentVertexWithColumnIndexOfTheConnectedVertex = new Tuple<float, int>(tempMin, indexOfVertexColumn);

            return minOfTheCurrentVertexWithColumnIndexOfTheConnectedVertex;
        }
    }
}
