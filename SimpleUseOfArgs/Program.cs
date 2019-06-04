using System;

namespace SimpleUseOfArgs
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            { 
            Args arg = new Args("1,p#,d*", args);
            bool logging = arg.getBoolean('l');
            int port = arg.getInt('p');
            string directory = arg.getString('d');
            executeApplication(logging, port, directory);
            }
            catch(ArgsException e)
            {
                Console.Write($"Argument error:{e.errorMessage()}\n");
            }
        }

        private static void executeApplication(bool logging, int port, string directory)
        {
            Console.Write("Executing application");
        }
    }
}
