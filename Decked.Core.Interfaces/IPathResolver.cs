using System;

using JetBrains.Annotations;

namespace Decked.Core.Interfaces
{
    public interface IPathResolver
    {
        [NotNull]
        string ResolveRelativePath([NotNull] string path);
    }
}
