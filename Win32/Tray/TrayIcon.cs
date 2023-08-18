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
    private const int NIM_ADD = 0x00000000;
    private const int NIM_MODIFY = 0x00000001;
    private const int NIM_DELETE = 0x00000002;
    private const int NIM_SETFOCUS = 0x00000003;
    private const int NIM_SETVERSION = 0x00000004;
    private const int NIF_MESSAGE = 0x00000001;
    private const int NIF_ICON = 0x00000002;
    private const int NIF_TIP = 0x00000004;
    private const int NIF_STATE = 0x00000008;
    private const int NIF_INFO = 0x00000010;
    private const int NIF_GUID = 0x00000020;
    private const int NIF_REALTIME = 0x00000040;
    private const int NIF_SHOWTIP = 0x00000080;
    private const int WM_USER = 0x0400;
    private const int WM_LBUTTONDOWN = 0x0201;
    private const int WM_RBUTTONDOWN = 0x0204;
    private const int WM_CONTEXTMENU = 0x007B;

    private NotifyIconData notifyIconData;
    public string Label { get; private set; }
    public string Icon { get; private set; }

    public Action OnClick { get; set; }
    public Action OnContext { get; set; }

    internal TrayIcon(string label, string icon)
    {
        Label = label;
        Icon = icon;
        OnClick = OnContext = () => { };

        AppDomain.CurrentDomain.ProcessExit += (s, e) =>
        {
            Destroy();
        };
    }

    public bool Create()
    {
        try
        {
            notifyIconData = new NotifyIconData
            {
                cbSize = Marshal.SizeOf(typeof(NotifyIconData)),
                hWnd = GetForegroundWindow(),
                uID = 0,
                uFlags = NIF_ICON | NIF_MESSAGE | NIF_TIP,
                uCallbackMessage = WM_USER | WM_CONTEXTMENU,
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

    [DllImport("Shell32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
    private static extern bool Shell_NotifyIconW(int dwMessage, ref NotifyIconData lpdata);

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    private static extern IntPtr DefWindowProcW(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

    private IntPtr WndProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
    {
        if (msg == WM_USER)
        {
            if (lParam == (IntPtr)WM_LBUTTONDOWN)
            {
                OnClick.Invoke();
            }
            else if (lParam == (IntPtr)WM_RBUTTONDOWN)
            {
                OnContext.Invoke();
            }
        }
        return DefWindowProcW(hWnd, msg, wParam, lParam);
    }
}