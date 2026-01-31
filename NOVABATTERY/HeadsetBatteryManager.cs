using System.Diagnostics;
using System.Text.Json;

public class HeadsetBatteryManager
{
    private readonly string _exePath;

    public HeadsetBatteryManager(string exePath = "headsetcontrol.exe")
    {
        _exePath = exePath;
    }

    public async Task<HeadsetData?> GetBatteryDataAsync()
    {
        using Process process = new();
        process.StartInfo.FileName = _exePath;
        process.StartInfo.Arguments = "-o json";
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.RedirectStandardError = true;
        process.StartInfo.CreateNoWindow = true;

        try
        {
            process.Start();
            string jsonOutput = await process.StandardOutput.ReadToEndAsync();
            await process.WaitForExitAsync();

            if (process.ExitCode != 0)
            {
                string error = await process.StandardError.ReadToEndAsync();
                Console.WriteLine($"Error running {_exePath}: {error}");
                return null;
            }

            var response = JsonSerializer.Deserialize<HeadsetControlResponse>(jsonOutput);
            return response?.Devices?.FirstOrDefault() is DeviceInfo device
                ? new HeadsetData(device.device, device.vendor, device.product, device.Battery.status, device.Battery.level)
                : null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception: {ex.Message}");
            return null;
        }
    }
}

public record HeadsetData(string DeviceName, string Vendor, string Product, string BatteryStatus, int BatteryLevel);
