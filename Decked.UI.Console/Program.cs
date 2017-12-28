using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Decked.Core;

using JetBrains.Annotations;
using Opt;

using static Decked.Core.ReSharperValidations;

namespace Decked.UI.Console
{
    [UsedImplicitly]
    class Program
    {
        static async Task Main(string[] args)
        {
            if (Debugger.IsAttached)
            {
                await Execute(args);

                return;
            }

            try
            {
                await Execute(args);
            }
            catch (Exception ex)
            {
                System.Console.Error.WriteLine($"{ex.GetType().Name}: {ex.Message}");
                System.Console.Error.WriteLine(ex.StackTrace);
                Environment.Exit(1);
            }
        }

        private static Task Execute(string[] args)
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
            return deck.Run();
        }
    }
}
