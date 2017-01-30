using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tasks
{
    [TestClass]
    public class StringTasksChapter1
    {
        private const string UniqueString = "abcdfg";
        private const string NotUniqueString = "abcdaghrh";
        private const string DuplicatedString = "abcdacd";


        [TestMethod]
        public void ShouldRemoveDuplicatedChars()
        {
            string result = RemoveDuplicates(DuplicatedString);
            Assert.IsTrue(IsStringUniqueHashSetImpl(result));
        }

        [TestMethod]
        public void StringShouldBeUniqueHashSet()
        {
            Assert.IsTrue(IsStringUniqueHashSetImpl(UniqueString));
        }

        [TestMethod]
        public void StringShouldNOTBeUniqueHashSet()
        {
            Assert.IsFalse(IsStringUniqueHashSetImpl(NotUniqueString));
        }

        [TestMethod]
        public void StringShouldBeUniqueLoops()
        {
            Assert.IsTrue(IsStringUniqueLoopImpl(UniqueString));
        }

        [TestMethod]
        public void StringShouldNOTBeUniqueLoops()
        {
            Assert.IsFalse(IsStringUniqueLoopImpl(NotUniqueString));
        }

        #region Implementations

        private bool IsStringUniqueHashSetImpl(string str)
        {
            var isUnique = true;
            var hashSet = new HashSet<char>();

            for (int i = 0; i < str.Length; i++)
            {
                if (hashSet.Contains(str[i]))
                {
                    isUnique = false;
                    break;
                }

                hashSet.Add(str[i]);
            }

            return isUnique;
        }

        private bool IsStringUniqueLoopImpl(string str)
        {
            bool isUnique = true;
            for (int i = 0; i < str.Length; i++)
            {

                for (int j = i + 1; j < str.Length; j++)
                {
                    if (str[i] == str[j])
                    {
                        isUnique = false;
                        break;
                    };
                }

                if (!isUnique) break;
            }

            return isUnique;
        }

        private string RemoveDuplicates(string str)
        {
            var array = str.ToCharArray();
            for (var i = 0; i < array.Length; i++)
            {
                if (array[i] == '\0') continue;
                for (var j = i + 1; j < array.Length; j++)
                {
                    if (array[j] == '\0') continue;

                    if (array[i] == array[j])
                    {
                        array[j] = '\0';
                        for (var k = j; k < array.Length - 1; k++)
                        {
                            var temp = array[k + 1];
                            array[k + 1] = array[k];
                            array[k] = temp;
                        }
                    }
                }
            }

            return array.ToString();
        }

        #endregion
    }
}
