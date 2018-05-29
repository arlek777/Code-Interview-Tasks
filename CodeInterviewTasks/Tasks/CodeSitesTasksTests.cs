using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tasks
{
    [TestClass]
    public class CodeSitesTasksTests
    {
        /// <summary>
        /// There are several methods to solve it: with XOR, with Dictionary, with additional list or 2 for.
        /// </summary>
        [TestMethod]
        public void SingleNumberWithXOR()
        {
            var a = 0;
            int[] nums = new int[] { 4, 1, 2, 1, 2 };
            for (int i = 0; i < nums.Length; i++)
            {
                a ^= nums[i];
            }

            Assert.AreEqual(4, a);
        }

        [TestMethod]
        public void SingleNumberWithDictionary()
        {
            var dictionary = new Dictionary<int, int>();
            int[] nums = new int[] { 4, 1, 2, 1, 2 };
            for (int i = 0; i < nums.Length; i++)
            {
                if (dictionary.ContainsKey(nums[i]))
                {
                    dictionary.Remove(nums[i]);
                }
                else
                {
                    dictionary[nums[i]] = i;
                }
            }

            var result = dictionary.ToList().LastOrDefault().Key;
            Assert.AreEqual(4, result);
        }

        [TestMethod]
        public void ShouldConvertTime()
        {
            var num = 45;
            int hours = num / 60;
            int min = num % 60;

            var result = $"{hours}:{min}";

            Assert.AreEqual("0:45", result);
        }

        [TestMethod]
        public void ClosestEnemy()
        {
            //int[,] matrix1 = new int[,]
            //{
            //    { 0,0,0 },
            //    { 1,0,0 },
            //    { 2,0,0 }
            //};

            int[][] matrix1 = new int[][]
            {
                new int[] { 0,0,0,0 },
                new int[] { 1,0,0,0 },
                new int[] { 0,0,0,2 },
                new int[] { 0,0,0,2 }
            };

            var heroIndexR = -1;
            var heroIndexC = -1;
            for (int i = 0; i < matrix1.Length; i++)
            {
                for (int j = 0; j < matrix1[i].Length; j++)
                {
                    if (matrix1[i][j] == 1)
                    {
                        heroIndexR = i;
                        heroIndexC = j;
                        break;
                    }
                }
                if (heroIndexR >= 0) break;
            }

            var closest = 0;
            for (int i = 0; i < matrix1.Length; i++)
            {
                for (int j = 0; j < matrix1[i].Length; j++)
                {
                    if (matrix1[i][j] == 2)
                    {
                        var rSteps = Math.Abs(heroIndexR - i);
                        var cSteps = Math.Abs(heroIndexC - j);
                        if (cSteps == matrix1[i].Length-1)
                        {
                            cSteps = 1;
                        }

                        var result = rSteps + cSteps;
                        if (closest == 0 || result < closest)
                        {
                            closest = result;
                        }
                    }
                }
            }

            Assert.AreEqual(2, closest);
        }
    }
}
