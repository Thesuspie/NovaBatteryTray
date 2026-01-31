# NOVABATTERY

NovaBatteryTray is a lightweight Windows system tray utility for monitoring the battery status of the SteelSeries Arctis Nova 7 headset.
The project uses [HeadsetControl](https://github.com/Sapd/HeadsetControl) under the hood.

It runs silently in the background and displays the current battery level directly in the system tray using dynamic icons, including charging indicators.

⚠️ This is an experimental build. Expect rough edges.

## Features

- **Dynamic Battery Icons**: Displays battery level in 10% increments (0%, 10%, 20%, ... 100%)
- **Charging Indicators**: Changes icons for charging and discharging states
- **Icon Caching**: Efficient resource management with built-in icon caching


## Setup

### Icon Files

1. Run the installer from `Release1`
2. An icon should pop-up in your system tray

### Visual Studio Configuration

**Important**: All icon files must be marked as Embedded Resources:

1. Select all `.ico` files in Solution Explorer
2. Right-click → Properties
3. Set **Build Action** to `Embedded Resource`


## Requirements

- .NET Framework or .NET 6.0+
- Windows OS
- System.Drawing namespace

## Roadmap 
- More icon themes
- 
