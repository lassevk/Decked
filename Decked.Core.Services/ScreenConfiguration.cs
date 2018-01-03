using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using Decked.Core.Framework;

using JetBrains.Annotations;

using Newtonsoft.Json;

namespace Decked.Core.Services
{
    [UsedImplicitly]
    public class ScreenConfiguration
    {
        [UsedImplicitly]
        public Dictionary<string, string> Assemblies { get; } = new Dictionary<string, string>();

        [UsedImplicitly, NotNull]
        public Dictionary<int, Dictionary<int, ScreenButtonConfiguration>> Buttons { get; } = new Dictionary<int, Dictionary<int, ScreenButtonConfiguration>>();

        [NotNull, ItemNotNull]
        public IEnumerable<string> GetProblems()
        {
            if (Buttons.Count == 0)
                yield return "No buttons configured";

            foreach (var row in Buttons)
            {
                if (row.Key < 1 || row.Key > 3)
                    yield return $"Button row ${row.Key} does not exist, only 1-3 are legal";

                if (row.Value == null)
                    continue;

                foreach (var column in row.Value)
                {
                    if (column.Key < 1 || column.Key > 5)
                        yield return $"Button column ${column.Key} does not exist, only 1-5 are legal";

                    if (column.Value == null)
                        continue;

                    foreach (var buttonProblem in column.Value.GetProblems())
                        yield return $"Button {column.Key},{row.Key}: {buttonProblem}";
                }
            }
        }

        [NotNull]
        public static ScreenConfiguration Load([NotNull] string filename)
        {
            if (filename == null)
                throw new ArgumentNullException(nameof(filename));

            var configuration = JsonConvert.DeserializeObject<ScreenConfiguration>(File.ReadAllText(filename, Encoding.UTF8));
            ReSharperValidations.assert(configuration != null);

            return configuration;
        }
    }
}