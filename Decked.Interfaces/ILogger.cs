using System;
using JetBrains.Annotations;

namespace Decked.Interfaces
{
    [PublicAPI]
    public interface ILogger
    {
        void Debug([NotNull] string line);
        void Log([NotNull] string line);
    }
}
