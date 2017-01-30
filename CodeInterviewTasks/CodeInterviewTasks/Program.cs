using ArrayAndStrings;
using Common;

namespace CodeInterviewTasks
{
    class Program
    {
        static void Main(string[] args)
        {
            ITask[] tasks = { new StringTasks() };

            foreach (var task in tasks)
            {
                task.PrintResults();
            }
        }
    }
}
