using System;
using System.Collections.Generic;
using System.IO;
using JetBrains.Annotations;

namespace Decked.UI.Console
{
    public class CommandLineOptions
    {
        [Opt.Argument(OrderIndex = 1)]
        public string MainScreenFilename
        {
            get;

            [UsedImplicitly]
            set;
        }

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
