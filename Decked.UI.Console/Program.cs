using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

using Decked.BuildingBlocks;
using Decked.Core;
using Decked.Interfaces;

using DryIoc;

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
            PreloadAssemblies();

            if (Debugger.IsAttached)
            {
                Execute(args);

                return;
            }

            try
            {
                Execute(args);
            }
            catch (Exception ex)
            {
                System.Console.Error.WriteLine($"{ex.GetType().Name}: {ex.Message}");
                System.Console.Error.WriteLine(ex.StackTrace);
                Environment.Exit(1);
            }
        }

        private static void PreloadAssemblies()
        {
            GC.KeepAlive(typeof(BasicStreamDeckButton));
            // GC.KeepAlive(typeof(.. builtin ));
        }

        private static void Execute(string[] args)
        {
            var options = OptParser.Parse<CommandLineOptions>(args);
            assume(options != null);

            var logger = new ConsoleLogger(options);

            var container = new Container(Rules.Default.WithAutoConcreteTypeResolution());
            container.RegisterInstance<ILogger>(logger);

            var problems = options.GetProblems().ToList();
            if (problems.Any())
            {
                foreach (var problem in problems)
                    System.Console.Error.WriteLine(problem);
                Environment.Exit(1);
            }

            assume(options.MainScreenFilename != null);
            System.Console.WriteLine($"loading screen {Path.GetFullPath(options.MainScreenFilename)}");

            var initialScreenConfiguration = ScreenConfiguration.Load(options.MainScreenFilename);
            var pathResolver = new PathResolver(Path.GetDirectoryName(Path.GetFullPath(options.MainScreenFilename)));
            container.RegisterInstance<IPathResolver>(pathResolver);

            var deck = new DeckRunner(container, logger, pathResolver);
            container.RegisterInstance<IStreamDeckServices>(deck);

            deck.InitializeScreen(initialScreenConfiguration);

            //AppDomain.CurrentDomain.AssemblyResolve += delegate(object sender, ResolveEventArgs eventArgs)
            //                                           {
            //                                               System.Console.WriteLine(eventArgs.Name);

            //                                               return null;
            //                                           };

            deck.Run().GetAwaiter().GetResult();
        }
    }
}
