using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

using JetBrains.Annotations;

// ReSharper disable InconsistentNaming

namespace Decked
{
    internal static class ReSharperValidations
    {
        [Conditional("DEBUG")]
        [ContractAnnotation("expression:false => halt")]
        [UsedImplicitly]
        public static void assume(bool expression, [CanBeNull] string justification = null, [CallerMemberName] string callerMemberName = null, [CallerFilePath] string callerFilePath = null, [CallerLineNumber] int callerLineNumber = 0)
        {
            string justificationFormatted = justification == null ? string.Empty : $"[{justification}] ";
            if (!expression)
                Debug.WriteLine($"assumption {justificationFormatted}did not hold in {callerMemberName} at {callerFilePath}#{callerLineNumber}");
        }

        [ContractAnnotation("expression:false => halt")]
        [UsedImplicitly]
        public static void assert(bool expression, [CanBeNull] string assertion = null, [CallerMemberName] string callerMemberName = null, [CallerFilePath] string callerFilePath = null, [CallerLineNumber] int callerLineNumber = 0)
        {
            string assertionFormatted = assertion == null ? string.Empty : $"[{assertion}] ";
            if (!expression)
                Trace.WriteLine($"assertion {assertionFormatted} failed in {callerMemberName} at {callerFilePath}#{callerLineNumber}");
        }

        [NotNull]
        [ContractAnnotation("value:null => halt")]
        public static T NotNull<T>(this T value, [CallerMemberName] string callerMemberName = null, [CallerFilePath] string callerFilePath = null, [CallerLineNumber] int callerLineNumber = 0)
            where T : class
        {
            // ReSharper disable ExplicitCallerInfoArgument
            assert(value != null, callerMemberName: callerMemberName, callerFilePath: callerFilePath, callerLineNumber: callerLineNumber);
            // ReSharper restore ExplicitCallerInfoArgument

            return value;
        }
    }
}
