namespace ChatBot
{
    static class Program
    {
        public static void Main()
        {
            ChatBot.Hello();
            while (!ChatBot.Quite)
            {
                ChatBot.GetInput();
                ChatBot.Respond();
            }
        }
    }
}
