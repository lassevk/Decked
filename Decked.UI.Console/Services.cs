using System;
using System.Linq;

using Decked.Interfaces;

using DryIoc;

using JetBrains.Annotations;

using Opt;

using static Decked.ReSharperValidations;

namespace Decked.UI.Console
{
    internal static class Services
    {
        public static Container Configure([NotNull] string[] commandLineArguments)
        {
            var container = new Container(Rules.Default.NotNull().WithAutoConcreteTypeResolution());

            var options = OptParser.Parse<CommandLineOptions>(commandLineArguments);
            assume(options != null);

            var problems = options.GetProblems().ToList();
            if (problems.Any())
            {
                foreach (var problem in problems)
                    System.Console.Error.WriteLine(problem);
                Environment.Exit(1);
            }

            container.RegisterInstance<IDeckRunnerOptions>(options);
            container.Register<ILogger, ConsoleLogger>(Reuse.Singleton);
            
            Decked.Services.Register(container);
            Decked.Devices.Services.Register(container);
            Decked.BuildingBlocks.Services.Register(container);
            Decked.BuiltIn.Services.Register(container);

            return container;
        }
    }
}
