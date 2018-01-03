using System;

using JetBrains.Annotations;

namespace Decked.Core.Interfaces
{
    public interface IStreamDeckLocator
    {
        [NotNull]
        IStreamDeck GetInstance();
    }
}
