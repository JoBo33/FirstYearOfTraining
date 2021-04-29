using OxyPlot;
using OxyPlot.WindowsForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AllTogether
{
    public class SortingAlgorithm
    {

        private double[] _list;


        public double[] GetList()
        {
            return _list;
        }
        public void SetList(double[] value)
        {
            _list = value;
        }


        public SortingAlgorithm(double[] list)
        {
            _list = list;
        }

        public static double[] BubbleSort(double[] sortingArray, PlotView plotView)
        {
            for (int j = 0; j < sortingArray.Length; j++)
            {
                for (int i = 0; i < sortingArray.Length - 1; i++)
                {
                    if (sortingArray[i] > sortingArray[i + 1])
                    {
                        Swap(sortingArray, i, i + 1, plotView);
                    }
                }
            }
            return sortingArray;
        }
        public static double[] InsertionSort(double[] sortingArray, PlotView plotView)
        {
            for (int j = 0; j < sortingArray.Length; j++)
            {
                for (int i = j; i > 0; i--)
                {
                    if (sortingArray[i] < sortingArray[i - 1])
                    {
                        Swap(sortingArray, i, i - 1, plotView);
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return sortingArray;
        }

        public static double[] SelectionSort(double[] sortingArray, PlotView plotView)
        {
            for (int i = 0; i < sortingArray.Length; i++)
            {

                double smallest = sortingArray[i];
                int index = i;

                for (int j = i; j < sortingArray.Length; j++)
                {
                    if (smallest > sortingArray[j])
                    {
                        smallest = sortingArray[j];
                        index = j;
                    }
                }
                Swap(sortingArray, i, index, plotView);
            }
            return sortingArray;
        }

        public static double[] QuickSortInPlace(double[] sortingArray, int first, int last, PlotView plotView)
        {
            if ((last - first) >= 1)
            {
                int pivotposition = Partition(sortingArray, first, last, plotView);

                QuickSortInPlace(sortingArray, first, pivotposition - 1, plotView);
                QuickSortInPlace(sortingArray, pivotposition + 1, last, plotView);
            }
            return sortingArray;

        }

        public static int Partition(double[] sortingArray, int first, int last, PlotView plotView)
        {
            double pivot = sortingArray[last];
            int wallindex = first;
            for (int currentindex = first; currentindex < last; ++currentindex)
            {
                if (sortingArray[currentindex] < pivot)
                {
                    Swap(sortingArray, wallindex, currentindex, plotView);
                    ++wallindex;
                }
            }
            Swap(sortingArray, wallindex, last, plotView);
            return wallindex;

        }

        public static void Swap(double[] sortingArray, int indexA, int indexB, PlotView plotView)
        {
            double temp = sortingArray[indexA];
            sortingArray[indexA] = sortingArray[indexB];
            sortingArray[indexB] = temp;

            WorkWithOxyPlot.PlotColumnSeries(ref plotView, sortingArray);
            Thread.Sleep(500/sortingArray.Length);
        }


        public static double[] MergeSort(double[] sortingArray, PlotView plotView)
        {
            if (sortingArray.Length > 1)
            {
                double[] left = new double[sortingArray.Length / 2];
                double[] right = new double[(int)Math.Ceiling((decimal)sortingArray.Length / 2)];

                for (int i = 0; i < sortingArray.Length/2; i++)
                {
                    left[i] = sortingArray[i];
                }
                for (int i = sortingArray.Length/2; i < sortingArray.Length; i++)
                {
                    right[i-sortingArray.Length/2] = sortingArray[i];
                }

                left = MergeSort(left, plotView);
                right = MergeSort(right, plotView);
                return Merge(left, right, plotView);

            }
            return sortingArray;
            

        }

        private static double[] Merge(double[] left, double[] right, PlotView plotView)
        {
            double[] mergedList = new double[left.Length + right.Length];

            for (int i = 0; i < mergedList.Length; i++)
            {
                if(left.Length == 0)
                {
                    mergedList[i] = right[0];
                    right = right.Skip(1).ToArray();
                }
                else if(right.Length == 0)
                {
                    mergedList[i] = left[0];
                    left = left.Skip(1).ToArray();
                }
                else if (left[0] < right[0])
                {
                    mergedList[i] = left[0];
                    left = left.Skip(1).ToArray();
                }
                else
                {
                    mergedList[i] = right[0];
                    right = right.Skip(1).ToArray();
                }

                WorkWithOxyPlot.PlotColumnSeries(ref plotView, mergedList);
                Thread.Sleep(500/mergedList.Length);
            }
            return mergedList;
        }

        public static void MergeSortInPlace(double[] arr, int firstOfSubarray, int lastOfSubarray, PlotView plotView)
        {
            if (firstOfSubarray < lastOfSubarray)
            {
                // Same as (l + r) / 2, but avoids overflow
                int mid = firstOfSubarray + (lastOfSubarray - firstOfSubarray) / 2;

                MergeSortInPlace(arr, firstOfSubarray, mid, plotView);
                MergeSortInPlace(arr, mid + 1, lastOfSubarray, plotView);

                MergeInPlace(arr, firstOfSubarray, mid, lastOfSubarray, plotView);
            }
        }
        private static void MergeInPlace(double[] arr, int start, int mid,
                  int end, PlotView plotView)
        {
            int start2 = mid + 1;

            // is it already sorted?
            if (arr[mid] <= arr[start2])
            {
                return;
            }

            // Two pointers to maintain start
            // of both arrays to merge
            while (start <= mid && start2 <= end)
            {

                // If element 1 is in right place
                if (arr[start] <= arr[start2])
                {
                    start++;
                }
                else
                {
                    double value = arr[start2];
                    int index = start2;

                    // Shift all the elements between element 1
                    // element 2, right by 1.
                    while (index != start)
                    {
                        arr[index] = arr[index - 1];
                        WorkWithOxyPlot.PlotColumnSeries(ref plotView, arr);
                        Thread.Sleep(500/arr.Length);
                        index--;
                    }
                    arr[start] = value;

                    // Update all the pointers
                    start++;
                    mid++;
                    start2++;
                }
            }
        }

    }
}
// asynch 
// await