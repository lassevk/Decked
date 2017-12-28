using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;

using Decked.BuildingBlocks;
using Decked.Interfaces;

using JetBrains.Annotations;

namespace Decked.Example.Notepad
{
    [PublicAPI]
    public class NotepadApplication : NotifyPropertyChangedBase, IStreamDeckApplication
    {
        [NotNull]
        private readonly IStreamDeckServices _Services;

        [CanBeNull]
        private Process _NotepadProcess;

        public NotepadApplication([NotNull] IStreamDeckServices services)
        {
            _Services = services;

            _NotepadProcess = GetNotepadProcess();

            StartNotepadButton = new BasicStreamDeckButton(StartNotepadButtonIcon, StartNotepadAction);
            StopNotepadButton = new BasicStreamDeckButton(StopNotepadButtonIcon, StopNotepadAction);
        }

        private void StopNotepadAction()
        {
            _NotepadProcess?.Kill();
            _NotepadProcess = null;

            ConfigureIcons();
        }

        private void StartNotepadAction()
        {
            _NotepadProcess = Process.Start("notepad.exe");
            ConfigureIcons();
        }

        [NotNull]
        private Bitmap StartNotepadButtonIcon => _NotepadProcess == null ? IconResources.OnButton : IconResources.OffButton;
        
        [NotNull]
        private Bitmap StopNotepadButtonIcon => _NotepadProcess == null ? IconResources.OffButton : IconResources.OnButton;

        private void ConfigureIcons()
        {
            StartNotepadButton.SetIcon(StartNotepadButtonIcon);
            StopNotepadButton.SetIcon(StopNotepadButtonIcon);
        }

        public void OnSetUp()
        {
        }

        public void OnTearDown()
        {
        }

        [NotNull]
        public BasicStreamDeckButton StartNotepadButton { get; }
        
        [NotNull]
        public BasicStreamDeckButton StopNotepadButton { get; }

        public void OnIdle()
        {
            var notepadProcess = GetNotepadProcess();

            bool notepadWasRunning = _NotepadProcess != null;
            bool notepadIsRunning = notepadProcess != null;

            _NotepadProcess = notepadProcess;

            if (notepadWasRunning != notepadIsRunning)
                ConfigureIcons();
        }

        private static Process GetNotepadProcess()
        {
            return (from process in Process.GetProcesses()
                    where process != null
                    let mainModule = process.MainModule
                    where mainModule != null && StringComparer.CurrentCultureIgnoreCase.Equals(mainModule.ModuleName, "NOTEPAD.EXE")
                    select process).FirstOrDefault();
        }
    }
}