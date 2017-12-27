using System;
using System.Drawing;
using JetBrains.Annotations;

namespace Decked.Interfaces
{
    [PublicAPI]
    public interface IStreamDeckButton
    {
        Bitmap Icon
        {
            get;
        }

        event EventHandler IconChanged;

        void OnSetUp();
        void OnTearDown();
        void OnIdle();

        void Push();
        void Release();
    }
}