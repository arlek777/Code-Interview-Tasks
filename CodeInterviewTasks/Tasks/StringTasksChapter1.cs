using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tasks
{
    [TestClass]
    public class StringTasksChapter1
    {
        private const string UniqueString = "abcdfg";
        private const string NotUniqueString = "abcdaghrh";

        [TestMethod]
        public void StringShouldBeAnnagrams()
        {
            string firstAnagramString = "Muka".ToLowerInvariant();
            string secondAnagramString = "Kuma".ToLowerInvariant();

            Assert.IsTrue(IsAnagramm(firstAnagramString, secondAnagramString));
        }

        [TestMethod]
        public void StringShouldNOTBeAnnagrams()
        {
            string firstAnagramString = "Mika".ToLowerInvariant();
            string secondAnagramString = "Kina".ToLowerInvariant();

            Assert.IsFalse(IsAnagramm(firstAnagramString, secondAnagramString));
        }

        [TestMethod]
        public void ShouldRemoveDuplicatedChars()
        {
            string duplicatedString = "abcdacd";
            string result = RemoveDuplicates(duplicatedString);
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

        // TODO work on it - isn't optimal decision
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

        private bool IsAnagramm(string firstAnagramString, string secondAnagramString)
        {
            if (firstAnagramString.Length != secondAnagramString.Length)
            {
                return false;
            }

            int[] characters1 = new int[256];
            int[] characters2 = new int[256];

            for (int i = 0; i < firstAnagramString.Length; i++)
            {
                characters1[firstAnagramString[i]]++;
                characters2[secondAnagramString[i]]++;
            }

            for (int i = 0; i < firstAnagramString.Length; i++)
            {
                if (characters2[firstAnagramString[i]] == 0 || characters1[firstAnagramString[i]] != characters2[firstAnagramString[i]])
                {
                    return false;
                }
            }

            return true;
        }

        #endregion
    }
}
