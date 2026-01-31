using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;

public static class TrayIconProvider
{
    private static readonly Dictionary<string, Icon> Cache = new();

    public static Icon GetIcon(
        int batteryPercent,
        bool isCharging)
    {
        int bucket = Math.Clamp((batteryPercent / 10) * 10, 0, 100);

        string fileName = isCharging
            ? $"battery-charging-{bucket}.ico"
            : $"battery-{bucket}.ico";

        string resourceName =
            $"NOVABATTERY.Assets.Tray.dark.{fileName}";

        if (Cache.TryGetValue(resourceName, out var icon))
            return icon;

        using var stream = Assembly.GetExecutingAssembly()
            .GetManifestResourceStream(resourceName)
            ?? throw new InvalidOperationException($"Missing icon: {resourceName}");

        icon = new Icon(stream);
        Cache[resourceName] = icon;
        return icon;
    }
}
