using System;

using JetBrains.Annotations;

namespace Decked.Core
{
    [UsedImplicitly]
    public class ScreenButtonConfiguration
    {
        [UsedImplicitly, CanBeNull]
        public string Assembly { get; set; }

        [UsedImplicitly, CanBeNull]
        public string Type { get; set; }

        [UsedImplicitly, CanBeNull]
        public string Property { get; set; }
    }
}