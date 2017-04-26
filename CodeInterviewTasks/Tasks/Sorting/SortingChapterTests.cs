using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tasks.Sorting
{
    public class AnagrammComparator : IComparer<string>
    {
        public string SortChars(string s)
        {
            char[] content = s.ToCharArray();
            Array.Sort(content);
            return new string(content);
        }

        public int Compare(string x, string y)
        {
            return SortChars(x).CompareTo(SortChars(y));
        }
    }

    [TestClass]
    public class SortingChapterTests
    {
        #region Sorting implementations

        readonly int[] _expected = {10, 30, 40, 50, 70, 80, 90};

        [TestMethod]
        public void ClassicQuickSort_Test()
        {
            int[] arr = {10, 80, 30, 90, 40, 50, 70};
            SortHelper.ClassicQuickSort(arr, 0, arr.Length - 1);

            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] != _expected[i]) Assert.Fail("Simple Quick Sort Failed.");
            }
        }

        [TestMethod]
        public void SelectionSort_Test()
        {
            int[] arr = {10, 80, 30, 90, 40, 50, 70};
            SortHelper.SelectionSort(arr);

            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] != _expected[i]) Assert.Fail("Selection Sort Failed.");
            }
        }

        [TestMethod]
        public void InsertionSort_Test()
        {
            int[] arr = {10, 80, 30, 90, 40, 50, 70};
            SortHelper.InsertionSort(arr);

            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] != _expected[i]) Assert.Fail("Insertion Sort Failed.");
            }
        }

        [TestMethod]
        public void MergeSort_Test()
        {
            int[] arr = { 10, 80, 30, 90, 40, 50, 70 };
            SortHelper.MergeSort(arr, 0, arr.Length - 1);

            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] != _expected[i]) Assert.Fail("Merge Sort Failed.");
            }
        }

        #endregion Sorting Implementations

        #region Sorting Chapter Tasks

        /// <summary>
        /// This task can be solved by usual Merge 
        /// from merge sort or by more sort version whish is used here.
        /// </summary>
        [TestMethod]
        public void MergeTwoArrays_Test()
        {
            int[] a = new int[6];
            a[0] = 10;
            a[1] = 20;
            a[2] = 30;

            int[] b =  { 5, 15, 35 };

            int[] expected = {5, 10, 15, 20, 30, 35};

            MergeTask(a, b, 3, 3);

            for (int s = 0; s < a.Length; s++)
            {
                if (a[s] != expected[s]) Assert.Fail("Merge Two Arrays task failed.");
            }
        }

        private void MergeTask(int[] a, int[] b, int aLen, int bLen)
        {
            // we have 3 arrays, 1 - Left side a[0-aLen-1], 2 - Right side b[0-bLen-1], 3 - merge to a[aLen-1 - lastAIndex]
            // aLen and bLen are actual count of elements and not Length property

            int k = aLen + bLen - 1; // new top index afer array merge
            int i = aLen - 1; // starting from the end of arrays
            int j = bLen - 1;

            while (i >=0 && j >= 0)
            {
                // since we go from top to bottom we take bigger to the end of array
                if (a[i] > b[j])
                {
                    a[k--] = a[i--];
                }
                else
                {
                    a[k--] = b[j--];
                }
            }

            while (j >= 0)
            {
                a[k--] = b[j--];
            }
        }

        [TestMethod]
        public void SortStringsWithAnnagramsTest()
        {
            var arr = new[] {"янтарь", "лось", "араб", "соль", "импорт", "портим"};
            var expected = new[] { "араб", "янтарь", "импорт", "портим", "лось", "соль" };
            Array.Sort(arr, new AnagrammComparator());


            for (int s = 0; s < arr.Length; s++)
            {
                if (arr[s] != expected[s]) Assert.Fail("SortStringsWithAnnagramsTest task failed.");
            }
        }

        [TestMethod]
        public void CustomSortStringsWithAnnagramsTest()
        {
            var arr = new[] { "янтарь", "лось", "араб", "соль", "импорт", "портим" };
            var expected = new[] { "араб", "янтарь", "импорт", "портим", "лось", "соль" };

            // todo sort with custom anagramm logic
            for (int i = 0; i < arr.Length; i++)
            {
                var minI = i;
                for (int j = i + 1; j < arr.Length; j++)
                {
                    if (arr[j].CompareTo(arr[minI]) < 0 || IsAnagramm(arr[j], arr[minI]))
                    {
                        minI = j;
                    }
                }

                SortHelper.Swap(arr, i, minI);
            }


            for (int s = 0; s < arr.Length; s++)
            {
                if (arr[s] != expected[s]) Assert.Fail("CustomSortStringsWithAnnagramsTest task failed.");
            }
        }

        private bool IsAnagramm(string firstAnagramString, string secondAnagramString)
        {
            char[] content = firstAnagramString.ToCharArray();
            Array.Sort(content);
            var first = new string(content);

            content = secondAnagramString.ToCharArray();
            Array.Sort(content);
            var second = new string(content);

            // sorted chars that equals each other
            return first == second;
        }

        #endregion Sorting Chapter Tasks
    }
}
