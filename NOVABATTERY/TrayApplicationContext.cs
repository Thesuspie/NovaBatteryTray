using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;
using System.Reflection;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

public class TrayApplicationContext : ApplicationContext
{
    private readonly NotifyIcon trayIcon;
    public string BatteryStatus { get; set; }
    private readonly Timer timer;
    private readonly HeadsetBatteryManager manager = new();
    private bool _updating;

    public TrayApplicationContext()
    {
        var names = Assembly.GetExecutingAssembly().GetManifestResourceNames();
        Debug.WriteLine("=== EMBEDDED RESOURCES ===");
        foreach (var n in names)
            Debug.WriteLine(n);
        trayIcon = new NotifyIcon
        {
            Icon = SystemIcons.Information,
            Visible = true,
            Text = "Arctis Nova 7 – starting..."
        };

        trayIcon.ContextMenuStrip = new ContextMenuStrip();
        trayIcon.ContextMenuStrip.Items.Add("Refresh", null, async (_, _) => await UpdateAsync());
        trayIcon.ContextMenuStrip.Items.Add("Exit", null, (_, _) => Exit());

        timer = new Timer { Interval = 1_000 };
        timer.Tick += async (_, _) => await UpdateAsync();
        timer.Start();

        _ = UpdateAsync();
    }

    private async Task UpdateAsync()
    {
        if (_updating) return;
        _updating = true;

        try
        {
            var data = await manager.GetBatteryDataAsync();

            if (data == null)
            {
                trayIcon.Text = "Arctis Nova 7 – not detected";
                trayIcon.Icon = SystemIcons.Warning;
                return;
            }

            trayIcon.Text = $"{data.DeviceName} — {data.BatteryLevel}% ({data.BatteryStatus})"
                .Truncate(63);
            trayIcon.Icon = TrayIconProvider.GetIcon(
                data.BatteryLevel,
                data.BatteryStatus.Equals("BATTERY_CHARGING") ? true : false);     
        }
        finally
        {
            _updating = false;
        }
    }


    private void Exit()
    {
        trayIcon.Visible = false;
        timer.Stop();
        Application.Exit();
    }
}

static class StringExtensions
{
    public static string Truncate(this string value, int maxLength) =>
        value.Length <= maxLength ? value : value[..maxLength];
}
