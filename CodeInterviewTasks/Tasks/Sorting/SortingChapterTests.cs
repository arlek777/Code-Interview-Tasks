using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tasks.Sorting
{
    [TestClass]
    public class SortingChapterTests
    {
        readonly int[] expected = { 10, 30, 40, 50, 70, 80, 90 };

        [TestMethod]
        public void ClassicQuickSort_Test()
        {
            int[] arr = { 10, 80, 30, 90, 40, 50, 70 };
            SortHelper.ClassicQuickSort(arr, 0, arr.Length-1);

            for (int i = 0; i < arr.Length; i++)
            {
                if(arr[i] != expected[i]) Assert.Fail("Simple Quick Sort Failed.");
            }
        }

        [TestMethod]
        public void SelectionSort_Test()
        {
            int[] arr = { 10, 80, 30, 90, 40, 50, 70 };
            SortHelper.SelectionSort(arr);

            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] != expected[i]) Assert.Fail("Selection Sort Failed.");
            }
        }

        [TestMethod]
        public void InsertionSort_Test()
        {
            int[] arr = { 10, 80, 30, 90, 40, 50, 70 };
            SortHelper.InsertionSort(arr);

            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] != expected[i]) Assert.Fail("Insertion Sort Failed.");
            }
        }
    }

    public static class SortHelper
    {
        /*  This function takes last element as pivot, places
            the pivot element at its correct position in sorted
            array, and places all smaller (smaller than pivot)
            to left of pivot and all greater elements to right
            of pivot. 
            The worst case when array is sorted it's O(n^2).
            The best case when a pivot is middle element and it's randomly sorted O(NlogN).
            Possible improvements: 3 way quick sort, insertion sort on small parts of array. 
        */

        public static void ClassicQuickSort(int[] arr, int low, int high)
        {
            if (high > low)
            {
                int pi = Partition(arr, low, high);

                ClassicQuickSort(arr, low, (pi - 1));
                ClassicQuickSort(arr, (pi + 1), high);
            }
        }

        /* The selection sort algorithm sorts an array by repeatedly 
           finding the minimum element (considering ascending order) 
           from unsorted part and putting it at the beginning.
         */
        public static void SelectionSort(int[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                var minI = i;
                for (int j = i+1; j < arr.Length; j++)
                {
                    if (arr[j] < arr[minI])
                    {
                        minI = j;
                    }
                }

                Swap(arr, i, minI);
            }
        }

        /*  Works good with almost sorted array,
            small array that will be added to big sorted array.
            The best case: O(n) - sorted array
            The worst case: O(n^2) - unsorted array
         */
        public static void InsertionSort(int[] arr)
        {
            for (int i = 1; i < arr.Length; i++)
            {
                int j;
                var key = arr[i];
                for (j = i-1; j >=0 && arr[j] > key; j--)
                {
                    arr[j + 1] = arr[j];
                }

                arr[j + 1] = key;
            }
        }

        private static int Partition(int[] arr, int low, int high)
        {
             //TODO find problems

            int pivot = arr[high];
            int smallestIndex = -1;
            // traverse from start till the pivot
            for (int i = 0; i < high - 1; i++)
            {
                if (pivot >= arr[i])
                {
                    smallestIndex++;
                    Swap(arr, smallestIndex, i);
                }
            }

            Swap(arr, smallestIndex+1, high);

            return smallestIndex + 1;
        }

        private static void Swap(int[] arr, int i, int j)
        {
            var temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
        }
    }
}
