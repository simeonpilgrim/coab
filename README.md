# coab - Curse of the Azure Bonds
This is a reprogramming of the DOS game by the same name for modern Windows, macOS and Linux (via .NET). Coded in C#.

The game is almost 100% feature complete, besides player importation from Pool of Radiance or Hillsfar.

All the original game cheats plus more are available, just right click on the game window to enable/disable.

Currently installer: [CotAB Installer 1.1.7.msi](http://simeonpilgrim.com/files/CotAB%20Installer%201.1.7.msi)

## Building

The engine and support libraries target .NET 8 and build on any platform with the
[.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) installed.

There are two frontends:

* `Main` - the original Windows Forms frontend (`net8.0-windows`, runs on Windows only)
* `MainAvalonia` - a cross-platform [Avalonia](https://avaloniaui.net) frontend for macOS, Linux and Windows

```sh
dotnet build MainAvalonia/MainAvalonia.csproj
```

## Running a downloaded build

The original game data is not included in the build artifacts. The launcher looks
for the `ITEMS` file and original `*.DAX` files beside the application, in a `Data`
subdirectory beside it, or in a directory supplied with `--data-dir`. File name
case does not matter.

### macOS

Extract the archive, place `coab.app` in the directory containing the original
game data, then double-click it. The first launch of an unnotarized download may
require right-clicking `coab.app` and choosing **Open**.

Alternatively, run the executable inside the application bundle from a terminal:

```sh
/path/to/coab.app/Contents/MacOS/coab --data-dir /path/to/game/data
```

### Linux

Place the `coab` executable in the game data directory and run it, or provide the
data directory explicitly:

```sh
chmod +x coab
./coab --data-dir /path/to/game/data
```

Saves and `settings.json` are stored under `~/Curse of the Azure Bonds`. Diagnostic
logs, including `Crash Log.txt` after an unexpected failure, are stored under
`~/Curse of the Azure Bonds/Logs`.
Sound effects use the system command-line wav player (`afplay` on macOS,
`paplay`/`pw-play`/`aplay`/`play` on Linux); if none is present the game runs silently.
