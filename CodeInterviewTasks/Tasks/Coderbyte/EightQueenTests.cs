using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tasks.Coderbyte
{
    [TestClass]
    public class EightQueenTests
    {

        [TestMethod]
        public void TimeConverter()
        {
            var num = 45;
            var result = "";
            int divRes = num / 60;
            if (divRes == 0)
            {
                result = "0:";
            }
            else
            {
                result = divRes + ":";
            }
            
            int min = num % 60;
            result += min;

            Assert.AreEqual("0:45", result);

        }

        [TestMethod]
        public void Test()
        {
            int[,] arr = new[,]
            {
                {2, 1}, 
                {4, 3}, 
                {6, 3}, 
                {8, 4}, 
                {3, 4}, 
                {1, 6}, 
                {7, 7}, 
                {5, 8}
            };

            var length = arr.Length / 2;
            var result = true;
            int[,] firstAttacking = null;
            for (int i = 0; i < length; i++)
            {
                for (int j = i+1; j < length; j++)
                {
                    if (arr[i, 0] == arr[j,0] || arr[i, 1] == arr[j, 1])
                    {
                        result = false;
                        firstAttacking = new[,] { {arr[i, 0], arr[i, 1]} };
                        break;
                    }
                }

                if (!result) break;
            }

            var test = firstAttacking;
            Assert.IsFalse(result);
        }
    }
}
