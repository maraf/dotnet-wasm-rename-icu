using System;
using System.Globalization;
using System.Runtime.InteropServices.JavaScript;

Console.WriteLine("Hello, Browser!");

public partial class MyClass
{
    [JSExport]
    internal static string Greeting()
    {
        string GetDayOfWeek(CultureInfo cultureInfo) => cultureInfo.DateTimeFormat.GetDayName(DateTime.Now.DayOfWeek);

        var text = $"EN: {GetDayOfWeek(CultureInfo.CurrentCulture)}, DE: {GetDayOfWeek(new CultureInfo("de-DE"))}";
        Console.WriteLine(text);
        return text;
    }

    [JSImport("window.location.href", "main.js")]
    internal static partial string GetHRef();
}
