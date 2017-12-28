using System;
using System.Drawing;

using JetBrains.Annotations;

namespace Decked.BuildingBlocks
{
    public class BasicToggleStreamDeckButton : BasicStreamDeckButton
    {
        [NotNull]
        private readonly Bitmap _OnIcon;

        [NotNull]
        private readonly Bitmap _OffIcon;

        private bool _State;

        public BasicToggleStreamDeckButton([NotNull] Bitmap onIcon, [NotNull] Bitmap offIcon, bool initialState, [NotNull] Action pushAction, [CanBeNull] Action releaseAction = null)
            : base(GetIcon(initialState, onIcon, offIcon), pushAction, releaseAction)
        {
            _OnIcon = onIcon ?? throw new ArgumentNullException(nameof(onIcon));
            _OffIcon = offIcon ?? throw new ArgumentNullException(nameof(offIcon));
            _State = initialState;
        }

        [NotNull]
        private static Bitmap GetIcon(bool state, [NotNull] Bitmap onIcon, [NotNull] Bitmap offIcon)
        {
            if (onIcon == null)
                throw new ArgumentNullException(nameof(onIcon));

            if (offIcon == null)
                throw new ArgumentNullException(nameof(offIcon));

            return state ? onIcon : offIcon;
        }

        public bool State
        {
            get => _State;
            set
            {
                if (_State == value)
                    return;

                _State = value;
                OnPropertyChanged();

                SetIcon(GetIcon(_State, _OnIcon, _OffIcon));
            }
        }
    }
}