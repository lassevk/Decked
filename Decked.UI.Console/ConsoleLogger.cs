using System;
using Decked.Interfaces;

using JetBrains.Annotations;

namespace Decked.UI.Console
{
    public class ConsoleLogger : ILogger
    {
        [NotNull]
        private readonly CommandLineOptions _Options;

        public ConsoleLogger([NotNull] CommandLineOptions options)
        {
            _Options = options ?? throw new ArgumentNullException(nameof(options));
        }

        public void Debug(string line)
        {
            if (!_Options.Verbose)
                return;

            Log(line);
        }

        public void Log([NotNull] string line)
        {
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            if (line != null)
                System.Console.Out.WriteLine(line);
        }
    }
}
