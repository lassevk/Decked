using System;
using System.Threading.Tasks;

using JetBrains.Annotations;

namespace Decked
{
    public interface IDeckRunner
    {
        [NotNull]
        Task Run();
    }
}