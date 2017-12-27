using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

// ReSharper disable InconsistentNaming

namespace Decked.Core
{
    internal static class ReSharperAssumptions
    {
        [Conditional("DEBUG")]
        [ContractAnnotation("expression:false => halt")]
        [UsedImplicitly]
        public static void assume(bool expression, [CallerMemberName] string callerMemberName = null, [CallerFilePath] string callerFilePath = null, [CallerLineNumber] int callerLineNumber = 0)
        {
            if (!expression)
                Debug.WriteLine($"assumption did not hold in {callerMemberName} at {callerFilePath}#{callerLineNumber}");
        }
    }
}
