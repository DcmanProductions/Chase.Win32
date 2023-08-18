/*
    Chase.Win32 - LFInteractive LLC. 2021-2024
    Chase.Win32 is a C# library to handle various Win32 API Calls.
    Licensed under GPL-3.0
    https://www.gnu.org/licenses/gpl-3.0.en.html#license-text
*/

using Chase.Win32.Tray.menu;
using System.Runtime.InteropServices;

namespace Chase.Win32.Tray;

public class ContextMenu
{
    [DllImport("user32.dll")]
    private static extern IntPtr? CreateMenu();

    [DllImport("user32.dll")]
    private static extern bool AppendMenuW(IntPtr hMenu, uint uFlags, UIntPtr uIDNewItem, string lpNewItem);

    [DllImport("user32.dll")]
    private static extern bool DeleteMenu(IntPtr hMenu, uint uPosition, uint uFlags);

    [DllImport("user32.dll")]
    private static extern bool DestroyMenu(IntPtr hMenu);

    [DllImport("user32.dll")]
    private static extern IntPtr InsertMenuItemW(IntPtr hMenu, uint item, bool fByPosition, MenuItemInfoA lpmi);
}