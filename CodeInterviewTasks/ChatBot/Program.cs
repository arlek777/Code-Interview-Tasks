using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBot
{
    static class Program
    {
        public static void Main()
        {
            while (!ChatBot.Quite)
            {
                ChatBot.GetInput();
                ChatBot.Respond();
            }
        }
    }
}
