using System;
using System.Threading.Tasks;

using JetBrains.Annotations;

namespace Decked.Core.Interfaces
{
    public interface IDeckRunner
    {
        [NotNull]
        Task Run();
    }
}