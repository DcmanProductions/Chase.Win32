/*
    Chase.Win32 - LFInteractive LLC. 2021-2024
    Chase.Win32 is a C# library to handle various Win32 API Calls.
    Licensed under GPL-3.0
    https://www.gnu.org/licenses/gpl-3.0.en.html#license-text
*/

using System.Runtime.InteropServices;
using static Chase.Win32.Win32API;

namespace Chase.Win32.Tray;

public class TrayIcon
{
    private NOTIFYICONDATA notifyIconData;
    public string Label { get; private set; }
    public string Icon { get; private set; }

    internal TrayIcon(string label, string icon)
    {
        Label = label;
        Icon = icon;

        AppDomain.CurrentDomain.ProcessExit += (s, e) =>
        {
            Destroy();
        };
    }

    public bool Create()
    {
        try
        {
            notifyIconData = new()
            {
                cbSize = Marshal.SizeOf(typeof(NOTIFYICONDATA)),
                hWnd = GetForegroundWindow(),
                uID = 0,
                uFlags = NIF_ICON | NIF_MESSAGE | NIF_TIP,
                uCallbackMessage = WM_USER,
                hIcon = LoadIconFromFile(Icon),
                szTip = Label
            };

            return Shell_NotifyIconW(NIM_ADD, ref notifyIconData);
        }
        catch (Exception e)
        {
            Console.Error.WriteLine($"Unable to create tray icon: {e.Message}");
            Console.Error.WriteLine(e.StackTrace ?? "");
            return false;
        }
    }

    public bool UpdateIcon(string newIcon)
    {
        Icon = newIcon;
        return Destroy() && Create();
    }

    public bool UpdateLabel(string label)
    {
        Label = label;
        return Destroy() && Create();
    }

    public bool Destroy()
    {
        try
        {
            return Shell_NotifyIconW(NIM_DELETE, ref notifyIconData);
        }
        catch (Exception e)
        {
            Console.Error.WriteLine($"Unable to destroy tray icon: {e.Message}");
            Console.Error.WriteLine(e.StackTrace ?? "");
            return false;
        }
    }
}