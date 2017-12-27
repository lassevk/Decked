using System;
using JetBrains.Annotations;

namespace Decked.Interfaces
{
    [PublicAPI]
    public interface ILogger
    {
        void Log(string line);
    }
}
