using System;
using JetBrains.Annotations;

namespace Decked.Interfaces
{
    [PublicAPI]
    public interface IStreamDeckButtonIdle
    {
        void OnIdle();
    }
}