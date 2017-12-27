using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using JetBrains.Annotations;

using Newtonsoft.Json;

namespace Decked.Core
{
    [UsedImplicitly]
    public class ScreenConfiguration
    {
        [UsedImplicitly]
        public Dictionary<string, string> Assemblies { get; } = new Dictionary<string, string>();

        [UsedImplicitly]
        public Dictionary<int, Dictionary<int, ScreenButtonConfiguration>> Buttons { get; } = new Dictionary<int, Dictionary<int, ScreenButtonConfiguration>>();

        public static ScreenConfiguration Load([NotNull] string filename)
        {
            if (filename == null)
                throw new ArgumentNullException(nameof(filename));

            return JsonConvert.DeserializeObject<ScreenConfiguration>(File.ReadAllText(filename, Encoding.UTF8));
        }
    }
}