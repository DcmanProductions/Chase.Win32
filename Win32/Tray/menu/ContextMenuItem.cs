/*
    Chase.Win32 - LFInteractive LLC. 2021-2024
    Chase.Win32 is a C# library to handle various Win32 API Calls.
    Licensed under GPL-3.0
    https://www.gnu.org/licenses/gpl-3.0.en.html#license-text
*/

namespace Chase.Win32.Tray;

public class ContextMenuItem
{
    public string Label { get; set; }

    public string Icon { get; set; } = "";

    public Action OnClick { get; set; }

    public bool Disabled { get; set; } = false;

    public ContextMenuItem(string label, Action onClick)
    {
        Label = label;
        OnClick = onClick;
    }

    public virtual void Click()
    {
        OnClick.Invoke();
    }
}