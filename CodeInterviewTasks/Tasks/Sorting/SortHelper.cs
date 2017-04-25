namespace Tasks.Sorting
{
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
            if (high <= low) return;

            int pi = Partition(arr, low, high);

            ClassicQuickSort(arr, low, (pi - 1));
            ClassicQuickSort(arr, (pi + 1), high);
        }

        // TODO find what's wrong
        // TODO implement 3 partition of arrya
        // TODO implement merge sort and bucket sort
        private static int Partition(int[] arr, int low, int high)
        {
            int pivot = arr[high];
            int smallestIndex = -1;

            // traverse from start till the pivot
            for (int i = 0; i <= high - 1; i++)
            {
                if (pivot >= arr[i])
                {
                    smallestIndex++;
                    if (smallestIndex == i) continue;
                    Swap(arr, smallestIndex, i);
                }
            }

            Swap(arr, smallestIndex + 1, high);

            return smallestIndex + 1;
        }

        /* The selection sort algorithm sorts an array by repeatedly 
           finding the minimum element (considering ascending order) 
           from unsorted part and putting it at the beginning.
           O(n^2)
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

        public static void MergeSort(int[] arr, int l, int r)
        {
            if (l >= r) return;

            int m = (l + r) / 2;

            MergeSort(arr, l, m);
            MergeSort(arr, m+1, r);

            Merge(arr, l, m, r);
        }

        public static void Merge(int[] arr, int l, int m, int r)
        {
            int i, j;
            int lLen = m - l + 1;
            int rLen = r - m;

            int[] L = new int[lLen];
            int[] R = new int[rLen];

            for (i = 0; i < lLen; i++)
                L[i] = arr[l + i];
            for (j = 0; j < rLen; j++)
                R[j] = arr[m + 1 + j];

            /* Merge the temp arrays back into arr[l..r]*/
            i = 0; // Initial index of first subarray
            j = 0; // Initial index of second subarray
            int k = l;

            while (i < lLen && j < rLen)
            {
                if (L[i] <= R[j])
                {
                    arr[k] = L[i];
                    i++;
                }
                else
                {
                    arr[k] = R[j];
                    j++;
                }
                k++;
            }

            while (i < lLen)
            {
                arr[k] = L[i];
                i++;
                k++;
            }

            while (j < rLen)
            {
                arr[k] = R[j];
                j++;
                k++;
            }
        }

        public static void InsertAt(int[] arr, int pos, int value)
        {
            if(pos >= arr.Length) return;

            for (var i = arr.Length - 1; i >= 0; i--)
            {
                if (i == pos)
                {
                    arr[i] = value;
                    break;
                }
                arr[i] = arr[i - 1];
            }
        }

        public static void Swap(int[] arr, int i, int j)
        {
            var temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
        }
    }
}