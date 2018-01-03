using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;

using Decked.Interfaces;

using JetBrains.Annotations;

namespace Decked.UI.Console
{
    [PublicAPI]
    public class CommandLineOptions : IDeckRunnerOptions
    {
        private string _MainScreenFilename = string.Empty;

        [Description("The path to the configuration file for the initial screen")]
        [Opt.Argument(OrderIndex = 1)]
        public string MainScreenFilename
        {
            get { return _MainScreenFilename; }

            [UsedImplicitly]
            set { _MainScreenFilename = value ?? string.Empty; }
        }

        [Opt.BooleanOption("-v")]
        [Opt.BooleanOption("--verbose")]
        [Description("Verbose output, include debug messages")]
        public bool Verbose { get; set; }

        public IEnumerable<string> GetProblems()
        {
            if (string.IsNullOrWhiteSpace(MainScreenFilename))
                yield return "no main screen filename specified";

            if (!File.Exists(MainScreenFilename))
                yield return "main screen file does not exist";
        }
    }
}
