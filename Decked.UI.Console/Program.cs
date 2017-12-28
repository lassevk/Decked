using System;
using System.IO;
using System.Linq;

using Decked.Core;

using JetBrains.Annotations;
using Opt;

using static Decked.Core.ReSharperValidations;

namespace Decked.UI.Console
{
    [UsedImplicitly]
    class Program
    {
        static void Main(string[] args)
        {
            var logger = new ConsoleLogger();

            var options = OptParser.Parse<CommandLineOptions>(args);
            assume(options != null);

            var problems = options.GetProblems().ToList();
            if (problems.Any())
            {
                foreach (var problem in problems)
                    System.Console.Error.WriteLine(problem);
                Environment.Exit(1);
            }

            assume(options.MainScreenFilename != null);
            System.Console.WriteLine($"loading screen {Path.GetFullPath(options.MainScreenFilename)}");

            var initialScreen = ScreenConfiguration.Load(options.MainScreenFilename);

            var deck = new DeckRunner(logger, initialScreen);
            deck.Run();
        }
    }
}
