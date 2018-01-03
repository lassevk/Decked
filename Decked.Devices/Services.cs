using System;

using DryIoc;

using JetBrains.Annotations;

namespace Decked.Devices
{
    internal static class Services
    {
        public static void Register([NotNull] Container container)
        {
            if (container == null)
                throw new ArgumentNullException(nameof(container));

            container.Register<IStreamDeckLocator, StreamDeckLocator>();
        }
    }
}
