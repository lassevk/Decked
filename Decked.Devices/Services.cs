﻿using System;

using Decked.Core.Interfaces;
using Decked.Interfaces;

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
            container.Register<IStreamDeck, StreamDeck>();
        }
    }
}