using System;
using Decked.Interfaces;

namespace Decked.UI.Console
{
    public class ConsoleLogger : ILogger
    {
        public void Log(string line)
        {
            if (line != null)
                System.Console.Out.WriteLine(line);
        }
    }
}
