using System;
using System.ComponentModel;
using System.Drawing;
using JetBrains.Annotations;

namespace Decked.Interfaces
{
    [PublicAPI]
    public interface IStreamDeckButton : INotifyPropertyChanged
    {
        [NotNull]
        Bitmap Icon
        {
            get;
        }

        void Push();
        void Release();
    }
}