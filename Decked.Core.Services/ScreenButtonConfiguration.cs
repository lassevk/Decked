using System;
using System.Collections.Generic;

using JetBrains.Annotations;

namespace Decked.Core.Services
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

        [NotNull, ItemNotNull]
        public IEnumerable<string> GetProblems()
        {
            if (Assembly == null)
                yield return "Missing assembly name";

            if (Type == null)
                yield return "Missing type name";
        }
    }
}