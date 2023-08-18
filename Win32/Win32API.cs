/*
    Chase.Win32 - LFInteractive LLC. 2021-2024
    Chase.Win32 is a C# library to handle various Win32 API Calls.
    Licensed under GPL-3.0
    https://www.gnu.org/licenses/gpl-3.0.en.html#license-text
*/

using System.Drawing;
using System.Runtime.InteropServices;

namespace Chase.Win32;

internal static class Win32API
{
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

    [DllImport("user32.dll")]
    public static extern IntPtr GetForegroundWindow();
}