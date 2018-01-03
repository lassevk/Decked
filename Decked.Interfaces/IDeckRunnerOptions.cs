using System;
using System.Collections.Generic;

using JetBrains.Annotations;

namespace Decked.Interfaces
{
    public interface IDeckRunnerOptions
    {
        [NotNull]
        string MainScreenFilename
        {
            get;
        }

        bool Verbose { get; }
        
        [NotNull, ItemNotNull]
        IEnumerable<string> GetProblems();
    }
}
