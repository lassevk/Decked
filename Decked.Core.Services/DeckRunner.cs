using System;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;

using Decked.Core.Framework;
using Decked.Core.Interfaces;
using Decked.Interfaces;

using JetBrains.Annotations;

namespace Decked.Core.Services
{
    public class DeckRunner : IStreamDeckServices, IDeckRunner
    {
        [NotNull]
        private readonly ILogger _Logger;

        [NotNull]
        private readonly IDeckRunnerOptions _Options;

        [NotNull]
        private readonly IStreamDeckLocator _StreamDeckLocator;

        [CanBeNull]
        private Screen _Screen;

        //[NotNull]
        //private readonly DeckDevice _Device;

        public DeckRunner([NotNull] ILogger logger, [NotNull] IDeckRunnerOptions options, [NotNull] IStreamDeckLocator streamDeckLocator)
        {
            _Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _Options = options ?? throw new ArgumentNullException(nameof(options));
            _StreamDeckLocator = streamDeckLocator ?? throw new ArgumentNullException(nameof(streamDeckLocator));
        }

        public void InitializeScreen([NotNull] ScreenConfiguration initialScreen)
        {
            _Screen = LoadScreen(initialScreen ?? throw new ArgumentNullException(nameof(initialScreen)));
        }

        private Screen LoadScreen([NotNull] ScreenConfiguration configuration)
        {
            throw new NotImplementedException();

            //var screen = new Screen(_Container, configuration);
            //screen.InitializeButtons();

            //return screen;
        }

        public void SwitchToScreen(string screenName)
        {
            throw new NotImplementedException();
        }
        
        public async Task Run()
        {
            await Task.Delay(1).NotNull();

            _Logger.Log($"loading screen {Path.GetFullPath(_Options.MainScreenFilename)}");

            var initialScreenConfiguration = ScreenConfiguration.Load(_Options.MainScreenFilename);

            //BindToScreen();

            //while (true)
            //{
            //    foreach (var app in _Screen.GetApplications())
            //        app.OnIdle();

            //    await Task.Yield();
            //    (int column, int row, bool isPush)? key = _Device.GetNextKeyEvent();

            //    if (key.HasValue)
            //    {
            //        ReSharperValidations.assume(_Screen != null);
            //        var button = _Screen.GetButtons().Where(btn => btn.row == key.Value.row && btn.column == key.Value.column).Select(btn => btn.button).FirstOrDefault();

            //        if (key.Value.isPush)
            //            button?.Push();
            //        else
            //            button?.Release();
            //    }
            //}
        }

        private void BindToScreen()
        {
            //if (_Screen == null)
            //    return;

            //foreach (var app in _Screen.GetApplications())
            //    app.OnSetUp();

            //foreach (var button in _Screen.GetButtons())
            //{
            //    _Device.SetButtonIcon(button.column, button.row, button.button.NotNull().Icon);
            //    button.button.PropertyChanged += ButtonPropertyChanged;
            //}
        }

        private void ButtonPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (sender == null || e == null)
                return;

            var button = (IStreamDeckButton)sender;

            switch (e.PropertyName)
            {
                case "Icon":
                    RefreshIconForButton(button);

                    break;
            }
        }

        private void RefreshIconForButton(IStreamDeckButton button)
        {
            //foreach (var existingButton in _Screen.GetButtons())
            //{
            //    if (ReferenceEquals(button, existingButton.button))
            //        _Device.SetButtonIcon(existingButton.column, existingButton.row, existingButton.button.NotNull().Icon);
            //}
        }

        private void UnbindFromScreen()
        {
            if (_Screen == null)
                return;

            foreach (var button in _Screen.GetButtons())
                button.button.PropertyChanged -= ButtonPropertyChanged;
            foreach (var app in _Screen.GetApplications())
                app.OnTearDown();
        }
    }
}