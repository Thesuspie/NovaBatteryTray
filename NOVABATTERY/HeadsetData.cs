using System.Text.Json.Serialization;

public class HeadsetControlResponse
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = "";
    [JsonPropertyName("version")]
    public string Version { get; set; } = "";
    [JsonPropertyName("device_count")]
    public int DeviceCount { get; set; }
    [JsonPropertyName("devices")]
    public List<DeviceInfo> Devices { get; set; } = new();
}

public class DeviceInfo
{
    public string device { get; set; } = "";
    public string vendor { get; set; } = "";
    public string product { get; set; } = "";
    [JsonPropertyName("battery")]
    public BatteryInfo Battery { get; set; } = new();
}

public class BatteryInfo
{
    public string status { get; set; } = "";
    public int level { get; set; }
}
