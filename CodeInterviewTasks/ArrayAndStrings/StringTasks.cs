using System;
using System.Collections.Generic;
using Common;

namespace ArrayAndStrings
{
    public class StringTasks: ITask
    {
        public void PrintResults()
        {
            Console.WriteLine("1.1 String characters uniqueness task. Enter 2 string for check.");

            var uniqueStr = Console.ReadLine();
            var notUniqueStr = Console.ReadLine();
            Console.WriteLine();

            LoopStringCharactersUnique(uniqueStr);
            LoopStringCharactersUnique(notUniqueStr);

            //HashSetStringCharactersUnique(uniqueStr);
            //HashSetStringCharactersUnique(notUniqueStr);

            Console.WriteLine("----------------------------------------------------------------------------");


            Console.WriteLine("1.3 String duplicates. Enter string for the task.");

            var duplicateString = Console.ReadLine();
            Console.WriteLine();

            RemoveDuplicates(duplicateString);

            Console.WriteLine("----------------------------------------------------------------------------");
        }

        private void RemoveDuplicates(string str)
        {
            
        }

        private void LoopStringCharactersUnique(string str)
        {
            var isUnique = true;
            var loops1 = 0;
            var loops2 = 0;

            for (int i = 0; i < str.Length; i++)
            {
               
                for (int j = str.Length - 1; j >= 0; j--)
                {
                    loops1++;
                    if (str[i] == str[j] && i != j)
                    {
                        isUnique = false;
                        break;
                    }
                }

                if (!isUnique) break;
            }

            isUnique = true;
            for (int i = 0; i < str.Length; i++)
            {
                
                for (int j = i + 1; j < str.Length; j++)
                {
                    loops2++;
                    if (str[i] == str[j])
                    {
                        isUnique = false;
                        break;
                    };
                }

                if (!isUnique) break;
            }

            Console.WriteLine($"String {str} is unique: {isUnique}. My loops: {loops1}, Alex Loops: {loops2}");
        }

        private void HashSetStringCharactersUnique(string str)
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

            Console.WriteLine($"String {str} is unique: {isUnique}.");
        }
    }
}
