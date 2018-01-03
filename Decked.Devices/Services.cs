using System;

using Decked.Core.Framework;
using Decked.Core.Interfaces;

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
            container.RegisterDelegate(resolver => resolver.Resolve<IStreamDeckLocator>().NotNull().GetInstance());
            container.Register<IStreamDeckDriver, StreamDeckDriver>(Reuse.Singleton);
        }
    }
}
