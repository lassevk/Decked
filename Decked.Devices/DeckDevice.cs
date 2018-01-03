using System;
using System.Collections.Concurrent;
using System.Drawing;
using System.Threading.Tasks;

using Decked.Core.Interfaces;

using JetBrains.Annotations;

namespace Decked.Devices
{
    public class DeckDevice : IDeckDevice
    {
        private const int _MaxColumns = 5;
        private const int _MaxRows = 3;
        private const int _IconWidth = 72;
        private const int _IconHeight = 72;

        //[NotNull]
        //private readonly IStreamDeck _Device;

        [NotNull]
        private readonly ConcurrentQueue<(int column, int row, bool isDown)> _KeyEvents = new ConcurrentQueue<(int column, int row, bool isDown)>();

        private DeckDevice([NotNull] IStreamDeck device)
        {
            //_Device = device ?? throw new ArgumentNullException(nameof(device));
            //_Device.KeyPressed += _Device_KeyPressed;
        }

        //private void _Device_KeyPressed(object sender, StreamDeckKeyEventArgs e)
        //{
        //    //int row = e.Key / _MaxColumns + 1;
        //    //int column = _MaxColumns - e.Key % _MaxColumns;

        //    //_KeyEvents.Enqueue((column, row, e.IsDown));
        //}

        public Task SetButtonIcon(int column, int row, [NotNull] Bitmap icon)
        {
            return Task.CompletedTask;

            //if (column < 1 || column > _MaxColumns)
            //    throw new ArgumentOutOfRangeException(nameof(column), $"column must be in the range 1..{_MaxColumns}");

            //if (row < 1 || row > _MaxRows)
            //    throw new ArgumentOutOfRangeException(nameof(row), $"row must be in the range 1..{_MaxRows}");

            //if (icon == null)
            //    throw new ArgumentNullException(nameof(icon));

            //if (icon.Width != _IconWidth || icon.Height != _IconHeight)
            //    throw new ArgumentException($"icon must be {_IconWidth}x{_IconHeight} pixels", nameof(icon));

            //var index = _MaxColumns - column + (row - 1) * _MaxColumns;
            //byte[] bytes = BitmapToBytes(icon);

            //return Task.Run(() => _Device.SetKeyBitmap(index, bytes));

        }

        [NotNull]
        private byte[] BitmapToBytes([NotNull] Bitmap icon)
        {
            var result = new byte[3 * icon.Width * icon.Height];
            int index = 0;

            for (int y = 0; y < icon.Height; y++)
            {
                for (int x = 0; x < icon.Width; x++)
                {
                    var pixel = icon.GetPixel(x, y);
                    result[index++] = pixel.B;
                    result[index++] = pixel.G;
                    result[index++] = pixel.R;
                }
            }

            return result;
        }

        [CanBeNull]
        public (int column, int row, bool isPush)? GetNextKeyEvent()
        {
            if (_KeyEvents.TryDequeue(out var result))
                return result;

            return null;
        }
    }
}