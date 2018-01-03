using System;
using System.Linq;

using Decked.Core.Framework;
using Decked.Core.Interfaces;

using HidLibrary;

using JetBrains.Annotations;

namespace Decked.Devices
{
    [UsedImplicitly]
    public class StreamDeckLocator : IStreamDeckLocator
    {
        private const int _ElgatoVendorId = 0x00000fd9;
        private const int _ElgatoStreamDeckProductId = 0x00000060;

        public IStreamDeck GetInstance()
        {
            var device = HidDevices.Enumerate(_ElgatoVendorId, _ElgatoStreamDeckProductId).NotNull().FirstOrDefault();

            if (device == null)
                throw new InvalidOperationException("No Stream Deck device found");

            return new StreamDeck(device);
        }
    }
}