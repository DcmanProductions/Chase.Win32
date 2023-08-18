/*
    Chase.Win32 - LFInteractive LLC. 2021-2024
    Chase.Win32 is a C# library to handle various Win32 API Calls.
    Licensed under GPL-3.0
    https://www.gnu.org/licenses/gpl-3.0.en.html#license-text
*/

using Chase.Win32.Tray;

namespace Test;

internal class Program
{
    private static void Main()
    {
        TrayIcon icon = new TrayIconBuilder("test", @"D:\Workspaces\Visual Studio Workspace\C#\MAUI\PolygonMC\PolygonMC\wwwroot\images\icon.ico").Build();
        if (icon.Create())
        {
            Console.WriteLine("Tray icon created...");
        }
        else
        {
            Console.WriteLine("Failed to create icon");
        }

        Console.ReadLine();
    }
}