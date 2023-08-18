/*
    Chase.Win32 - LFInteractive LLC. 2021-2024
    Chase.Win32 is a C# library to handle various Win32 API Calls.
    Licensed under GPL-3.0
    https://www.gnu.org/licenses/gpl-3.0.en.html#license-text
*/

namespace Chase.Win32.Tray;

public sealed class TrayIconBuilder
{
    private string icon, label;

    public TrayIconBuilder(string label, string icon)
    {
        this.label = label;
        this.icon = icon;
    }

    public TrayIcon Build()
    {
        return new TrayIcon(label, icon);
    }
}