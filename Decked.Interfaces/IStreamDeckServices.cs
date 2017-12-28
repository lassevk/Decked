using System;

using JetBrains.Annotations;

namespace Decked.Interfaces
{
    [PublicAPI]
    public interface IStreamDeckServices
    {
        void SwitchToScreen([NotNull] string screenName);
    }
}
