﻿using System;

using Decked.Interfaces;

using JetBrains.Annotations;

namespace Decked.Core
{
    public class DeckRunner : IStreamDeckServices
    {
        [NotNull]
        private readonly ILogger _Logger;

        [CanBeNull]
        private Screen _Screen;

        public DeckRunner([NotNull] ILogger logger, [NotNull] ScreenConfiguration initialScreen)
        {
            _Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _Screen = LoadScreen(initialScreen ?? throw new ArgumentNullException(nameof(initialScreen)));
        }

        private Screen LoadScreen([NotNull] ScreenConfiguration configuration)
        {
            return new Screen(this, _Logger, configuration);
        }

        public void SwitchToScreen(string screenName)
        {
            throw new NotImplementedException();
        }

        public void Run()
        {
            throw new NotImplementedException();
        }
    }
}