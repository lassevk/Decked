using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using JetBrains.Annotations;

namespace Decked.UI.Console
{
    [PublicAPI]
    public class CommandLineOptions
    {
        [Description("The path to the configuration file for the initial screen")]
        [Opt.Argument(OrderIndex = 1)]
        public string MainScreenFilename
        {
            get;

            [UsedImplicitly]
            set;
        }

        [Opt.BooleanOption("-v")]
        [Opt.BooleanOption("--verbose")]
        [Description("Verbose output, include debug messages")]
        public bool Verbose { get; set; }

        [NotNull, ItemNotNull]
        public IEnumerable<string> GetProblems()
        {
            if (string.IsNullOrWhiteSpace(MainScreenFilename))
                yield return "no main screen filename specified";

            if (!File.Exists(MainScreenFilename))
                yield return "main screen file does not exist";
        }
    }
}
