using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using JetBrains.Annotations;

namespace Decked.BuildingBlocks
{
    [PublicAPI]
    public class NotifyPropertyChangedBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}