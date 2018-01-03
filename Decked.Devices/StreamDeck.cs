using System;

using Decked.Core.Interfaces;

using HidLibrary;

using JetBrains.Annotations;

namespace Decked.Devices
{
    [UsedImplicitly]
    public class StreamDeck : IStreamDeck
    {
        [NotNull]
        private readonly HidDevice _Device;

        public StreamDeck([NotNull] HidDevice device)
        {
            _Device = device ?? throw new ArgumentNullException(nameof(device));
        }

        public override string ToString() => _Device.DevicePath ?? base.ToString();
    }
}