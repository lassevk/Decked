﻿using System;

using Decked.Interfaces;

using DryIoc;

using JetBrains.Annotations;

namespace Decked
{
    internal static class Services
    {
        public static void Register([NotNull] Container container)
        {
            if (container == null)
                throw new ArgumentNullException(nameof(container));

            container.Register<IDeckRunner, DeckRunner>(Reuse.Singleton);
            container.RegisterDelegate<IStreamDeckServices>(resolver => resolver.Resolve<IDeckRunner>() as IStreamDeckServices);
        }
    }
}
