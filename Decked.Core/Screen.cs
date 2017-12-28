using System;

using Decked.Interfaces;

using JetBrains.Annotations;

namespace Decked.Core
{
    public class Screen
    {
        [NotNull]
        private readonly IStreamDeckServices _StreamDeckServices;

        [NotNull]
        private readonly ILogger _Logger;

        [NotNull]
        private readonly ScreenConfiguration _Configuration;

        public Screen([NotNull] IStreamDeckServices streamDeckServices, [NotNull] ILogger logger, [NotNull] ScreenConfiguration configuration)
        {
            _StreamDeckServices = streamDeckServices ?? throw new ArgumentNullException(nameof(streamDeckServices));
            _Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }
    }
}