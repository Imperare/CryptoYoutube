using Microsoft.Win32;
using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace CryptoYoutube.Services
{
    public class WindowsSession : IDisposable
    {
        const int desktop_ReadObjects = 0x0001;
        const int desktop_WriteObjects = 0x0080;

        private bool disposed;
        public event EventHandler<SessionSwitchEventArgs> StateChanged;

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr OpenInputDesktop(int dwFlags, bool fInherit, int dwDesiredAccess);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool CloseDesktop(IntPtr hDesktop);


        [PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
        public WindowsSession()
        {
            SystemEvents.SessionSwitch += new SessionSwitchEventHandler(SystemEvents_SessionSwitch);
        }

        void SystemEvents_SessionSwitch(object sender, SessionSwitchEventArgs e)
        {
            this.OnStateChanged(e);
        }

        protected virtual void OnStateChanged(SessionSwitchEventArgs e)
        {
            StateChanged?.Invoke(this, e);
        }

        public bool IsLocked()
        {
            IntPtr hDesktop = IntPtr.Zero; ;
            try
            {
                hDesktop = OpenInputDesktop(0, false,
                    desktop_ReadObjects | desktop_WriteObjects);

                return hDesktop == IntPtr.Zero;
            }
            finally
            {
                if (hDesktop != IntPtr.Zero)
                    CloseDesktop(hDesktop);
            }
        }

        [PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        [PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
                SystemEvents.SessionSwitch -= new SessionSwitchEventHandler(SystemEvents_SessionSwitch);

            disposed = true;
        }
    }
}
