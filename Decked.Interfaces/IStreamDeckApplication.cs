using System;
using System.ComponentModel;

using JetBrains.Annotations;

namespace Decked.Interfaces
{
    [PublicAPI]
    public interface IStreamDeckApplication : INotifyPropertyChanged
    {
        void OnSetUp();
        void OnTearDown();

        void OnIdle();
    }
}