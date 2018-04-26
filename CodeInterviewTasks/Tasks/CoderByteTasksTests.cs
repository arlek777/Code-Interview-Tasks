using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tasks
{
    [TestClass]
    public class CoderByteTasksTests
    {
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
            int[,] matrix1 = new int[,]
            {
                { 0,0,0 },
                { 1,0,0 },
                { 2,0,0 }
            };

            int[,] matrix2 = new int[,]
            {
                { 0,0,0,0 },
                { 2,0,1,0 },
                { 0,0,0,0 },
                { 2,0,0,2 }
            };

            var heroIndexR = -1;
            var heroIndexC = -1;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (matrix1[i, j] == 1)
                    {
                        heroIndexR = i;
                        heroIndexC = j;
                        break;
                    }
                }
                if (heroIndexR >= 0) break;
            }


        }
    }
}
