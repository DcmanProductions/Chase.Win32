/*
    Chase.Win32 - LFInteractive LLC. 2021-2024
    Chase.Win32 is a C# library to handle various Win32 API Calls.
    Licensed under GPL-3.0
    https://www.gnu.org/licenses/gpl-3.0.en.html#license-text
*/

using System.Drawing;
using System.Runtime.InteropServices;

namespace Chase.Win32;

public static class Win32API
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct NOTIFYICONDATA
    {
        public int cbSize;
        public IntPtr hWnd;
        public int uID;
        public int uFlags;
        public int uCallbackMessage;
        public IntPtr hIcon;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string szTip;
    }

    public const int NIM_ADD = 0x00000000;
    public const int NIM_MODIFY = 0x00000001;
    public const int NIM_DELETE = 0x00000002;
    public const int NIM_SETFOCUS = 0x00000003;
    public const int NIM_SETVERSION = 0x00000004;

    public const int NIF_MESSAGE = 0x00000001;
    public const int NIF_ICON = 0x00000002;
    public const int NIF_TIP = 0x00000004;
    public const int NIF_STATE = 0x00000008;
    public const int NIF_INFO = 0x00000010;
    public const int NIF_GUID = 0x00000020;
    public const int NIF_REALTIME = 0x00000040;
    public const int NIF_SHOWTIP = 0x00000080;
    public const int WM_USER = 0x0400;

    public static IntPtr LoadIconFromFile(string filePath)
    {
        if (OperatingSystem.IsWindows())
        {
            try
            {
                Icon icon = new(filePath);
                return icon.Handle;
            }
            catch
            {
            }
        }
        return IntPtr.Zero;
    }

    [DllImport("Shell32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
    public static extern bool Shell_NotifyIconW(int dwMessage, ref NOTIFYICONDATA lpdata);

    [DllImport("user32.dll")]
    public static extern IntPtr GetForegroundWindow();
}