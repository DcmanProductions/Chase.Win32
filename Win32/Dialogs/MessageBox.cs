/*
    Chase.Win32 - LFInteractive LLC. 2021-2024
    Chase.Win32 is a C# library to handle various Win32 API Calls.
    Licensed under GPL-3.0
    https://www.gnu.org/licenses/gpl-3.0.en.html#license-text
*/

using System.Diagnostics;
using System.Runtime.InteropServices;
using static Chase.Win32.Win32API;

namespace Chase.Win32.Dialogs;

public static class MessageBox
{
    public static MessageBoxResult Show(string title, string message, MessageBoxFlags flags = MessageBoxFlags.MB_OKCANCEL)
    {
        return (MessageBoxResult)ShellMessageBoxA(Process.GetCurrentProcess().MainModule.BaseAddress, GetForegroundWindow(), message, title, (uint)flags);
    }

    [DllImport("Shell32.dll", CharSet = CharSet.Ansi)]
    private static extern int ShellMessageBoxA(IntPtr hAppInst, IntPtr hWnd, string lpcText, string lpcTitle, uint fuStyle);
}