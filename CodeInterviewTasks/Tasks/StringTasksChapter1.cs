using System;
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
        public void BookSolutionDuplicates()
        {
            char[] str = new[] {'i', 's', 't', 't', 'a', 's'};
            if (str == null) return;
            int len = str.Length;
            if (len < 2) return;

            int tail = 1;

            for (int i = 1; i < len; ++i)
            {
                int j;
                for (j = 0; j < tail; ++j)
                {
                    if (str[i] == str[j]) break;
                }
                if (j == tail)
                {
                    str[tail] = str[i];
                    ++tail;
                }
            }
            str[tail] = '\0';
        }

        [TestMethod]
        public void ShouldReplaceSpaces()
        {
            string str = "this is a test";
            var result = ReplaceSpaces(str);
            Assert.AreEqual(result, "this%20is%20a%20test");
        }

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

        private string RemoveDuplicates(string str)
        {
            var array = str.ToCharArray();
            int length = array.Length;

            for (var i = 0; i < length; i++)
            {
                for (var j = i + 1; j < length; j++)
                {
                    if (array[i] == array[j])
                    {
                        array[j] = '\0';
                        for (var k = j; k < length - 1; k++)
                        {
                            var temp = array[k + 1];
                            array[k + 1] = array[k];
                            array[k] = temp;
                        }

                        length--;
                    }
                }
            }

           Array.Resize(ref array, length);
           return new string(array);
        }

        private string RemoveDuplicates2(string str)
        {
            var array = str.ToCharArray();
            int newLength = array.Length;

            for (var i = 0; i < array.Length; i++)
            {
                int j;
                for (j = i + 1; j < array.Length; j++)
                {
                    if (array[i] == array[j]) break;
                }


            }

            Array.Resize(ref array, newLength);
            return new string(array);
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

        private string ReplaceSpaces(string str)
        {
            int spaceCounter = 0;

            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == ' ') { spaceCounter += 2; }
            }

            int newStrI = 0;
            char[] newStr = new char[str.Length + spaceCounter];
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == ' ')
                {
                    newStr[newStrI++] = '%';
                    newStr[newStrI++] = '2';
                    newStr[newStrI++] = '0';
                }
                else
                {
                    newStr[newStrI++] = str[i];
                }
            }

            return new string(newStr);
        }

        #endregion
    }
}
