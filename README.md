# PTNSHIFT Companion

Companion app for Tim Exile's amazing PTNSHIFT, the Push 2- and 3-compatible variant of Scapeshift. They're both available for sale here: https://www.patreon.com/c/timexile/shop.

## Other uses

While the primary purpose of this app is to move the PTNSHIFT UI to a Push device, it's really just a generic tool to mirror a portion of a display and send that to the Push. There are no hard dependencies between PTNSHIFT and this app, so if you have some other reason for needing this functionality, go right ahead! Let me know if I can improve the app for your use case, or fork it and change it as you see fit (just remember the licence).

## Binaries

For convenience, builds are available at https://bergius.org/ptnshift (direct download for Windows and macOS).

## Building

### Windows

1. Install the [.NET SDK 9.0](https://dotnet.microsoft.com/en-us/download) and the [Visual Studio Build Tools](https://visualstudio.microsoft.com/downloads/#build-tools-for-visual-studio) (for the native `WinScreenStream` library).
2. From PowerShell in the repo root, run:
   ```
   .\build-win.ps1
   ```
   The script builds the native library and then publishes Release Windows configurations for both `win-x64` and `win-arm64` into `publish\x64` and `publish\arm64`.

### macOS

Use the existing helper to produce universal binaries:
```
./build-mac.sh
```

## Logging

The app writes logs both to the console (when launched from a terminal) and to a rolling log file:

- **Windows (including ARM64 builds):** `%AppData%\PtnshiftCompanion\ptnshift.log`
- **macOS:** `~/Library/Application Support/PtnshiftCompanion/ptnshift.log`

A startup entry is written every time the app launches, so if nothing appears to happen after running the downloaded artifact you can open the log file to see whether the app started and any errors that occurred. From within the app, the **Log** menu option opens the folder containing the log file.

## Licensing

This project is licensed under the GNU General Public License v3.0 (GPLv3), ensuring that any modifications or derivative works are also made available under the same license.

However, if you are interested in using this project under different terms—for example, to distribute a modified version without being bound by GPLv3—you may contact the project maintainer to discuss alternative licensing options. Additional permissions may be granted on a case-by-case basis.

For inquiries, please reach out via https://github.com/palpha/ptnshift-companion/issues.

## libusb relinking

This software dynamically links to libusb (LGPL). If you wish to replace libusb.dylib with a modified version in a macOS build of this software, you may do so by swapping the .dylib file in Contents/MonoBundle/ without needing to rebuild the application. For Windows, you'll have to download the source and build.
