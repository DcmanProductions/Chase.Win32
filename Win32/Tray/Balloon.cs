/*
    Chase.Win32 - LFInteractive LLC. 2021-2024
    Chase.Win32 is a C# library to handle various Win32 API Calls.
    Licensed under GPL-3.0
    https://www.gnu.org/licenses/gpl-3.0.en.html#license-text
*/

namespace Chase.Win32.Tray;

public struct Balloon
{
    public string Title { get; set; }
    public string Message { get; set; }
    public TimeSpan Timeout { get; set; }
    public Action OnClick { get; set; }
}