﻿using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

using Decked.BuildingBlocks;
using Decked.Interfaces;

using DryIoc;

using JetBrains.Annotations;
using Opt;

using static Decked.ReSharperValidations;

namespace Decked.UI.Console
{
    [UsedImplicitly]
    class Program
    {
        static void Main(string[] args)
        {
            if (Debugger.IsAttached)
            {
                Execute(args);

                return;
            }

            try
            {
                Execute(args);
            }
            catch (Exception ex)
            {
                System.Console.Error.WriteLine($"{ex.GetType().Name}: {ex.Message}");
                System.Console.Error.WriteLine(ex.StackTrace);
                Environment.Exit(1);
            }
        }

        private static void Execute(string[] args)
        {
            Services.Configure(args ?? new string[0]).Resolve<IDeckRunner>().NotNull().Run().GetAwaiter().GetResult();
        }
    }
}