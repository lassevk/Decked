using System;
using System.Collections.Generic;

using JetBrains.Annotations;

namespace Decked.Devices
{
    public interface IStreamDeckLocator
    {
        [NotNull, ItemNotNull]
        IEnumerable<IStreamDeck> FindAll();
        
        [NotNull]
        IStreamDeck OpenByPath([NotNull] string devicePath);
    }
}
