using System;
using System.IO;
using System.Linq;

using Decked.Core;

using JetBrains.Annotations;
using Opt;

using static Decked.Core.ReSharperAssumptions;

namespace Decked.UI.Console
{
    [UsedImplicitly]
    class Program
    {
        static void Main(string[] args)
        {
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

            var screen = ScreenConfiguration.Load(options.MainScreenFilename);
        }
    }
}
