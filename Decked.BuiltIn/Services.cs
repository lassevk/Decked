using System;

using DryIoc;

using JetBrains.Annotations;

namespace Decked.BuiltIn
{
    internal static class Services
    {
        public static void Register([NotNull] Container container)
        {
            if (container == null)
                throw new ArgumentNullException(nameof(container));
        }
    }
}
