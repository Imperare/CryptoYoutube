using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CryptoYoutube.Services
{
    public static class ServiceRaccourcis
    { 
        public static readonly NativeMethods.LowLevelKeyboardProc _proc = HookCallback;
        private static readonly IntPtr hookID = IntPtr.Zero;

        public delegate void KeyPressHandler();
        public static event KeyPressHandler OnKeyPress;

        static ServiceRaccourcis()
        {
            hookID = SetHook(_proc);
        }

        private static IntPtr SetHook(NativeMethods.LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return NativeMethods.SetWindowsHookEx(NativeMethods.WH_KEYBOARD_LL, proc, NativeMethods.GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if ((nCode >= 0) && (wParam == (IntPtr)NativeMethods.WM_KEYDOWN) && ((Keys)Marshal.ReadInt32(lParam) == Keys.F4))
            {
                OnKeyPress?.Invoke();
            }

            return NativeMethods.CallNextHookEx(hookID, nCode, wParam, lParam);
        }
    }

    public static class NativeMethods
    {
        public delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        public const int WH_KEYBOARD_LL = 13;
        public const int WM_KEYDOWN = 0x0100;

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);
    }
}