using System;
using System.IO;

using Decked.Interfaces;

using JetBrains.Annotations;

namespace Decked
{
    public class PathResolver : IPathResolver
    {
        [NotNull]
        private readonly string _BasePath;

        public PathResolver([NotNull] IDeckRunnerOptions options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            if (options.MainScreenFilename == null)
                throw new ArgumentException("options.MainScreenFilename is null", nameof(options));

            _BasePath = Path.GetDirectoryName(Path.GetFullPath(options.MainScreenFilename)).NotNull();
        }

        public string ResolveRelativePath([NotNull] string path)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));

            if (Path.IsPathRooted(path))
                return Path.GetFullPath(path);

            return Path.Combine(_BasePath, path);
        }
    }
}
