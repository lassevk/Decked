using System;
using System.Drawing;

using Decked.Interfaces;

using JetBrains.Annotations;

namespace Decked.BuildingBlocks
{
    [PublicAPI]
    public class BasicStreamDeckButton : NotifyPropertyChangedBase, IStreamDeckButton
    {
        [NotNull]
        private readonly Action _PushAction;

        [CanBeNull]
        private readonly Action _ReleaseAction;

        public BasicStreamDeckButton([NotNull] Bitmap icon, [NotNull] Action pushAction, [CanBeNull] Action releaseAction = null)
        {
            Icon = icon ?? throw new ArgumentNullException(nameof(icon));
            _PushAction = pushAction ?? throw new ArgumentNullException(nameof(pushAction));
            _ReleaseAction = releaseAction;
        }

        public Bitmap Icon { get; private set; }

        public void SetIcon([NotNull] Bitmap value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            if (ReferenceEquals(Icon, value))
                return;

            Icon = value;
            OnPropertyChanged(nameof(Icon));
        }

        public void Push() => _PushAction();
        public void Release() => _ReleaseAction?.Invoke();
    }
}