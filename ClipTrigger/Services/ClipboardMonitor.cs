using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace ClipTrigger.Services
{
    public class ClipboardMonitor : IDisposable
    {
        private const int WmClipboardUpdate = 0x031D;

        private HwndSource windowHandleSource;

        public event Action<string> ClipboardTextChanged;

        public void Start(Window window)
        {
            windowHandleSource = (HwndSource)PresentationSource.FromVisual(window);
            if (windowHandleSource != null)
            {
                windowHandleSource.AddHook(WndProc);
                AddClipboardFormatListener(windowHandleSource.Handle);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            Stop();
        }

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool AddClipboardFormatListener(IntPtr windowHandle);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool RemoveClipboardFormatListener(IntPtr windowHandle);

        private void Stop()
        {
            if (windowHandleSource != null)
            {
                RemoveClipboardFormatListener(windowHandleSource.Handle);
                windowHandleSource.RemoveHook(WndProc);
                windowHandleSource = null;
            }
        }

        private IntPtr WndProc(IntPtr windowHandle, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg == WmClipboardUpdate)
            {
                if (Clipboard.ContainsText())
                {
                    var text = Clipboard.GetText().Trim();
                    ClipboardTextChanged?.Invoke(text);
                }

                handled = true;
            }

            return IntPtr.Zero;
        }
    }
}