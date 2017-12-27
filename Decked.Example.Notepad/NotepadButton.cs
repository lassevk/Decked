using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using Decked.Interfaces;
using JetBrains.Annotations;

namespace Decked.Example.Notepad
{
    [PublicAPI]
    public class NotepadButton : IStreamDeckButton, IStreamDeckButtonIdle
    {
        [CanBeNull]
        private Process _NotepadProcess;

        public Bitmap Icon
        {
            get;
            private set;
        }

        public event EventHandler IconChanged;

        public void Push()
        {
            OnIdle();
            if (_NotepadProcess != null)
            {
                _NotepadProcess.Kill();
                _NotepadProcess = null;
            }
            else
                Process.Start("notepad.exe");
        }

        public bool IsNotepadRunning => _NotepadProcess != null;
        
        public void Release()
        {
            // Do nothing
        }

        public void OnIdle()
        {
            var notepad = (from process in Process.GetProcesses()
                           where process != null
                           let mainModule = process.MainModule
                           where mainModule != null && StringComparer.CurrentCultureIgnoreCase.Equals(mainModule.ModuleName, "NOTEPAD.EXE")
                           select process).FirstOrDefault();

            if ((notepad != null) != (_NotepadProcess != null))
            {
                if (notepad != null)
                    Icon = null;
                else
                    Icon = null;
                OnIconChanged();
            }

            _NotepadProcess = notepad;
        }

        private void OnIconChanged()
        {
            IconChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
