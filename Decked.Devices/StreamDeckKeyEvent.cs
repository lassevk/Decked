using System;

namespace Decked.Devices
{
    public class StreamDeckKeyEvent
    {
        public StreamDeckKeyEvent(int column, int row, bool isKeyPush)
        {
            Column = column;
            Row = row;
            IsKeyPush = isKeyPush;
        }

        public int Column { get; }
        public int Row { get; }

        public bool IsKeyPush { get; }
        public bool IsKeyRelease => !IsKeyPush;

        public override string ToString() => $"{(IsKeyPush ? "push" : "release")} {Column},{Row}";
    }
}