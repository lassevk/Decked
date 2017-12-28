using System;

using JetBrains.Annotations;

namespace Decked.Interfaces
{
    public interface IPathResolver
    {
        [NotNull]
        string ResolveRelativePath([NotNull] string path);
    }
}
