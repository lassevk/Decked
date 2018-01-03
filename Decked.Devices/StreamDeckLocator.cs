using System;
using System.Collections.Generic;

using Decked.Core.Framework;
using Decked.Core.Interfaces;
using Decked.Interfaces;

using HidLibrary;

using JetBrains.Annotations;

namespace Decked.Devices
{
    [UsedImplicitly]
    public class StreamDeckLocator : IStreamDeckLocator
    {
        [NotNull]
        private readonly Func<HidDevice, IStreamDeck> _GetStreamDeck;

        private const int _ElgatoVendorId = 0x00000fd9;
        private const int _ElgatoStreamDeckProductId = 0x00000060;

        public StreamDeckLocator([NotNull] Func<HidDevice, IStreamDeck> getStreamDeck)
        {
            _GetStreamDeck = getStreamDeck ?? throw new ArgumentNullException(nameof(getStreamDeck));
        }

        public IEnumerable<IStreamDeck> FindAll()
        {
            foreach (var device in HidDevices.Enumerate(_ElgatoVendorId, _ElgatoStreamDeckProductId).NotNull())
            {
                yield return _GetStreamDeck(device).NotNull();
            }
        }

        public IStreamDeck OpenByPath(string devicePath)
        {
            if (devicePath == null)
                throw new ArgumentNullException(nameof(devicePath));

            var hidDevice = HidDevices.GetDevice(devicePath);

            if (hidDevice == null)
                throw new InvalidOperationException("No HidDevice found with the specified device path");

            return _GetStreamDeck(hidDevice).NotNull();
        }
    }
}