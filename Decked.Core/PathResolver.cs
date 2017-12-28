using System;
using System.IO;

using Decked.Interfaces;

using JetBrains.Annotations;

namespace Decked.Core
{
    public class PathResolver : IPathResolver
    {
        [NotNull]
        private readonly string _BasePath;

        public PathResolver([NotNull] string basePath)
        {
            _BasePath = basePath ?? throw new ArgumentNullException(nameof(basePath));
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
