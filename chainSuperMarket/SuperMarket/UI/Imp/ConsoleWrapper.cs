using System;

namespace SuperMarket.UI.Imp
{
    public class ConsoleWrapper : IConsoleWrapper
    {
        public void Clear()
        {
            Console.Clear();
        }

        public string ReadLine()
        {
            return Console.ReadLine();
        }

        public void WriteLine(string value)
        {
            Console.WriteLine(value);
        }
    }
}
