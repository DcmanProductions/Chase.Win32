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
    private Action onClick, onContext;
    private List<ContextMenuItem> items;
    private Balloon balloon;

    public TrayIconBuilder(string label, string icon)
    {
        this.label = label;
        this.icon = icon;
        items = new();
    }

    public TrayIconBuilder AddLeftClickEvent(Action onClick)
    {
        this.onClick = onClick;
        return this;
    }

    public TrayIconBuilder AddRightClickEvent(Action onClick)
    {
        onContext = onClick;
        return this;
    }

    public TrayIconBuilder AddContextMenuItem(ContextMenuItem item)
    {
        return this;
    }

    public TrayIconBuilder AddBalloon(Balloon balloon)
    {
        this.balloon = balloon;

        return this;
    }

    public TrayIcon Build()
    {
        TrayIcon tray = new(label, icon);

        return tray;
    }

    public bool BuildCreate(out TrayIcon trayIcon) => (trayIcon = Build()).Create();
}