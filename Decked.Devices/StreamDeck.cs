using System;
using System.Linq;
using System.Threading.Tasks;

using Decked.Core.Framework;
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

        public async Task<bool[]> GetNextKeyState()
        {
            var report = (await _Device.ReadReportAsync().NotNull()).NotNull();

            return report.Data.NotNull().Take(15).Select(i => i != 0).ToArray();
        }

        public (int column, int row) DecodeButtonIndexToPosition(int buttonIndex)
        {
            var column = 5 - buttonIndex % 5;
            var row = 1 + buttonIndex / 5;

            return (column, row);
        }

        public override string ToString() => _Device.DevicePath ?? base.ToString();
    }
}