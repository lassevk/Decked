using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

using Decked.Core.Framework;
using Decked.Core.Interfaces;
using Decked.Interfaces;

using DryIoc;

using JetBrains.Annotations;

namespace Decked.Core.Services
{
    public class Screen
    {
        [NotNull]
        private static readonly Dictionary<string, Assembly> _Assemblies = new Dictionary<string, Assembly>();

        [NotNull]
        private readonly Container _Container;

        [NotNull]
        private readonly ScreenConfiguration _Configuration;

        [NotNull]
        private readonly List<(int column, int row, IStreamDeckButton button)> _Buttons = new List<(int column, int row, IStreamDeckButton button)>();

        [NotNull]
        private readonly List<IStreamDeckApplication> _Applications = new List<IStreamDeckApplication>();

        public Screen([NotNull] Container container, [NotNull] ScreenConfiguration configuration)
        {
            _Container = container ?? throw new ArgumentNullException(nameof(container));
            _Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public void InitializeButtons()
        {
            foreach (var buttonRow in _Configuration.Buttons)
            {
                if (buttonRow.Value == null)
                    continue;

                foreach (var buttonColumn in buttonRow.Value)
                {
                    var screenButtonConfiguration = buttonColumn.Value;

                    if (screenButtonConfiguration == null)
                        continue;

                    var appAndButton = InitializeButton(screenButtonConfiguration);
                    _Buttons.Add((buttonColumn.Key, buttonRow.Key, appAndButton.button));

                    if (appAndButton.application != null)
                        _Applications.Add(appAndButton.application);
                }
            }
        }

        [NotNull]
        private (IStreamDeckApplication application, IStreamDeckButton button) InitializeButton([NotNull] ScreenButtonConfiguration buttonConfigurationValue)
        {
            ReSharperValidations.assume(buttonConfigurationValue.Type != null, "Type presence should've been validated during load");
            ReSharperValidations.assume(buttonConfigurationValue.Assembly != null, "Assembly presence should've been validated during load");

            var instance = LoadInstance(buttonConfigurationValue.Type, buttonConfigurationValue.Assembly);

            if (!string.IsNullOrWhiteSpace(buttonConfigurationValue.Property))
            {
                var property = instance.GetType().GetProperty(buttonConfigurationValue.Property);
                if (property == null)
                    throw new InvalidOperationException($"Button property {buttonConfigurationValue.Property} on type {buttonConfigurationValue.Type} in assembly {buttonConfigurationValue.Assembly} was not found");

                var propertyInstance = property.GetValue(instance);

                if (propertyInstance is IStreamDeckButton propertyButton)
                    return (instance as IStreamDeckApplication, propertyButton);

                throw new InvalidOperationException($"Button property {buttonConfigurationValue.Property} on type {buttonConfigurationValue.Type} in assembly {buttonConfigurationValue.Assembly} is not a IStreamDeckButton instance");
            }

            if (instance is IStreamDeckButton button)
                return (instance as IStreamDeckApplication, button);

            throw new InvalidOperationException($"Button {buttonConfigurationValue.Type} in assembly {buttonConfigurationValue.Assembly} is not a IStreamDeckButton instance, and no Property configuration was found");
        }

        [NotNull]
        private object LoadInstance([NotNull] string typeName, [NotNull] string assemblyName)
        {
            var resolvedAssemblyName = ResolveAssembly(assemblyName);
            var assembly = LoadAssembly(resolvedAssemblyName);

            var type = assembly.GetType(typeName);
            if (type == null)
                throw new InvalidOperationException($"Assembly {assemblyName} does not have a type named {typeName}");

            return _Container.Resolve(type).NotNull();
        }

        [NotNull]
        private Assembly LoadAssembly([NotNull] string assemblyName)
        {
            var key = Path.GetFullPath(assemblyName).ToUpper();

            if (_Assemblies.TryGetValue(key, out var assembly))
                return assembly;

            _Container.Resolve<ILogger>().NotNull().Debug($"loading assembly ${assemblyName}");
            assembly = Assembly.LoadFile(assemblyName);
            _Assemblies[key] = assembly;

            return assembly;
        }

        [NotNull]
        private string ResolveAssembly([NotNull] string assemblyName)
        {
            // TODO: Resolve through screen dictionary
            return _Container.Resolve<IPathResolver>().NotNull().ResolveRelativePath(assemblyName);
        }

        [NotNull, ItemNotNull]
        public IEnumerable<IStreamDeckApplication> GetApplications()
        {
            return _Applications;
        }

        [NotNull]
        public IEnumerable<(int column, int row, IStreamDeckButton button)> GetButtons()
        {
            return _Buttons;
        }
    }
}