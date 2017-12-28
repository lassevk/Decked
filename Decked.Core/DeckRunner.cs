using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Decked.Interfaces;

using JetBrains.Annotations;

using static Decked.Core.ReSharperValidations;

using Container = DryIoc.Container;

namespace Decked.Core
{
    public class DeckRunner : IStreamDeckServices
    {
        [NotNull]
        private readonly Container _Container;

        [CanBeNull]
        private Screen _Screen;

        [NotNull]
        private readonly DeckDevice _Device;

        public DeckRunner([NotNull] Container container, [NotNull] ILogger logger, [NotNull] PathResolver pathResolver)
        {
            _Container = container ?? throw new ArgumentNullException(nameof(container));
            _Device = DeckDevice.GetInstance() ?? throw new InvalidOperationException("No Stream Deck device found");
        }

        public void InitializeScreen([NotNull] ScreenConfiguration initialScreen)
        {
            _Screen = LoadScreen(initialScreen ?? throw new ArgumentNullException(nameof(initialScreen)));
        }

        private Screen LoadScreen([NotNull] ScreenConfiguration configuration)
        {
            var screen = new Screen(_Container, configuration);
            screen.InitializeButtons();

            return screen;
        }

        public void SwitchToScreen(string screenName)
        {
            throw new NotImplementedException();
        }

        public async Task Run()
        {
            BindToScreen();

            while (true)
            {
                foreach (var app in _Screen.GetApplications())
                    app.OnIdle();

                await Task.Yield();
                (int column, int row, bool isPush)? key = _Device.GetNextKeyEvent();

                if (key.HasValue)
                {
                    assume(_Screen != null);
                    var button = _Screen.GetButtons().Where(btn => btn.row == key.Value.row && btn.column == key.Value.column).Select(btn => btn.button).FirstOrDefault();

                    if (key.Value.isPush)
                        button?.Push();
                    else
                        button?.Release();
                }
            }
        }

        private void BindToScreen()
        {
            if (_Screen == null)
                return;

            foreach (var app in _Screen.GetApplications())
                app.OnSetUp();

            foreach (var button in _Screen.GetButtons())
            {
                _Device.SetButtonIcon(button.column, button.row, button.button.NotNull().Icon);
                button.button.PropertyChanged += ButtonPropertyChanged;
            }
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
            foreach (var existingButton in _Screen.GetButtons())
            {
                if (ReferenceEquals(button, existingButton.button))
                    _Device.SetButtonIcon(existingButton.column, existingButton.row, existingButton.button.NotNull().Icon);
            }
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